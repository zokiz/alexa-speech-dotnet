using FluentAssertions;
using Xunit;

namespace Alexa.Speech.Tests
{
    public class TagTests
    {
        [Fact(DisplayName = "Should Mark Part Of The Sentence As Paragraph")]
        public void ShouldMarkPartOfTheSentenceAsParagraph()
        {
            string speech = new Speech()
                .Say("This is the first paragraph. There should be a pause after this text is spoken.")
                    .AsParagraph()
                .Say("This is the second paragraph.")
                    .AsParagraph()
                .Build();

            speech.Should().Be("<speak><p>This is the first paragraph. There should be a pause after this text is spoken.</p><p>This is the second paragraph.</p></speak>");
        }

        [Fact(DisplayName = "Should Mark The Text As Sentence")]
        public void ShouldMarkTheTextAsSentence()
        {
            string speech = new Speech()
                .Say("This is a sentence")
                    .AsSentence()
                .Say("There should be a short pause before this second sentence")
                    .AsSentence()
                .Say("This sentence ends with a period and should have the same pause.")
                .Build();

            speech.Should().Be("<speak><s>This is a sentence</s><s>There should be a short pause before this second sentence</s> This sentence ends with a period and should have the same pause.</speak>");
        }
    }
}
