using Cysharp.Threading.Tasks;
using System;
using UniRx;

/// <summary>
/// 敵のターンの処理を行う
/// </summary>
public class EnemyTurnState : IInGameState
{
    public event Func<UniTask> OnEnterEvent;
    public event Func<UniTask> OnExitEvent;


    public async UniTask OnEnter()
    {
        OnEnterEvent?.Invoke();
        await UniTask.CompletedTask;
        OnExit().Forget();
    }

    public void OnUpdate()
    {

    }

    public UniTask OnExit()
    {
        return OnExitEvent?.Invoke() ?? UniTask.FromResult(Unit.Default);
    }
}
