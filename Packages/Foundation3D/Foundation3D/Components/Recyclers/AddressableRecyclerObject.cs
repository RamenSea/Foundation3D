using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RamenSea.Foundation3D.Components.Recyclers {
    public class AddressableRecyclerObject: MonoBehaviour {
        public AssetReference assetReference { internal set; get; }
        public IRecycler recycler { internal set; get; }
        [CanBeNull] internal MonoBehaviour script;

        public void Recycle() => this.recycler.Recycle(this);
        public virtual void OnGet() {}
        public virtual void OnRecycle() {}
    }
}