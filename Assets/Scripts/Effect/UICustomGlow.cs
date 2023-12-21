using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICustomGlow : AbstractCustomImage
{
    [Header("Glow1")]
    [SerializeField, Tooltip("Glow1を有効にするかどうか")]
    private bool _glow1Enable = true;
    [SerializeField, Tooltip("VoronoiのOffset")]
    private float _voronoiAngleOffset = 2.0F;
    [SerializeField, Tooltip("Voronoiの細かさ")]
    private float _voronoiCellDensity = 3.0F;
    [SerializeField, Tooltip("Intensity")]
    private float _glow1Intensity = 1.0F;
    [SerializeField, Range(0.0F, 1.0F), Tooltip("Opacity")]
    private float _glow1Opacity = 0.3F;
    [SerializeField, Tooltip("Scroll Speed")]
    private float _glow1ScrollSpeed = 0.5F;

    [Header("Glow2")]
    [SerializeField, Tooltip("Glow2を有効にするかどうか")]
    private bool _glow2Enable = true;
    [SerializeField, Tooltip("Intensity")]
    private float _glow2Intensity = 1.0F;
    [SerializeField, Range(0.0F, 1.0F), Tooltip("Opacity")]
    private float _glow2Opacity = 0.3F;
    [SerializeField, Tooltip("Tilling Offset")]
    private Vector4 _glow2TillingOffset = new Vector4(1.0F, 1.0F, 0.0F, 0.0F);
    [SerializeField, Tooltip("Scroll Speed")]
    private Vector2 _glow2ScrollSpeed = new Vector2(0.5F, 0.5F);

    private readonly int _voronoiAngleOffsetId = Shader.PropertyToID("_VoronoiAngleOffset");
    private readonly int _voronoiCellDensityId = Shader.PropertyToID("_VoronoiCellDensity");
    private readonly int _glow1IntensityId = Shader.PropertyToID("_Glow1Intensity");
    private readonly int _glow1OpacityId = Shader.PropertyToID("_Glow1Opacity");
    private readonly int _glow1ScrollSpeedId = Shader.PropertyToID("_Glow1ScrollSpeed");

    private readonly int _glow2IntensityId = Shader.PropertyToID("_Glow2Intensity");
    private readonly int _glow2OpacityId = Shader.PropertyToID("_Glow2Opacity");
    private readonly int _glow2TillingOffsetId = Shader.PropertyToID("_Glow2TillingOffset");
    private readonly int _glow2ScrollSpeedId = Shader.PropertyToID("_Glow2ScrollSpeed");
    
    protected override void UpdateMaterial(Material baseMaterial)
    {
        if (!_material)
        {
            Shader shader = Shader.Find("Custom/UIGlow");
            _material = new Material(shader);
            _material.CopyPropertiesFromMaterial(baseMaterial);
            _material.hideFlags = HideFlags.HideAndDontSave;
        }
        
        _material.SetFloat(_voronoiAngleOffsetId, _voronoiAngleOffset);
        _material.SetFloat(_voronoiCellDensityId, _voronoiCellDensity);
        _material.SetFloat(_glow1IntensityId, _glow1Intensity);
        _material.SetFloat(_glow1OpacityId, _glow1Opacity);
        _material.SetFloat(_glow1ScrollSpeedId, _glow1ScrollSpeed);

        if (_glow1Enable)
        {
            _material.EnableKeyword("_CUSTOM_EFFECT_GLOW1_ON");
        }
        else
        {
            _material.DisableKeyword("_CUSTOM_EFFECT_GLOW1_ON");
        }
        
        _material.SetFloat(_glow2IntensityId, _glow2Intensity);
        _material.SetFloat(_glow2OpacityId, _glow2Opacity);
        _material.SetVector(_glow2TillingOffsetId, _glow2TillingOffset);
        _material.SetVector(_glow2ScrollSpeedId, new Vector4(_glow2ScrollSpeed.x, _glow2ScrollSpeed.y, 0.0F, 0.0F));
        
        if (_glow2Enable)
        {
            _material.EnableKeyword("_CUSTOM_EFFECT_GLOW2_ON");
        }
        else
        {
            _material.DisableKeyword("_CUSTOM_EFFECT_GLOW2_ON");
        }
    }
}
