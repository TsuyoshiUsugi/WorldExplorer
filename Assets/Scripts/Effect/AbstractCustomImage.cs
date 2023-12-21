using System;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways, RequireComponent(typeof(Graphic))]
public abstract class AbstractCustomImage : MonoBehaviour, IMaterialModifier
{
    [NonSerialized]
    private Graphic _graphic = null;

    [NonSerialized]
    private Image _image = null;

    protected Material _material = null;

    private CanvasScaler _canvasScaler = null;

    private readonly int _textureRectId = Shader.PropertyToID("_textureRect");

    /// <summary>変更を加える対象のGraphic</summary>
    public Graphic Graphic
    {
        get
        {
            if (!_graphic)
            {
                _graphic = GetComponent<Graphic>();
            }

            return _graphic;
        }
    }

    /// <summary>このGameObjectのCanvasのCanvasScaler</summary>
    protected CanvasScaler CanvasScaler
    {
        get
        {
            if (!_canvasScaler)
            {
                _canvasScaler = Graphic.canvas.GetComponent<CanvasScaler>();
            }

            return _canvasScaler;
        }
    }

    Material IMaterialModifier.GetModifiedMaterial(Material baseMaterial)
    {
        if (!isActiveAndEnabled || !Graphic)
        {
            return baseMaterial;
        }
        
        UpdateMaterial(baseMaterial);
        SetAtlasInfo();

        return _material;
    }

    private void OnDidApplyAnimationProperties()
    {
        if (!isActiveAndEnabled || !Graphic)
        {
            return;
        }
        
        Graphic.SetMaterialDirty();
    }

    protected abstract void UpdateMaterial(Material baseMaterial);

    private void SetAtlasInfo()
    {
        if (!_image)
        {
            return;
        }

        if (!_image.sprite.packed)
        {
            _material.DisableKeyword("USE_ATLAS");
            return;
        }

        Rect textureRect = _image.sprite.textureRect;
        Vector4 r = new Vector4(textureRect.x, textureRect.y, textureRect.width, textureRect.height);
        _material.SetVector(_textureRectId, r);
        _material.EnableKeyword("USE_ATLAS");
    }

    protected void OnEnable()
    {
        if (!Graphic)
        {
            return;
        }
        
        _image = Graphic as Image;
        Graphic.SetMaterialDirty();
    }

    protected void OnDisable()
    {
        if (_material)
        {
            DestroyMaterial();
        }

        if (Graphic)
        {
            Graphic.SetMaterialDirty();
        }
    }

    public void DestroyMaterial()
    {
#if UNITY_EDITOR
        if (!EditorApplication.isPlaying)
        {
            DestroyImmediate(_material);
            _material = null;
            return;
        }
#endif
        Destroy(_material);
        _material = null;
    }
    
#if UNITY_EDITOR
    protected void OnValidate()
    {
        if (!isActiveAndEnabled || !Graphic)
        {
            return;
        }
        
        Graphic.SetMaterialDirty();
    }
#endif
}
