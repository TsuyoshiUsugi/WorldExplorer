using UnityEngine;

/// <summary>
/// 指定したターン数だけ攻撃力を下げるステータス
/// </summary>
public class AttackPowerDownStatus : TurnStatusBase
{
    private int _downPower = 10;

    public AttackPowerDownStatus(int downPower, int turn)
    {
        _downPower = downPower;
        _remainTurn.Value = turn;
    }

    protected override void ExecuteEffect(Status status)
    {
        if (status.AttackPower <= 0)
        {
            _downPower = 0;
        }
        status.AddAttackPower(_downPower * -1);
    }

    protected override void CancelEffect(Status status)
    {
        status.AddAttackPower(_downPower);
    }
}

/// <summary>
/// 指定したターン数だけブロック力を上げるステータス
/// </summary>
public class BlockPowerUpStatus : TurnStatusBase
{
    private int _addBlockPower = 10;

    public BlockPowerUpStatus(int downPower, int turn)
    {
        _addBlockPower = downPower;
        _remainTurn.Value = turn;
    }

    protected override void ExecuteEffect(Status status)
    {
        status.AddBlockPower(_addBlockPower);
    }

    protected override void CancelEffect(Status status)
    {
        status.AddBlockPower(_addBlockPower *= -1);
    }
}
