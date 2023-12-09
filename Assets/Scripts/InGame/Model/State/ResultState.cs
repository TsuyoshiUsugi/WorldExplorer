using Cysharp.Threading.Tasks;
using System;

/// <summary>
/// リザルトステートを管理するクラス
/// </summary>
public class ResultState : IInGameState
{
    public event Func<UniTask> OnEnterEvent;
    public event Func<UniTask> OnExitEvent;
    public event Action<Winner> OnGameEnd;

    public async UniTask OnEnter(){ await UniTask.CompletedTask; }

    public async UniTask OnEnter(Winner winner)
    {
        OnEnterEvent?.Invoke();
        OnGameEnd?.Invoke(winner);
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
