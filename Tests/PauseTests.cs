using FluentAssertions;
using System;
using Xunit;

namespace Alexa.Speech.Tests
{
    public class PauseTests
    {
        [Fact]
        public void ShouldPause()
        {
            string speech = new Speech()
                .Say("There will be pause here")
                .Pause()
                .Say("then the speech continues.")
                .Build();

            speech.Should().Be("<speak>There will be pause here <break /> then the speech continues.</speak>");
        }

        [Theory]
        [InlineData(PauseStrength.None, "none")]
        [InlineData(PauseStrength.ExtraWeak, "x-weak")]
        [InlineData(PauseStrength.Weak, "weak")]
        [InlineData(PauseStrength.Medium, "medium")]
        [InlineData(PauseStrength.Strong, "strong")]
        [InlineData(PauseStrength.ExtraStrong, "x-strong")]
        public void ShouldSetThePauseStrength(PauseStrength strength, string expected)
        {
            string speech = new Speech()
                .Say("There will be pause here")
                .Pause()
                    .WithStrength(strength)
                .Say("then the speech continues.")
                .Build();

            speech.Should().Be($"<speak>There will be pause here <break strength=\"{expected}\" /> then the speech continues.</speak>");
        }

        [Fact]
        public void ShouldSetThePauseWithTimeDelay()
        {
            string speech = new Speech()
                .Say("There will be 3 seconds pause here")
                .Pause()
                    .For(TimeSpan.FromSeconds(3))
                .Say("then the speech continues.")
                .Build();

            speech.Should().Be("<speak>There will be 3 seconds pause here <break time=\"3000ms\" /> then the speech continues.</speak>");
        }

        [Fact]
        public void ShouldSetThePauseWithStrengthAndTimeDelay()
        {
            string speech = new Speech()
                .Say("There will be pause here")
                .Pause()
                    .WithStrength(PauseStrength.Medium)
                    .For(TimeSpan.FromMilliseconds(1000.4))
                .Say("then the speech continues.")
                .Build();

            speech.Should().Be("<speak>There will be pause here <break strength=\"medium\" time=\"1000ms\" /> then the speech continues.</speak>");
        }

        [Fact]
        public void ShouldThrowExceptionWhenPassedInPauseTimeDelayIsOverTenSeconds()
        {
            ISpeech speech = new Speech()
                .Say("There will be pause here")
                .Pause()
                    .For(TimeSpan.FromSeconds(10.1))
                .Say("then the speech continues.");

            Action action = () => speech.Build();
            action.Should()
                .Throw<SpeechException>()
                .WithMessage("The duration of the pause should be up to 10 seconds (10s) or 10000 milliseconds (10000ms).");
        }
    }
}
