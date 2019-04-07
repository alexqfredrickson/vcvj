using System;

namespace vcvj.Exceptions
{
    public class UnidentifiedBlockException : Exception
    {
        public UnidentifiedBlockException()
        {
        }

        public UnidentifiedBlockException(string message) : base(message)
        {
        }

        public UnidentifiedBlockException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
