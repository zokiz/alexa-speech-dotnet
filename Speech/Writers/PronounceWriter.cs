using System.Collections.Generic;
using System.Xml;

namespace Alexa.Speech.Writers
{
    public class PronounceWriter : ISpeechWriter
    {
        private readonly ISpeechWriter _writer;
        private readonly PronounceRole _role;
        private static readonly IReadOnlyDictionary<PronounceRole, string> PronounceRoleAttributeValueMap =
            new Dictionary<PronounceRole, string>()
            {
                { PronounceRole.NonDefaultSense, "amazon:SENSE_1" },
                { PronounceRole.Noun, "amazon:NN" },
                { PronounceRole.PastParticiple, "amazon:VBD" },
                { PronounceRole.Verb, "amazon:VB" }
            };

        public PronounceWriter(ISpeechWriter writer, PronounceRole role)
        {
            _writer = writer;
            _role = role;
        }

        public void Write(XmlWriter writer)
        {
            writer.WriteStartElement("w");
            string attributeValue = PronounceRoleAttributeValueMap[_role];
            writer.WriteAttributeString("role", attributeValue);
            _writer.Write(writer);
            writer.WriteEndElement();
        }
    }
}
