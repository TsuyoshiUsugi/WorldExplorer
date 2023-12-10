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
