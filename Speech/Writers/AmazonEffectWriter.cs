using System.Collections.Generic;
using System.Xml;

namespace Alexa.Speech.Writers
{
    public class AmazonEffectWriter : ISpeechWriter
    {
        private readonly ISpeechWriter _writer;
        private readonly AmazonEffect _effect;
        private static readonly IReadOnlyDictionary<AmazonEffect, string> AmazonEffectAttributeValueMap =
            new Dictionary<AmazonEffect, string>()
            {
                {AmazonEffect.Whisper, "whispered"}
            };

        public AmazonEffectWriter(ISpeechWriter writer, AmazonEffect effect)
        {
            _writer = writer;
            _effect = effect;
        }

        public void Write(XmlWriter writer)
        {
            writer.WriteStartElement("amazon", "effect", "amazon");
            string attributeValue = AmazonEffectAttributeValueMap[_effect];
            writer.WriteAttributeString("name", attributeValue);
            _writer.Write(writer);
            writer.WriteEndElement();
        }
    }
}
