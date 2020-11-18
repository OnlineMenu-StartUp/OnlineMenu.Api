using System;

namespace OnlineMenu.Domain.Exceptions
{
    public class ValueNotFoundException: Exception
    {
        public ValueNotFoundException(string message): base(message) { }
    }
}