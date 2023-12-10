using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カードのデータベース。カードのPrefabと効果の情報をもつ
/// </summary>
[CreateAssetMenu(fileName = "CardData", menuName = "ScriptableObjects/CreateCardDataAsset")]
public class CardData : ScriptableObject
{
    [SerializeField, Header("場にでるカードのPrefab")] 
    public GameObject CardPrefab;
    [SerializeReference, SubclassSelector, Header("カードの効果一覧")]
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
        CardEntity = cardData.CardPrefab;
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
