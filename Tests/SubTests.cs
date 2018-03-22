using FluentAssertions;
using Xunit;

namespace Alexa.Speech.Tests
{
    public class SubTests
    {
        [Fact(DisplayName = "Should Set The Alias For Tagged Text")]
        public void ShouldSetTheAliasForTaggedText()
        {
            string speech = new Speech()
                .Say("My favorite chemical element is")
                .Say("Al")
                    .AsAlias("aluminum")
                .Say("but Al prefers")
                .Say("Mg")
                    .AsAlias("magnesium")
                .Say(".")
                .Build();

            speech.Should().Be("<speak>My favorite chemical element is <sub alias=\"aluminum\">Al</sub> but Al prefers <sub alias=\"magnesium\">Mg</sub>.</speak>");
        }
    }
}
