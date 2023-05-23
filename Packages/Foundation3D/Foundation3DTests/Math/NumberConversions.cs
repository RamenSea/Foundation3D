using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using RamenSea.Foundation.Extensions;
using UnityEngine;
using UnityEngine.TestTools;

namespace RamenSea.Foundation3DTests.Math {
    public class NumberConversions {
        [Test]
        public void ToByteTranslations() {
            
            Assert.AreEqual(((short) 1).ToByte(), (byte) 1); 
            Assert.AreEqual(((short) 255).ToByte(), (byte) 255); 
            Assert.AreEqual(((short) 256).ToByte(), (byte) 0); 
            Assert.AreEqual(((short) 256 * 4).ToByte(), (byte) 0); 
            Assert.AreEqual(((short) 257).ToByte(), (byte) 1); 
            
            Assert.AreEqual(((ushort) 1).ToByte(), (byte) 1); 
            Assert.AreEqual(((ushort) 255).ToByte(), (byte) 255); 
            Assert.AreEqual(((ushort) 256).ToByte(), (byte) 0); 
            Assert.AreEqual(((ushort) 256 * 4).ToByte(), (byte) 0); 
            Assert.AreEqual(((ushort) 257).ToByte(), (byte) 1); 
            
            Assert.AreEqual(((int) 1).ToByte(), (byte) 1); 
            Assert.AreEqual(((int) 255).ToByte(), (byte) 255); 
            Assert.AreEqual(((int) 256).ToByte(), (byte) 0); 
            Assert.AreEqual(((int) 256 * 4).ToByte(), (byte) 0); 
            Assert.AreEqual(((int) 257).ToByte(), (byte) 1);
            
            Assert.AreEqual(((uint) 1).ToByte(), (byte) 1); 
            Assert.AreEqual(((uint) 255).ToByte(), (byte) 255); 
            Assert.AreEqual(((uint) 256).ToByte(), (byte) 0); 
            Assert.AreEqual(((uint) 256 * 4).ToByte(), (byte) 0); 
            Assert.AreEqual(((uint) 257).ToByte(), (byte) 1); 
            
            Assert.AreEqual(((long) 1).ToByte(), (byte) 1); 
            Assert.AreEqual(((long) 255).ToByte(), (byte) 255); 
            Assert.AreEqual(((long) 256).ToByte(), (byte) 0); 
            Assert.AreEqual(((long) 256 * 4).ToByte(), (byte) 0); 
            Assert.AreEqual(((long) 257).ToByte(), (byte) 1); 
            
            Assert.AreEqual(((ulong) 1).ToByte(), (byte) 1); 
            Assert.AreEqual(((ulong) 255).ToByte(), (byte) 255); 
            Assert.AreEqual(((ulong) 256).ToByte(), (byte) 0); 
            Assert.AreEqual(((ulong) 256 * 4).ToByte(), (byte) 0); 
            Assert.AreEqual(((ulong) 257).ToByte(), (byte) 1); 
            
            Assert.AreEqual(((float) 1f).ToByte(), (byte) 1); 
            Assert.AreEqual(((float) 255f).ToByte(), (byte) 255); 
            Assert.AreEqual(((float) 256f).ToByte(), (byte) 0); 
            Assert.AreEqual(((float) 256f * 4f).ToByte(), (byte) 0); 
            Assert.AreEqual(((float) 257f).ToByte(), (byte) 1); 
            
            Assert.AreEqual(((double) 1.0).ToByte(), (byte) 1); 
            Assert.AreEqual(((double) 255.0).ToByte(), (byte) 255); 
            Assert.AreEqual(((double) 256.0).ToByte(), (byte) 0); 
            Assert.AreEqual(((double) 256.0 * 4.0).ToByte(), (byte) 0); 
            Assert.AreEqual(((double) 257.0).ToByte(), (byte) 1); 
            
        }
        // // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // // `yield return null;` to skip a frame.
        // [UnityTest]
        // public IEnumerator NumberConversionsWithEnumeratorPasses()
        // {
        //     // Use the Assert class to test conditions.
        //     // Use yield to skip a frame.
        //     yield return null;
        // }
    }
}
