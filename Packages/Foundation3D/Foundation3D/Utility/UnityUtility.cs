using UnityEngine;

namespace RamenSea.Foundation3D.Utility {
    public static class UnityUtility {
        public static bool ObjectIsTrueNull(Object unityObject) {
#if UNITY_EDITOR
            if (IsEditor() && unityObject == null) return true;
#endif
            if (ReferenceEquals(unityObject, null)) return true;

            return false;
        }

        public static bool IsEditor() {
#if UNITY_EDITOR
            return Application.isPlaying == false;
#endif
#pragma warning disable CS0162
            return false;
#pragma warning restore CS0162
        }

        public static bool IsPlayMode() {
#if UNITY_EDITOR
            return Application.isPlaying;
#endif
#pragma warning disable CS0162
            return false;
#pragma warning restore CS0162
        }
    }
}