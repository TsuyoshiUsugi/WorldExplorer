using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// もしヒョウタンのカードを手札に持っていなかった場合手札に加える
/// </summary>
[System.Serializable]
public class IfNotHaveGourdAddHandEffect : ICardEffect
{
    [Header("ヒョウタンのCardId")]
    [SerializeField] private int _gourdId;

    public void EvolveCardEffect(int addPower)
    {
        
    }
    
    public string GetEffectDescription()
    {
        var playerManager = FieldInfo.Instance.PlayerManager;

        return $"を手札に加える";
    }

    public void ExcuteCardEffect()
    {
        var playerManager = FieldInfo.Instance.PlayerManager;

        if (!playerManager.TryGetTargetCard(_gourdId, out var cardData))
        {
            //もしヒョウタンのカードを手札に持っていなかった場合手札に加える
            playerManager.TryFromDeckAddSpecificCard(_gourdId);
        }
    }
}
