using System.Xml;

namespace Alexa.Speech.Writers
{
    public class SayWriter : BaseSpeach, ISay, ISpeechWriter
    {
        private readonly string _value;
        private ISpeechWriter _writer;

        public SayWriter(string value, ISpeech speech) : base(speech)
        {
            _value = value;
            _writer = new PlainTextWriter(value);
        }

        public ISpeech Emphasise()
        {
            return Emphasise(EmphasiseLevel.NotSet);
        }

        public ISpeech Emphasise(EmphasiseLevel level)
        {
            _writer = new EmphasiseWriter(_writer, level);
            return this;
        }

        public ISpeech AsParagraph()
        {
            _writer = new ParagraphWriter(_writer);
            return this;
        }

        public ISpeech AsPhoneme(PhoneticAlphabet alphabet, string phoneticPronunciation)
        {
            _writer = new PhonemeWriter(_writer, alphabet, phoneticPronunciation);
            return this;
        }

        public ISpeech WithEffect(AmazonEffect effect)
        {
            _writer = new AmazonEffectWriter(_writer, effect);
            return this;
        }

        public ISpeech WithRate(SpeechRate rate)
        {
            _writer = new ProsodyWriter(_writer, rate);
            return this;
        }

        public ISpeech WithRate(double percentage)
        {
            _writer = new ProsodyWriter(_writer, rate: percentage);
            return this;
        }

        public ISpeech WithPitch(SpeechPitch pitch)
        {
            _writer = new ProsodyWriter(_writer, pitch);
            return this;
        }

        public ISpeech WithPitch(double percentage)
        {
            _writer = new ProsodyWriter(_writer, pitch: percentage);
            return this;
        }

        public ISpeech WithVolume(SpeechVolume volume)
        {
            _writer = new ProsodyWriter(_writer, volume);
            return this;
        }

        public ISpeech WithVolume(double decibels)
        {
            _writer = new ProsodyWriter(_writer, volume: decibels);
            return this;
        }

        public void Write(XmlWriter writer)
        {
            _writer.Write(writer);
        }
    }
}
