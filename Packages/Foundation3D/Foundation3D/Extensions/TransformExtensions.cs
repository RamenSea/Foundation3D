using UnityEngine;

namespace RamenSea.Foundation3D.Extensions {
    public static class TransformExtensions {
        /// <summary>
        /// Looks the opposite direction from the target transform
        /// </summary>
        /// <param name="t"></param>
        /// <param name="lookAwayTransform"></param>
        public static void LookAway(this Transform t, Transform lookAwayTransform) {
            t.rotation = Quaternion.LookRotation(t.position - lookAwayTransform.position);
        }
        public static void LookAway(this Transform t, Vector3 position) {
            t.rotation = Quaternion.LookRotation(t.position - position);
        }
        public static void SetPositionAndRotation(this Transform t, Transform copy) {
            t.SetPositionAndRotation(copy.position, copy.rotation);
        }
        public static void SetLocalPositionAndRotation(this Transform t, Transform copy) {
            t.SetLocalPositionAndRotation(copy.position, copy.rotation);
        }
    }
}