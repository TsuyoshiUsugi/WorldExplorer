using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// カードのエフェクトを管理するクラス
/// </summary>
public class CardEffectViewManager : AbstractSingleton<CardEffectViewManager>
{
    [SerializeField] private List<CardEffectBase> _cardEffectViews = new List<CardEffectBase>();

    public async UniTask ShowCardEffect(int id)
    {
        var result = _cardEffectViews.Find(x => x.ID == id);
        if (result == null) return;
        await _cardEffectViews.Find(x => x.ID == id).PlayCardEffectView();
    }   
}
