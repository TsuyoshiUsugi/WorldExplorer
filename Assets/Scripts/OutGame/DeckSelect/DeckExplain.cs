using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 現在選択しているデッキの説明を表示する
/// </summary>
public class DeckExplain : MonoBehaviour
{
    [SerializeField] private DeckInfo _deckInfo;
    [SerializeField] private Text _deckTitle;
    [SerializeField] private Text _deckExplain;

    public void SetDeckInfo(DeckInfo deckInfo)
    {
        _deckInfo = deckInfo;
        _deckTitle.text = _deckInfo.DeckName;
        _deckExplain.text = _deckInfo.DeckDescription;
    }
}
