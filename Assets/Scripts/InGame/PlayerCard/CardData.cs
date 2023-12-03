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
}
