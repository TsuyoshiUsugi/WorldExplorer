using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵の攻撃行動のクラス
/// </summary>
[System.Serializable]
public class EnemyAttack : IEnemyBehavior
{
    public IEnemyBehavior.EnemyAction Action => IEnemyBehavior.EnemyAction.Attack;

    public void Excute()
    {
        var player = FieldInfo.Instance.PlayerManager;
        var enemyPower = FieldInfo.Instance.EnemyManager.Status.AttackPower;
        player?.Status.ApplyDamage(enemyPower);
    }
}
