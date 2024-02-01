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
    [SerializeField] private Text _deckTitle;
    [SerializeField] private Text _deckExplain;
    [SerializeField] private List<GameObject> _deckObjList;
    private int _currentDeckIndex;
    private DeckInfo _deckInfo;
    private float _size = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        _currentDeckIndex = 0;
        SetDeckDescription(_decks[_currentDeckIndex]);
    }

    void Update()
    {
        //デッキは二種類で上下にデッキの画像が移動する
        if (Input.mouseScrollDelta.y > 0 && _currentDeckIndex != 0)
        {
            SetDeckDescription(_decks[_currentDeckIndex -= 1]);
            SetDeckImage(true);
        }
        if (Input.mouseScrollDelta.y < 0 && _currentDeckIndex != _decks.Count - 1)
        {
            SetDeckDescription(_decks[_currentDeckIndex += 1]);
            SetDeckImage(false);
        }
    }

    /// <summary>
    /// デッキの画像を上下に移動する
    /// </summary>
    /// <param name="isUp"></param>
    void SetDeckImage(bool isUp)
    {
        if (isUp)
        {
            _deckObjList[0].transform.SetAsFirstSibling();
            _deckObjList[0].transform.localScale *= _size;
            _deckObjList[1].transform.localScale /= _size;
        }
        else
        {
            _deckObjList[1].transform.SetAsFirstSibling();
            _deckObjList[1].transform.localScale *= _size;
            _deckObjList[0].transform.localScale /= _size;
        }
    }

    /// <summary>
    /// デッキの説明をセットする
    /// </summary>
    /// <param name="deckInfo"></param>
    void SetDeckDescription(DeckInfo deckInfo)
    {
        _deckInfo = deckInfo;
        _deckTitle.text = _deckInfo.DeckName;
        _deckExplain.text = _deckInfo.DeckDescription;
    }
}
