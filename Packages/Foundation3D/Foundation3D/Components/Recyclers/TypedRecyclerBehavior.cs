using System;
using System.Collections.Generic;
using UnityEngine;

namespace RamenSea.Foundation3D.Components.Recyclers {
    public class TypedRecyclerBehavior<T>: KeyedRecyclerBehavior<Type, T> where T: MonoBehaviour, IKeyedRecyclableObject<Type> {
        protected override Dictionary<Type, T> GetIndexedPrefabs() {
            var d = new Dictionary<Type, T>();
            foreach (var prefab in this.prefabs) {
                var t = prefab.GetType();
                d[t] = prefab;
            }

            return d;
        }
    }
}