using System.Xml;

namespace Alexa.Speech.Writers
{
    public class PlainTextWriter : ISpeechWriter
    {
        private readonly string _value;

        public PlainTextWriter(string value)
        {
            _value = value;
        }

        public void Write(XmlWriter writer)
        {
            writer.WriteString(_value);
        }
    }
}
