using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// インゲームの進行管理を行う
/// 具体的には、
/// プレイヤーターン、エネミーターン、リザルトのながれを行う
/// </summary>
public class InGameSequenceManager : MonoBehaviour
{
    IInGameState _playerTurnState;
    IInGameState _enemyTurnState;

    private void Start()
    {
        _playerTurnState = new PlayerTurnState();
        _enemyTurnState = new EnemyTurnState();

        BindEvent();
        _playerTurnState.OnEnter();
    }

    /// <summary>
    /// 各ステートのバインド処理を行う
    /// </summary>
    private void BindEvent()
    {
        _playerTurnState.OnExitEvent += () => _enemyTurnState.OnEnter();
        _enemyTurnState.OnExitEvent += () => _playerTurnState.OnEnter();
    }
}
