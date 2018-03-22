using FluentAssertions;
using Xunit;

namespace Alexa.Speech.Tests
{
    public class AudioTests
    {
        [Fact(DisplayName = "Should Be Able To Play Audio")]
        public void ShouldBeAbleToPlayAudio()
        {
            string speech = new Speech()
                .Say("Welcome to Car-Fu.")
                .Play("https://s3.amazonaws.com/ask-soundlibrary/transportation/amzn_sfx_car_accelerate_01.mp3")
                .Say("You can order a ride, or request a fare estimate. Which will it be?")
                .Build();

            speech.Should().Be("<speak>Welcome to Car-Fu. <audio src=\"https://s3.amazonaws.com/ask-soundlibrary/transportation/amzn_sfx_car_accelerate_01.mp3\" /> You can order a ride, or request a fare estimate. Which will it be?</speak>");
        }
    }
}
