using System;
using System.Collections.Generic;
using System.Text;

namespace RamenSea.Foundation.Pools {
    public readonly struct StringBuilderScope: IDisposable {
        public readonly StringBuilder builder;
        private readonly StringBuilderPool pool;
        public StringBuilderScope(StringBuilder builder, StringBuilderPool pool = null) {
            this.builder = builder;
            this.pool = pool;
        }

        /**
         * Attempt to mimic String Builder so you can drop it in
         */
        /// <summary>
        /// </summary>
        public int Length => this.builder.Length; //break convention here to match StringBuilder's api
        public StringBuilder Append(string value) => this.builder.Append(value);
        public StringBuilder Append(object value) => this.builder.Append(value);

        public StringBuilder Insert(int index, string value) => this.builder.Insert(index, value);
        public StringBuilder Insert(int index, object value) => this.builder.Insert(index, value);
        
        public override string ToString() => this.builder.ToString();
        public override int GetHashCode() => this.builder.GetHashCode();

        public static StringBuilderScope operator +(StringBuilderScope scope, string str) { 
            scope.Append(str);
            return scope;
        }
        public static StringBuilderScope operator +(StringBuilderScope scope, object obj) { 
            scope.Append(obj);
            return scope;
        }
        
        public void Dispose() => this.pool?.Recycle(this.builder);
        public override bool Equals(object obj) {
            if (obj is StringBuilderScope s) {
                return s.builder == this.builder;
            }
            return false;
        }
    }
    /// <summary>
    /// A string builder pool
    /// </summary>
    /// <example>
    /// using var sb = StringBuilderPool.Get();
    /// sb.Append("Some string");
    /// string s = sb.ToString();
    /// </example>
    public class StringBuilderPool {
        private readonly Stack<StringBuilder> pool = new();
        public StringBuilderScope LocalGet() => new StringBuilderScope(this.LocalGetRaw(), this);
        public StringBuilder LocalGetRaw() {
            if (this.pool.Count > 0) {
                return this.pool.Pop();
            }
            return new StringBuilder();
        }
        public void Recycle(StringBuilder b) {
            b.Clear();
            this.pool.Push(b);
        }
        
        /**
         * The global pool
         */
        private static readonly StringBuilderPool GlobalPool = new StringBuilderPool();

        /// <summary>
        /// </summary>
        /// <example>
        /// using var sb = StringBuilderPool.Get();
        /// sb.Append("Some string");
        /// string s = sb.ToString();
        /// </example>
        /// <returns></returns>
        public static StringBuilderScope Get() => GlobalPool.LocalGet();
        public static void GlobalRecycle(StringBuilderScope s) {
            GlobalPool.Recycle(s.builder);
        }
        public static void GlobalRecycle(StringBuilder s) {
            GlobalPool.Recycle(s);
        }
    }
}