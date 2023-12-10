/// <summary>
/// 指定したターン数だけ攻撃力を下げるステータス
/// </summary>
public class AttackPowerDownStatus : ITurnStatus
{
    private int _remainTurn;
    public int RemainTurn => _remainTurn;

    public void Execute()
    {
        _remainTurn--;

    }
}
