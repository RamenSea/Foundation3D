using NaughtyAttributes;
using RamenSea.Foundation3D.Components.Recyclers;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Tests.Recyclers {
    public class TestRecycler: MonoBehaviour{

        [SerializeField] private AddressableRecyclerBehavior recycler;
        [SerializeField] private AssetReference testAsset;
        [SerializeField] private AssetReference testAsset2;
        [SerializeField] private AssetReference testAsset3;
        [Button("Test spawn")]
        public void TestSpawn() {
            var t = this.recycler.Get<AddressableTestObj1>(this.testAsset);
        }
        [Button("Test spawn 2")]
        public void TestSpawn2() {
            var t = this.recycler.Get<AddressableTestObj2>(this.testAsset2);
        }
        [Button("Test spawn 3")]
        public void TestSpawn3() {
            var t = this.recycler.Get<AddressableTestObj1>(this.testAsset3);
        }
    }
}