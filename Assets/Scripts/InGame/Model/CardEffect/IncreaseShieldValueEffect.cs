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
    [SerializeField] private int _addIncreaseValue;

    public void EvolveCardEffect(int addPower)
    {
       _increaseValue += _addIncreaseValue;
    }
    
    public string GetEffectDescription()
    {
        return $"防御力を{_increaseValue}得る";
    }

    public void ExcuteCardEffect()
    {
        FieldInfo.Instance.PlayerManager.Status.AddBlockPower(_increaseValue);
    }
}
