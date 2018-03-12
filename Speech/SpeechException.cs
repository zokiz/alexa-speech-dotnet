using System;

namespace Alexa.Speech
{
    public class SpeechException : Exception
    {
        public SpeechException()
        {
        }

        public SpeechException(string message) : base(message)
        {
        }

        public SpeechException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
