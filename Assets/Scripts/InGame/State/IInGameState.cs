/// <summary>
/// インゲームの各ステートのインターフェース
/// </summary>
public interface IInGameState
{
    public void OnEnter();
    public void OnUpdate();
    public void OnExit(); 
}
