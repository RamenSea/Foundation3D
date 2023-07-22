using NaughtyAttributes;
using RamenSea.Foundation3D.Components.Recyclers;
using UnityEngine;

namespace Tests.Recyclers {
    public class AddressableTestObj1: MonoBehaviour {
        
        [Button("Test Recycle")]
        public void TestRecycle() => this.gameObject.GetComponent<AddressableRecyclerObject>().Recycle();
    }
}