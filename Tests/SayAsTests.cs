using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Alexa.Speech.Tests
{
    public class SayAsTests
    {
        [Fact(DisplayName = "Should Be Able To Interpret Word As Cardinal Number")]
        public void ShouldBeAbleToInterpretWordAsCardinalNumber()
        {
            string speech = new Speech()
                .Say("Here is a number spoken as a cardinal number:")
                .Say(72519)
                    .AsCardinal()
                .Build();

            speech.Should().Be("<speak>Here is a number spoken as a cardinal number: <say-as interpret-as=\"cardinal\">72519</say-as></speak>");
        }

        [Fact(DisplayName = "Should Be Able To Interpret Word As Ordinal Number")]
        public void ShouldBeAbleToInterpretWordAsOrdinalNumber()
        {
            string speech = new Speech()
                .Say("Here is a number spoken as a ordinal number:")
                .Say(25)
                    .AsOrdinal()
                .Build();

            speech.Should().Be("<speak>Here is a number spoken as a ordinal number: <say-as interpret-as=\"ordinal\">25</say-as></speak>");
        }

        [Fact(DisplayName = "Should Be Able To Interpret Number As Digits")]
        public void ShouldBeAbleToInterpretNumberAsDigits()
        {
            string speech = new Speech()
                .Say("Here is a number where each digit is spelled separately:")
                .Say(1098468341)
                    .AsDigits()
                .Build();

            speech.Should().Be("<speak>Here is a number where each digit is spelled separately: <say-as interpret-as=\"digits\">1098468341</say-as></speak>");
        }

        [Fact(DisplayName = "Should Be Able To Interpret Word As Fraction")]
        public void ShouldBeAbleToInterpretWordAsFraction()
        {
            string speech = new Speech()
                .Say("Here is one common fraction, such as")
                .Say("3/20")
                    .AsFraction()
                .Say("and here is one mixed fraction such as")
                .Say("1+1/2")
                    .AsFraction()
                .Build();

            speech.Should().Be("<speak>Here is one common fraction, such as <say-as interpret-as=\"fraction\">3/20</say-as> and here is one mixed fraction such as <say-as interpret-as=\"fraction\">1+1/2</say-as></speak>");
        }

        [Fact(DisplayName = "Should Be Able To Interpret Word As Measurement Unit")]
        public void ShouldBeAbleToInterpretWordAsMeasurementUnit()
        {
            string speech = new Speech()
                .Say("I have just bought")
                .Say("3kg")
                    .AsUnit()
                .Say("of apples and")
                .Say("5oz")
                    .AsUnit()
                .Say("of something else.")
                .Build();

            speech.Should().Be("<speak>I have just bought <say-as interpret-as=\"unit\">3kg</say-as> of apples and <say-as interpret-as=\"unit\">5oz</say-as> of something else.</speak>");
        }

        [Fact(DisplayName = "Should Be Able To Interpret Word As Part Of Street Address")]
        public void ShouldBeAbleToInterpretWordAsPartOfStreetAddress()
        {
            string speech = new Speech()
                .Say("My nephew currently lives somewhere between")
                .Say("10 Fulton Street")
                    .AsAddress()
                .Say("and")
                .Say("100 John St.")
                    .AsAddress()
                .Build();

            speech.Should().Be("<speak>My nephew currently lives somewhere between <say-as interpret-as=\"address\">10 Fulton Street</say-as> and <say-as interpret-as=\"address\">100 John St.</say-as></speak>");
        }

        [Fact(DisplayName = "Should Be Able To Interpret Word As Interjection")]
        public void ShouldBeAbleToInterpretWordAsInterjection()
        {
            string speech = new Speech()
                .Say("The following text will be spoken in a more expressive voice")
                    .Pause()
                .Say("Gotcha.")
                    .AsInterjection()
                .Build();

            speech.Should().Be("<speak>The following text will be spoken in a more expressive voice <break /><say-as interpret-as=\"interjection\">Gotcha.</say-as></speak>");
        }

        [Fact(DisplayName = "Should Be Able To Interpret Word Expletive")]
        public void ShouldBeAbleToInterpretWordExpletive()
        {
            string speech = new Speech()
                .Say("Don't you think that this comment is kind of")
                .Say("stupid")
                    .Expletive()
                .Say("and")
                .Say("ignorant?")
                    .Expletive()
                .Build();

            speech.Should().Be("<speak>Don't you think that this comment is kind of <say-as interpret-as=\"expletive\">stupid</say-as> and <say-as interpret-as=\"expletive\">ignorant?</say-as></speak>");
        }

        [Fact(DisplayName = "Should Be Able To Interpret Word As Duration In Minutes And Seconds")]
        public void ShouldBeAbleToInterpretWordAsDurationInMinutesAndSeconds()
        {
            string speech = new Speech()
                .Say("The fastest runner was able to finish the race in")
                .Say("4'28''")
                    .AsTime()
                .Build();

            speech.Should().Be("<speak>The fastest runner was able to finish the race in <say-as interpret-as=\"time\">4'28''</say-as></speak>");
        }

        [Fact(DisplayName = "Should Be Able To Interpret Word As Telephone")]
        public void ShouldBeAbleToInterpretWordAsTelephone()
        {
            string speech = new Speech()
                .Say("To contact us please call")
                .Say("+46 (0)70 111 0033")
                    .AsTelephone()
                .Build();

            speech.Should().Be("<speak>To contact us please call <say-as interpret-as=\"telephone\">+46 (0)70 111 0033</say-as></speak>");
        }

        [Fact(DisplayName = "Should Be Able To Spell Out Characters")]
        public void ShouldBeAbleToSpellOutCharacters()
        {
            string speech = new Speech()
                .Say("It is easy for to win a spelling bee contest, it is easy as")
                .Say("abcdefg")
                    .AsCharacters()
                .Build();

            speech.Should().Be("<speak>It is easy for to win a spelling bee contest, it is easy as <say-as interpret-as=\"characters\">abcdefg</say-as></speak>");
        }

        [Fact(DisplayName = "Should Be Able To Spell Out A Word")]
        public void ShouldBeAbleToSpellOutAWord()
        {
            string speech = new Speech()
                .Say("It is easy for to win a spelling bee contest, it is easy as")
                .Say("abcd")
                    .SpellOut()
                .Build();

            speech.Should().Be("<speak>It is easy for to win a spelling bee contest, it is easy as <say-as interpret-as=\"spell-out\">abcd</say-as></speak>");
        }
        
        [Fact(DisplayName = "Should Be Able To Interpret Word As Date")]
        public void ShouldBeAbleToInterpretWordAsDate()
        {
            string speech = new Speech()
                .Say("What date is today?")
                .Say("Well, as far as I know, today's date is")
                .Say(new DateTime(2018, 3, 15))
                .Build();

            speech.Should().Be("<speak>What date is today? Well, as far as I know, today's date is <say-as interpret-as=\"date\">20180315</say-as></speak>");
        }

        [Theory(DisplayName = "Should Be Able To Interpret Word As Date In Specified Format")]
        [InlineData(2018, 03, 15, DateFormat.Day, "15")]
        [InlineData(2018, 03, 15, DateFormat.DayMonth, "1503")]
        [InlineData(2018, 03, 15, DateFormat.DayMonthYear, "15032018")]
        [InlineData(2018, 03, 15, DateFormat.Month, "03")]
        [InlineData(2018, 03, 15, DateFormat.MonthDay, "0315")]
        [InlineData(2018, 03, 15, DateFormat.MonthDayYear, "03152018")]
        [InlineData(2018, 03, 15, DateFormat.MonthYear, "032018")]
        [InlineData(2018, 03, 15, DateFormat.Year, "2018")]
        [InlineData(2018, 03, 15, DateFormat.YearMonth, "201803")]
        [InlineData(2018, 03, 15, DateFormat.YearMonthDay, "20180315")]
        public void ShouldBeAbleToInterpretWordAsDateInSpecifiedFormat(int year, int month, int day, DateFormat format, string expected)
        {
            string speech = new Speech()
                .Say("What date is today?")
                .Say("Well, as far as I know, today's date is")
                .Say(new DateTime(year, month, day))
                    .As(format)
                .Build();

            IReadOnlyDictionary<DateFormat, string> dateFormatMap =
            new Dictionary<DateFormat, string>()
            {
                { DateFormat.Day, "d" },
                { DateFormat.DayMonth, "dm" },
                { DateFormat.DayMonthYear, "dmy" },
                { DateFormat.Month, "m" },
                { DateFormat.MonthDay, "md" },
                { DateFormat.MonthDayYear, "mdy" },
                { DateFormat.MonthYear, "my" },
                { DateFormat.Year, "y" },
                { DateFormat.YearMonth, "ym" },
                { DateFormat.YearMonthDay, "ymd" }
            };

            speech.Should().Be($"<speak>What date is today? Well, as far as I know, today's date is <say-as interpret-as=\"date\" format=\"{dateFormatMap[format]}\">{expected}</say-as></speak>");
        }
    }
}
