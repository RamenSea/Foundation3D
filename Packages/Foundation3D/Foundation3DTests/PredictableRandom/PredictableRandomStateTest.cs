using NUnit.Framework;
using RamenSea.Foundation.Extensions;

namespace RamenSea.Foundation3DTests.PredictableRandom {
    public class PredictableRandomStateTest {
        [Test]
        public void TestState() {
            var r1 = new Foundation.General.PredictableRandom(1);
            var r2 = new Foundation.General.PredictableRandom(2);
            
            Assert.AreEqual(false, r1.Next() == r2.Next());
            r2.state = r1.state;
            Assert.AreEqual(true, r1.Next() == r2.Next());
            r1.Next();
            Assert.AreEqual(false, r1.Next() == r2.Next());
            r2.state = r1.state;
            for (int i = 0; i < 10000; i++) {
                Assert.AreEqual(true, r1.Next() == r2.Next());
            }
            
            r1 = new Foundation.General.PredictableRandom(); 
            r2 = new Foundation.General.PredictableRandom();
            for (int i = 0; i < 10000; i++) {
                r1.Next();
            }
            for (int i = 0; i < 400; i++) {
                r2.Next();
            }
            r2.state = r1.state;
            for (int i = 0; i < 10000; i++) {
                Assert.AreEqual(true, r1.Next() == r2.Next());
            }


        }
    }
}