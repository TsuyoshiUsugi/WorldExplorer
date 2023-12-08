using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵の攻撃行動のクラス
/// </summary>
[System.Serializable]
public class EnemyAttack : IEnemyBehavior
{
    public void Excute()
    {
        //var player = FieldInfo.Instance.PlayerManager;
        //var enemyPower = FieldInfo.Instance.EnemyManager.AttackPower;
        //player?.ApplyDamage(enemyPower);
    }
}
