using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// リザルトの表示処理を管理する
/// </summary>
public class ResultView : MonoBehaviour
{
    [SerializeField] GameObject _winResultPanel;
    [SerializeField] Button _healRewardButton;
    [SerializeField] Button _powerUpRewardButton;
    [SerializeField] GameObject _lsoeResultPanel;
    [SerializeField] Button _retryButton;
    [SerializeField] Button _backToTitleButton;
    public event Action OnClickHealRewardButton;
    public event Action OnClickPowerUpRewardButton;
    public event Action OnClickRetryButton;
    public event Action OnClickBackToTitleButton;

    /// <summary>
    /// 勝利時のリザルトを表示する
    /// </summary>
    public void ShowWinResultPanel()
    {
        _winResultPanel.SetActive(true);
    }

    /// <summary>
    /// 敗北時のリザルトを表示する
    /// </summary>
    public void ShowLoseResultPanel()
    {
        _lsoeResultPanel.SetActive(true);
    }

    /// <summary>
    /// UIの初期化処理とイベントの登録を行う
    /// </summary>
    private void Start()
    {
        _winResultPanel.SetActive(false);
        _lsoeResultPanel.SetActive(false);
        _healRewardButton.onClick.AddListener(() => OnClickHealRewardButton?.Invoke());
        _powerUpRewardButton.onClick.AddListener(() => OnClickPowerUpRewardButton?.Invoke());
        _backToTitleButton.onClick.AddListener(() => OnClickBackToTitleButton?.Invoke());
        _retryButton.onClick.AddListener(() => OnClickRetryButton?.Invoke());
    }
}
