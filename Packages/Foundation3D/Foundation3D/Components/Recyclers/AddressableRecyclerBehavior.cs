using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using RamenSea.Foundation.Extensions;
using RamenSea.Foundation.General;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using RamenSea.Foundation3D.Extensions;

namespace RamenSea.Foundation3D.Components.Recyclers {

    public interface IAddressableRecycler {
        public UniTask<AddressableRecyclerObject> Get();
        public Transform? parentTransform { get; set; }
        public UniTask LoadPrefabAsync();
        public void Recycle(AddressableRecyclerObject obj);
        public void Release();
    }
    public class AddressableRecycler<T>: IAddressableRecycler where T: MonoBehaviour {
        public Transform? parentTransform { get; set; }
        public readonly AssetReference reference;
        public T? prefab;
        private readonly Stack<AddressableRecyclerObject> stack;
        public UniTaskCompletionSource? prefabLoad;
        private AsyncOperationHandle<GameObject> handle;

        private bool scriptIsAddressableRecyclerObject;
        public AddressableRecycler(AssetReference reference, Transform? parentTransform = null) {
            this.parentTransform = parentTransform;
            this.reference = reference;
            this.prefab = null;
            this.prefabLoad = null;
            this.stack = new();
            this.scriptIsAddressableRecyclerObject = typeof(T).IsSubclassOf(typeof(AddressableRecyclerObject));
        }
        public async UniTask<AddressableRecyclerObject> Get() {
            AddressableRecyclerObject o;
            float spawnDelay = 0f;
            if (this.stack.Count > 0) {
                o = this.stack.Pop();
                o.gameObject.SetActive(true);
            } else {
                if (this.prefab == null) {
                    var loadTime = Time.time;
                    await this.LoadPrefabAsync();
                    spawnDelay = Time.time - loadTime;
                }
                var m = this.prefab!.Instantiate(this.parentTransform);
                if (scriptIsAddressableRecyclerObject) {
                    o = m as AddressableRecyclerObject;
                } else {
                    o = m.gameObject.AddGetComponent<AddressableRecyclerObject>();
                }
                o.script = m;
                o.assetReference = this.reference;
            }

            o.spawnedDelay = spawnDelay;
            return o;
        }

        public async UniTask LoadPrefabAsync() {
            if (this.prefabLoad != null) {
                await this.prefabLoad.Task;
                return;
            }

            this.prefabLoad = new UniTaskCompletionSource();
            this.handle = Addressables.LoadAssetAsync<GameObject>(this.reference);
            await this.handle.Task;
            //todo log failures here
            this.prefab = this.handle.Result.GetComponent<T>();
            this.prefabLoad.TrySetResult();
        }
        public void Recycle(object o) {
            var castedObj = o as AddressableRecyclerObject;
            if (castedObj == null) {
                var mono = o as MonoBehaviour;
                if (mono == null) {
                    throw new BaseFoundationException("Can not recycle this object");
                }

                castedObj = mono.gameObject.GetComponent<AddressableRecyclerObject>();
            }
            this.Recycle(castedObj);
        }
        public void Recycle(AddressableRecyclerObject obj) {
            obj.gameObject.SetActive(false);
            this.stack.Push(obj);
        }
        public void Release() {
            Addressables.Release(this.handle);
        }
    }
    public class AddressableRecyclerBehavior: MonoBehaviour, IRecycler {
        [SerializeField] protected Transform defaultTransform;

        private Dictionary<string, IAddressableRecycler> indexedRecyclers;

        protected virtual void Awake() {
            this.indexedRecyclers = new();
        }

        public void SetParent<T>(AssetReference key, Transform parent) where T : MonoBehaviour {
            IAddressableRecycler? r = this.indexedRecyclers.GetNullable(key.AssetGUID);
            if (r == null) {
                r = new AddressableRecycler<T>(key, parent);
                this.indexedRecyclers[key.AssetGUID] = r;
            } else {
                r.parentTransform = parent;
            }

        }
        public UniTask Preload<T>(AssetReference key) where T: MonoBehaviour {
            IAddressableRecycler? r = this.indexedRecyclers.GetNullable(key.AssetGUID);
            if (r == null) {
                r = new AddressableRecycler<T>(key, this.defaultTransform);
                this.indexedRecyclers[key.AssetGUID] = r;
            }
            return r.LoadPrefabAsync();
        }
        public async UniTask<T> Get<T>(AssetReference key) where T: MonoBehaviour {
            IAddressableRecycler? r = this.indexedRecyclers.GetNullable(key.AssetGUID);;
            if (r == null) {
                r = new AddressableRecycler<T>(key, this.defaultTransform);
                this.indexedRecyclers[key.AssetGUID] = r;
            }

            var o = await r.Get();
            o.recycler = this;
            o.OnGet();
            return (T) o.script;
        }
        public void Recycle(object o) {
            this.Recycle((AddressableRecyclerObject) o);
        }
        public void Recycle(AddressableRecyclerObject t) {
            this.OnRecycle(t);
            IAddressableRecycler? r = this.indexedRecyclers.GetNullable(t.assetReference.AssetGUID);
            if (r != null) {
                t.OnRecycle();
                r.Recycle(t);
            }
        }
        protected virtual void OnRecycle(AddressableRecyclerObject t) { }
        protected virtual void Reset() {
            this.defaultTransform = this.transform;
        }
        private void OnDestroy() {
            foreach (var pair in this.indexedRecyclers) {
                pair.Value.Release();
            }
            this.indexedRecyclers.Clear();
        }
    }
}