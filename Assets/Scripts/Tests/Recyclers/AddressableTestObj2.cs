using NaughtyAttributes;
using RamenSea.Foundation3D.Components.Recyclers;
using UnityEngine;

namespace Tests.Recyclers {
    public class AddressableTestObj2: AddressableRecyclerObject {

        [Button("Test Recycle")]
        public void TestRecycle() => this.Recycle();

    }
}