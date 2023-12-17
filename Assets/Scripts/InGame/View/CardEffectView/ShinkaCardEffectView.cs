using Cysharp.Threading.Tasks;
using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

/// <summary>
/// 進化カードをプレイしたときの演出処理
/// </summary>
[System.Serializable]
public class ShinkaCardEffectView : CardEffectBase
{
    [SerializeField] private GameObject _shinkaEffectPrefab;
    private Animator _animator;

    private void Start()
    {
        _animator = _shinkaEffectPrefab.GetComponent<Animator>();
    }
    /// <summary>
    /// カードをプレイしたときの演出処理を行う
    /// </summary>
    /// <returns></returns>
    public override async UniTask PlayCardEffectView()
    {
        _shinkaEffectPrefab.SetActive(true);
        _animator.Play("CutinAnim");
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        _shinkaEffectPrefab.SetActive(false);
    }
}
