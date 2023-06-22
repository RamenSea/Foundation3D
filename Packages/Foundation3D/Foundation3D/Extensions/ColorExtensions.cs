using RamenSea.Foundation3D.General;
using UnityEngine;

namespace RamenSea.Foundation3D.Extensions {
    public static class ColorExtensions {
        public static Color WithAlpha(this Color color, float alpha) {
            color.a = alpha;
            return color;
        }

        public static HSVColor ToHSV(this Color color) => HSVColor.FromColor(color);

    }
}