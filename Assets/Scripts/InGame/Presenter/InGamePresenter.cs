using UnityEngine;
using UniRx;
using VContainer;
using System.Collections.Generic;

/// <summary>
/// インゲームのViewとModelをつなぐクラス
/// </summary>
public class InGamePresenter : MonoBehaviour
{
    [SerializeField] Transform _deckTransform;
    [Inject]
    PlayerManager _playerManager;
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
        _playerManager.DeckCardsChanged += cards =>
        {
            Debug.Log($"これまでに生成済みのカード数{_cardViews.Count}");
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
    }
}
