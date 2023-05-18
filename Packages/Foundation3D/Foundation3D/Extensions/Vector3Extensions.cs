using UnityEngine;

namespace RamenSea.Foundation3D.Extensions {
    public static class Vector3Extensions {
        public static Vector2 ToVector2(this Vector3 vec3) {
            return new Vector2(vec3.x, vec3.y);
        }
    }
}