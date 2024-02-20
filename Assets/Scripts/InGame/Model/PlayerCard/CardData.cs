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
    [Header("カードの名前")]
    public string CardName;
    [Header("カードの説明"), TextArea(1, 3)]
    public string CardExplain;
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
    public string CardName;
    public GameObject CardEntity;
    public List<ICardEffect> CardEffects;
    public CardEffectBase CardEffectView;
    public PlayCardFieldType PlayFieldCardType;
    public string CardExplain;

    public CardDataEntity(CardData cardData, int iD)
    {
        CardEntity = cardData.CardPrefab;
        CardEffects = cardData.CardEffects;
        PlayFieldCardType = cardData.PlayFieldCardType;
        string description = "";
        if (CardEffects != null)
        {
            foreach (var effect in CardEffects)
            {
                description += effect.GetEffectDescription() + "\n";
            }
        }
        CardExplain = description;
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
