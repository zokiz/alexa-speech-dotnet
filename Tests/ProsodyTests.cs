using FluentAssertions;
using System;
using Xunit;

namespace Alexa.Speech.Tests
{
    public class ProsodyTests
    {
        [Theory(DisplayName = "Should Set The Speech Rate On Predefined Value")]
        [InlineData(SpeechRate.ExtraSlow, "x-slow")]
        [InlineData(SpeechRate.Slow, "slow")]
        [InlineData(SpeechRate.Medium, "medium")]
        [InlineData(SpeechRate.Fast, "fast")]
        [InlineData(SpeechRate.ExtraFast, "x-fast")]
        public void ShouldSetTheSpeechRateOnPredefinedValue(SpeechRate rate, string expected)
        {
            string speech = new Speech()
                .Say("When I wake up,")
                .Say("I can speak with different rate")
                    .WithRate(rate)
                .Say(".")
                .Build();

            speech.Should().Be($"<speak>When I wake up, <prosody rate=\"{expected}\">I can speak with different rate</prosody>.</speak>");
        }

        [Theory(DisplayName = "Should Set The Speech Rate As Percentage")]
        [InlineData(20, "20%")]
        [InlineData(55.3, "55.3%")]
        [InlineData(100, "100%")]
        [InlineData(130.3, "130.3%")]
        public void ShouldSetTheSpeechRateAsPercentage(double rate, string expected)
        {
            string speech = new Speech()
                .Say("When I wake up,")
                .Say("I can speak with different rate set by percentage number")
                    .WithRate(rate)
                .Say(".")
                .Build();

            speech.Should().Be($"<speak>When I wake up, <prosody rate=\"{expected}\">I can speak with different rate set by percentage number</prosody>.</speak>");
        }

        [Fact(DisplayName = "Should Throw Exception When The Set Speech Rate As Percentage Is Lower Than Twenty")]
        public void ShouldThrowExceptionWhenTheSetSpeechRateAsPercentageIsLowerThanTwenty()
        {
            ISpeech speech = new Speech()
                .Say("When I wake up,")
                .Say("I can speak with different rate set by percentage number")
                    .WithRate(19.99)
                .Say(".");

            Action action = () => speech.Build();
            action.Should()
                .Throw<SpeechException>()
                .WithMessage("The minimum value you can provide for speech rate is 20%.");
        }

        [Theory(DisplayName = "Should Set The Speech Pitch On Predefined Value")]
        [InlineData(SpeechPitch.ExtraLow, "x-low")]
        [InlineData(SpeechPitch.Low, "low")]
        [InlineData(SpeechPitch.Medium, "medium")]
        [InlineData(SpeechPitch.High, "high")]
        [InlineData(SpeechPitch.ExtraHigh, "x-high")]
        public void ShouldSetTheSpeechPitchOnPredefinedValue(SpeechPitch pitch, string expected)
        {
            string speech = new Speech()
                .Say("I can speak with my normal pitch,")
                .Say("but also with a different pitch")
                    .WithPitch(pitch)
                .Say(".")
                .Build();

            speech.Should().Be($"<speak>I can speak with my normal pitch, <prosody pitch=\"{expected}\">but also with a different pitch</prosody>.</speak>");
        }

        [Theory(DisplayName = "Should Set The Speech Pitch As Percentage")]
        [InlineData(5, "+5%")]
        [InlineData(10, "+10%")]
        [InlineData(50, "+50%")]
        [InlineData(-10, "-10%")]
        [InlineData(-20, "-20%")]
        [InlineData(-33.3, "-33.3%")]
        public void ShouldSetTheSpeechPitchAsPercentage(double pitch, string expected)
        {
            string speech = new Speech()
                .Say("I can speak with my normal pitch,")
                .Say("but also with a different pitch")
                    .WithPitch(pitch)
                .Say(".")
                .Build();

            speech.Should().Be($"<speak>I can speak with my normal pitch, <prosody pitch=\"{expected}\">but also with a different pitch</prosody>.</speak>");
        }

        [Theory(DisplayName = "Should Set The Speech Volume On Predefined Value")]
        [InlineData(SpeechVolume.Silent, "silent")]
        [InlineData(SpeechVolume.ExtraSoft, "x-soft")]
        [InlineData(SpeechVolume.Soft, "soft")]
        [InlineData(SpeechVolume.Medium, "medium")]
        [InlineData(SpeechVolume.Loud, "loud")]
        [InlineData(SpeechVolume.ExtraLoud, "x-loud")]
        public void ShouldSetTheSpeechVolumeOnPredefinedValue(SpeechVolume volume, string expected)
        {
            string speech = new Speech()
                .Say("Normal volume for the first sentence.")
                .Say("Different volume for the second sentence")
                    .WithVolume(volume)
                .Say(".")
                .Build();

            speech.Should().Be($"<speak>Normal volume for the first sentence. <prosody volume=\"{expected}\">Different volume for the second sentence</prosody>.</speak>");
        }

        [Theory(DisplayName = "Should Set The Speech Volume As Decibel")]
        [InlineData(0, "+0dB")]
        [InlineData(2, "+2dB")]
        [InlineData(4.08, "+4.08dB")]
        [InlineData(-6, "-6dB")]
        public void ShouldSetTheSpeechVolumeAsDecibel(double volume, string expected)
        {
            string speech = new Speech()
                .Say("Normal volume for the first sentence.")
                .Say("Different volume for the second sentence")
                    .WithVolume(volume)
                .Say(".")
                .Build();

            speech.Should().Be($"<speak>Normal volume for the first sentence. <prosody volume=\"{expected}\">Different volume for the second sentence</prosody>.</speak>");
        }
    }
}
