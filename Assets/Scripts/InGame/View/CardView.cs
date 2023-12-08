using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーが選択するカードのクラス
/// </summary>
public class CardView : MonoBehaviour
{
    [SerializeField] Button _cardSelectButton;
    public event Action OnCardSelect;

    private void Start()
    {
        _cardSelectButton?.onClick.AddListener(() =>
        {
            Debug.Log("Call");
            OnCardSelect?.Invoke();
        });
    }
}
