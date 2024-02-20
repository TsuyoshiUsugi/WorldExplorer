using System.Collections;
using System.Collections.Generic;
using TsuyoshiBehaviorTree;
using UnityEngine;

/// <summary>
/// 敵の攻撃行動のクラス
/// </summary>
[System.Serializable]
public class EnemyAttack : IAnyAction
{
    [SerializeField, Header("行動終了後に増やす値")] int _afterActionAddValue = 1;
    public NodeState OnUpdate()
    {
        var player = FieldInfo.Instance.PlayerManager;
        var enemyPower = FieldInfo.Instance.EnemyManager.Status.AttackPower;
        player?.Status.ApplyDamage(enemyPower);
        FieldInfo.Instance.EnemyManager.Status.AddAttackPower(_afterActionAddValue);
        FieldInfo.Instance.EnemyManager.Status.AddBlockPower(_afterActionAddValue);
        Debug.Log("攻撃行動を実行しました。");
        return NodeState.Success;
    }
    
    public IAnyAction Clone()
    {
        return new EnemyAttack();
    }
}
