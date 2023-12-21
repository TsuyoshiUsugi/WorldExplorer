#ifndef CUSTOM_EFFECT_UI_OUTLINE_INCLUDED
#define CUSTOM_EFFECT_UI_OUTLINE_INCLUDED

#define max3(value1, value2, value3) max(max(value1, value2), value3)

#define max4(value1, value2, value3, value4) max(max(value1, value2), max(value3, value4))

#define min3(value1, value2, value3) min(min(value1, value2), value3)

#define min4(value1, value2, value3, value4) min(min(value1, value2), min(value3, value4))

#define remap(value, inMin, inMax, outMin, outMax) ((value - inMin) * ((outMax - outMin) / (inMax - inMin)) + outMin)

#endif
