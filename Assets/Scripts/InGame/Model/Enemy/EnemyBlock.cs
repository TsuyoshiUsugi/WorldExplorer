using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵のブロック行動。1ターンの間、ブロック力を上げる
/// </summary>
[System.Serializable]
public class EnemyBlock : IEnemyBehavior
{
    [SerializeField] private int _addBlockPower = 1;
    [SerializeField, Header("行動終了後に増やす値")] int _afterActionAddValue = 1;
    public IEnemyBehavior.EnemyAction Action => IEnemyBehavior.EnemyAction.Block;

    public void Excute()
    {
        //FieldInfo.Instance.EnemyManager.Status.AddBlockPower(_addBlockPower);
        FieldInfo.Instance.EnemyManager.ApplyEffect(new BlockPowerUpStatus(_addBlockPower, 2)); //実行したターンも含めるので2ターン
        //行動終了後VALUEの値を増やす
        FieldInfo.Instance.EnemyManager.Status.AddAttackPower(_afterActionAddValue);
        FieldInfo.Instance.EnemyManager.Status.AddBlockPower(_afterActionAddValue);
    }
}
