using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

/// <summary>
/// プレイヤーが選択するカードのクラス
/// 現状はボタンを押したら選択されたカードのインデックスを返す
/// </summary>
public class CardView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler
{
    [SerializeField] private RectTransform _cardRectpos;
    [SerializeField] Button _cardSelectButton;
    [Header("カーソルを合わせたときのカードの大きさ倍率")]
    [SerializeField] private float _cardPickMagnification;
    [Header("カードをデフォルトの大きさ")]
    [SerializeField] private float _cardDefaultSize;
    [Header("カードの効果テキスト")]
    [SerializeField] private Text _cardEffectText;
    
    public event Action<int> OnCardSelect;
    private int _index = 0;
    private bool _moveFenish;
    private Vector3 _savePos;
    [Tooltip("カードをPlayできるかどうか")]
    private bool _isPlay = false;

    public void SetCardText(string text)
    {
        _cardEffectText.text = text;
    }

    public void SetIndex(int index)
    {
        _index = index;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_moveFenish)
        {
            _savePos = transform.localPosition;
        }
        var playCardFieldType = FieldInfo.Instance.PlayerManager.HandCard[_index].PlayFieldCardType;
        FieldInfo.Instance.PlayCardField.ActiveField(playCardFieldType);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_moveFenish) return;
        GoBackToStartAnim();
        if (_isPlay) 
        {
            OnCardSelect?.Invoke(_index);
        }
        FieldInfo.Instance.PlayCardField.NonActive();
    }

    private void GoBackToStartAnim()
    {
        //クリックの終了時クリックし始めたpositionまで戻る
        DOTween.To(() => transform.localPosition,
        x => transform.localPosition = x,
        _savePos, 0.5f)
        .OnStart(() => _moveFenish = true)
        .SetLink(gameObject)
        .OnComplete(() =>
        {
            transform.localPosition = _savePos;
            _moveFenish = false;
        });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _cardRectpos.localScale *= _cardPickMagnification;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _cardRectpos.localScale = new Vector3(_cardDefaultSize, _cardDefaultSize, _cardDefaultSize);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_moveFenish)
        {
            _cardRectpos.position = eventData.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayCardField")) 
        {
            _isPlay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayCardField"))
        {
            _isPlay = false;
        }
    }
}
