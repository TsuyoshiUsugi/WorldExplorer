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
        Debug.Log("現在の敵の攻撃力" + status.AttackPower);
        status.AddAttackPower(_downPower * -1);
        Debug.Log("攻撃力を" + _downPower + "下げました");
        Debug.Log("現在の敵の攻撃力" + status.AttackPower);
        Debug.Log("残りターン数" + _remainTurn.Value);
    }

    protected override void CancelEffect(Status status)
    {
        status.AddAttackPower(_downPower);
    }
}
