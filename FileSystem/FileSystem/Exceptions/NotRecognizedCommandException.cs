using System;

namespace FileSystem.Exceptions
{
    public class NotRecognizedCommandException : Exception
    {
        public NotRecognizedCommandException(string message) : base(message)
        {
            
        }

        public NotRecognizedCommandException(string message, Exception innerException) : base(message, innerException)
        {
   
        }
    }
}
