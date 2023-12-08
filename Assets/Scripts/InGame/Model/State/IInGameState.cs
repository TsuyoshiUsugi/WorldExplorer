using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
/// <summary>
/// インゲームの各ステートのインターフェース
/// </summary>
public interface IInGameState
{
    public event Func<UniTask> OnEnterEvent;
    public event Func<UniTask> OnExitEvent;

    public UniTask OnEnter();
    public void OnUpdate();
    public UniTask OnExit(); 
}
