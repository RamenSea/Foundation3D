using System.Collections.Generic;
using UnityEngine;

namespace RamenSea.Foundation3D.Components.Recyclers {
    public abstract class KeyedRecyclerBehavior<K, T>: MonoBehaviour, ITypedKeyedRecycler<K, T> where T: MonoBehaviour, IKeyedRecyclableObject<K> {
        [SerializeField] protected Transform parentTransform;
        [SerializeField] protected T[] prefabs;
        protected KeyedPrefabRecycler<K, T> prefabRecycler;


        protected virtual void Awake() {
            this.prefabRecycler = new KeyedPrefabRecycler<K, T>(this.GetIndexedPrefabs(), this.transform);
        }
        protected virtual Dictionary<K, T> GetIndexedPrefabs() {
            var d = new Dictionary<K, T>();
            foreach (var prefab in this.prefabs) {
                d[prefab.recyclerKey] = prefab;
            }

            return d;
        }
        public T Get(K key) {
            var t = this.prefabRecycler.Get(key);
            t.recycler = this;
            return t;
        }
        
        public void Recycle(K key, object o) {
            
        }
        public void Recycle(K key, T t) {
            this.OnRecycle(t);
            this.prefabRecycler.Recycle(key, t);
        }
        protected virtual void OnRecycle(T t) { }
        protected virtual void Reset() {
            this.parentTransform = this.transform;
        }
    }
}