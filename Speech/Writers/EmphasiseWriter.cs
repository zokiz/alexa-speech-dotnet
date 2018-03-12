using System.Collections.Generic;
using System.Xml;

namespace Alexa.Speech.Writers
{
    public class EmphasiseWriter : ISpeechWriter
    {
        private readonly ISpeechWriter _writer;
        private readonly EmphasiseLevel _level;
        private static readonly IReadOnlyDictionary<EmphasiseLevel, string> EmphasiseAttributeValueMap =
            new Dictionary<EmphasiseLevel, string>()
            {
                {EmphasiseLevel.Strong, "strong"},
                {EmphasiseLevel.Moderate, "moderate"},
                {EmphasiseLevel.Reduced, "reduced"}
            };

        public EmphasiseWriter(ISpeechWriter writer, EmphasiseLevel level)
        {
            _writer = writer;
            _level = level;
        }

        public void Write(XmlWriter writer)
        {
            writer.WriteStartElement("emphasis");

            if (_level != EmphasiseLevel.NotSet)
            {
                string attributeValue = EmphasiseAttributeValueMap[_level];
                writer.WriteAttributeString("level", attributeValue);
            }

            _writer.Write(writer);
            writer.WriteEndElement();
        }
    }
}
