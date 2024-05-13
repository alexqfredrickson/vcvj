namespace vcvj.Exceptions
{
    public class ApplicationExtensionParserException : Exception
    {
        public ApplicationExtensionParserException()
        {
        }

        public ApplicationExtensionParserException(string message) : base(message)
        {
        }

        public ApplicationExtensionParserException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
