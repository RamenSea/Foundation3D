using System;
using NaughtyAttributes;
using RamenSea.Foundation3D.Scene.Runner;
using UnityEngine;

namespace RamenSea.Foundation3D.Scene.Mechanic {
    public class BaseMechanicBehavior: MonoBehaviour, IComparable<BaseMechanicBehavior> {
        public virtual int sortValue => 1000;
        
        public virtual int CompareTo(BaseMechanicBehavior other) {
            if (other.sortValue == this.sortValue) {
                return this.gameObject.GetInstanceID().CompareTo(other.gameObject.GetInstanceID());
            }
            return this.sortValue.CompareTo(other.sortValue);
        }
        public virtual void EditorSetUp() {
            
        }

        [Button("Set up", EButtonEnableMode.Editor)]
        public virtual void EditorSetUp(BaseMechanicRunner runner) { }

    }
}