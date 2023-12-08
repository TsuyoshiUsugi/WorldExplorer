using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵のデータをもつスクリプタブルオブジェクト
/// </summary>
[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/CreateEnemyDataAsset")]
public class EnemyData : ScriptableObject
{
    [SerializeField] public GameObject EnemyPrefab;
    [SerializeReference, SubclassSelector] public List<IEnemyBehavior> EnemyBehavior;
}
