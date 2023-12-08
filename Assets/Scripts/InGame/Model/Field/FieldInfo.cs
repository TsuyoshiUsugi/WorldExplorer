using VContainer;

/// <summary>
/// 場の情報を持つクラス
/// </summary>
public class FieldInfo : AbstractSingleton<FieldInfo>
{
    public PlayerManager PlayerManager;
    public EnemyManager EnemyManager;
}
