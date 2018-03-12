using System.Collections.Generic;
using System.Xml;

namespace Alexa.Speech.Writers
{
    public class PhonemeWriter : ISpeechWriter
    {
        private readonly ISpeechWriter _writer;
        private readonly PhoneticAlphabet _alphabet;
        private readonly string _phoneticPronunciation;
        private static readonly IReadOnlyDictionary<PhoneticAlphabet, string> PhoneticAlphabetAttributeValueMap =
            new Dictionary<PhoneticAlphabet, string>()
            {
                { PhoneticAlphabet.IPA, "ipa" },
                { PhoneticAlphabet.XSampa, "x-sampa" }
            };

        public PhonemeWriter(ISpeechWriter writer, PhoneticAlphabet alphabet, string phoneticPronunciation)
        {
            _writer = writer;
            _alphabet = alphabet;
            _phoneticPronunciation = phoneticPronunciation;
        }

        public void Write(XmlWriter writer)
        {
            writer.WriteStartElement("phoneme");
            string attributeValue = PhoneticAlphabetAttributeValueMap[_alphabet];
            writer.WriteAttributeString("alphabet", attributeValue);
            writer.WriteAttributeString("ph", _phoneticPronunciation);
            _writer.Write(writer);
            writer.WriteEndElement();
        }
    }
}
