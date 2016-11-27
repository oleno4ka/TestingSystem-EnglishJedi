using System;

// Review TK: It would great to move these classes into separated classes.
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
