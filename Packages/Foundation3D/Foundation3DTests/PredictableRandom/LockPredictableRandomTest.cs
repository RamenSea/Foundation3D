using System;
using NUnit.Framework;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RamenSea.Foundation3DTests.PredictableRandom {
    /// <summary>
    /// This tests that PredictableRandom class is predictably random and locks it to a set of values
    /// </summary>
    public class LockPredictableRandomTest {

        private Foundation.General.PredictableRandom GetRandom(bool reseed, int seed, Foundation.General.PredictableRandom r) {
            if (reseed && r != null) {
                r.Reseed(seed);
                return r;
            }
            return new Foundation.General.PredictableRandom(seed);
        }
        [Test]
        public void LockedToValues() {
            var seedToTest = 1;
            var r = this.GetRandom(false, seedToTest, null);
            var predictedValues = new int[] {
                534011718,
                237820880,
                1002897798,
                1657007234,
                1412011072,
                929393559,
                760389092,
                2026928803,
                217468053,
                1379662799,
                61497087,
                532638534,
                687431273,
                2125508764,
                1464848243,
                1406361028,
                607156385,
            };
            foreach (var predictedValue in predictedValues) {
                Assert.AreEqual(predictedValue, r.Next());
            }
            r = this.GetRandom(true, seedToTest, r);
            foreach (var predictedValue in predictedValues) {
                Assert.AreEqual(predictedValue, r.Next());
            }
            

            seedToTest = 9542134;
            r = this.GetRandom(false, seedToTest, r);
            predictedValues = new int[] {
                170141001,
                1255127886,
                1798626013,
                1295276686,
                1361427752,
                1143655373,
                2048728263,
                514425006,
                667735569,
                1417454631,
                1527751240,
                684949959,
                1620658301,
                2068006388,
                689576623,
                233215310,
                940144908,
                1404472625,
                1999616929,
                403597866,
                638393404,
                1054848208,
                960758322,
                484266787,
                753091294,
                2144598502,
                916968125,
                1860824932,
                2077033064,
                459197822,
                1433733084,
                1388266118,
                149397463,
                510038120,
                1844139646,
                1986235778,
                233341833,
                907819335,
                13214293,
                40857057,
                191350924,
                5965743,
                1256222264,
                713839861,
                547503595,
                155155929,
                1932161344,
                1127580855,
                1312791253,
                151889060,
                1062325969,
                1817124022,
                737077824,
                2108387938,
                1009993790,
                1262776440,
                294369564,
                1314359226,
                542185392,
                1364312897,
                226687248,
                187903331,
                584875589,
                208537747,
                2131205194,
                139485122,
                535552496,
                1110620181,
                223866742,
                850824492,
                2147357124,
                32325573,
                1391258332,
                1958759872,
                212246942,
                632427661,
                1946109591,
                246918461,
                2084246839,
                597935365,
                212437158,
                1936870917,
                548033679,
                1925144004,
                1544355500,
                1764092709,
                651188294,
                188493172,
                1647527977,
                581363206,
                1691866214,
                1066466254,
                365633943,
                796385043,
                1961653456,
                3447593,
                1568573801,
                1047684517,
                730118314,
                408018473,
            };
            foreach (var predictedValue in predictedValues) {
                Assert.AreEqual(predictedValue, r.Next());
            }
            r = this.GetRandom(true, seedToTest, r);
            foreach (var predictedValue in predictedValues) {
                Assert.AreEqual(predictedValue, r.Next());
            }
            r = this.GetRandom(true, 243, r);
            for (int i = 0; i < 2000; i++) {
                r.Next();
            }
            r = this.GetRandom(true, 1, r);
            for (int i = 0; i < 2000; i++) {
                r.Next();
            }
            r = this.GetRandom(true, 234234, r);
            for (int i = 0; i < 2000; i++) {
                r.Next();
            }
            r = this.GetRandom(true, seedToTest, r);
            foreach (var predictedValue in predictedValues) {
                Assert.AreEqual(predictedValue, r.Next());
            }
        }
    }
}