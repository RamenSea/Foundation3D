using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace RamenSea.Foundation.General {
    public class BaseFoundationException: Exception {
        public BaseFoundationException() { }
        protected BaseFoundationException([NotNull] SerializationInfo info, StreamingContext context) : base(info, context) { }
        public BaseFoundationException(string message) : base(message) { }
        public BaseFoundationException(string message, Exception innerException) : base(message, innerException) { }
    }
}