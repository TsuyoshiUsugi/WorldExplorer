using UnityEngine;

/// <summary>
/// 指定したターン数だけ攻撃力を下げるステータス
/// </summary>
public class AttackPowerDownStatus : TurnStatus
{
    private int _downPower = 10;

    public AttackPowerDownStatus(int downPower, int turn)
    {
        _downPower = downPower;
        _remainTurn.Value = turn;
    }

    protected override void ExecuteEffect(Status status)
    {
        status.AddAttackPower(_downPower * -1);
    }

    protected override void CancelEffect(Status status)
    {
        status.AddAttackPower(_downPower);
    }
}
