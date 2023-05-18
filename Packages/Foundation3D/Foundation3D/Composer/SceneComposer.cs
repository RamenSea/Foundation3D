
using System;
using JetBrains.Annotations;
using RamenSea.Foundation3D.Extensions;
using UnityEngine;

namespace RamenSea.Foundation3D.Composer {
    [DefaultExecutionOrder(EXECUTION_ORDER_SCENE_PROVIDER)]
    public class BaseSceneComposer<T>: MonoBehaviour where T: Context {
        public const int EXECUTION_ORDER_SCENE_PROVIDER = Context.EXECUTION_ORDER_CONTEXT + 1;
        public T context { private set; get; }

        [SerializeField, CanBeNull] protected T contextPrefab;

        protected virtual void Awake() {
            if (Context.Shared == null) {
                if (this.contextPrefab != null) {
                    Context.Shared = this.contextPrefab.Instatiate();
                } else {
                    var go = new GameObject("Context");
                    Context.Shared = go.AddComponent<T>();
                }
            }
            
            this.context = (T) Context.Shared;
        }
    }

    // Sealed version of the class, useful for getting started
    public class SceneComposer : BaseSceneComposer<Context> { } 
}