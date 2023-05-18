using UnityEngine;

namespace RamenSea.Foundation3D.Extensions {
    public static class ColorExtensions {
        public static Color WithAlpha(this Color color, float alpha) {
            color.a = alpha;
            return color;
        }
    }
}