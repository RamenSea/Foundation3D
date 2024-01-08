using RamenSea.Foundation3D.Services.Context;
using VContainer;
using VContainer.Unity;

namespace Tests.Context {
    public class Context: BaseContext {
        protected override void Configure(IContainerBuilder builder) {
            base.Configure(builder);

            builder.Register<SomeService1>(Lifetime.Singleton);
            builder.RegisterComponentOnNewGameObject<SomeService2>(Lifetime.Singleton, "SomeService2").UnderTransform(this.transform);
        }
    }
}