using FluentAssertions;
using Xunit;

namespace Alexa.Speech.Tests
{
    public class EmphasiseTests
    {
        [Fact(DisplayName = "Should Emphasise Given Word In Sentence")]
        public void ShouldEmphasiseGivenWordInSentence()
        {
            string speech = new Speech()
                .Say("I already told you I")
                .Say("really like")
                    .Emphasise()
                .Say("that person.")
                .Build();

            speech.Should().Be("<speak>I already told you I <emphasis>really like</emphasis> that person.</speak>");
        }

        [Theory(DisplayName = "Should Emphasise And Set The Emphasise Level To Given Part Of The Sentence")]
        [InlineData(EmphasiseLevel.Strong, "strong")]
        [InlineData(EmphasiseLevel.Moderate, "moderate")]
        [InlineData(EmphasiseLevel.Reduced, "reduced")]
        public void ShouldEmphasiseAndSetTheEmphasiseLevelToGivenPartOfTheSentence(EmphasiseLevel level, string expected)
        {
            string speech = new Speech()
                .Say("I already told you I")
                .Say("really like")
                    .Emphasise(level)
                .Say("that person.")
                .Build();

            speech.Should().Be($"<speak>I already told you I <emphasis level=\"{expected}\">really like</emphasis> that person.</speak>");
        }
    }
}
