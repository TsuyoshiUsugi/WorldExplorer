using VContainer;
using VContainer.Unity;
using UnityEngine;

/// <summary>
/// インゲームのコンテナ
/// </summary>
public class InGameLifeTimeScope : LifetimeScope
{
    [SerializeField] InGamePresenter _ingamePresenter;

    protected override void Configure(IContainerBuilder builder)
    {
        //アプリケーション層
        builder.Register(resolver => new EnemyManager( GameDataManager.Instance.EnemyData.BehaviorTree,
            GameDataManager.Instance.EnemyData.Status), Lifetime.Singleton);
        builder.Register(resolver => new PlayerManager(GameDataManager.Instance.PlayerData.Status), Lifetime.Singleton);
        builder.Register<EnemyTurnState>(Lifetime.Singleton);
        builder.Register<PlayerTurnState>(Lifetime.Singleton);
        builder.Register<ResultState>(Lifetime.Singleton);

        //Presenter
        builder.RegisterComponent(_ingamePresenter);
    }
}
