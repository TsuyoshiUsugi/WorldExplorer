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
        _playerManager.HandCardsChanged += cards =>
        {
            _cardViews.ForEach(cardViews => Destroy(cardViews.gameObject));
            _cardViews.Clear();

            for (var i = 0; i < cards.Count; i++)
            {
                var card = cards[i];
                var cardEntity = Instantiate(card.CardEntity, _deckTransform);
                _cardViews.Add(cardEntity);
                var view = cardEntity.GetComponent<CardView>();
                view.OnCardSelect += index => _playerManager.PlayCard(index);
            }
        };

        #region PlayerManagerのイベント登録処理
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

      

        #endregion

        #region EnemyManagerのイベント登録処理
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
