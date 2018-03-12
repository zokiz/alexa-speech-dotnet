using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace Alexa.Speech.Writers
{
    public class ProsodyWriter : ISpeechWriter
    {
        private readonly ISpeechWriter _writer;
        private readonly SpeechRate _rate;
        private readonly SpeechPitch _pitch;
        private readonly SpeechVolume _volume;
        private double? _rateInPercentage;
        private double? _pitchInPercentage;
        private double? _volumeInDecibels;
        private static readonly IReadOnlyDictionary<SpeechRate, string> SpeechRateAttributeValueMap =
            new Dictionary<SpeechRate, string>()
            {
                { SpeechRate.ExtraSlow, "x-slow" },
                { SpeechRate.Slow, "slow" },
                { SpeechRate.Medium, "medium" },
                { SpeechRate.Fast, "fast" },
                { SpeechRate.ExtraFast, "x-fast" }
            };
        private static readonly IReadOnlyDictionary<SpeechPitch, string> SpeechPitchAttributeValueMap =
            new Dictionary<SpeechPitch, string>()
            {
                { SpeechPitch.ExtraLow, "x-low" },
                { SpeechPitch.Low, "low" },
                { SpeechPitch.Medium, "medium" },
                { SpeechPitch.High, "high" },
                { SpeechPitch.ExtraHigh, "x-high" }
            };
        private static readonly IReadOnlyDictionary<SpeechVolume, string> SpeechVolumeAttributeValueMap =
            new Dictionary<SpeechVolume, string>()
            {
                { SpeechVolume.Silent, "silent" },
                { SpeechVolume.ExtraSoft, "x-soft" },
                { SpeechVolume.Soft, "soft" },
                { SpeechVolume.Medium, "medium" },
                { SpeechVolume.Loud, "loud" },
                { SpeechVolume.ExtraLoud, "x-loud" }
            };

        public ProsodyWriter(ISpeechWriter writer, SpeechRate rate)
        {
            _writer = writer;
            _rate = rate;
        }

        public ProsodyWriter(ISpeechWriter writer, SpeechPitch pitch)
        {
            _writer = writer;
            _pitch = pitch;
        }

        public ProsodyWriter(ISpeechWriter writer, SpeechVolume volume)
        {
            _writer = writer;
            _volume = volume;
        }

        public ProsodyWriter(ISpeechWriter writer, double? rate = null, double? pitch = null, double? volume = null)
        {
            _writer = writer;
            _rateInPercentage = rate;
            _pitchInPercentage = pitch;
            _volumeInDecibels = volume;
        }

        public void Write(XmlWriter writer)
        {
            writer.WriteStartElement("prosody");

            if (_rate != SpeechRate.NotSet)
            {
                string value = SpeechRateAttributeValueMap[_rate];
                writer.WriteAttributeString("rate", value);
            }
            else if (_rateInPercentage.HasValue)
            {
                if (_rateInPercentage.Value < 20)
                {
                    throw new SpeechException("The minimum value you can provide for speech rate is 20%.");
                }
                string percentage = _rateInPercentage.Value.ToString("0.##", CultureInfo.InvariantCulture);
                writer.WriteAttributeString("rate", $"{percentage}%");
            }
            if (_pitch != SpeechPitch.NotSet)
            {
                string value = SpeechPitchAttributeValueMap[_pitch];
                writer.WriteAttributeString("pitch", value);
            }
            else if (_pitchInPercentage.HasValue)
            {
                string percentage = _pitchInPercentage.Value.ToString("0.##", CultureInfo.InvariantCulture);
                writer.WriteAttributeString("pitch", $"{(_pitchInPercentage.Value > 0 ? "+" : string.Empty)}{percentage}%");
            }
            if (_volume != SpeechVolume.NotSet)
            {
                string value = SpeechVolumeAttributeValueMap[_volume];
                writer.WriteAttributeString("volume", value);
            }
            else if (_volumeInDecibels.HasValue)
            {
                string decibels = _volumeInDecibels.Value.ToString("0.##", CultureInfo.InvariantCulture);
                writer.WriteAttributeString("volume", $"{(_volumeInDecibels.Value >= 0 ? "+" : string.Empty)}{decibels}dB");
            }
            _writer.Write(writer);
            writer.WriteEndElement();
        }
    }
}
