using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 画面上に表示するダメージ表示
/// </summary>
public class DamageCountView : MonoBehaviour
{
    public async UniTask ShowDamage(int damage)
    {
        var text = GetComponent<Text>();
        text.text = damage.ToString();
        await transform.DOMoveY(transform.position.y + 100, 1f)
            .OnComplete(() => Destroy(this.gameObject));
    }

    public void SetColor(Color color)
    {
        var text = GetComponent<Text>();
        text.color = color;
    }
}
