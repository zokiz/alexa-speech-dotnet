using System.Xml;

namespace Alexa.Speech.Writers
{
    public class SubWriter : ISpeechWriter
    {
        private readonly ISpeechWriter _writer;
        private readonly string _alias;

        public SubWriter(ISpeechWriter writer, string alias)
        {
            _writer = writer;
            _alias = alias;
        }

        public void Write(XmlWriter writer)
        {
            writer.WriteStartElement("sub");
            writer.WriteAttributeString("alias", _alias);
            _writer.Write(writer);
            writer.WriteEndElement();
        }
    }
}
