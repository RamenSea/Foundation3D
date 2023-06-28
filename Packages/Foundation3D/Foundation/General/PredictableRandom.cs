using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace RamenSea.Foundation.General {
    /// <summary>
    /// THIS IS A COPY AND PASTE OF C#'s BUILTIN RANDOM CLASS.
    /// C# runtimes give no guarantee that randomness will stay consistent across platforms and version. This class
    /// locks the random implementation down to a specific implementation to guarantee predictable randomness.
    ///
    /// !DO NOT USE THIS CLASS FOR ANYTHING REQUIRING "TRUE" RANDOMNESS!
    ///
    /// The class has been modified to allow for object reuse as well as easy serialization and state sharing
    ///
    /// No offense to Microsoft but the way they are naming their private variables leaves a lot to be desired...
    /// </summary>
    [ComVisible(true)]
    [Serializable]
    public class PredictableRandom {
         /// <summary>
         /// State for the random class
         /// Stores the array of scrambled ints and the index. There has to be a better way for this lol
         /// </summary>
         [Serializable]
         public struct State {
             public int iNext;
             public int iNextP;
             public int[] seedArray;

             public void Copy(State other) {
                 this.iNext = other.iNext;
                 this.iNextP = other.iNextP;
                 other.seedArray.CopyTo(this.seedArray, 0);
             }
         }
        //
        // Private Constants 
        //
        private const int MBIG = int.MaxValue;
        private const int MSEED = 161803398;
        private const int MZ = 0;

        private State _state;
        public State state {
            get => this._state;
            set => this._state.Copy(value);
        }
        public PredictableRandom()
            : this(Environment.TickCount) { }

        public PredictableRandom(int seed) {
            this._state = new State() {
                seedArray = new int[56],
            };
            this.Reseed(seed);
        }
        public PredictableRandom(State state) {
            this._state = new State() {
                seedArray = new int[56],
            };
            this.state = state;
        }

        public void Reseed(int seed) {
            int ii;
            int mj, mk;

            //Initialize our Seed array.
            var subtraction = seed == int.MinValue ? int.MaxValue : Math.Abs(seed);
            mj = MSEED - subtraction;
            this._state.seedArray[55] = mj;
            mk = 1;
            for (var i = 1; i < 55; i++) {
                //Apparently the range [1..55] is special (Knuth) and so we're wasting the 0'th position.
                ii = 21 * i % 55;
                this._state.seedArray[ii] = mk;
                mk = mj - mk;
                if (mk < 0) mk += MBIG;
                mj = this._state.seedArray[ii];
            }

            for (var k = 1; k < 5; k++)
            for (var i = 1; i < 56; i++) {
                this._state.seedArray[i] -= this._state.seedArray[1 + (i + 30) % 55];
                if (this._state.seedArray[i] < 0) this._state.seedArray[i] += MBIG;
            }

            this._state.iNext = 0;
            this._state.iNextP = 21;
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
            var locINext = this._state.iNext;
            var locINextp = this._state.iNextP;

            if (++locINext >= 56) locINext = 1;
            if (++locINextp >= 56) locINextp = 1;

            retVal = this._state.seedArray[locINext] - this._state.seedArray[locINextp];

            if (retVal == MBIG) retVal--;
            if (retVal < 0) retVal += MBIG;

            this._state.seedArray[locINext] = retVal;

            this._state.iNext = locINext;
            this._state.iNextP = locINextp;

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
