using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 指定したターン数だけ無敵になるステータス
/// </summary>
public class InvincibleStatus : TurnStatusBase
{
    public InvincibleStatus(int turn)
    {
        _remainTurn.Value = turn;
    }

    protected override void CancelEffect(Status status)
    {
        status.AddBlockPower(Mathf.Min(9999 * -1));
    }

    protected override void ExecuteEffect(Status status)
    {
        status.AddBlockPower(9999);
    }
}
