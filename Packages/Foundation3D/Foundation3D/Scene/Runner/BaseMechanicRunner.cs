using System.Linq;
using NaughtyAttributes;
using RamenSea.Foundation.Extensions;
using RamenSea.Foundation3D.Scene.Mechanic;
using RamenSea.Foundation3D.Services.Context;
using UnityEditor;
using UnityEngine;

namespace RamenSea.Foundation3D.Scene.Runner {
    
    [DefaultExecutionOrder(EXECUTION_ORDER_SCENE_PROVIDER)]
    public class BaseMechanicRunner: MonoBehaviour {
        public const int EXECUTION_ORDER_SCENE_PROVIDER = BaseContext.EXECUTION_ORDER_CONTEXT + 2;
        
        [SerializeField] protected BaseMechanicBehavior[] internalMechanics;
        [SerializeField] protected BaseMechanicBehavior[] externalMechanics;

#if UNITY_EDITOR
        protected virtual void Reset() {
            this.EditorSetUp();
        }

        [Button("Fetch behaviors", EButtonEnableMode.Editor)]
        protected void EditorSetUp() {
            var mechanics = this.gameObject.GetComponentsInChildren<BaseMechanicBehavior>(true).ToList();
            mechanics.Sort();
            this.internalMechanics = mechanics.ToArray();
            
            for (var i = 0; i < this.internalMechanics.Length; i++) {
                this.internalMechanics[i].EditorSetUp(this);
                EditorUtility.SetDirty(this.internalMechanics[i]);
            }

            
            mechanics = FindObjectsByType<BaseMechanicBehavior>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID).ToList();
            mechanics = mechanics.Filter(mechanic => this.internalMechanics.Contains(mechanic) == false);
            mechanics.Sort();
            this.externalMechanics = mechanics.ToArray();
            
            for (var i = 0; i < this.externalMechanics.Length; i++) {
                this.externalMechanics[i].EditorSetUp(this);
                EditorUtility.SetDirty(this.externalMechanics[i]);
            }

            EditorUtility.SetDirty(this);
        }
#endif
        
    }
}