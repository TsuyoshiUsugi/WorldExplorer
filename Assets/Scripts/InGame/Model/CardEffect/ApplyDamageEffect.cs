using UnityEngine;

/// <summary>
/// 敵にダメージを与える
/// </summary>
[System.Serializable]
public class ApplyDamageEffect : ICardEffect
{
    public void ExcuteCardEffect()
    {
        var enemy = FieldInfo.Instance.EnemyManager;
        var playerPower = FieldInfo.Instance.PlayerManager.Status.AttackPower;
        enemy?.Status.ApplyDamage(playerPower);
    }
}

/// <summary>
/// 敵の攻撃力を指定したターン下げる
/// </summary>
[System.Serializable]
public class ApplyAttackPowerDownEffect : ICardEffect
{
    [SerializeField] private int _turn = 1;
    [SerializeField] private int _downPower = 10;
    
    public void ExcuteCardEffect()
    {
        var enemy = FieldInfo.Instance.EnemyManager;
        enemy?.ApplyEffect(new AttackPowerDownStatus(_downPower, _turn));
    }
}
