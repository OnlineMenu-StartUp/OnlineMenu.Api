using System;

namespace OnlineMenu.Domain.Exceptions
{
    public class BadValueException: ArgumentException
    {
        public BadValueException(string message) : base(message)
        { }
    }
}