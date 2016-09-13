using System;

namespace TMock
{
    /// <summary>
    /// ArgumentsNotMatchedException 
    /// </summary>
    public class ArgumentsNotMatchedException : Exception
    {
        public ArgumentsNotMatchedException(string message) : base(message) { }
    }
}
