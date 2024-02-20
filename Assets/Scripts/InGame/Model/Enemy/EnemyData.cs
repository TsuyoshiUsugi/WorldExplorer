using System.Collections;
using System.Collections.Generic;
using GraphProcessor;
using UnityEngine;

/// <summary>
/// 敵のデータをもつスクリプタブルオブジェクト
/// </summary>
[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/CreateEnemyDataAsset")]
public class EnemyData : ScriptableObject
{
    [SerializeField, Header("敵の画像")] public Sprite EnemySprite;
    [SerializeField, Header("敵のステータス")] public Status Status;
    [SerializeReference, SubclassSelector] public List<IEnemyBehavior> EnemyBehavior;
    [SerializeField, Header("敵の行動ツリー")] public BaseGraph BehaviorTree;
}
