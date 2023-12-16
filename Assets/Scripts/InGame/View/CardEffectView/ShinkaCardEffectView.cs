using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 進化カードをプレイしたときの演出処理
/// </summary>
[System.Serializable]
public class ShinkaCardEffectView : CardEffectBase
{
    [SerializeField] private GameObject _shinkaEffectPrefab;
    /// <summary>
    /// カードをプレイしたときの演出処理を行う
    /// </summary>
    /// <returns></returns>
    public override async UniTask PlayCardEffectView()
    {
        Debug.Log("進化カードの演出処理を行う");
        _shinkaEffectPrefab.GetComponent<Animator>().Play("CutinAnim");
    }
}
