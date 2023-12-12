/// <summary>
/// カードの効果のインターフェース
/// </summary>
public interface ICardEffect
{
    /// <summary>
    /// カードの効果を実行する。ここにカードの効果を書く
    /// </summary>
    public void ExcuteCardEffect();

    /// <summary>
    /// カードの効果を進化させる
    /// </summary>
    public void EvolveCardEffect();
}
