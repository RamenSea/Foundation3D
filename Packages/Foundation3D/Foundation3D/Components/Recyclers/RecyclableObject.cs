using UnityEngine;

namespace RamenSea.Foundation3D.Components.Recyclers {
    public class RecyclableObject: MonoBehaviour, IRecyclableObject {
        public IRecycler recycler { get; set; }
    }
}