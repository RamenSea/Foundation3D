
using System;
using RamenSea.Foundation3D.Composer;
using UnityEngine;


namespace RamenSea.Foundation3D.Composer {
    [DefaultExecutionOrder(Context.EXECUTION_ORDER_CONTEXT)]
    public class Context: MonoBehaviour {
        public const int EXECUTION_ORDER_CONTEXT = -100000;
        internal static Context Shared;
        
        public static Context Get() {
            if (Shared == null) {
                var provider = FindFirstObjectByType<SceneComposer>();
                Shared = provider.context;
            }

            return Shared;
        }
        private bool hasInit = false;
        
        protected virtual void Awake() {
            DontDestroyOnLoad(this.gameObject);
            this.Init();
        }

        private void Init() {
            if (this.hasInit) {
                return;
            }

            this.hasInit = true;
        }
        // private T AddService<T>(string serviceName) where T : MonoBehaviour { //todo integrate in vcountainer
        //     var go = new GameObject(serviceName);
        //     go.transform.SetParent(this.transform);
        //     return go.AddComponent<T>();
        // }
    }
}