using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

/// <summary>
/// 敵とPlayerのステータスを表示する
/// </summary>
public class DebugStatusView : MonoBehaviour
{
    [SerializeField] Text _playerStatusText;
    [SerializeField] Text _enemyStatusText;
    [Inject]
    private PlayerManager _playerManager;
    [Inject]
    private EnemyManager _enemyManager;

    private void Update()
    {
        ShowEnemyStatus(_enemyManager.Status);
        ShowPlayerStatus(_playerManager.Status);
    }

    private void ShowPlayerStatus(Status status)
    {
        _playerStatusText.text = $"HP: {status.HP}\n" +
            $"ATK: {status.AttackPower}\n" +
            $"DEF: {status.BlockPower}\n";
    }

    private void ShowEnemyStatus(Status status)
    {
        _enemyStatusText.text = $"HP: {status.HP}\n" +
            $"ATK: {status.AttackPower}\n" +
            $"DEF: {status.BlockPower}\n";
    }
}
