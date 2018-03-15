namespace Alexa.Speech
{
    public interface ISay : ISpeech
    {
        /// <summary>
        /// Emphasize the tagged words or phrases. Emphasis changes rate and volume of the speech. More emphasis is spoken louder and slower. Less emphasis is quieter and faster.
        /// </summary>
        /// <returns></returns>
        ISpeech Emphasise();

        /// <summary>
        /// Emphasize the tagged words or phrases. Emphasis changes rate and volume of the speech. More emphasis is spoken louder and slower. Less emphasis is quieter and faster.
        /// </summary>
        /// <returns></returns>
        ISpeech Emphasise(EmphasiseLevel level);

        /// <summary>
        /// Represents a paragraph. This tag provides extra-strong breaks before and after the tag. This is equivalent to specifying a pause with <break strength="x-strong"/>.
        /// </summary>
        /// <returns></returns>
        ISpeech AsParagraph();

        /// <summary>
        /// Provides a phonemic/phonetic pronunciation for the contained text. For example, people may pronounce words like "pecan" differently.
        /// </summary>
        /// <returns></returns>
        ISpeech AsPhoneme(PhoneticAlphabet alphabet, string phoneticPronunciation);

        /// <summary>
        /// Represents a sentence. This tag provides strong breaks before and after the tag. It is equivalent to ending a sentence with a period(.)
        /// or specifying a pause with <see cref="PauseStrength.Strong"/>.
        /// </summary>
        /// <returns></returns>
        ISpeech AsSentence();

        /// <summary>
        /// Applies Amazon-specific effects to the speech.
        /// </summary>
        /// <param name="effect">The name of the effect to apply to the speech.</param>
        /// <returns></returns>
        ISpeech WithEffect(AmazonEffect effect);

        /// <summary>
        /// Modifies the rate of the tagged speech.
        /// </summary>
        /// <param name="rate">Set the rate to a predefined value.</param>
        /// <returns></returns>
        ISpeech WithRate(SpeechRate rate);

        /// <summary>
        /// Modifies the rate of the tagged speech.
        /// <para>100% indicates no change from the normal rate.</para>
        /// <para>Percentages greater than 100% increase the rate.</para>
        /// <para>Percentages below 100% decrease the rate.</para>
        /// <para>The minimum value you can provide is 20%.</para>
        /// </summary>
        /// <param name="rate">Specify a percentage to increase or decrease the speed of the speech.</param>
        /// <returns></returns>
        ISpeech WithRate(double percentage);

        /// <summary>
        /// Modifies the pitch of the tagged speech.
        /// </summary>
        /// <param name="pitch">Set the tone (pitch) of the speech to a predefined value.</param>
        /// <returns></returns>
        ISpeech WithPitch(SpeechPitch pitch);

        /// <summary>
        /// Modifies the pitch of the tagged speech.
        /// <para>+n%: Increase the pitch by the specified percentage. For example: +10%, +5%. The maximum value allowed is +50%. A value higher than +50% is rendered as +50%.</para>
        /// <para>-n%: Decrease the pitch by the specified percentage.For example: -10%, -20%. The smallest value allowed is -33.3%. A value lower than -33.3% is rendered as -33.3%.</para>
        /// </summary>
        /// <param name="percentage">Specify a percentage to raise or lower the tone (pitch) of the speech.</param>
        /// <returns></returns>
        ISpeech WithPitch(double percentage);

        /// <summary>
        /// Modifies the volume of the tagged speech.
        /// </summary>
        /// <param name="volume">Set volume to a predefined value for current voice.</param>
        /// <returns></returns>
        ISpeech WithVolume(SpeechVolume volume);

        /// <summary>
        /// Modifies the volume of the tagged speech.
        /// <para>+ndB: Increase volume relative to the current volume level. For example, +0dB means no change of volume. +6dB is approximately twice the current amplitude. The maximum positive value is about +4.08dB.</para>
        /// <para>-ndB: Decrease the volume relative to the current volume level.For example, -6dB means approximately half the current amplitude.</para>
        /// </summary>
        /// <param name="decibels">Increase or decrease volume relative to the current volume level.</param>
        /// <returns></returns>
        ISpeech WithVolume(double decibels);
    }
}
