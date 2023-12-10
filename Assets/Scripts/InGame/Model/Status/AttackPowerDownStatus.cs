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
public class BlockPowerUpStatus : TurnStatus
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
        Debug.Log($"ブロック力上昇{_addBlockPower}、現在のブロック値{status.BlockPower}");
    }

    protected override void CancelEffect(Status status)
    {
        status.AddBlockPower(_addBlockPower *= -1);
        Debug.Log($"ブロック力上昇状態解除{_addBlockPower}、現在のブロック値{status.BlockPower}");
    }
}
