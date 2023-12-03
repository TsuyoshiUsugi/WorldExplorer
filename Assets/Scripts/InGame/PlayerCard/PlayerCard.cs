using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのカードクラス
/// </summary>
public class PlayerCard
{
    private List<ICardEffect> _cardEffects = new List<ICardEffect>();

    /// <summary>
    /// カードをプレイする。ここに効果を書く
    /// </summary>
    public void PlayCard()
    {
        _cardEffects.Add(new ApplyDamageEffect());  //テスト用

        foreach (ICardEffect effect in _cardEffects)
        {
            effect.ExcuteCardEffect();
        }

        _cardEffects.Clear();  //テスト用

    }
}
