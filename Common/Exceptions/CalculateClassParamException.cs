using System;

namespace Common.Exceptions
{
    public class CalculateClassParamException : Exception
    {
        public CalculateClassParamException()
        {
        }

        public CalculateClassParamException(string message) : base(message)
        {
        }
    }
}