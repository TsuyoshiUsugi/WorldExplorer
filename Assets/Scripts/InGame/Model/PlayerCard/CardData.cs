using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カードのデータベース。カードのPrefabと効果の情報をもつ
/// </summary>
[CreateAssetMenu(fileName = "CardData", menuName = "ScriptableObjects/CreateCardDataAsset")]
public class CardData : ScriptableObject
{
    [SerializeField] 
    public GameObject CardEntity;
    [SerializeReference, SubclassSelector]
    public List<ICardEffect> CardEffects; 

    /// <summary>
    /// カードの効果を発動する
    /// </summary>
    public void PlayCard()
    {
        foreach (var effet in CardEffects)
        {
            effet.ExcuteCardEffect();
        }
    }
}

/// <summary>
/// カードのデータを持つクラス
/// </summary>
public class CardDataEntity
{
    public GameObject CardEntity;
    public List<ICardEffect> CardEffects;

    public CardDataEntity(CardData cardData) 
    {
        CardEntity = cardData.CardEntity;
        CardEffects = cardData.CardEffects;
    }

    /// <summary>
    /// カードの効果を発動する
    /// </summary>
    public void PlayCard()
    {
        foreach (var effet in CardEffects)
        {
            effet.ExcuteCardEffect();
        }
    }
}
