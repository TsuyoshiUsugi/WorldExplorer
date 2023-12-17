using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// α酒力を得る
/// 体力をα回復する
/// 「酔い状態」の場合
/// 追加で体力をα回復する
/// </summary>
[System.Serializable]
public class MinnadeOsakeEffect : ICardEffect
{
    [SerializeField] private int _sakePoint = 3;//数値は仮
    [SerializeField] private int _healPoint = 3;//数値は仮
    [SerializeField] private int _drankHealPoint = 2;//数値は仮
    public void EvolveCardEffect(int addPower)
    {
        //特になし
    }

    public void ExcuteCardEffect()
    {
        var playerManager = FieldInfo.Instance.PlayerManager;
        var playerStatus = playerManager.Status;
        //酒力を得る
        playerManager.SakePower.AddSakePower(_sakePoint);
        //体力を回復する
        playerStatus.HealHp(_healPoint);
        //「酔い状態」かどうか
        if (!playerManager.SakePower.IsDrank) return;
        //追加で体力を回復する
        playerStatus.HealHp(_drankHealPoint);
    }
}
