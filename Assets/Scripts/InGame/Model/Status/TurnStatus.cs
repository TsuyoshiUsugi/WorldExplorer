/// <summary>
/// プレイヤーや敵が持つターンで持続するステータスの基底クラス
/// </summary>
public abstract class TurnStatus
{
    /// <summary>
    /// ステータスの種類を取得するプロパティ
    /// </summary>
    //public StatusType StatusType { get; }

    /// <summary>
    /// ステータスの効果が持続するターン数を取得するプロパティ
    /// </summary>
    public int RemainTurn { get; }

    /// <summary>
    /// ステータスの効果を実行するメソッド
    /// </summary>
    public void Run()
    {
        Effect();
    }

    /// <summary>
    /// ここにステータスの実際の効果を記述する
    /// </summary>
    public abstract void Effect();
}

/// <summary>
/// ステータスの種類
/// </summary>
public enum StatusType
{
    PowerUp,
    PowerDown,
    GuardUp,
    GuardDown,
}
