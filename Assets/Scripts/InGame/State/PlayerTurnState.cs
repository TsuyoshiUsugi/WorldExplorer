using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnState : IInGameState
{
    public void OnEnter()
    {
        //デッキをから手札を取得
        //アクションカウントを回復
        //プレイヤーの選択待ち処理を開始
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public void OnUpdate()
    {
        throw new System.NotImplementedException();
    }
}
