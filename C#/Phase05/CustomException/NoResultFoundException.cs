using System;

namespace Phase05.CustomException
{
    public class NoResultFoundException : Exception
    {
        public NoResultFoundException() : this("No result found")
        {
        }

        public NoResultFoundException(string message) : base(message)
        {
        }
    }
}