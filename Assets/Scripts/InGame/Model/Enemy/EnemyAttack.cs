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
        FieldInfo.Instance.PlayerManager.ApplyDamage(FieldInfo.Instance.EnemyManager.AttackPower);
    }
}
