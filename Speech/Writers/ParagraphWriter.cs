using System.Xml;

namespace Alexa.Speech.Writers
{
    public class ParagraphWriter : ISpeechWriter
    {
        private readonly ISpeechWriter _writer;

        public ParagraphWriter(ISpeechWriter writer)
        {
            _writer = writer;
        }

        public void Write(XmlWriter writer)
        {
            writer.WriteStartElement("p");
            _writer.Write(writer);
            writer.WriteEndElement();
        }
    }
}
