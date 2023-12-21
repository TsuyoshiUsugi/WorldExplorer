#ifndef CUSTOM_EFFECT_BLEND_INCLUDED
#define CUSTOM_EFFECT_BLEND_INCLUDED

inline void BlendMultiply(float4 base, float4 blend, float opacity, out float4 output)
{
    output = base * blend;
    output = lerp(base, output, opacity);
}

inline void BlendAdd(float4 base, float4 blend, float opacity, out float4 output)
{
    output = base + blend;
    output = lerp(base, output, opacity);
}

inline void BlendBurn(float4 base, float4 blend, float opacity, out float4 output)
{
    output =  1.0 - (1.0 - blend) / base;
    output = lerp(base, output, opacity);
}

void BlendLinearBurn(float4 base, float4 blend, float opacity, out float4 output)
{
    output = base + blend - 1.0;
    output = lerp(base, output, opacity);
}

#endif
