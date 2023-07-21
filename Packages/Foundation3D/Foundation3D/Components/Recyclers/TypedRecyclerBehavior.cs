using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace RamenSea.Foundation3D.Components.Recyclers {
    public class TypedRecyclerObject : MonoBehaviour, IKeyedRecyclableObject<Type> {
        private Type cachedRecyclerKey = null;

        public Type recyclerKey { 
            //todo benchmark this on Mono to see if their is any performance game
            get {
                if (this.cachedRecyclerKey == null) {
                    this.cachedRecyclerKey = this.GetType();
                }

                return this.cachedRecyclerKey;
            }
        }
        public IKeyedRecycler<Type> recycler { get; set; }
    }
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