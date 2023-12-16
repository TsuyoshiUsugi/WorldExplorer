using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// カードをプレイしたときの演出処理のインターフェース
/// </summary>
public abstract class CardEffectBase : MonoBehaviour
{
    public int ID;
    /// <summary>
    /// カードをプレイしたときの演出処理を行う
    /// </summary>
    public abstract UniTask PlayCardEffectView();
}
