using FluentAssertions;
using Xunit;

namespace Alexa.Speech.Tests
{
    public class PhonemeTests
    {
        [Fact(DisplayName = "Should Mark The Tagged Part Of The Sentence As Phoneme")]
        public void ShouldMarkTheTaggedPartOfTheSentenceAsPhoneme()
        {
            string speech = new Speech()
                .Say("You say,")
                .Say("pecan")
                    .AsPhoneme(PhoneticAlphabet.IPA, "pɪˈkɑːn")
                .Say(".")
                .Say("I say,")
                .Say("pecan")
                    .AsPhoneme(PhoneticAlphabet.IPA, "ˈpi.kæn")
                .Say(".")
                .Build();

            speech.Should().Be("<speak>You say, <phoneme alphabet=\"ipa\" ph=\"pɪˈkɑːn\">pecan</phoneme>. I say, <phoneme alphabet=\"ipa\" ph=\"ˈpi.kæn\">pecan</phoneme>.</speak>");
        }
    }
}
