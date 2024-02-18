using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICustomOutline : AbstractCustomImage
{
    [Serializable]
    private enum Blend
    {
        Blend,
        Overwrite
    }

    [SerializeField, Tooltip("Outlineと元の画像のBlend方法")]
    private Blend _outlineBlend = Blend.Blend;
    [SerializeField, ColorUsage(true,true), Tooltip("Outlineの色")]
    private Color _outlineColor = Color.red;
    [SerializeField, Range(0.0F, 1.0F), Tooltip("Outlineの幅")]
    private float _outlineWidth = 0.1F;

    private readonly int _outlineColorId = Shader.PropertyToID("_OutlineColor");
    private readonly int _outlineWidthId = Shader.PropertyToID("_OutlineWidth");
    

    protected override void UpdateMaterial(Material baseMaterial)
    {
        if (!_material)
        {
            Shader shader = Shader.Find("Custom/UIOutline");
            _material = new Material(shader);
            _material.CopyPropertiesFromMaterial(baseMaterial);
            _material.hideFlags = HideFlags.HideAndDontSave;
        }
        
        _material.SetColor(_outlineColorId, _outlineColor);
        _material.SetFloat(_outlineWidthId, _outlineWidth);

        if (_outlineBlend == Blend.Blend)
        {
            _material.DisableKeyword("_OUTLINE_TYPE_OVERWRITE");
            _material.EnableKeyword("_OUTLINE_TYPE_BLEND");
        }
        else
        {
            _material.DisableKeyword("_OUTLINE_TYPE_BLEND");
            _material.EnableKeyword("_OUTLINE_TYPE_OVERWRITE");
        }
    }
}
