using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// インゲームの表示処理を管理する
/// </summary>
public class InGameView : MonoBehaviour
{
    [SerializeField] List<GameObject> _actionSymbolCount;
    [SerializeField] Text _actionSymbolCountText;
    [SerializeField] Text _deckCount;
    [SerializeField] Slider _playerHPBar;
    [SerializeField] Slider _enemyHPBar;

    /// <summary>
    /// 行動可能回数を表示するシンボルカウントを指定した個数表示する
    /// </summary>
    /// <param name="count"></param>
    public void SetActionSimbleImage(int count)
    {
        for (var i = 0; i < _actionSymbolCount.Count; i++)
        {
            if (i <= count - 1)
            {
                _actionSymbolCount[i].SetActive(true);
            }
            else
            {
                _actionSymbolCount[i].SetActive(false);
            }
        }
        _actionSymbolCountText.text = $"{count}/3";
    }

    /// <summary>
    /// デッキのカード枚数を表示する
    /// </summary>
    /// <param name="remainDecknum"></param>
    /// <param name="maxDeckNum"></param>
    public void SetDeckCardNumText(int remainDecknum, int maxDeckNum)
    {
        _deckCount.text = $"{remainDecknum}/{maxDeckNum}";
    }

    public void ShowPlayerHP(int current, int max)
    {
        _playerHPBar.value = (float)current / max;
    }

    public void ShowEnemyHP(int current, int max)
    {
        _enemyHPBar.value = (float)current / max;
    }
}
