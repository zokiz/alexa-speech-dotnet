using System.Globalization;
using System.Xml;

namespace Alexa.Speech.Writers
{
    public class SayAsNumberWriter : ISayAsNumber, ISpeechWriter
    {
        private SayAsWriter _writer;
        private readonly ISpeech _speech;
        private readonly int _value;

        public SayAsNumberWriter(ISpeech speech, int value)
        {
            _speech = speech;
            _value = value;
        }

        public ISpeech AsCardinal()
        {
            _writer = new SayAsWriter("cardinal", _value.ToString(CultureInfo.InvariantCulture));
            return _speech;
        }

        public ISpeech AsOrdinal()
        {
            _writer = new SayAsWriter("ordinal", _value.ToString(CultureInfo.InvariantCulture));
            return _speech;
        }

        public ISpeech AsDigits()
        {
            _writer = new SayAsWriter("digits", _value.ToString(CultureInfo.InvariantCulture));
            return _speech;
        }

        public void Write(XmlWriter writer)
        {
            _writer.Write(writer);
        }
    }
}
