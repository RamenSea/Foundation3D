using UnityEngine;

namespace RamenSea.Foundation3D.Utility {
    public static class UnityUtility {
        /// <summary>
        /// Determines if the referenced object is truly null and not just a destroyed Unity Object (such as a MonoBehavior)
        /// </summary>
        /// <param name="unityObject"></param>
        /// <returns></returns>
        public static bool ObjectIsTrueNull(Object unityObject) {
#if UNITY_EDITOR
            if (IsEditor() && unityObject == null) return true;
#endif
            if (ReferenceEquals(unityObject, null)) return true;

            return false;
        }

        /// <summary>
        /// Returns if you are in the editor but not in play mode
        /// </summary>
        /// <returns></returns>
        public static bool IsEditor() {
#if UNITY_EDITOR
            return Application.isPlaying == false;
#endif
#pragma warning disable CS0162
            return false;
#pragma warning restore CS0162
        }

        /// <summary>
        /// Returns if you are in the editor and in play mode
        /// </summary>
        /// <returns></returns>
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