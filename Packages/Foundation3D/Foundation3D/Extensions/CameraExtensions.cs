using UnityEngine;

namespace RamenSea.Foundation3D.Extensions {
    public static class CameraExtensions {
        public static Vector2 Size(this Camera camera) {
            var height = camera.orthographicSize * 2;
            return new Vector2(height * camera.aspect, height);
        }
        public static Vector2Int PixelSize(this Camera camera) {
            return new Vector2Int(camera.pixelWidth, camera.pixelHeight);
        }
    }
}