using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace RamenSea.Foundation3D.Components.Recyclers {
    public abstract class KeyedRecyclerBehavior<K, T>: MonoBehaviour, ITypedKeyedRecycler<K, T> where T: MonoBehaviour, IKeyedRecyclableObject<K> {
        [SerializeField] protected Transform parentTransform;
        [SerializeField] protected T[] prefabs;
        [SerializeField] private string prefabResourceFolder = "";

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
            this.Recycle(key, (T) o);
        }
        public void Recycle(K key, T t) {
            this.OnRecycle(t);
            this.prefabRecycler.Recycle(key, t);
        }
        protected virtual void OnRecycle(T t) { }
        protected virtual void Reset() {
            this.parentTransform = this.transform;
        }
        
        
#if UNITY_EDITOR
        [Button("Fetch Prefabs", EButtonEnableMode.Editor)]
        public void EditorFetchPrefabsFromResources() {
            if (this.prefabResourceFolder != null) {
                this.prefabs = Resources.LoadAll<T>(this.prefabResourceFolder);
            }
        }
#endif
    }
}