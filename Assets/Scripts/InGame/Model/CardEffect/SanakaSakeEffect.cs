using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 「三河武士」を進化させる
/// 手札に「三河武士」がある場合、行動回数を減らさずに使用できる
/// 手札に「三河武士」がない場合、手札に「三河武士」を加える
/// </summary>
[System.Serializable]
public class SanakaSakeEffect : ICardEffect
{
    [SerializeField] CardData _sanka;
    [SerializeField] CardData _shinkaSanka;

    public void EvolveCardEffect()
    {
        //特になし
    }

    public void ExcuteCardEffect()
    {
        var player = FieldInfo.Instance.PlayerManager;
        var playerHand = player.HandCard;

        //手札に「三河武士」があるかどうか
        var cardsWithBothEffects = playerHand
            .Where(card => card.CardEffects.OfType<ApplyDamageEffect>().Any() 
            && card.CardEffects.OfType<ApplyDamageEffectIfDrunk>().Any())
            .ToList();

        if (player.TryGetTargetCard(4, out var card))
        {
            Debug.Log(card.CardEntity.name);
            //手札に「三河武士」がある場合、行動回数を減らさずに使用できる
            player.AddActionCost(1);
            //手札から「三河武士」を捨て、進化した「三河武士」を加える
            playerHand.Remove(card);
            player.AddHandCard(new CardDataEntity(_shinkaSanka, _shinkaSanka.Id));   
        }
        else
        {
            //手札に「三河武士」がない場合、手札に「三河武士」を加える
            player.AddHandCard(new CardDataEntity(_sanka, _sanka.Id));
        }
    }
}