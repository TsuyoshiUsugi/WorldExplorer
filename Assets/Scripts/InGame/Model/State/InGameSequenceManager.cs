using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

/// <summary>
/// インゲームの進行管理を行う
/// 具体的には、
/// プレイヤーターン、エネミーターン、リザルトのながれを行う
/// </summary>
public class InGameSequenceManager : MonoBehaviour
{
    [Inject]
    PlayerTurnState _playerTurnState;
    [Inject]
    EnemyTurnState _enemyTurnState;

    private void Start()
    {
        BindEvent();
        _playerTurnState.OnEnter().Forget();
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
