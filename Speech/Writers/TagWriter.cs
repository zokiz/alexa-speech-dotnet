using System.Xml;

namespace Alexa.Speech.Writers
{
    public class TagWriter : ISpeechWriter
    {
        private readonly ISpeechWriter _writer;
        private readonly string _tag;

        public TagWriter(ISpeechWriter writer, string tag)
        {
            _writer = writer;
            _tag = tag;
        }

        public void Write(XmlWriter writer)
        {
            writer.WriteStartElement(_tag);
            _writer.Write(writer);
            writer.WriteEndElement();
        }
    }
}
