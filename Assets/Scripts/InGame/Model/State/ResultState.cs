using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リザルトステートを管理するクラス
/// </summary>
public class ResultState : IInGameState
{
    public event Func<UniTask> OnEnterEvent;
    public event Func<UniTask> OnExitEvent;

    public async UniTask OnEnter()
    {
        OnEnterEvent?.Invoke();
        await UniTask.CompletedTask;
    }

    public async UniTask OnExit()
    {
        OnExitEvent?.Invoke();
        await UniTask.CompletedTask;    
    }

    public void OnUpdate()
    {
        
    }
}
