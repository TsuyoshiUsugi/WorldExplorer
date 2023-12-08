using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

/// <summary>
/// プレイヤーの手札・山札の情報、ドロー処理などを行うクラス
/// </summary>
public class PlayerManager
{
    private List<CardData> _handcards = new();       //手札
    private List<CardData> _deckCards = new();        //山札
    private int _sakePower = 0;                          //酒力
    private static readonly int _defaultActionCost = 3; //行動回数、デフォルトは3
    private readonly IntReactiveProperty _actionCost = new(_defaultActionCost);  
    public IReadOnlyReactiveProperty<int> ActionCost => _actionCost;
    public event Action<List<CardData>> DeckCardsChanged;

    public PlayerManager()
    {
        _deckCards = new List<CardData>(GameDataManager.Instance.DeckInfo.Cards);
    }

    /// <summary>
    /// 山札からカードを引く処理
    /// デフォルトで呼ばれると五枚カードを引く
    /// </summary>
    public void DrawCard(int drawCount = 5)
    {
        if (_deckCards.Count == 0) return;
        for (var i = 0; i < drawCount; i++)
        {
            var index = UnityEngine.Random.Range(0, _deckCards.Count);
            if (_deckCards[index] == null)
            {
                Debug.Log("カードが足りません！");
                return;
            }
            
            _handcards.Add(_deckCards[index]);  //山札から手札に加える
            _deckCards.RemoveAt(index);         //山札から引いたカードを消す
        }
        DeckCardsChanged?.Invoke(_deckCards);
        Debug.Log(_handcards.Count);
    }

    /// <summary>
    /// 手札のカードを指定してプレイする。アクションコストを1減らす
    /// インデックスは画面左のカードが0
    /// プレイしたカードは手札から外して山札へ
    /// </summary>
    /// <param name="handCardIndex"></param>
    public void PlayCard(int handCardIndex)
    {
        _actionCost.Value--;
        _handcards[handCardIndex].PlayCard();
        _deckCards.Add(_handcards[handCardIndex]);
        _handcards.RemoveAt(handCardIndex);
        DeckCardsChanged?.Invoke(_handcards);
        Debug.Log("call");
    }

    /// <summary>
    /// アクションコストをリセットする
    /// </summary>
    /// <param name=""></param>
    public void RestActionCost()
    {
        _actionCost.Value += _defaultActionCost;
    }
}
