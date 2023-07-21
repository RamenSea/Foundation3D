using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using RamenSea.Foundation.Extensions;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using RamenSea.Foundation3D.Extensions;

namespace RamenSea.Foundation3D.Components.Recyclers {
    public interface IAsyncTypedRecycler<T>: IRecycler {
        public UniTask<T> Get();
    }

    public class AddressableRecyclerObject : MonoBehaviour {
        public AssetReference assetReference { internal set; get; }
        public IRecycler recycler { internal set; get; }
    }
    public class AddressableRecycler<T>: IAsyncTypedRecycler<T> where T: MonoBehaviour {
        public Transform? parentTransform;
        public readonly AssetReference reference;
        public T? prefab;
        public readonly Stack<T> stack;
        public UniTaskCompletionSource? prefabLoad;
        private AsyncOperationHandle<T> handle;

        public AddressableRecycler(AssetReference reference, Transform? parentTransform = null) {
            this.parentTransform = parentTransform;
            this.reference = reference;
            this.prefab = null;
            this.prefabLoad = null;
            this.stack = new();
        }
        public async UniTask<T> Get() {
            T t;
            if (this.stack.Count > 0) {
                t = this.stack.Pop();
                t.gameObject.SetActive(true);
            } else {
                if (this.prefab == null) {
                    await this.LoadPrefab();
                }
                t = this.prefab!.Instantiate(this.parentTransform);
            }

            return t;
        }

        private async UniTask LoadPrefab() {
            if (this.prefabLoad != null) {
                await this.prefabLoad.Task;
                return;
            }

            this.prefabLoad = new UniTaskCompletionSource();
            this.handle = Addressables.LoadAssetAsync<T>(this.reference);
            await this.handle;
            this.prefab = this.handle.Result;
            this.prefabLoad.TrySetResult();
        }
        public void Recycle(object o) {
            this.Recycle((T) o);
        }
        public void Recycle(T t) {
            t.gameObject.SetActive(false);
            this.stack.Push(t);
        }
        public void Release() {
            Addressables.Release(this.handle);
        }
    }
    public class AddressableRecyclerBehavior: MonoBehaviour, IRecycler {
        [SerializeField] protected Transform _parentTransform;
        public Transform? parentTransform {
            get => _parentTransform;
            set {
                if (this._parentTransform != value) {
                    this._parentTransform = value;
                    foreach (var keyValuePair in this.indexedRecyclers) {
                        var p = this.indexedRecyclers[keyValuePair.Key];
                        p.parentTransform = value;
                        this.indexedRecyclers[keyValuePair.Key] = p;
                    }
                }
            }
        }
        private Dictionary<AssetReference, AddressableRecycler<AddressableRecyclerObject>> indexedRecyclers;

        protected virtual void Awake() {
            this.indexedRecyclers = new();
        }

        public async UniTask<T> Get<T>(AssetReference key) where T: AddressableRecyclerObject  {
            AddressableRecycler<AddressableRecyclerObject>? r = this.indexedRecyclers.GetNullable(key);;
            if (r == null) {
                r = new AddressableRecycler<AddressableRecyclerObject>(key, this.parentTransform);
                this.indexedRecyclers[key] = r;
            }

            var t = await r.Get();
            t.recycler = this;
            return (T) t;
        }

        public void Recycle(object o) {
            this.Recycle((AddressableRecyclerObject) o);
        }
        public void Recycle(AddressableRecyclerObject t) {
            this.OnRecycle(t);
            AddressableRecycler<AddressableRecyclerObject>? r = this.indexedRecyclers.GetNullable(t.assetReference);
            if (r != null) {
                r.Recycle(t);
            }
        }
        protected virtual void OnRecycle(AddressableRecyclerObject t) { }
        protected virtual void Reset() {
            this.parentTransform = this.transform;
        }
        private void OnDestroy() {
            foreach (var pair in this.indexedRecyclers) {
                pair.Value.Release();
            }
        }
    }
}