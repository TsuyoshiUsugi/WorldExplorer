#ifndef CUSTOM_EFFECT_RANDOM_INCLUDED
#define CUSTOM_EFFECT_RANDOM_INCLUDED

inline float random(float2 seeds)
{
    return frac(sin(dot(seeds, float2(12.9898F, 78.233F))) * 43758.5453F);
}

inline float2 random2(float2 seeds)
{
    seeds = float2(dot(seeds, float2(127.1F, 311.7F)),
                   dot(seeds, float2(269.5F, 183.3F)));

    return frac(sin(seeds) * 43758.5453123F);
}

inline float3 random3(float3 seeds)
{
    seeds = float3(dot(seeds, float3(127.1F, 311.7F, 542.3F)),
    dot(seeds, float3(269.5F, 183.3F, 461.7F)),
    dot(seeds, float3(732.1F, 845.3F, 231.7F)));
    
    return frac(sin(seeds) * 43758.5453123F);
}

inline float2 VoronoiRandomVector(float2 seeds, float offset)
{
    float2x2 mat = float2x2(15.27F, 47.63F, 99.41F, 89.98F);
    seeds = frac(sin(mul(seeds, mat)) * 46839.32F);
    return float2(sin(seeds.y * offset) * 0.5F + 0.5F, cos(seeds.x * offset) * 0.5F + 0.5F);
}

void Voronoi(float2 seeds, float angleOffset, float cellDensity, out float value, out float cells)
{
    float2 g = floor(seeds * cellDensity);
    float2 f = frac(seeds * cellDensity);
    float3 res = float3(8.0F, 0.0F, 0.0F);

    [unroll]
    for(int y = -1; y <= 1; y++)
    {
        [unroll]
        for(int x = -1; x <= 1; x++)
        {
            float2 lattice = float2(x, y);
            float2 offset = VoronoiRandomVector(lattice + g, angleOffset);

            float d = distance(lattice + offset, f);

            if (d < res.x)
            {
                res = float3(d, offset.x, offset.y);
                value = res.x;
                cells = res.y;
            }
        }
    }
}

#endif
