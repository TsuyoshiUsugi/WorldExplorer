using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ヒョウタンを持っている場合行動回数を減らさない
/// </summary>
[System.Serializable]
public class GourdNotDecreasingActionEffect : ICardEffect
{
    [Header("ヒョウタンのCardId")]
    [SerializeField] private int _gourdId; 
    public void EvolveCardEffect(int addPower)
    {
        
    }

    public string GetEffectDescription()
    {
        return $"アクションコストを減らさない";
    }

    public void ExcuteCardEffect()
    {
        var playerManager = FieldInfo.Instance.PlayerManager;

        if (playerManager.TryGetTargetCard(_gourdId, out var cardData)) 
        {
            //アクションコストを減らさない
            FieldInfo.Instance.PlayerManager.AddActionCost(1);
        }

    }
}
