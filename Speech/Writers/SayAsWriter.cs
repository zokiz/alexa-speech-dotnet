using System;
using System.Xml;

namespace Alexa.Speech.Writers
{
    public class SayAsWriter : ISpeechWriter
    {
        private readonly string _interpretAs;
        private readonly string _format;
        private readonly string _value;

        public SayAsWriter(string interpretAs, string format, string value)
        {
            _interpretAs = interpretAs;
            _format = format;
            _value = value;
        }

        public SayAsWriter(string interpretAs, string value)
            : this(interpretAs, null, value)
        {
        }

        public void Write(XmlWriter writer)
        {
            writer.WriteStartElement("say-as");
            writer.WriteAttributeString("interpret-as", _interpretAs);
            if (!String.IsNullOrWhiteSpace(_format))
            {
                writer.WriteAttributeString("format", _format);
            }
            writer.WriteString(_value);
            writer.WriteEndElement();
        }
    }
}
