using System;
using System.Collections.Generic;
using System.Xml;

namespace Alexa.Speech.Writers
{
    public class PauseWriter : BaseSpeach, IPause, ISpeechWriter
    {
        private TimeSpan? _duration;
        private PauseStrength _strength;
        private static readonly IReadOnlyDictionary<PauseStrength, string> BreakStrengthAttributeMap = 
            new Dictionary<PauseStrength, string>()
            {
                {PauseStrength.None, "none"},
                {PauseStrength.ExtraWeak, "x-weak"},
                {PauseStrength.Weak, "weak"},
                {PauseStrength.Medium, "medium"},
                {PauseStrength.Strong, "strong"},
                {PauseStrength.ExtraStrong, "x-strong"},
            };

        public PauseWriter(ISpeech speech) : base(speech)
        {
        }

        public IPause For(TimeSpan duration)
        {
            _duration = duration;
            return this;
        }
        
        public IPause WithStrength(PauseStrength strength)
        {
            _strength = strength;
            return this;
        }

        public void Write(XmlWriter writer)
        {
            writer.WriteStartElement("break");

            if (_strength != PauseStrength.NotSet)
            {
                string value = BreakStrengthAttributeMap[_strength];
                writer.WriteAttributeString("strength", value);
            }
            if (_duration.HasValue)
            {
                double milliseconds = _duration.Value.TotalMilliseconds;
                if (milliseconds > 10000)
                {
                    throw new SpeechException("The duration of the pause should be up to 10 seconds (10s) or 10000 milliseconds (10000ms).");
                }
                writer.WriteAttributeString(null, "time", null, $"{milliseconds:F0}ms");
            }
            writer.WriteEndElement();
        }
    }
}
