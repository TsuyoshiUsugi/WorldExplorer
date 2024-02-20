using UnityEngine;

/// <summary>
/// 敵にこのカードのダメージとプレイヤーの攻撃力を足したダメージを与える
/// </summary>
[System.Serializable]
public class ApplyDamageEffect : ICardEffect
{
    [SerializeField] private int _cardDamage = 10;
    [SerializeField] private int _addDamageWhenEvolved = 20;
    public void EvolveCardEffect(int addPower)
    {
        //ここに強化後のカードの効果を書く
        _cardDamage += _addDamageWhenEvolved;
    }
    
    public string GetEffectDescription()
    {
        return $"敵に{_cardDamage}のダメージを与える";
    }

    public void ExcuteCardEffect()
    {
        var enemy = FieldInfo.Instance.EnemyManager;
        var playerPower = FieldInfo.Instance.PlayerManager.Status.AttackPower;
        enemy?.Status.ApplyDamage(playerPower + _cardDamage);
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
    [SerializeField] private int _addDownPowerWhenEvolved = 10;

    public void EvolveCardEffect(int addPower)
    {
        //ここに強化後のカードの効果を書く
        _downPower += _addDownPowerWhenEvolved;
    }
    
    public string GetEffectDescription()
    {
        return $"敵の攻撃力を{_turn}ターン{_downPower}下げる";
    }

    public void ExcuteCardEffect()
    {
        var enemy = FieldInfo.Instance.EnemyManager;
        enemy?.ApplyEffect(new AttackPowerDownStatus(_downPower, _turn));
    }
}

/// <summary>
/// 酔い状態の時に敵にダメージを与える
/// </summary>
[System.Serializable]
public class ApplyDamageEffectIfDrunk : ICardEffect
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private int _addDamageWhenEvolved = 10;

    public void EvolveCardEffect(int addPower)
    {
        //ここに強化後のカードの効果を書く
        _damage += _addDamageWhenEvolved;
    }
    
    public string GetEffectDescription()
    {
        return $"覚醒状態なら敵に{_damage}のダメージを与える";
    }

    public void ExcuteCardEffect()
    {
        var player = FieldInfo.Instance.PlayerManager;
        if (!player.SakePower.IsDrank) return;

        var enemy = FieldInfo.Instance.EnemyManager;
        enemy?.Status.ApplyDamage(_damage);
    }
}
