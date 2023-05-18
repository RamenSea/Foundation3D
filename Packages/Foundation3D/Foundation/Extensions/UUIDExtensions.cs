using System;
using System.Linq;

namespace RamenSea.Foundation.Extensions {
    public static class UUIDExtensions {
        private static readonly int[] uuidByteOrder = { 15, 14, 13, 12, 11, 10, 9, 8, 6, 7, 4, 5, 0, 1, 2, 3 };

        public static Guid Increment(this Guid guid) {
            var bytes = guid.ToByteArray();
            var canIncrement = uuidByteOrder.Any(i => ++bytes[i] != 0);
            if (canIncrement) return new Guid(bytes);
            return Guid.Empty;
        }

        public static bool IsEmpty(this Guid uuid) {
            return uuid == Guid.Empty;
        }
    }
}