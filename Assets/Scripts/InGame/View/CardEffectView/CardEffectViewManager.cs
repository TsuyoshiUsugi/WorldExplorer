using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カードのエフェクトを管理するクラス
/// </summary>
public class CardEffectViewManager : AbstractSingleton<CardEffectViewManager>
{
    [SerializeField] private List<CardEffectBase> _cardEffectViews = new List<CardEffectBase>();

    public void ShowCardEffect(int id)
    {
        _cardEffectViews.Find(x => x.ID == id)?.PlayCardEffectView();
    }   
}
