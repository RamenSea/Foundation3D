
using UnityEngine;

namespace RamenSea.Foundation3D.General {
    /// <summary>
    /// A convenience class for dealing with colors in the HSV format
    /// </summary>
    public struct HSVColor {
        public float hue;
        public float saturation;
        public float value;

        public bool hdr;
        
        // This is added just for clarity
        public float brightness {
            get => this.value;
            set => this.value = value;
        }

        public Color color => Color.HSVToRGB(this.hue, this.saturation, this.value, this.hdr);
        public Vector3 ToVector3() => new Vector3(this.hue, this.saturation, this.value);
        
        public static implicit operator Color(HSVColor c) => c.color;
        public static implicit operator Vector3(HSVColor c) => c.ToVector3();

        public static HSVColor FromColor(Color c) {
            Color.RGBToHSV(c, out float h, out float s, out float v);
            return new HSVColor() {
                hue = h,
                saturation = s,
                value = s,
                hdr = false, // should this be default off?
            };
        }
    }
}