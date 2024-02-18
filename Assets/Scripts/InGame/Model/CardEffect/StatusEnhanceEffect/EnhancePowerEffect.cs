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
    [SerializeField] int _addEnhancePower;
    [SerializeField] int _enhancePowerTurn;

    public void EvolveCardEffect(int addPower)
    {
        _enhancePower += _addEnhancePower; 
    }

    public void ExcuteCardEffect()
    {
        FieldInfo.Instance.PlayerManager.ApplyEffect(new EnhancePowerStatus(_enhancePowerTurn, _enhancePower));
    }
}

public class EnhancePowerStatus : TurnStatusBase
{
    private int _enhancePower;

    public EnhancePowerStatus(int turn, int enhancePower)
    {
        _remainTurn.Value = turn;
        _enhancePower = enhancePower;
    }

    protected override void CancelEffect(Status status)
    {
        FieldInfo.Instance.PlayerManager.Status.AddAttackPower(_enhancePower * -1);
    }

    protected override void ExecuteEffect(Status status)
    {
        FieldInfo.Instance.PlayerManager.Status.AddAttackPower(_enhancePower);
        Debug.Log(FieldInfo.Instance.PlayerManager.Status.AttackPower);
    }
}
