using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace RamenSea.Foundation3D.Services.Context {
    [DefaultExecutionOrder(BaseContext.EXECUTION_ORDER_CONTEXT)]
    public class BaseContext: LifetimeScope {
        public const int EXECUTION_ORDER_CONTEXT = -100000;

        protected override void Awake() {
            DontDestroyOnLoad(this.gameObject);
            this.IsRoot = true;
            base.Awake();
        }
        protected override void Configure(IContainerBuilder builder) {
            base.Configure(builder);
        }
        public T Resolve<T>() {
            return this.Container.Resolve<T>();
        }
    }
}