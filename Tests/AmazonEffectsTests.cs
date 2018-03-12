using FluentAssertions;
using Xunit;

namespace Alexa.Speech.Tests
{
    public class AmazonEffectsTests
    {
        [Fact]
        public void ShouldSetTheWisperedEffectToGivenPartOfTheSentence()
        {
            string speech = new Speech()
                .Say("I want to tell you a secret.")
                .Say("I am not a real human.")
                    .WithEffect(AmazonEffect.Whisper)
                .Say("Can you believe it?")
                .Build();

            speech.Should().Be("<speak>I want to tell you a secret. <amazon:effect name=\"whispered\">I am not a real human.</amazon:effect> Can you believe it?</speak>");
        }
    }
}
