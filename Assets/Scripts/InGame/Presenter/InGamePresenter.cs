using UnityEngine;
using UniRx;
using VContainer;
using System.Collections.Generic;

/// <summary>
/// インゲームのViewとModelをつなぐクラス
/// </summary>
public class InGamePresenter : MonoBehaviour
{
    [Inject]
    EnemyManager _enemyManager;
    [Inject]
    PlayerManager _playerManager;
    [SerializeField] Transform _deckTransform;
    [SerializeField] InGameView _gameView;
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

        _playerManager.ActionCost.Subscribe(num =>
        {
            _gameView.SetActionSimbleImage(num);
        });

        _playerManager.Deck.Subscribe(deck =>
        {
            _gameView.SetDeckCardNumText(deck.Count, _playerManager.MaxDeckCount);
        });

        _playerManager.HP.Subscribe(hp =>
        {
            _gameView.ShowPlayerHP(hp, _playerManager.MaxHp);
        });

        _enemyManager.HP.Subscribe(hp =>
        {
            _gameView.ShowEnemyHP(hp, _enemyManager.MaxHp);
        });
    }
}
