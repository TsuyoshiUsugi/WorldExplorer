using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

/// <summary>
/// インゲームの進行管理を行う
/// 具体的には、
/// プレイヤーターン、エネミーターン、リザルトのながれを行う
/// </summary>
public class InGameSequenceManager : MonoBehaviour
{
    [Inject]
    PlayerTurnState _playerTurnState;
    [Inject]
    EnemyTurnState _enemyTurnState;
    [Inject]
    ResultState _resultState;

    private void Start()
    {
        BindEvent();
        _playerTurnState.OnEnter().Forget();
        SetFieldInfo();
    }

    /// <summary>
    /// FieldInfoに各ステートのマネージャーをセットする
    /// </summary>
    private void SetFieldInfo()
    {
        var fieldInfo = FindObjectOfType<FieldInfo>();
        fieldInfo.PlayerManager = _playerTurnState.PlayerManager;
        fieldInfo.EnemyManager = _enemyTurnState.EnemyManager;
    }

    /// <summary>
    /// 各ステートのバインド処理を行う
    /// </summary>
    private void BindEvent()
    {
        _playerTurnState.OnExitEvent += () => _enemyTurnState.OnEnter();
        _enemyTurnState.OnExitEvent += () => _playerTurnState.OnEnter();

        //強制的な遷移なので、演出が後ろで動いてしまうかも
        _playerTurnState.OnGameEnd += winner => _resultState.OnEnter(winner).Forget();
        _enemyTurnState.OnGameEnd += winner => _resultState.OnEnter(winner).Forget();
    }
}
