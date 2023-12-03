using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UniRx;

/// <summary>
/// プレイヤーターンの処理を管理するクラス
/// </summary>
public class PlayerTurnState : IInGameState
{
    public event Func<UniTask> OnEnterEvent;
    public event Func<UniTask> OnExitEvent;
    private PlayerManager _playerManager;

    public async UniTask OnEnter()
    {
        if (_playerManager == null) _playerManager = new();
        //デッキをから手札を取得
        _playerManager.DrawCard();
        //アクションコストを回復
        _playerManager.RestActionCost();    //アクションコストは3
        //プレイヤーの選択待ち処理を開始
        await UniTask.WaitUntil(() => _playerManager.ActionCost.Value <= 0);
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
