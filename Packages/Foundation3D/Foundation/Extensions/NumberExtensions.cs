using System;

namespace RamenSea.Foundation.Extensions {
    public static class NumberExtensions {
        //bytes
        public static short ToShort(this byte b) => b;
        public static int ToInt(this byte b) => b;
        public static long ToLong(this byte b) => b;
        public static float ToFloat(this byte b) => b;
        public static double ToDouble(this byte b) => b;
        public static ushort ToUShort(this byte b) => b;
        public static uint ToUInt(this byte b) => b;
        public static ulong ToULong(this byte b) => b;
        
        //Shorts
        public static short Abs(this short s) => Math.Abs(s);
        public static byte ToByte(this short s) => (byte) s;
        public static int ToInt(this short s) => s;
        public static long ToLong(this short s) => s;
        public static float ToFloat(this short s) => s;
        public static double ToDouble(this short s) => s;
        public static ushort ToUShort(this short s) => (ushort) s;
        public static uint ToUInt(this short s) => (uint) s;
        public static ulong ToULong(this short s) => (ulong) s;
        
        //Unsigned Shorts
        public static byte ToByte(this ushort s) => (byte) s;
        public static short ToShort(this ushort s) => (short) s;
        public static int ToInt(this ushort s) => s;
        public static long ToLong(this ushort s) => s;
        public static float ToFloat(this ushort s) => s;
        public static double ToDouble(this ushort s) => s;
        public static uint ToUInt(this ushort s) => (uint) s;
        public static ulong ToULong(this ushort s) => (ulong) s;
        
        //Ints
        public static int Abs(this int i) => Math.Abs(i);
        public static byte ToByte(this int i) => (byte) i;
        public static short ToShort(this int i) => (short) i;
        public static long ToLong(this int i) => i;
        public static float ToFloat(this int i) => i;
        public static double ToDouble(this int i) => i;
        public static ushort ToUShort(this int i) => (ushort) i;
        public static uint ToUInt(this int i) => (uint) i;
        public static ulong ToULong(this int i) => (ulong) i;
        public static bool GetBit(this int i, int rightPosition) {
            return (i & (1 << rightPosition)) >= 1;
        }

        public static int SetBit(this int i, int rightPosition, bool value) {
            if (value)
                return i | (1 << rightPosition);
            return i & ~(1 << rightPosition);
        }
        
        //Unsigned Ints
        public static byte ToByte(this uint i) => (byte) i;
        public static short ToShort(this uint i) => (short) i;
        public static int ToInt(this uint i) => (int) i;
        public static long ToLong(this uint i) => i;
        public static float ToFloat(this uint i) => i;
        public static double ToDouble(this uint i) => i;
        public static ushort ToUShort(this uint i) => (ushort) i;
        public static ulong ToULong(this uint i) => i;
        
        //Longs
        public static long Abs(this long l) => Math.Abs(l);
        public static byte ToByte(this long l) => (byte) l;
        public static short ToShort(this long l) => (short) l;
        public static int ToInt(this long l) => (int) l;
        public static float ToFloat(this long l) => l;
        public static double ToDouble(this long l) => l;
        public static ushort ToUShort(this long l) => (ushort) l;
        public static uint ToUInt(this long l) => (uint) l;
        public static ulong ToULong(this long l) => (ulong) l;
        
        //Unsigned Longs
        public static byte ToByte(this ulong l) => (byte) l;
        public static short ToShort(this ulong l) => (short) l;
        public static int ToInt(this ulong l) => (int) l;
        public static long ToLong(this ulong l) => (long) l;
        public static float ToFloat(this ulong l) => l;
        public static double ToDouble(this ulong l) => l;
        public static ushort ToUShort(this ulong l) => (ushort) l;
        public static uint ToUInt(this ulong l) => (uint) l;

        //Floats
        
        public static float Abs(this float f) => Math.Abs(f);
        public static byte ToByte(this float f) => (byte) f;
        public static short ToShort(this float f) => (short) f;
        public static int ToInt(this float f) => (int) f;
        public static double ToDouble(this float f) => f;
        public static ushort ToUShort(this float f) => (ushort) f;
        public static uint ToUInt(this float f) => (uint) f;
        public static ulong ToULong(this float f) => (ulong) f;
        public static float Ceil(this float f) => MathF.Ceiling(f);
        public static float Floor(this float f) => MathF.Floor(f);
        public static float Clamp01(this float f) => Math.Max(Math.Min(1f, f), 0f);

        //Doubles
        public static double Abs(this double d) => Math.Abs(d);
        public static byte ToByte(this double d) => (byte) d;
        public static short ToShort(this double d) => (short) d;
        public static int ToInt(this double d) => (int) d;
        public static float ToFloat(this double d) => (float) d;
        public static ushort ToUShort(this double d) => (ushort) d;
        public static uint ToUInt(this double d) => (uint) d;
        public static ulong ToULong(this double d) => (ulong) d;
        public static double Ceil(this double d) => Math.Ceiling(d);
        public static double Floor(this double d) => Math.Floor(d);
    }
}