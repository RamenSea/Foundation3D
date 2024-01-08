using JetBrains.Annotations;
using UnityEngine;
using RamenSea.Foundation3D.Extensions;

namespace RamenSea.Foundation3D.Services.Context {
    [DefaultExecutionOrder(EXECUTION_ORDER_SCENE_PROVIDER)]
    public class BaseContextProvider<T>: MonoBehaviour where T: BaseContext {
        public const int EXECUTION_ORDER_SCENE_PROVIDER = BaseContext.EXECUTION_ORDER_CONTEXT + 1;
        
        internal static T _Shared = null;
        
        public static T Get() {
            return _Shared;
        }
        public T context { private set; get; }

        [SerializeField, CanBeNull] protected T contextPrefab;

        protected virtual void Awake() {
            if (_Shared == null) {
                if (this.contextPrefab != null) {
                    _Shared = this.contextPrefab.Instantiate();
                } else {
                    var go = new GameObject("Context");
                    _Shared = go.AddComponent<T>();
                }
            }
            
            this.context = _Shared;
        }
    }

}