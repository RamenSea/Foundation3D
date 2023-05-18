using System.Collections.Generic;

namespace RamenSea.Foundation.Extensions {
    public static class DictionaryExtensions {
        public static V GetDefault<K, V>(this Dictionary<K, V> d, K key) {
            if (d.ContainsKey(key)) return d[key];

            return default;
        }

        public static V GetNullable<K, V>(this Dictionary<K, V> d, K key) where V : class {
            if (d.TryGetValue(key, out var nullable)) return nullable;

            return null;
        }
    }
}