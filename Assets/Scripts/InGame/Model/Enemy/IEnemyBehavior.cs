using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵の行動のインターフェース
/// </summary>
public interface IEnemyBehavior
{
    public EnemyAction Action { get;}
    
    /// <summary>
    /// ここに行動の中身を書く
    /// </summary>
    public void Excute();

    /// <summary>
    /// 行動のタイプ
    /// </summary>
    public enum EnemyAction
    {
        Attack,
        Block,
    }
}

