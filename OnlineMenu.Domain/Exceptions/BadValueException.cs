using System;

namespace OnlineMenu.Domain.Exceptions
{
    public class BadValueException: Exception
    {
        public BadValueException(string message) : base(message)
        { }
    }
}