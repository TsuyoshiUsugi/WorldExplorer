using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カードのデータベース。カードのPrefabと効果の情報をもつ
/// </summary>
[CreateAssetMenu(fileName = "CardData", menuName = "ScriptableObjects/CreateCardDataAsset")]
public class CardData : ScriptableObject
{
    [Header("プレイするフィールドのタイプ")]
    public PlayCardFieldType PlayFieldCardType; 
    [Header("カードのID")]
    public int Id;
    [Header("場にでるカードのPrefab")] 
    public GameObject CardPrefab;
    [SerializeReference, SubclassSelector, Header("カードの効果一覧")]
    public List<ICardEffect> CardEffects;
    public CardEffectBase CardEffectView;
}

/// <summary>
/// カードのデータを持つクラス
/// </summary>
public class CardDataEntity
{
    public int ID;
    public GameObject CardEntity;
    public List<ICardEffect> CardEffects;
    public CardEffectBase CardEffectView;
    public PlayCardFieldType PlayFieldCardType;

    public CardDataEntity(CardData cardData, int iD)
    {
        CardEntity = cardData.CardPrefab;
        CardEffects = cardData.CardEffects;
        PlayFieldCardType = cardData.PlayFieldCardType;
        ID = iD;
    }

    /// <summary>
    /// カードの効果を発動する
    /// </summary>
    public async void PlayCard()
    {
        foreach (var effet in CardEffects)
        {
            
            effet.ExcuteCardEffect();
        }
    }
}
