using System;

namespace OnlineMenu.Domain.Exceptions
{
    /// <summary>
    /// It is shown to the user only in development
    /// </summary>
    public class ConfigurationException: Exception // TODO: Add in filter middleware
    {
        public ConfigurationException(string message): base(message)
        {
            
        }
    }
}