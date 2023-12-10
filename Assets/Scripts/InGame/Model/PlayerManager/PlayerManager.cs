using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

/// <summary>
/// プレイヤーの手札・山札の情報、ドロー処理などを行うクラス
/// </summary>
public class PlayerManager
{
    private Status _status;
    bool _active = false;
    private int _maxDeckCount = 0;
    private List<CardDataEntity> _handcards = new();       //手札
    private ReactiveCollection<CardDataEntity> _deckCards = new();        //山札
    private int _sakePower = 0;                          //酒力
    private static readonly int _defaultActionCost = 3; //行動回数、デフォルトは3
    private readonly IntReactiveProperty _actionCost = new(_defaultActionCost);
    public event Action<Winner> OnGameEnd;
    
    public Status Status => _status;
    public IReadOnlyReactiveProperty<int> ActionCost => _actionCost;
    public ReactiveCollection<CardDataEntity> Deck => _deckCards;
    public event Action<List<CardDataEntity>> HandCardsChanged;
    public int MaxDeckCount => _maxDeckCount;

    public PlayerManager()
    {
        _deckCards = new(new List<CardDataEntity>());
        foreach (var card in GameDataManager.Instance.DeckInfo.Cards)
        {
            // ここでCardDataのディープコピーを作成
            _deckCards.Add(new CardDataEntity(card));
        }
        _maxDeckCount = _deckCards.Count;
        _status = new Status(100, 10, 0);
    }


    /// <summary>
    /// 山札からカードを引く処理
    /// デフォルトで呼ばれると五枚カードを引く
    /// </summary>
    public void DrawCard(int drawCount = 5)
    {
        if (!_active) return;
        if (_deckCards.Count == 0) return;

        for (var i = 0; i < drawCount; i++)
        {   //ドロー処理
            var index = UnityEngine.Random.Range(0, _deckCards.Count);
            if (_deckCards.Count <= index)
            {
                Debug.Log($"カードが足りません！ 呼び出しインデックス{index},デッキの枚数{_deckCards.Count}");
                return;
            }

            _handcards.Add(_deckCards[index]);  //山札から手札に加える
            _deckCards.RemoveAt(index);         //山札から引いたカードを消す
        }
        HandCardsChanged?.Invoke(_handcards);
    }

    /// <summary>
    /// ターン開始時に最初に呼ばれる
    /// </summary>
    public void ResetHandCard()
    {
        //前のターンで引いていたカードを山札に戻して_handcardsをリセット
        for (var i = _handcards.Count - 1; i >= 0; i--)
        {
            var card = _handcards[i];
            _deckCards.Add(card);
            _handcards.RemoveAt(i);
        }
        _handcards.Clear();
    }

    /// <summary>
    /// 手札のカードを指定してプレイする。アクションコストを1減らす
    /// インデックスは画面左のカードが0
    /// プレイしたカードは手札から外して山札へ
    /// </summary>
    /// <param name="handCardIndex"></param>
    public void PlayCard(int handCardIndex)
    {
        if (!_active) return;
        if (_actionCost.Value == 0) return;
        _actionCost.Value -= 1;
        _handcards[handCardIndex].PlayCard();   //ここでカードの効果呼び出し
        _deckCards.Add(_handcards[handCardIndex]);
        _handcards.RemoveAt(handCardIndex);
        HandCardsChanged?.Invoke(_handcards);
    }

    /// <summary>
    /// プレイヤーの行動を設定する
    /// </summary>
    /// <param name="active">アクティブにする場合true</param>
    public void SetActivePlayer(bool active)
    {
        _active = active;
    }

    /// <summary>
    /// アクションコストをリセットする
    /// </summary>
    /// <param name=""></param>
    public void RestActionCost()
    {
        _actionCost.Value = 3;
    }
}

/// <summary>
/// 勝敗を表す
/// </summary>
public enum Winner
{
    Player,
    Enemy,
}
