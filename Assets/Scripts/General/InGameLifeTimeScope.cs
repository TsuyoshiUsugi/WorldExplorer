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
        builder.Register(resolver => new EnemyManager(new (GameDataManager.Instance.EnemyData.EnemyBehavior)), Lifetime.Singleton);
        builder.Register<PlayerManager>(Lifetime.Singleton);
        builder.Register<EnemyTurnState>(Lifetime.Singleton);
        builder.Register<PlayerTurnState>(Lifetime.Singleton);
        builder.Register<ResultState>(Lifetime.Singleton);

        //Presenter
        builder.RegisterComponent(_ingamePresenter);
    }
}