using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵の行動等を管理するクラス
/// </summary>
public class EnemyManager
{
    private int _hp = 10;
    private int _attackPower = 1;
    private int _blockPower = 1;
    List<IEnemyBehavior> _behaviors;

    public int HP => _hp;
    public int AttackPower => _attackPower;
    public int BlockPower => _blockPower;

    public void ExcuteEnemyAction()
    {
        var index = Random.Range(0, _behaviors.Count);
        _behaviors[index].Excute();
    }
}
