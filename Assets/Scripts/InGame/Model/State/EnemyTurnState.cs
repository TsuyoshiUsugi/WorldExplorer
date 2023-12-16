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
    public EnemyManager EnemyManager => _enemyManager;

    public EnemyTurnState(EnemyManager enemyManager)
    {
        _enemyManager = enemyManager;
        _enemyManager.Status.HP.Subscribe(hp =>
        {
            if (hp <= 0) OnGameEnd?.Invoke(Winner.Player);
        });
    }

    /// <summary>
    /// 敵の情報をセットする
    /// </summary>
    public void SetEnemyInfo()
    {
        FieldInfo.Instance.EnemyManager = _enemyManager;
        _enemyManager.SetNextBehaviorIndex();

    }

    public async UniTask OnEnter()
    {
        //前処理
        await OnEnterEvent.Invoke();
        //メイン処理
        //敵の行動を実行する
        _enemyManager.ExcuteEnemyAction();

        //後処理
        //次に実行する行動のインデックスを設定する
        _enemyManager.SetNextBehaviorIndex();
        //持続する効果のターンを減らす
        _enemyManager.DecreaseEffectTurn();
        OnExit().Forget();
        await UniTask.CompletedTask;
    }

    public void OnUpdate() { }

    public async UniTask OnExit()
    {
        OnExitEvent?.Invoke();
        await UniTask.CompletedTask;
    }
}
