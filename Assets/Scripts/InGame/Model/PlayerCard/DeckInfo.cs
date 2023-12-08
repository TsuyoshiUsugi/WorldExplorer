using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// デッキの情報を記したクラス
/// </summary>
[CreateAssetMenu(fileName = "DeckData", menuName = "ScriptableObjects/CreateDeckDataAsset")]
public class DeckInfo : ScriptableObject
{
    public List<CardData> Cards;
}
