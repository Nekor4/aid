using System;

namespace Aid.Data
{
    public class PrimitiveDataException : Exception
    {
        public PrimitiveDataException()
        {
            throw new Exception("Complex Data Property is not supporting primitive types.");
        }
    }
}