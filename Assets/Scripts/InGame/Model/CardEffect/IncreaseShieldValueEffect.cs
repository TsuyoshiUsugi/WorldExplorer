using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 指定した防御値を得る
/// </summary>
[System.Serializable]
public class IncreaseShieldValue : ICardEffect
{
    [SerializeField] private int _increaseValue;

    public void EvolveCardEffect(int addPower)
    {
       _increaseValue += addPower;
    }

    public void ExcuteCardEffect()
    {
        FieldInfo.Instance.PlayerManager.Status.AddBlockPower(_increaseValue);
    }
}
