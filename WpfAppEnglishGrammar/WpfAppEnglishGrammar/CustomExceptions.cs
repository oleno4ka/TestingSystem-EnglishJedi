using System;


namespace WpfAppEnglishGrammar
{
    public class NoConnectionToDBException : Exception
    {
        public NoConnectionToDBException()
        {
        }

        public NoConnectionToDBException(string message)
        : base(message)
        {
        }

        public NoConnectionToDBException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }

    public class EmptyTableException : Exception
    {
        public EmptyTableException()
        {
        }

        public EmptyTableException(string message)
        : base(message)
        {
        }

        public EmptyTableException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
