using System;
using RamenSea.Foundation.General;


namespace RamenSea.Foundation.Extensions {
    public static class RandomExtensions {
        public static float Next(this Random r, float minValue, float maxValue) {
            var f = r.NextDouble().ToFloat();
            var diff = maxValue - minValue;
            return minValue + (f * diff);
        }
        public static float Next(this PredictableRandom r, float minValue, float maxValue) {
            var f = r.NextDouble().ToFloat();
            var diff = maxValue - minValue;
            return minValue + (f * diff);
        }
        public static bool NextBool(this Random r, float percentChance) {
            return r.Next(0, 1.0f) < percentChance;
        }
        public static bool NextBool(this PredictableRandom r, float percentChance) {
            return r.Next(0, 1.0f) < percentChance;
        }
    }
}