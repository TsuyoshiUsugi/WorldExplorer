using Cysharp.Threading.Tasks;
using System;
using UniRx;
using UnityEngine;

/// <summary>
/// 敵のターンの処理を行う
/// </summary>
public class EnemyTurnState : IInGameState
{
    public event Func<UniTask> OnEnterEvent;
    public event Func<UniTask> OnExitEvent;
    private EnemyManager _enemyManager;
    public event Action<Winner> OnGameEnd;

    public EnemyTurnState(EnemyManager enemyManager)
    {
        _enemyManager = enemyManager;
        _enemyManager.Status.HP.Subscribe(hp =>
        {
            if (hp <= 0) OnGameEnd?.Invoke(Winner.Player);
        });
    }

    public async UniTask OnEnter()
    {
        Debug.Log("enemyターン開始");
        OnEnterEvent?.Invoke();
        _enemyManager.ExcuteEnemyAction();
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
