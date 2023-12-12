using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

/// <summary>
/// 敵の行動等を管理するクラス
/// </summary>
public class EnemyManager
{
    private Status _status;
    private List<IEnemyBehavior> _behaviors;
    private List<TurnStatus> _turnStatuses;
    private readonly IntReactiveProperty _nextBehaviorIndex = new(0);
    public event Action<Winner> OnGameEnd;
    public Status Status => _status;
    public List<IEnemyBehavior> Behaviors => _behaviors;
    public IReadOnlyReactiveProperty<int> NextBehaviorIndex => _nextBehaviorIndex;

    public EnemyManager(List<IEnemyBehavior> enemyBehaviors, Status status)
    {
        //ここはステータス全てを入れるようにする
        _status = new Status(status);
        _behaviors = enemyBehaviors;
        _turnStatuses = new List<TurnStatus>();
    }

    /// <summary>
    /// 自身が持つ行動から指定されたものを実行する
    /// </summary>
    public void ExcuteEnemyAction()
    {
        _behaviors[_nextBehaviorIndex.Value].Excute();
    }

    /// <summary>
    /// ターンで発動する効果を追加する
    /// </summary>
    /// <param name="turnStatus"></param>
    public void ApplyEffect(TurnStatus turnStatus)
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
    /// 次に実行する行動のインデックスを設定する
    /// </summary>
    public void SetNextBehaviorIndex()
    {
        _nextBehaviorIndex.Value = UnityEngine.Random.Range(0, _behaviors.Count);
    }
}
