using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻撃力を強化するカードの効果
/// </summary>
[System.Serializable]
public class EnhancePowerEffect : ICardEffect
{
    [SerializeField] int _enhancePower;

    public void EvolveCardEffect(int addPower)
    {
       _enhancePower += addPower;
    }

    public void ExcuteCardEffect()
    {
        FieldInfo.Instance.PlayerManager.Status.AddAttackPower(_enhancePower);
    }
}
