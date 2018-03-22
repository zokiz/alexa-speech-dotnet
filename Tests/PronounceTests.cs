using FluentAssertions;
using Xunit;

namespace Alexa.Speech.Tests
{
    public class PronounceTests
    {
        [Fact(DisplayName = "Should Be Able To Set How To Pronounce Word")]
        public void ShouldBeAbleToSetHowToPronounceWord()
        {
            string speech = new Speech()
                .Say("The word")
                .Say("read")
                    .AsCharacters()
                .Say("may be interpreted as either the present simple form")
                .Say("read")
                    .PronounceAs(PronounceRole.Verb)
                .Say("or the past participle form")
                .Say("read")
                    .PronounceAs(PronounceRole.PastParticiple)
                .Build();

            speech.Should().Be("<speak>The word <say-as interpret-as=\"characters\">read</say-as> may be interpreted as either the present simple form <w role=\"amazon:VB\">read</w> or the past participle form <w role=\"amazon:VBD\">read</w></speak>");
        }

        [Theory(DisplayName = "Should Be Able To Handle Different Pronunciations")]
        [InlineData(PronounceRole.NonDefaultSense, "amazon:SENSE_1")]
        [InlineData(PronounceRole.Noun, "amazon:NN")]
        [InlineData(PronounceRole.PastParticiple, "amazon:VBD")]
        [InlineData(PronounceRole.Verb, "amazon:VB")]
        public void ShouldBeAbleToHandleDifferentPronunciations(PronounceRole role, string expected)
        {
            string speech = new Speech()
                .Say("The word")
                .Say("read")
                    .AsCharacters()
                .Say("may be interpreted as")
                .Say("read")
                    .PronounceAs(role)
                .Build();

            speech.Should().Be($"<speak>The word <say-as interpret-as=\"characters\">read</say-as> may be interpreted as <w role=\"{expected}\">read</w></speak>");
        }
    }
}
