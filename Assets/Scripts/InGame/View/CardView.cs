using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーが選択するカードのクラス
/// 現状はボタンを押したら選択されたカードのインデックスを返す
/// </summary>
public class CardView : MonoBehaviour
{
    [SerializeField] Button _cardSelectButton;
    public event Action<int> OnCardSelect;
    private int _index = 0;

    private void Start()
    {
        _cardSelectButton?.onClick.AddListener(() =>
        {
            OnCardSelect?.Invoke(_index);
        });
    }

    public void SetIndex(int index)
    {
        _index = index;
    }
}
