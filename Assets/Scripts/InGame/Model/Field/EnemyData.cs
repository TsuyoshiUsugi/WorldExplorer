using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵のデータをもつスクリプタブルオブジェクト
/// </summary>
[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/CreateEnemyDataAsset")]
public class EnemyData : ScriptableObject
{
    [SerializeField, Header("敵の画像")] public Sprite EnemySprite;
    [SerializeField] public Status Status;
    [SerializeReference, SubclassSelector] public List<IEnemyBehavior> EnemyBehavior;
}
