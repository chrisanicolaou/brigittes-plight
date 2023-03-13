using System;

namespace ChiciStudios.BrigittesPlight.Exceptions
{
    public class ScenefabException : Exception
    {
        public ScenefabException() { }
        public ScenefabException(string message) : base(message) { }
        public ScenefabException(string message, Exception inner) : base(message, inner) { }
    }
}