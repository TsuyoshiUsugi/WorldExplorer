/// <summary>
/// プレイヤーや敵が持つターンで持続するステータスのインターフェース
/// </summary>
public interface ITurnStatus
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
    /// ここにステータスの効果を記述する
    /// </summary>
    public void Execute();
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
