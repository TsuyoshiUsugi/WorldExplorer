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
    [SerializeField] Text _playerHPText;
    [SerializeField] Slider _enemyHPBar;
    [SerializeField] Text _enemyHPText;
    [SerializeField] Image _enemyActionImage;
    [SerializeField] Text _enemyActionText;
    [SerializeField] List<IconPair> _enemyActionIcons;

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
        _playerHPText.text = $"{current}/{max}";
    }

    public void ShowEnemyHP(int current, int max)
    {
        _enemyHPBar.value = (float)current / max;
        _enemyHPText.text = $"{current}/{max}";
    }

    /// <summary>
    /// 敵の行動を表示する
    /// </summary>
    /// <param name="enemyAction"></param>
    /// <param name="actionRelatedNum">行動に関係する値がある場合は入れる</param>
    public void ShowEnemyAction(IEnemyBehavior.EnemyAction enemyAction, int actionRelatedNum = 0)
    {
        switch (enemyAction)
        {
            case IEnemyBehavior.EnemyAction.Attack:
                _enemyActionImage.sprite = _enemyActionIcons.Find(sprite => sprite.EnemyAction == IEnemyBehavior.EnemyAction.Attack).Sprite;
                _enemyActionText.text = actionRelatedNum.ToString();
                break;
            case IEnemyBehavior.EnemyAction.Block:
                _enemyActionImage.sprite = _enemyActionIcons.Find(sprite => sprite.EnemyAction == IEnemyBehavior.EnemyAction.Block).Sprite;
                _enemyActionText.text = actionRelatedNum.ToString();
                break;
        }
    }

}

[System.Serializable]
public class IconPair
{
    public IEnemyBehavior.EnemyAction EnemyAction;
    public Sprite Sprite;
}
