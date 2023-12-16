using Cysharp.Threading.Tasks;

/// <summary>
/// カードをプレイしたときの演出処理のインターフェース
/// </summary>
public interface ICardEffectView
{
    /// <summary>
    /// カードをプレイしたときの演出処理を行う
    /// </summary>
    public UniTask PlayCardEffectView();
}
