#nullable enable
using RamenSea.Foundation3D.Utility;
using UnityEditor;
using UnityEngine;

namespace RamenSea.Foundation3D.Extensions {
    public static class GameObjectExtensions {
        public static void Destroy(this Object obj) {
#if UNITY_EDITOR
            if (UnityUtility.IsPlayMode() == false) {
                Object.DestroyImmediate(obj);
                return;
            }
#endif
            Object.Destroy(obj);
        }

        public static T Instantiate<T>(this T t, Transform? parent = null) where T : Object {
#if UNITY_EDITOR
            if (UnityUtility.IsPlayMode() == false) {
                var prefab = (T)PrefabUtility.InstantiatePrefab(t, parent);
                return prefab;
            }
#endif
            return Object.Instantiate(t, parent);
        }
        public static T AddGetComponent<T>(this GameObject go) where T : Component {
            T t = go.GetComponent<T>();
            if (t != null) {
                return t;
            }

            return go.AddComponent<T>();
        }
    }
}