using System;

namespace FileSystem.Exceptions
{
    /// <summary>
    /// Represent an exception which is thrown when a command is not recognized.
    /// </summary>
    public sealed class NotRecognizedCommandException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the NotRecognizedException class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public NotRecognizedCommandException(string message) : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the NotRecognizedException class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        public NotRecognizedCommandException(string message, Exception innerException) : base(message, innerException)
        {
   
        }
    }
}
