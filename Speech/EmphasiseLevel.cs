namespace Alexa.Speech
{
    public enum EmphasiseLevel
    {
        NotSet = 0,

        /// <summary>
        /// Increase the volume and slow down the speaking rate so the speech is louder and slower.
        /// </summary>
        Strong,

        /// <summary>
        /// Increase the volume and slow down the speaking rate, but not as much as when set to <see cref="Strong"/>. This is used as a default if level is not provided.
        /// </summary>
        Moderate,

        /// <summary>
        /// Decrease the volume and speed up the speaking rate. The speech is softer and faster.
        /// </summary>
        Reduced
    }
}
