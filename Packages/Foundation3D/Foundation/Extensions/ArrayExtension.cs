using System;
using System.Collections.Generic;
using RamenSea.Foundation.General;

namespace RamenSea.Foundation.Extensions {
    public static class ArrayExtension {
        // public static bool IsEmpty(this Array array) => array.Length == 0;
        // public static bool IsNotEmpty(this Array array) => array.Length > 0;
        public static bool IsEmpty<T>(this T[] collection) {
            return collection.Length == 0;
        }

        public static bool IsNotEmpty<T>(this T[] collection) {
            return collection.Length > 0;
        }

        public static bool IsEmpty<T>(this List<T> collection) {
            return collection.Count == 0;
        }

        public static bool IsNotEmpty<T>(this List<T> collection) {
            return collection.Count > 0;
        }

        public static List<TMapTo> flatMap<TMapTo, TMapFrom>(this List<TMapFrom> array,
                                                             Func<TMapFrom, TMapTo> mapFromTo) {
            var a = new List<TMapTo>();
            for (var i = 0; i < array.Count; i++) {
                var value = mapFromTo(array[i]);
                if (value != null) a.Add(value);
            }

            return a;
        }

        public static List<TMapTo> flatMap<TMapTo, TMapFrom>(this TMapFrom[] array,
                                                             Func<TMapFrom, TMapTo> mapFromTo) {
            var a = new List<TMapTo>();
            for (var i = 0; i < array.Length; i++) {
                var value = mapFromTo(array[i]);
                if (value != null) a.Add(value);
            }

            return a;
        }

        public static List<T> filter<T>(this T[] array, Func<T, bool> filterFunc) {
            var a = new List<T>();
            for (var i = 0; i < array.Length; i++) {
                var t = array[i];
                if (filterFunc(t)) a.Add(t);
            }

            return a;
        }

        public static List<T> Filter<T>(this List<T> array, Func<T, bool> filterFunc) {
            var a = new List<T>();
            for (var i = 0; i < array.Count; i++) {
                var t = array[i];
                if (filterFunc(t)) a.Add(t);
            }

            return a;
        }

        public static TMapTo[] ArrayMap<TMapTo, TMapFrom>(this List<TMapFrom> array, Func<TMapFrom, TMapTo> mapFromTo) {
            var a = new TMapTo[array.Count];
            for (var i = 0; i < array.Count; i++) a[i] = mapFromTo(array[i]);

            return a;
        }
        public static int RandomIndex<T>(this List<T> list, Random random) {
            if (list.IsEmpty()) throw new BaseFoundationException("List is empty");
            return random.Next(0, list.Count);
        }

        public static int RandomIndex<T>(this T[] a, Random random) {
            if (a.IsEmpty()) throw new BaseFoundationException("List is empty");
            return random.Next(0, a.Length);
        }

        public static T RandomElement<T>(this List<T> list, Random random) {
            return list[list.RandomIndex(random)];
        }
        public static T RandomElement<T>(this T[] list, Random random) {
            return list[list.RandomIndex(random)];
        }
        public static int RandomIndex<T>(this List<T> list, PredictableRandom random) {
            if (list.IsEmpty()) throw new BaseFoundationException("List is empty");
            return random.Next(0, list.Count);
        }

        public static int RandomIndex<T>(this T[] a, PredictableRandom random) {
            if (a.IsEmpty()) throw new BaseFoundationException("List is empty");
            return random.Next(0, a.Length);
        }

        public static T RandomElement<T>(this List<T> list, PredictableRandom random) {
            return list[list.RandomIndex(random)];
        }

        public static T RandomElement<T>(this T[] a, PredictableRandom random) {
            return a[a.RandomIndex(random)];
        }

        public static void shuffle<T>(this T[] a, Random random) {
            var n = a.Length;
            while (n > 1) {
                var k = random.Next(n);
                n--;
                (a[n], a[k]) = (a[k], a[n]);
            }
        }

        public static void shuffle<T>(this List<T> a, Random random) {
            var n = a.Count;
            while (n > 1) {
                var k = random.Next(n);
                n--;
                (a[n], a[k]) = (a[k], a[n]);
            }
        }
    }
}