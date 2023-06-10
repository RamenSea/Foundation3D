using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace RamenSea.Foundation.General {
     /**
     * Copy and pasted from the built in random class in c#.
     * This allows full cross platform and c# version deterministic randomness.
     */
    [ComVisible(true)]
    [Serializable]
    public class PredictableRandom {
        //
        // Private Constants 
        //
        private const int MBIG = int.MaxValue;
        private const int MSEED = 161803398;
        private const int MZ = 0;

        //
        // Public Constants
        //

        //
        // Native Declarations
        //

        //
        // Constructors
        //

        private bool _clearSeedArray;


        //
        // Member Variables
        //
        private int inext;
        private int inextp;
        private int[] SeedArray = new int[56];

        public PredictableRandom()
            : this(Environment.TickCount) { }

        public PredictableRandom(int seed) {
            this.Reseed(seed);
        }

        public void Reseed(int seed) {
            if (this._clearSeedArray)
                for (var i = 0; i < this.SeedArray.Length; i++)
                    this.SeedArray[i] = 0; // this might not be necessary, maybe just this.SeedArray[0] = 0;

            this._clearSeedArray = true;
            int ii;
            int mj, mk;

            //Initialize our Seed array.
            var subtraction = seed == int.MinValue ? int.MaxValue : Math.Abs(seed);
            mj = MSEED - subtraction;
            this.SeedArray[55] = mj;
            mk = 1;
            for (var i = 1; i < 55; i++) {
                //Apparently the range [1..55] is special (Knuth) and so we're wasting the 0'th position.
                ii = 21 * i % 55;
                this.SeedArray[ii] = mk;
                mk = mj - mk;
                if (mk < 0) mk += MBIG;
                mj = this.SeedArray[ii];
            }

            for (var k = 1; k < 5; k++)
            for (var i = 1; i < 56; i++) {
                this.SeedArray[i] -= this.SeedArray[1 + (i + 30) % 55];
                if (this.SeedArray[i] < 0) this.SeedArray[i] += MBIG;
            }

            this.inext = 0;
            this.inextp = 21;
            seed = 1;
        }
        //
        // Package Private Methods
        //

        /*====================================Sample====================================
        **Action: Return a new random number [0..1) and reSeed the Seed array.
        **Returns: A double [0..1)
        **Arguments: None
        **Exceptions: None
        ==============================================================================*/
        protected virtual double Sample() {
            //Including this division at the end gives us significantly improved
            //random number distribution.
            return this.InternalSample() * (1.0 / MBIG);
        }

        private int InternalSample() {
            int retVal;
            var locINext = this.inext;
            var locINextp = this.inextp;

            if (++locINext >= 56) locINext = 1;
            if (++locINextp >= 56) locINextp = 1;

            retVal = this.SeedArray[locINext] - this.SeedArray[locINextp];

            if (retVal == MBIG) retVal--;
            if (retVal < 0) retVal += MBIG;

            this.SeedArray[locINext] = retVal;

            this.inext = locINext;
            this.inextp = locINextp;

            return retVal;
        }

        //
        // Public Instance Methods
        // 


        /*=====================================Next=====================================
        **Returns: An int [0..Int32.MaxValue)
        **Arguments: None
        **Exceptions: None.
        ==============================================================================*/
        public virtual int Next() {
            return this.InternalSample();
        }

        private double GetSampleForLargeRange() {
            // The distribution of double value returned by Sample 
            // is not distributed well enough for a large range.
            // If we use Sample for a range [Int32.MinValue..Int32.MaxValue)
            // We will end up getting even numbers only.

            var result = this.InternalSample();
            // Note we can't use addition here. The distribution will be bad if we do that.
            var negative = this.InternalSample() % 2 == 0 ? true : false; // decide the sign based on second sample
            if (negative) result = -result;
            double d = result;
            d += int.MaxValue - 1; // get a number in range [0 .. 2 * Int32MaxValue - 1)
            d /= 2 * (uint)int.MaxValue - 1;
            return d;
        }


        /*=====================================Next=====================================
        **Returns: An int [minvalue..maxvalue)
        **Arguments: minValue -- the least legal value for the Random number.
        **           maxValue -- One greater than the greatest legal return value.
        **Exceptions: None.
        ==============================================================================*/
        public virtual int Next(int minValue, int maxValue) {
            // if (minValue>maxValue) {
            // throw new ArgumentOutOfRangeException("minValue", Environment.GetResourceString("Argument_MinMaxValue", "minValue", "maxValue"));
            // }
            Contract.EndContractBlock();

            var range = (long)maxValue - minValue;
            if (range <= int.MaxValue)
                return (int)(this.Sample() * range) + minValue;
            return (int)((long)(this.GetSampleForLargeRange() * range) + minValue);
        }


        /*=====================================Next=====================================
        **Returns: An int [0..maxValue)
        **Arguments: maxValue -- One more than the greatest legal return value.
        **Exceptions: None.
        ==============================================================================*/
        public virtual int Next(int maxValue) {
            // if (maxValue<0) {
            // throw new ArgumentOutOfRangeException("maxValue", Environment.GetResourceString("ArgumentOutOfRange_MustBePositive", "maxValue"));
            // }
            Contract.EndContractBlock();
            return (int)(this.Sample() * maxValue);
        }


        /*=====================================Next=====================================
        **Returns: A double [0..1)
        **Arguments: None
        **Exceptions: None
        ==============================================================================*/
        public virtual double NextDouble() {
            return this.Sample();
        }


        /*==================================NextBytes===================================
        **Action:  Fills the byte array with random bytes [0..0x7f].  The entire array is filled.
        **Returns:Void
        **Arugments:  buffer -- the array to be filled.
        **Exceptions: None
        ==============================================================================*/
        public virtual void NextBytes(byte[] buffer) {
            if (buffer == null) throw new ArgumentNullException("buffer");
            Contract.EndContractBlock();
            for (var i = 0; i < buffer.Length; i++) buffer[i] = (byte)(this.InternalSample() % (byte.MaxValue + 1));
        }
    }
}
