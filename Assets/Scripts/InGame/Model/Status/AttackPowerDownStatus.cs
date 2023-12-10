/// <summary>
/// 指定したターン数だけ攻撃力を下げるステータス
/// </summary>
public class AttackPowerDownStatus : TurnStatus
{
    private int _remainTurn;
    public int RemainTurn => _remainTurn;

    public void Start()
    {
        _remainTurn--;

    }

    public void End()
    {
        
    }

}
