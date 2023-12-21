#ifndef CUSTOM_EFFECT_GRADIENT_INCLUDED
#define CUSTOM_EFFECT_GRADIENT_INCLUDED

#include "Macro.hlsl"

half3 GradientColor01(float value)
{
    half3 rgb;

    value = frac(value);
    value *= 6.0F;
    float i = floor(value);
    float f = value - i;
    float aa = 0.0F;
    float bb = 1 - f;
    float cc = f;

    if( i < 1 ) {
        rgb.r = 1;
        rgb.g = cc;
        rgb.b = aa;
    } else if( i < 2 ) {
        rgb.r = bb;
        rgb.g = 1;
        rgb.b = aa;
    } else if( i < 3 ) {
        rgb.r = aa;
        rgb.g = 1;
        rgb.b = cc;
    } else if( i < 4 ) {
        rgb.r = aa;
        rgb.g = bb;
        rgb.b = 1;
    } else if( i < 5 ) {
        rgb.r = cc;
        rgb.g = aa;
        rgb.b = 1;
    } else {
        rgb.r = 1;
        rgb.g = aa;
        rgb.b = bb;
    }
        
    return rgb;
}

#endif
