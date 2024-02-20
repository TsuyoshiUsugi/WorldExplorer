using System;
using System.Collections.Generic;
using GraphProcessor;
using TsuyoshiBehaviorTree;
using UniRx;

/// <summary>
/// 敵の行動等を管理するクラス
/// </summary>
public class EnemyManager
{
    private Status _status;
    private BehaviorTreeProcesser _behavior;
    private List<TurnStatusBase> _turnStatuses;
    private readonly IntReactiveProperty _nextBehaviorIndex = new(0);
    public event Action<Winner> OnGameEnd;
    public Status Status => _status;
    public BehaviorTreeProcesser Behavior => _behavior;
    public IReadOnlyReactiveProperty<int> NextBehaviorIndex => _nextBehaviorIndex;
    public List<TurnStatusBase> TurnStatuses => _turnStatuses;

    public EnemyManager(BaseGraph enemyBehavior, Status status)
    {
        //ここはステータス全てを入れるようにする
        _status = new Status(status);
        _behavior = new (enemyBehavior, null);
        _turnStatuses = new List<TurnStatusBase>();
    }

    /// <summary>
    /// 自身が持つ行動から指定されたものを実行する
    /// </summary>
    public void ExcuteEnemyAction()
    {
        _behavior.Run();
        _behavior.OnUpdate();
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
        var toRemove = new List<TurnStatusBase>();

        foreach (var turnStatus in _turnStatuses)
        {
            turnStatus.DecreaseTurn();
            if (turnStatus.RemainTurn.Value <= 0)
            {
                toRemove.Add(turnStatus);
            }
        }

        foreach (var item in toRemove)
        {
            _turnStatuses.Remove(item);
        }
    }
}
