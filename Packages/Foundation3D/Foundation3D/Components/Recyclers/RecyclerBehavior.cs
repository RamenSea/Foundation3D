using System;
using UnityEngine;

namespace RamenSea.Foundation3D.Components.Recyclers {
    /// <summary>
    /// The most basic of recyclers. Creates a single prefab and
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RecyclerBehavior<T>: MonoBehaviour, ITypedRecycler<T> where T: MonoBehaviour, IRecyclableObject {
        [SerializeField] protected T prefab;
        [SerializeField] protected Transform parentingTransform;
        protected PrefabRecycler<T> prefabRecycler;

        protected virtual void Awake() {
            this.prefabRecycler = new PrefabRecycler<T>(this.prefab, this.parentingTransform);
        }
        public T Get() {
            var t = this.prefabRecycler.Get();
            t.recycler = this;
            return t;
        }
        protected virtual void Reset() {
            this.parentingTransform = this.transform;
        }
        public void Recycle(object o) => this.Recycle((T) o);
        public void Recycle(T t) {
            this.OnRecycle(t);
            this.prefabRecycler.Recycle(t);
        }
        protected virtual void OnRecycle(T t) { }
    }
}