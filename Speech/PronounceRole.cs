namespace Alexa.Speech
{
    public enum PronounceRole
    {
        /// <summary>
        /// Interpret the word as a verb (present simple).
        /// </summary>
        Verb,

        /// <summary>
        /// Interpret the word as a noun.
        /// </summary>
        Noun,

        /// <summary>
        /// Interpret the word as a past participle.
        /// </summary>
        PastParticiple,

        /// <summary>
        /// Use the non-default sense of the word. For example, the noun "bass" is pronounced differently depending on meaning. The "default" meaning is the lowest part of the musical range. The alternate sense (which is still a noun) is a freshwater fish. Specifying <speak><w role="amazon:SENSE_1">bass</w></speak> renders the non-default pronunciation (freshwater fish).
        /// </summary>
        NonDefaultSense
    }
}
