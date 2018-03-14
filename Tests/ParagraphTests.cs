using FluentAssertions;
using Xunit;

namespace Alexa.Speech.Tests
{
    public class ParagraphTests
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
    }
}
