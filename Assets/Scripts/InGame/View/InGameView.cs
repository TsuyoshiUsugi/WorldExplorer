using Cysharp.Threading.Tasks;
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
    [SerializeField] Image _playerImage;
    [SerializeField] Slider _playerHPBar;
    [SerializeField] Text _playerHPText;
    [SerializeField] Slider _enemyHPBar;
    [SerializeField] Image _enemyImage;
    [SerializeField] Text _enemyHPText;
    [SerializeField] Text _sakePowerText;
    [SerializeField] Image _enemyActionImage;
    [SerializeField] Text _enemyActionText;
    [SerializeField] List<IconPair> _enemyActionIcons;
    [SerializeField] Button _turnEndButton;
    [SerializeField] GameObject _turnObject;
    [SerializeField] Text _turnNotifyText;
    [SerializeField] GameObject _damageCountPrefab;
    public Button TurnEndButton => _turnEndButton;

    #region プレイヤー関連の表示

    public async UniTask ShowTurnNotify(Turn turn)
    {
        _turnNotifyText.gameObject.SetActive(true);
        if (turn == Turn.PlayerTurn)
        {
            _turnObject.SetActive(true);
            _turnNotifyText.text = "プレイヤーのターン";
        }
        else
        {
            _turnObject.SetActive(true);
            _turnNotifyText.text = "敵のターン";
        }
        await UniTask.Delay(1000);
        _turnObject.SetActive(false);
    }

    public enum Turn
    {
        PlayerTurn,
        EnemyTurn,
    }

    /// <summary>
    /// プレイヤーの画像をSpriteにセットする
    /// </summary>
    /// <param name="sprite"></param>
    public void ShowPlayerImage(Sprite sprite)
    {
        _playerImage.sprite = sprite;
    }

    /// <summary>
    /// ダメージを表示する
    /// ダメージを受けた側の画像の位置に表示する
    /// </summary>
    /// <param name="turn"></param>
    /// <param name="count"></param>
    public void ShowDamageCount(Turn turn, int preHp, int curHp)
    {
        var count = preHp - curHp;
        if (turn == Turn.PlayerTurn)
        {
            var view = Instantiate(_damageCountPrefab, _enemyImage.transform).GetComponent<DamageCountView>();
            view.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
            view.ShowDamage(count).Forget();
            
            if (count > 0)
            {
                view.SetColor(Color.red);
            }
            else
            {
                view.SetColor(Color.green);
            }
        }
        else
        {
            _playerImage.transform.rotation = Quaternion.Euler(0, 0, 0);
            var view = Instantiate(_damageCountPrefab, _playerImage.transform).GetComponent<DamageCountView>();
            view.ShowDamage(count).Forget();
            if (count > 0)
            {
                view.SetColor(Color.red);
            }
            else
            {
                view.SetColor(Color.green);
            }
        }
    }

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

    public void ShowSakePower(int current, int max)
    {
        _sakePowerText.text = $"{current}/{max}";
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

    #endregion

    #region 敵関連の表示
    public void ShowEnemyImage(Sprite sprite)
    {
        _enemyImage.sprite = sprite;
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

    /// <summary>
    /// ターン終了ボタンを表示する
    /// </summary>
    public void SetTurnEndButtonActive(bool visible)
    {
        if (visible)
        {
            _turnEndButton.enabled = true;
        }
        else
        {
            _turnEndButton.enabled = false;
        }
    }

    #endregion

}

[System.Serializable]
public class IconPair
{
    public IEnemyBehavior.EnemyAction EnemyAction;
    public Sprite Sprite;
}
