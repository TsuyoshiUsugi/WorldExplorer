using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// α酒力を得る
/// 体力をα回復する
/// 「酔い状態」の場合
/// 追加で体力をα回復する
/// </summary>
public class MinnadeOsakeEffect : ICardEffect
{
    [SerializeField] private int _sakePoint;
    public void EvolveCardEffect(int addPower)
    {
        //特になし
    }

    public void ExcuteCardEffect()
    {
        var playerManager = FieldInfo.Instance.PlayerManager;
        var playerData = GameDataManager.Instance.PlayerData;
        //酒力を得る
        playerManager.SakePower.AddSakePower(_sakePoint);
        //体力を回復する
        playerData.Status.HealHp(_sakePoint);
        //「酔い状態」かどうか
        if (playerManager.SakePower.IsDrank)
        {
            //追加で体力を回復する
            playerData.Status.HealHp(_sakePoint);
        }
    }
}
