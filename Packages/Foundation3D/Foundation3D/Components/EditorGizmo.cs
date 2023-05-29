using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RamenSea.Foundation3D.Components {
    [Serializable]
    public enum EditorGizmoShape {
        None,
        Cube,
        WireCube,
        Sphere,
        WireSphere,
        Direction,
        WireDirection,
    }
    public class EditorGizmo: MonoBehaviour {
#if UNITY_EDITOR

        public EditorGizmoShape shape = EditorGizmoShape.Cube;

        public bool onlyDrawWhenSelected = false;

        public Vector3 size = new Vector3(0.2f, 0.2f, 0.2f);
        public Color color = new Color(1.0f, 0f, 0f, 0.3f);

        [SerializeField] [HideInInspector] private Mesh pyramidMesh1; //todo figure out a better way to cache this in the editor

#pragma warning disable CS1998
        protected async UniTask<Mesh> GetDirectionMesh() {
#pragma warning restore CS1998
            if (this.pyramidMesh1 == null) {
                this.pyramidMesh1 = AssetDatabase.LoadAssetAtPath<Mesh>("Packages/com.ramensea.foundation3d/Foundation3DEditor/Meshes/Pyramid.fbx");
            }
            return this.pyramidMesh1;
        }
        protected virtual void OnDrawGizmos() {
            if (this.onlyDrawWhenSelected == false) {
                this.DrawShape();
            }
        }

        protected virtual void OnDrawGizmosSelected() {
            if (this.onlyDrawWhenSelected) {
                this.DrawShape();
            }
        }

        protected virtual async void DrawShape() {
            
            switch (this.shape) {
                case EditorGizmoShape.None: {
                    return;
                }
                case EditorGizmoShape.Cube: {
                    Gizmos.color = this.color;
                    Gizmos.DrawCube(this.transform.position, this.size);
                    break;
                }
                case EditorGizmoShape.WireCube: {
                    Gizmos.color = this.color;
                    Gizmos.DrawWireCube(this.transform.position, this.size);
                    break;
                }
                case EditorGizmoShape.Sphere: {
                    Gizmos.color = this.color;
                    Gizmos.DrawSphere(this.transform.position, this.size.x);
                    break;
                }
                case EditorGizmoShape.WireSphere: {
                    Gizmos.color = this.color;
                    Gizmos.DrawWireSphere(this.transform.position, this.size.x);
                    break;
                }
                case EditorGizmoShape.Direction: {
                    Gizmos.color = this.color;
                    var mesh = await this.GetDirectionMesh();
                    Gizmos.DrawMesh(mesh, this.transform.position, this.transform.rotation, this.size);
                    break;
                }
                case EditorGizmoShape.WireDirection: {
                    Gizmos.color = this.color;
                    var mesh = await this.GetDirectionMesh();
                    Gizmos.DrawWireMesh(mesh, this.transform.position, this.transform.rotation, this.size);
                    break;
                }
            }
        }
#endif
    }
}