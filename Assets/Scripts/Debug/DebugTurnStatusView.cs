using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

/// <summary>
/// デバックようのプレイヤーと敵のターンの状態を表示する
/// </summary>
public class DebugTurnStatusView : MonoBehaviour
{
    [Inject]
    PlayerManager _playerManager;
    [Inject]
    EnemyManager _enemyManager;
    [SerializeField] private Text _enemyTurnStatusText;
    [SerializeField] private Text _playerTurnStatusText;

    private void Update()
    {
        var enemyString = new System.Text.StringBuilder();
        enemyString.AppendLine("付与効果");
        foreach (var status in _enemyManager.TurnStatuses)
        {
            enemyString.AppendLine($"{status.GetType().Name}:残りターン{status.RemainTurn}");
        }
        _enemyTurnStatusText.text = enemyString.ToString();

        var playerString = new System.Text.StringBuilder();
        playerString.AppendLine("付与効果");

        foreach (var status in _playerManager.TurnStatuses)
        {
            playerString.AppendLine($"{status.GetType().Name}:残りターン{status.RemainTurn}");
        }
        _playerTurnStatusText.text = playerString.ToString();
    }
}
