using System;
using UniRx;
using UnityEngine;
/// <summary>
/// プレイヤーや敵が持つターンで持続するステータスの基底クラス
/// </summary>
public abstract class TurnStatus
{
    protected readonly ReactiveProperty<int> _remainTurn = new ReactiveProperty<int>();
    private IDisposable _disposable;

    /// <summary>
    /// ステータスの効果が持続するターン数を取得するプロパティ
    /// </summary>
    public IReadOnlyReactiveProperty<int> RemainTurn => _remainTurn;

    /// <summary>
    /// ステータスの効果を開始する
    /// </summary>
    /// <param name="status">対象となるプレイヤーのステータスの参照</param>
    public void ApplyEffect(Status status)
    {
        ExecuteEffect(status);
        _disposable = _remainTurn.Subscribe(turn =>
        {
            if (turn <= 0)
            {
                CancelEffect(status);
                _disposable.Dispose();
            }
        });
    }

    /// <summary>
    /// ステータスの持続ターンを減らす
    /// </summary>
    public void DecreaseTurn()
    {
        _remainTurn.Value--;
    }

    /// <summary>
    /// ここにステータスの実際の効果を記述する
    /// </summary>
    /// <param name="status">対象となるプレイヤーのステータスの参照</param>
    protected abstract void ExecuteEffect(Status status);

    /// <summary>
    /// ステータスの効果が終了したときに呼び出すメソッド
    /// </summary>
    protected abstract void CancelEffect(Status status);
}
