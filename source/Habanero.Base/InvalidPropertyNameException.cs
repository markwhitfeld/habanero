using System;
using System.Runtime.Serialization;

namespace Habanero.Base.Exceptions
{
    /// <summary>
    /// Provides an exception to throw when a property value is invalid
    /// </summary>
    [Serializable]
    public class InvalidPropertyNameException : HabaneroApplicationException
    {
        /// <summary>
        /// Constructor to initialise the exception
        /// </summary>
        public InvalidPropertyNameException()
        {
        }

        /// <summary>
        /// Constructor to initialise the exception with a specific message
        /// to display
        /// </summary>
        /// <param name="message">The error message</param>
        public InvalidPropertyNameException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructor to initialise the exception with a specific message
        /// to display, and the inner exception specified
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="inner">The inner exception</param>
        public InvalidPropertyNameException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Constructor to initialise the exception with the serialisation info
        /// and streaming context provided
        /// </summary>
        /// <param name="info">The serialisation info</param>
        /// <param name="context">The streaming context</param>
        protected InvalidPropertyNameException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }


}