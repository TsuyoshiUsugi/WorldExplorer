using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのデータをもつスクリプタブルオブジェクト
/// </summary>
[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/CreatePlayerDataAsset")]
public class PlayerData : ScriptableObject
{
    [SerializeField, Header("プレイヤー画像")] public Sprite PlayerSprite;
    [SerializeField, Header("プレイヤーのステータス")] public Status Status;
}
