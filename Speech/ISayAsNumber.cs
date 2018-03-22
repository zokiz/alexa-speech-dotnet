namespace Alexa.Speech
{
    public interface ISayAsNumber
    {
        /// <summary>
        /// Interpret the value as a cardinal number.
        /// </summary>
        /// <returns></returns>
        ISpeech AsCardinal();

        /// <summary>
        /// Interpret the value as an ordinal number.
        /// </summary>
        /// <returns></returns>
        ISpeech AsOrdinal();

        /// <summary>
        /// Spell each digit separately.
        /// </summary>
        /// <returns></returns>
        ISpeech AsDigits();
    }
}
