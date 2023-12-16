using UnityEngine;

/// <summary>
/// 敵にこのカードのダメージとプレイヤーの攻撃力を足したダメージを与える
/// </summary>
[System.Serializable]
public class ApplyDamageEffect : ICardEffect
{
    [SerializeField] private int _cardDamage = 10;
    public void EvolveCardEffect(int addPower)
    {
        //ここに強化後のカードの効果を書く
        //_cardDamage += addPower;
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

    public void EvolveCardEffect(int addPower)
    {
        //ここに強化後のカードの効果を書く
    }

    public void ExcuteCardEffect()
    {
        Debug.Log("敵の攻撃力を" + _downPower + "下げる");
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

    public void EvolveCardEffect(int addPower)
    {
        //ここに強化後のカードの効果を書く
    }

    public void ExcuteCardEffect()
    {
        var player = FieldInfo.Instance.PlayerManager;
        if (!player.SakePower.IsDrank) return;

        var enemy = FieldInfo.Instance.EnemyManager;
        enemy?.Status.ApplyDamage(_damage);
    }
}
