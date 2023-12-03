using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

/// <summary>
/// プレイヤーの手札・山札の情報、ドロー処理などを行うクラス
/// </summary>
public class PlayerManager : MonoBehaviour
{
    private List<PlayerCard> _handcards = new ();
    private List<PlayerCard> _deckCards = new();
    private readonly IntReactiveProperty _actionCost = new(3);  //プレイヤーの行動回数は3
    public IReadOnlyReactiveProperty<int> ActionCost => _actionCost;

    /// <summary>
    /// 山札からカードを引く処理
    /// デフォルトで呼ばれると五枚カードを引く
    /// </summary>
    public void DrawCard(int drawCount = 5)
    {
        for (var i = 0; i < drawCount; i++)
        {
            var index = Random.Range(0, _deckCards.Count);
            _handcards.Add(_deckCards[index]);  //山札から手札に加える
            _deckCards.RemoveAt(index);         //山札から引いたカードを消す
        }
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
    }
}
