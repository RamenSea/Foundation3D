using NaughtyAttributes;
using RamenSea.Foundation3D.Services.Context;
using UnityEngine;

namespace Tests.Context {
    public class ContextProvider: BaseContextProvider<Context> {


        [Button("test Service 1")]
        public void TestService1() {
            Debug.Log(this.context.Resolve<SomeService1>());
        }
        [Button("test Service 2")]
        public void TestService2() {
            Debug.Log(this.context.Resolve<SomeService2>());
        }
    }
}