using VContainer;
using UnityEngine;

/// <summary>
/// 場の情報を持つクラス
/// </summary>
public class FieldInfo : AbstractSingleton<FieldInfo>
{
    public PlayCardField PlayCardField;
    public PlayerManager PlayerManager;
    public EnemyManager EnemyManager;
}
