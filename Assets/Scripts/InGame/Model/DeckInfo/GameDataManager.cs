using UnityEngine;

/// <summary>
/// プレイヤーステータスのデータ、デッキのデータ、敵のデータを保持する
/// </summary>
public class GameDataManager : AbstractSingleton<GameDataManager>
{
    public PlayerData PlayerManager;
    public DeckInfo DeckInfo;
    public EnemyData EnemyData;
}
