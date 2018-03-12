namespace Alexa.Speech
{
    public abstract class BaseSpeach : ISpeech
    {
        private readonly ISpeech _speech;

        protected BaseSpeach(ISpeech speech)
        {
            _speech = speech;
        }

        public IPause Pause()
        {
            return _speech.Pause();
        }

        public ISay Say(string value)
        {
            return _speech.Say(value);
        }

        public string Build()
        {
            return _speech.Build();
        }
    }
}
