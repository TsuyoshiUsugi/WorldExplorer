using System.Collections;
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
        
    }
}
