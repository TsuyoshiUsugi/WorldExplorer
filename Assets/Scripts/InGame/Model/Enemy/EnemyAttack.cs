using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵の攻撃行動のクラス
/// </summary>
[System.Serializable]
public class EnemyAttack : IEnemyBehavior
{
    [SerializeField, Header("行動終了後に増やす値")] int _afterActionAddValue = 1;
    public IEnemyBehavior.EnemyAction Action => IEnemyBehavior.EnemyAction.Attack;

    public void Excute()
    {
        var player = FieldInfo.Instance.PlayerManager;
        var enemyPower = FieldInfo.Instance.EnemyManager.Status.AttackPower;
        player?.Status.ApplyDamage(enemyPower);
        FieldInfo.Instance.EnemyManager.Status.AddAttackPower(_afterActionAddValue);
        FieldInfo.Instance.EnemyManager.Status.AddBlockPower(_afterActionAddValue);
    }
}
