using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

/// <summary>
/// 敵の行動等を管理するクラス
/// </summary>
public class EnemyManager
{
    private IntReactiveProperty _hp = new(100);
    public int MaxHp { get; private set; }
    private int _attackPower = 1;
    private int _blockPower = 1;
    List<IEnemyBehavior> _behaviors;

    public IReadOnlyReactiveProperty<int> HP => _hp;
    public int AttackPower => _attackPower;
    public int BlockPower => _blockPower;

    public EnemyManager(List<IEnemyBehavior> enemyBehaviors)
    {
        //ここはステータス全てを入れるようにする
        _behaviors = enemyBehaviors;
        MaxHp = _hp.Value;
    }

    /// <summary>
    /// 自身が持つ行動からランダムで実行する
    /// </summary>
    public void ExcuteEnemyAction()
    {
        var index = Random.Range(0, _behaviors.Count);
        _behaviors[index].Excute();
    }

    /// <summary>
    /// 指定したダメージを与える
    /// </summary>
    public void ApplyDamage(int damage)
    {
        _hp.Value -= damage;
        if (_hp.Value < 0) _hp.Value = 0;
    }
}
