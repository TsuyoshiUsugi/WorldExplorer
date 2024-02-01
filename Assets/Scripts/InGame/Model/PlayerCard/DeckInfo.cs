using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// デッキの情報を記したクラス
/// </summary>
[CreateAssetMenu(fileName = "DeckData", menuName = "ScriptableObjects/CreateDeckDataAsset")]
public class DeckInfo : ScriptableObject
{
    public string DeckName;
    public string DeckDescription;
    public GameObject DeckImage;
    public List<CardData> Cards;
}
