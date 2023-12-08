using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

/// <summary>
/// プレイヤーターンの処理を管理するクラス
/// </summary>
public class PlayerTurnState : IInGameState
{
    public event Func<UniTask> OnEnterEvent;
    public event Func<UniTask> OnExitEvent;
    private PlayerManager _playerManager;

    public PlayerTurnState(PlayerManager playerManager)
    {
        _playerManager = playerManager;
    }

    public async UniTask OnEnter()
    {
        Debug.Log("playerターン開始");
        _playerManager.SetActivePlayer(true);
        //前のターンで残っている手札を戻す
        _playerManager.ResetHandCard();
        //デッキをから手札を取得
        _playerManager.DrawCard();
        //アクションコストを回復
        _playerManager.RestActionCost();    //アクションコストは3
        //酒力を追加する処理
        //プレイヤーの選択待ち処理を開始
        await UniTask.WaitUntil(() => _playerManager.ActionCost.Value <= 0);
        _playerManager.SetActivePlayer(false);
        OnExit().Forget();
    }

    public void OnUpdate()
    {

    }

    public async UniTask OnExit()
    {
        OnExitEvent?.Invoke();
        await UniTask.CompletedTask;
    }
}
