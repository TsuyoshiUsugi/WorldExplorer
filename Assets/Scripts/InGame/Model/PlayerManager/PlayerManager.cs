using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

/// <summary>
/// プレイヤーの手札・山札の情報、ドロー処理などを行うクラス
/// </summary>
public class PlayerManager
{
    private Status _status;
    private bool _active = false;
    private int _maxDeckCount = 0;
    private List<TurnStatusBase> _turnStatuses;
    private List<CardDataEntity> _handcards = new();       //手札
    private ReactiveCollection<CardDataEntity> _deckCards = new();        //山札
    private SakePower _sakePower;                          //酒力
    private static readonly int _defaultActionCost = 3; //行動回数、デフォルトは3
    private readonly IntReactiveProperty _actionCost = new(_defaultActionCost);
    public event Action<Winner> OnGameEnd;
    
    public Status Status => _status;
    public List<CardDataEntity> HandCard => _handcards;
    public IReadOnlyReactiveProperty<int> ActionCost => _actionCost;
    public ReactiveCollection<CardDataEntity> Deck => _deckCards;
    public event Action<List<CardDataEntity>> HandCardsChanged;
    public int MaxDeckCount => _maxDeckCount;
    public SakePower SakePower => _sakePower;
    public List<TurnStatusBase> TurnStatuses => _turnStatuses;

    public PlayerManager(Status status)
    {
        _deckCards = new(new List<CardDataEntity>());
        foreach (var card in GameDataManager.Instance.DeckInfo.Cards)
        {
            // ここでCardDataのディープコピーを作成
            _deckCards.Add(new CardDataEntity(card, card.Id));
        }
        _maxDeckCount = _deckCards.Count;
        _status = new Status(status);
        _sakePower = new SakePower(_maxDeckCount);
        _turnStatuses = new List<TurnStatusBase>();
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
    public async void PlayCard(int handCardIndex)
    {
        if (!_active) return;
        if (_actionCost.Value == 0) return;
        _actionCost.Value -= 1;
        _sakePower.AddSakePower(1);             //カードをプレイすると酒力が1増える
        await CardEffectViewManager.Instance.ShowCardEffect(_handcards[handCardIndex].ID);
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

    public void AddActionCost(int cost)
    {
        _actionCost.Value += cost;
    }

    /// <summary>
    /// アクションコストをリセットする
    /// </summary>
    /// <param name=""></param>
    public void RestActionCost()
    {
        _actionCost.Value = 3;
    }

    /// <summary>
    /// 手札に引き数のカードを追加する
    /// </summary>
    /// <param name="card"></param>
    public void AddHandCard(CardDataEntity card)
    {
        _handcards.Add(card);
        HandCardsChanged?.Invoke(_handcards);
    }

    /// <summary>
    /// ターンで発動する効果を追加する
    /// </summary>
    /// <param name="turnStatus"></param>
    public void ApplyEffect(TurnStatusBase turnStatus)
    {
        turnStatus.ApplyEffect(_status);
        _turnStatuses.Add(turnStatus);
    }

    /// <summary>
    /// 持続する効果のターンを減らす
    /// </summary>
    public void DecreaseEffectTurn()
    {
        foreach (var turnStatus in _turnStatuses)
        {
            turnStatus.DecreaseTurn();
        }
    }

    /// <summary>
    /// 手札に目的のカードがあるかどうかを返す
    /// ある場合はそのカードの参照も返す
    /// </summary>
    public bool TryGetTargetCard(int id, out CardDataEntity card)
    {
        // 出力パラメータを初期化
        card = null;

        // _handcardsの各カードを検査
        foreach (var cardEntity in _handcards)
        {
            if (cardEntity.ID == id)
            {
                // 条件を満たすカードが見つかった場合
                card = cardEntity;
                return true;
            }
        }

        // 条件を満たすカードが見つからなかった場合
        return false;
    }

    /// <summary>
    /// 山札に目的のカードがあるかどうかを返す
    /// ある場合はそのカードの参照も返す
    /// </summary>
    public bool TryGetDeckTargetCard(int id, out CardDataEntity card)
    {
        // 出力パラメータを初期化
        card = null;

        // _deckCardsの各カードを検査
        foreach (var cardEntity in _deckCards)
        {
            if (cardEntity.ID == id)
            {
                // 条件を満たすカードが見つかった場合
                card = cardEntity;
                return true;
            }
        }

        // 条件を満たすカードが見つからなかった場合
        return false;
    }

    /// <summary>
    /// 山札に目的のカードがあるかどうかを返す
    /// </summary>
    public bool TryGetDeckTargetCard(int id)
    {
        // _deckCardsの各カードを検査
        foreach (var cardEntity in _deckCards)
        {
            if (cardEntity.ID == id)
            {
                // 条件を満たすカードが見つかった場合
                return true;
            }
        }
        // 条件を満たすカードが見つからなかった場合
        return false;
    }


    /// <summary>
    /// 山札から特定のカードを手札に加える
    /// </summary>
    public bool TryFromDeckAddSpecificCard(int id)
    {
        if (TryGetDeckTargetCard(id, out var card))
        {
            //山札から手札へ
            _deckCards.Remove(card);
            _handcards.Add(card);
            return true;
        }
        // 条件を満たすカードが見つからなかった場合
        return false;
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

/// <summary>
/// 酒力を表す
/// </summary>
public class SakePower
{
    private readonly IntReactiveProperty _currentSakePower = new(0);
    private int _maxSakePower = 0;
    private bool _isDrank = false;
    private int _remainDrunkTurn = 0;
    private int _maxDrunkTurn = 0;

    public IReadOnlyReactiveProperty<int> CurrentSakePower => _currentSakePower;
    public int MaxSakePower => _maxSakePower;
    public bool IsDrank => _isDrank;

    public SakePower(int maxSakePower)
    {
        _maxSakePower = maxSakePower;
    }

    /// <summary>
    /// 酒力を指定した値だけ追加する
    /// </summary>
    /// <param name="power"></param>
    public void AddSakePower(int power)
    {
        if (_isDrank) return;
        _currentSakePower.Value += power;
        if (_currentSakePower.Value >= _maxSakePower)
        {
            _currentSakePower.Value = _maxSakePower;
            _isDrank = true;
            ActiveDrunkPower();
        }
    }

    /// <summary>
    /// 酔い状態の効果を発揮する
    /// </summary>
    private void ActiveDrunkPower()
    {
        _remainDrunkTurn = _maxDrunkTurn;
        //ここに酔い状態の効果を書く
        foreach (var card in FieldInfo.Instance.PlayerManager.HandCard)
        {
            card.CardEffects.ForEach(c => c.EvolveCardEffect());
        }
    }

    /// <summary>
    /// 酔い状態の残りターンを減らす
    /// </summary>
    public void DecreaseDrunkTurn()
    {
        if (!_isDrank) return;
        _remainDrunkTurn--;
        if (_remainDrunkTurn == 0)
        {
            _isDrank = false;
        }
    }
}
