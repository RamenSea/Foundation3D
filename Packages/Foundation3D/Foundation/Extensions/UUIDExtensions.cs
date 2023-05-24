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
        
        public static Guid CreateUuidFrom(ulong lowBytes, ulong highBytes) {
            return new Guid(
                (uint)((lowBytes   & 0x00000000ffffffff) >> 0),
                (ushort)((lowBytes & 0x0000ffff00000000) >> 32),
                (ushort)((lowBytes & 0xffff000000000000) >> 48),
                (byte)((highBytes  & 0x00000000000000ff) >> 0),
                (byte)((highBytes  & 0x000000000000ff00) >> 8),
                (byte)((highBytes  & 0x0000000000ff0000) >> 16),
                (byte)((highBytes  & 0x00000000ff000000) >> 24),
                (byte)((highBytes  & 0x000000ff00000000) >> 32),
                (byte)((highBytes  & 0x0000ff0000000000) >> 40),
                (byte)((highBytes  & 0x00ff000000000000) >> 48),
                (byte)((highBytes  & 0xff00000000000000) >> 56));
        }
        public static void GetULong(this Guid uuid, out ulong lowBytes, out ulong highBytes) {
            var array = uuid.ToByteArray();
            lowBytes = BitConverter.ToUInt64(array, 0);
            highBytes = BitConverter.ToUInt64(array, 8);
        }
    }
}