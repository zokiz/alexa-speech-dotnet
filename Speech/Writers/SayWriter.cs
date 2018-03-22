using System.Xml;

namespace Alexa.Speech.Writers
{
    public class SayWriter : BaseSpeach, ISay, ISpeechWriter
    {
        private ISpeechWriter _writer;
        private readonly string _value;

        public SayWriter(ISpeech speech, string value) : base(speech)
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
            _writer = new TagWriter(_writer, "p");
            return this;
        }

        public ISpeech AsPhoneme(PhoneticAlphabet alphabet, string phoneticPronunciation)
        {
            _writer = new PhonemeWriter(_writer, alphabet, phoneticPronunciation);
            return this;
        }

        public ISpeech AsSentence()
        {
            _writer = new TagWriter(_writer, "s");
            return this;
        }

        public ISpeech AsAlias(string alias)
        {
            _writer = new SubWriter(_writer, alias);
            return this;
        }
        public ISpeech AsFraction()
        {
            _writer = new SayAsWriter("fraction", _value);
            return this;
        }

        public ISpeech AsUnit()
        {
            _writer = new SayAsWriter("unit", _value);
            return this;
        }

        public ISpeech AsAddress()
        {
            _writer = new SayAsWriter("address", _value);
            return this;
        }

        public ISpeech AsInterjection()
        {
            _writer = new SayAsWriter("interjection", _value);
            return this;
        }

        public ISpeech Expletive()
        {
            _writer = new SayAsWriter("expletive", _value);
            return this;
        }

        public ISpeech AsTime()
        {
            _writer = new SayAsWriter("time", _value);
            return this;
        }

        public ISpeech AsTelephone()
        {
            _writer = new SayAsWriter("telephone", _value);
            return this;
        }

        public ISpeech AsCharacters()
        {
            _writer = new SayAsWriter("characters", _value);
            return this;
        }

        public ISpeech SpellOut()
        {
            _writer = new SayAsWriter("spell-out", _value);
            return this;
        }

        public ISpeech PronounceAs(PronounceRole role)
        {
            _writer = new PronounceWriter(_writer, role);
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
