using VContainer;
using UnityEngine;

/// <summary>
/// 場の情報を持つクラス
/// </summary>
public class FieldInfo : AbstractSingleton<FieldInfo>
{
    [Inject]
    public PlayerManager PlayerManager;
    [Inject]
    public EnemyManager EnemyManager;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("敵の行動");
            EnemyManager.ExcuteEnemyAction();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("プレイヤーの行動");
            PlayerManager.Status.ApplyDamage(10);
        }
    }
}
