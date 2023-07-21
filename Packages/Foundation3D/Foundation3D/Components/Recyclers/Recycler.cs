#nullable enable
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using RamenSea.Foundation3D.Extensions;
using UnityEngine;

///
/// HEAVILY A WIP

namespace RamenSea.Foundation3D.Components.Recyclers {
    public interface IRecycler {
        public void Recycle(object o);
    }
    public interface ITypedRecycler<T>: IRecycler {
        public T Get();
    }
    public interface IRecyclableObject {
        public IRecycler recycler { get; set; }
        public void Recycle() => this.recycler.Recycle(this);
    }
    public struct PrefabRecycler<T>: ITypedRecycler<T> where T: MonoBehaviour {
        public Transform? parentTransform;
        public T prefab;
        public Stack<T> stack;

        public PrefabRecycler(T prefab, Transform? parentTransform = null) {
            this.prefab = prefab;
            this.parentTransform = parentTransform;
            this.stack = new();
        }
        public T Get() {
            T t;
            if (this.stack.Count > 0) {
                t = this.stack.Pop();
                t.gameObject.SetActive(true);
            } else {
                t = this.prefab.Instantiate(this.parentTransform);
            }

            return t;
        }
        public void Recycle(object o) {
            this.Recycle((T) o);
        }
        public void Recycle(T t) {
            t.gameObject.SetActive(false);
            this.stack.Push(t);
        }
    }
    
    
    public interface IKeyedRecycler<K> {
        public void Recycle(K key, object o);
    }
    public interface ITypedKeyedRecycler<K, T>: IKeyedRecycler<K> {
        public T Get(K key);
    }
    public interface IKeyedRecyclableObject<K> {
        public K recyclerKey { get; }
        public IKeyedRecycler<K> recycler { get; set; }
        public void Recycle() => this.recycler.Recycle(this.recyclerKey, this);
    }
    public class KeyedPrefabRecycler<K, T>: ITypedKeyedRecycler<K, T> where T : MonoBehaviour {
        protected Transform? _parentTransform;
        public Transform? parentTransform {
            get => _parentTransform;
            set {
                if (this._parentTransform != value) {
                    this._parentTransform = value;
                    foreach (var keyValuePair in this.indexedPrefabs) {
                        var p = this.indexedPrefabs[keyValuePair.Key];
                        p.parentTransform = value;
                        this.indexedPrefabs[keyValuePair.Key] = p;
                    }
                }
            }
        }
        [NotNull] public readonly Dictionary<K, PrefabRecycler<T>> indexedPrefabs;

        public KeyedPrefabRecycler([NotNull] Dictionary<K, T> indexedPrefabs, Transform? parentTransform = null) {
            this._parentTransform = parentTransform;
            this.indexedPrefabs = new();
            foreach (var keyValuePair in indexedPrefabs) {
                this.indexedPrefabs[keyValuePair.Key] = new PrefabRecycler<T>(keyValuePair.Value, parentTransform);
            }
        }
        public KeyedPrefabRecycler([NotNull] Dictionary<K, PrefabRecycler<T>> indexedPrefabs, Transform? parentTransform = null) {
            this._parentTransform = parentTransform;
            this.indexedPrefabs = indexedPrefabs;
        }

        public T Get(K key) => this.indexedPrefabs[key].Get();
        public void Recycle(K key, object o) {
            this.Recycle(key, (T) o);
        }
        public void Recycle(K key, T t) => this.indexedPrefabs[key].Recycle(t);
    }
}