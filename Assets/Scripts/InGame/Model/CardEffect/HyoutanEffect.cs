using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// α酒力を得る
///「酔い状態」の場合
/// αダメージ与える
/// </summary>
public class HyoutanEffect : ICardEffect
{
    [SerializeField] private int _sakePoint = 3; //数値は仮
    [SerializeField] private int _cardDamage = 5; //数値は仮
    public void EvolveCardEffect(int addPower)
    {
        //特になし
    }

    public void ExcuteCardEffect()
    {
        var player = FieldInfo.Instance.PlayerManager; 
        var enemy = FieldInfo.Instance.EnemyManager;
        //酒力を得る
        player.SakePower.AddSakePower(_sakePoint);
        //「酔い状態」かどうか
        if (player.SakePower.IsDrank)
        {
            //ダメージを与える
            enemy?.Status.ApplyDamage(_cardDamage);
        }
    }
}
