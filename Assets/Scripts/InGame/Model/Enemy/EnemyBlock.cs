using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵のブロック行動
/// </summary>
[System.Serializable]
public class EnemyBlock : IEnemyBehavior
{
    [SerializeField] private int _addBlockPower = 1;
    public IEnemyBehavior.EnemyAction Action => IEnemyBehavior.EnemyAction.Block;

    public void Excute()
    {
        FieldInfo.Instance.EnemyManager.Status.AddBlockPower(_addBlockPower);
    }
}
