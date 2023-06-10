using RamenSea.Foundation.Extensions;
using UnityEngine;

namespace RamenSea.Foundation3D.Extensions {
    public static class Vector3Extensions {
        public static Vector2 ToVector2(this Vector3 vec3) => new Vector2(vec3.x, vec3.y);
        public static Vector3 Abs(this Vector3 vec3) => new Vector3(vec3.x.Abs(), vec3.y.Abs(), vec3.z.Abs());
        public static float Distance(this Vector3 vec3, Vector3 target) => Vector3.Distance(vec3, target);
        public static Vector3 Direction(this Vector3 vec3, Vector3 target) {
            var heading = target - vec3;
            return heading / heading.magnitude;
        }
        public static Vector3 Multiply(this Vector3 vec3, Vector3 target) =>
            new Vector3(vec3.x * target.x, vec3.y * target.y, vec3.z * target.z);
        public static Vector3 Divide(this Vector3 vec3, Vector3 target) =>
            new Vector3(vec3.x / target.x, vec3.y / target.y, vec3.z / target.z);
    }
}