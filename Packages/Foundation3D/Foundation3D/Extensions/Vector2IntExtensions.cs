using System.Collections.Generic;
using RamenSea.Foundation.Extensions;
using UnityEngine;

namespace RamenSea.Foundation3D.Extensions {
    public static class Vector2IntExtensions {
        public static float Distance(this Vector2Int v, Vector2Int target) {
            return Vector2Int.Distance(v, target);
        }

        public static Vector2 ToVector2(this Vector2Int vec) {
            return vec;
        }

        public static Vector2Int Abs(this Vector2Int vec) {
            return new Vector2Int(vec.x.Abs(), vec.y.Abs());
        }

        public static IList<Vector2Int> HexagonSurroundingTiles(this Vector2Int center,
                                                                IList<Vector2Int> usingList = null) {
            if (usingList == null) usingList = new List<Vector2Int>();

            usingList.Add(center + new Vector2Int(1, 0));
            usingList.Add(center + new Vector2Int(-1, 0));

            usingList.Add(center + new Vector2Int(0, 1));
            usingList.Add(center + new Vector2Int(0, -1));

            if (center.y % 2 == 0) {
                usingList.Add(center + new Vector2Int(-1, 1));
                usingList.Add(center + new Vector2Int(-1, -1));
            } else {
                usingList.Add(center + new Vector2Int(1, 1));
                usingList.Add(center + new Vector2Int(1, -1));
            }

            return usingList;
        }
    }
}