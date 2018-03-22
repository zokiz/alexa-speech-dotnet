using System.Xml;

namespace Alexa.Speech.Writers
{
    public class AudioWriter : BaseSpeach, IPlayAudio, ISpeechWriter
    {
        private readonly string _source;

        public AudioWriter(ISpeech speech, string source) : base(speech)
        {
            _source = source;
        }

        public void Write(XmlWriter writer)
        {
            writer.WriteStartElement("audio");
            writer.WriteAttributeString("src", _source);
            writer.WriteEndElement();
        }
    }
}
