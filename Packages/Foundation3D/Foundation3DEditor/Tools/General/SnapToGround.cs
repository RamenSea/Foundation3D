using UnityEditor;
using UnityEngine;

namespace RamenSea.Foundation3DEditor.Tools.General {
    public static class SnapToGround {
        public const string NAME = "Snap To Ground";
        public const string MENU_ITEM_ROUTE = Tools.TOOL_MENU_PATH_BASE + "/" + NAME + " %g";
        
        [MenuItem(MENU_ITEM_ROUTE)]
        public static void Perform() {
            RegisterUndo();

            foreach (Transform transform in Selection.transforms) {
                HandleMovingToGround(transform);
            }
        }
        private static void HandleMovingToGround(Transform transform) {
            var hitDown = Physics.Raycast(transform.position, Vector3.down, out RaycastHit downHit);
            var hitUp = Physics.Raycast(downHit.point, Vector3.up, out RaycastHit upHit);

            if (hitDown && hitUp) {
                var translation = new Vector3(0, downHit.point.y - upHit.point.y, 0);
                transform.Translate(translation, Space.World);
            }
        }
        
        private static void RegisterUndo() {
            Undo.RegisterCompleteObjectUndo(Selection.transforms as UnityEngine.Object[], NAME);
        }

    }
}