// using UnityEngine;

using System;

namespace RamenSea.Foundation.Extensions {
    public static class StringExtensions {
        public static long ToLong(this string s, long defaultValue = 0) {
            try {
                return long.Parse(s);
            } catch (Exception e) {
                return defaultValue;
            }
        }
        public static double ToDouble(this string s, double defaultValue = 0) {
            try {
                return double.Parse(s);
            } catch (Exception e) {
                return defaultValue;
            }
        }
    }
}