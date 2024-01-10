using RamenSea.Foundation.Extensions;
using RamenSea.Foundation.General;
using UnityEngine;

namespace RamenSea.Foundation3D.Extensions {
    public static class Vector2Extensions {
        public static Vector2 RadianToDirection(this float radian) {
            return new Vector2(Mathf.Sin(radian), Mathf.Cos(radian));
        }

        public static Vector2 DegreeToDirection(this float degree) {
            return (degree * Mathf.Deg2Rad).RadianToDirection();
        }

        public static Vector2 Multiply(this Vector2 vec2, Vector2 target) =>
            new Vector2(vec2.x * target.x, vec2.y * target.y);
        public static Vector2 Divide(this Vector2 vec2, Vector2 target) =>
            new Vector2(vec2.x / target.x, vec2.y / target.y);
        public static Vector2 Abs(this Vector2 vector2) {
            return new Vector2(vector2.x.Abs(), vector2.y.Abs());
        }
        public static Vector3 ToVector3(this Vector2 vector2, float z = 0f) {
            return new Vector3(vector2.x, vector2.y, z);
        }
        public static float AspectRatio(this Vector2 vector2) {
            return vector2.x / vector2.y;
        }

        public static float AspectRatio(this Vector2Int vector2) {
            return vector2.x / (float)vector2.y;
        }

        public static bool HasNaN(this Vector2 v) {
            return float.IsNaN(v.x) || float.IsNaN(v.y);
        }

        public static float ScaleAspect(this Vector2 vec, Vector2 container, ScalingMode scaleMode) {
            switch (scaleMode) {
                case ScalingMode.None:
                    return vec.AspectRatio();
                case ScalingMode.AspectFill:
                    return vec.AspectFill(container);
                case ScalingMode.AspectFit:
                    return vec.AspectFit(container);
            }

            return 1f;
        }

        public static Vector2 ScaleTo(this Vector2 vec, Vector2 container, ScalingMode scaleMode) {
            switch (scaleMode) {
                case ScalingMode.None:
                    return vec;
                case ScalingMode.Fill:
                    return container / vec;
                case ScalingMode.AspectFill:
                case ScalingMode.AspectFit:
                    var a = vec.ScaleAspect(container, scaleMode);
                    return vec * a;
            }

            return vec;
        }

        public static float AspectFit(this Vector2 vector2, Vector2 container) {
            if (container.x / container.y > vector2.x / vector2.y)
                return container.y / vector2.y;
            return container.x / vector2.x;
        }

        public static float AspectFill(this Vector2 vector2, Vector2 container) {
            if (container.x / container.y > vector2.x / vector2.y)
                return container.x / vector2.x;
            return container.y / vector2.y;
        }

        public static float Angle(this Vector2 vector2) {
            if (vector2.x < 0)
                return 360 - Mathf.Atan2(vector2.x, vector2.y) * Mathf.Rad2Deg * -1;
            return Mathf.Atan2(vector2.x, vector2.y) * Mathf.Rad2Deg;
        }

        public static Vector2 Direction(this Vector2 vec2, Vector2 target) {
            var heading = target - vec2;
            return heading / heading.magnitude;
        }

        public static float Distance(this Vector2 vec2, Vector2 target) {
            return Vector2.Distance(vec2, target);
        }
    }
}