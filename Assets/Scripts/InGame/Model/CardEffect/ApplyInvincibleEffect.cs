using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 指定したターン無敵になる
/// </summary>
[System.Serializable]
public class ApplyInvincibleEffect : ICardEffect
{
    [SerializeField] private int _turn = 1;

    public void EvolveCardEffect(int addPower)
    {
        //特になし
    }

    public void ExcuteCardEffect()
    {
        var player = FieldInfo.Instance.PlayerManager;
        if (!player.SakePower.IsDrank) return;
        player?.ApplyEffect(new InvincibleStatus(_turn));
    }
}
