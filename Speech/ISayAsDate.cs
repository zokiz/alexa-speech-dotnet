namespace Alexa.Speech
{
    public interface ISayAsDate : ISpeech
    {
        /// <summary>
        /// Interpret the value as a date and specify the format.
        /// </summary>
        /// <param name="format">The date format that will be used during interpretation.</param>
        /// <returns></returns>
        ISpeech As(DateFormat format);
    }
}
