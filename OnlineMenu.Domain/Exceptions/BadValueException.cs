using System;

namespace OnlineMenu.Domain.Exceptions
{
    public class BadValueException: Exception
    {
        public BadValueException(String msg) : base(msg)
        { }
    }
}