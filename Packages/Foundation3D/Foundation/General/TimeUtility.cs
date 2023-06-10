using System;
using RamenSea.Foundation.Extensions;

namespace RamenSea.Foundation.General {
    public static class TimeUtility {
        public static long TimestampMilliseconds() {
            return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
        }

        public static long TimestampSeconds() {
            return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        }

        public static int TimestampSecondsInt() {
            return TimestampSeconds().ToInt();
        }

        public static double TimestampSecondsDouble() {
            return TimestampMilliseconds().ToDouble() / 1000.0;
        }
    }
}