﻿using FluentAssertions;
using Xunit;

namespace Alexa.Speech.Tests
{
    public class SayTests
    {
        [Fact(DisplayName = "Should Be Able To Wrap Sentence With Speak Tag")]
        public void ShouldBeAbleToWrapSentenceWithSpeakTag()
        {
            string speech = new Speech()
                .Say("I am trying to make a point here.")
                .Build();

            speech.Should().Be("<speak>I am trying to make a point here.</speak>");
        }

        [Fact(DisplayName = "Should Be Able To Concatenate Words Or Sentences")]
        public void ShouldBeAbleToConcatenateWordsOrSentences()
        {
            string speech = new Speech()
                .Say("This is an example sentence and this is only one")
                .Say("'word'")
                .Say(".")
                .Build();

            speech.Should().Be("<speak>This is an example sentence and this is only one 'word'.</speak>");
        }
    }
}
