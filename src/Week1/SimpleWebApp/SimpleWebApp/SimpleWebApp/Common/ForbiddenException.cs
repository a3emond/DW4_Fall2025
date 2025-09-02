using System;

namespace SimpleWebApp.Common
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException() : base("Forbidden") { }

        public ForbiddenException(string message) : base(message) { }
    }
}