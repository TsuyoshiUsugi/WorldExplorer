using UnityEngine;
using UniRx;
using VContainer;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

/// <summary>
/// インゲームのViewとModelをつなぐクラス
/// </summary>
public class InGamePresenter : MonoBehaviour
{
    //モデル
    [Inject]
    private EnemyManager _enemyManager;
    [Inject]
    private PlayerManager _playerManager;
    [Inject]
    private ResultState _resultState;
    [Inject]
    private PlayerTurnState _playerTurnState;
    [Inject]
    private EnemyTurnState _enemyTurnState;

    //ビュー
    [SerializeField] private Transform _deckTransform;
    [SerializeField] private InGameView _gameView;
    [SerializeField] private ResultView _resultView;
    private List<GameObject> _cardViews = new List<GameObject>();

    private void Awake()
    {
        SubscribeMethod();
    }

    /// <summary>
    /// 登録処理を行う
    /// </summary>
    private void SubscribeMethod()
    {
        #region PlayerManagerのイベント登録処理
        _gameView.ShowPlayerImage(GameDataManager.Instance.PlayerData.PlayerSprite);

        _playerManager.HandCardsChanged += cards =>
        {
            _cardViews.ForEach(cardViews => Destroy(cardViews));
            _cardViews.Clear();

            for (var i = 0; i < cards.Count; i++)
            {
                var card = cards[i];
                var cardPrefab = Instantiate(card.CardEntity, _deckTransform);
                _cardViews.Add(cardPrefab);
                var view = cardPrefab.GetComponent<CardView>();
                // ここで新しいローカル変数を作成
                var localIndex = i;

                // localIndexをラムダ式にキャプチャさせる
                view.SetIndex(localIndex);
                view.OnCardSelect += _ => _playerManager.PlayCard(localIndex);
            }
        };

        _playerTurnState.OnEnterEvent += async () =>
        {
            await _gameView.ShowTurnNotify(InGameView.Turn.PlayerTurn);
        };

        _gameView.TurnEndButton.OnClickAsObservable().Subscribe(_ =>
        {
            _playerTurnState.EndTurn();
        });

        _playerManager.ActionCost.Subscribe(num =>
        {
            _gameView.SetActionSimbleImage(num);
        });

        _playerManager.Deck.ObserveCountChanged().Subscribe(deckCount =>
        {
            _gameView.SetDeckCardNumText(deckCount, _playerManager.MaxDeckCount);
        });

        _playerManager.Status.HP.Subscribe(hp =>
        {
            _gameView.ShowPlayerHP(hp, _playerManager.Status.MaxHp);
        });

        _playerManager.Status.HP.Zip(_playerManager.Status.HP.Skip(1), (x, y) => new { OldValue = x, NewValue = y })
            .Subscribe(t => _gameView.ShowDamageCount(InGameView.Turn.EnemyTurn, t.OldValue, t.NewValue));

        _playerManager.SakePower.CurrentSakePower.Subscribe(power =>
        {
            _gameView.ShowSakePower(power, _playerManager.SakePower.MaxSakePower);
        });

        _playerManager.ActionCost.Where(x => x == 0).Subscribe(cost =>
        {
            _gameView.SetTurnEndButtonActive(true);
        });
        
        _playerManager.ActionCost.Where(x => x > 0).Subscribe(cost =>
        {
            _gameView.SetTurnEndButtonActive(false);
        });

        #endregion

        #region EnemyManagerのイベント登録処理

        _gameView.ShowEnemyImage(GameDataManager.Instance.EnemyData.EnemySprite);

        _enemyManager.Status.HP.Zip(_enemyManager.Status.HP.Skip(1), (x, y) => new { OldValue = x, NewValue = y })
            .Subscribe(t => _gameView.ShowDamageCount(InGameView.Turn.PlayerTurn, t.OldValue, t.NewValue));

        _enemyTurnState.OnEnterEvent += async () =>
        {
            await _gameView.ShowTurnNotify(InGameView.Turn.EnemyTurn);
        };

        _enemyManager.Status.HP.Subscribe(hp =>
        {
            _gameView.ShowEnemyHP(hp, _enemyManager.Status.MaxHp);
        });

        _enemyManager.NextBehaviorIndex.Subscribe(index =>
        {
            var action = _enemyManager.Behaviors[index].Action;
            var actionRelateNum = 0;

            switch (action)
            {
                case IEnemyBehavior.EnemyAction.Attack:
                    actionRelateNum = _enemyManager.Status.AttackPower;
                    break;
                case IEnemyBehavior.EnemyAction.Block:
                    actionRelateNum = _enemyManager.Status.BlockPower;
                    break;
            }
            _gameView.ShowEnemyAction(_enemyManager.Behaviors[index].Action, actionRelateNum);
        });

        #endregion

        _resultState.OnGameEnd += async (winner) =>
        {
            if (winner == Winner.Player)
            {
                _resultView.ShowWinResultPanel();
            }
            else
            {
                _resultView.ShowLoseResultPanel();
            }
            await UniTask.CompletedTask;
        };
    }
}
