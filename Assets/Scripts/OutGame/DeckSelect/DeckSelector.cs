using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 入力を検知してデッキを選択する
/// </summary>
public class DeckSelector : MonoBehaviour
{
    [SerializeField] private List<DeckInfo> _decks;
    [SerializeField] private DeckInfo _deckInfo;
    [SerializeField] private Text _deckTitle;
    [SerializeField] private Text _deckExplain;
    [SerializeField] private List<GameObject> _deckObjList;
    [SerializeField] private int _currentDeckIndex;
    // Start is called before the first frame update
    void Start()
    {
        _currentDeckIndex = 0;
        SetDeckInfo(_decks[_currentDeckIndex]);
    }

    void Update()
    {
        //デッキは二種類で上下にデッキの画像が移動する
        if (Input.mouseScrollDelta.y > 0 && _currentDeckIndex != 0)
        {
            SetDeckInfo(_decks[_currentDeckIndex -= 1]);
            SetDeckImage(true);
        }
        if (Input.mouseScrollDelta.y < 0 && _currentDeckIndex != _decks.Count - 1)
        {
            SetDeckInfo(_decks[_currentDeckIndex += 1]);
            SetDeckImage(false);
        }
    }

    void SetDeckImage(bool isUp)
    {
        if (isUp)
        {
            _deckObjList[0].transform.SetAsFirstSibling();
        }
        else
        {
            _deckObjList[1].transform.SetAsFirstSibling();
        }
    }


    public void SetDeckInfo(DeckInfo deckInfo)
    {
        _deckInfo = deckInfo;
        _deckTitle.text = _deckInfo.DeckName;
        _deckExplain.text = _deckInfo.DeckDescription;
    }
}
