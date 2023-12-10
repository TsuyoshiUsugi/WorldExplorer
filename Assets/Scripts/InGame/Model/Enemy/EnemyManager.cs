using System;
using System.Collections.Generic;
using UniRx;

/// <summary>
/// 敵の行動等を管理するクラス
/// </summary>
public class EnemyManager
{
    private Status _status;
    private List<IEnemyBehavior> _behaviors;
    private List<TurnStatus> _turnStatuses;
    public event Action<Winner> OnGameEnd;
    public Status Status => _status;

    public EnemyManager(List<IEnemyBehavior> enemyBehaviors)
    {
        //ここはステータス全てを入れるようにする
        _status = new Status(100, 10, 0);
        _behaviors = enemyBehaviors;
        _turnStatuses = new List<TurnStatus>();
    }

    /// <summary>
    /// 自身が持つ行動からランダムで実行する
    /// </summary>
    public void ExcuteEnemyAction()
    {
        var index = UnityEngine.Random.Range(0, _behaviors.Count);
        _behaviors[index].Excute();
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
}
