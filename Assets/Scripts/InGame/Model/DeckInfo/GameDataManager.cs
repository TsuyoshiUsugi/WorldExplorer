using UnityEngine;

/// <summary>
/// デッキのデータ、敵のデータを保持する
/// </summary>
public class GameDataManager : AbstractSingleton<GameDataManager>
{
    [SerializeField] private DeckInfo _deckInfo;
    public DeckInfo DeckInfo => _deckInfo;
}
