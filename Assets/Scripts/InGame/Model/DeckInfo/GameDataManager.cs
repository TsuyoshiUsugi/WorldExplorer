using UnityEngine;

/// <summary>
/// プレイヤーステータスのデータ、デッキのデータ、敵のデータを保持する
/// </summary>
public class GameDataManager : AbstractSingleton<GameDataManager>
{
    public PlayerData PlayerData;
    public DeckInfo DeckInfo;
    public EnemyData EnemyData;
}
