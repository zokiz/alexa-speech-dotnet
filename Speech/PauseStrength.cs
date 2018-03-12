namespace Alexa.Speech
{
    public enum PauseStrength
    {
        NotSet = 0,

        /// <summary>
        /// No pause should be outputted. This can be used to remove a pause that would normally occur (such as after a period).
        /// </summary>
        None,

        /// <summary>
        /// No pause should be outputted (same as <see cref="None"/>).
        /// </summary>
        ExtraWeak,

        /// <summary>
        /// Treat adjacent words as if separated by a single comma (equivalent to <see cref="Medium"/>).
        /// </summary>
        Weak,

        /// <summary>
        /// Treat adjacent words as if separated by a single comma.
        /// </summary>
        Medium,

        /// <summary>
        /// Make a sentence break (equivalent to using the &lt;s&gt; tag).
        /// </summary>
        Strong,

        /// <summary>
        /// Make a paragraph break (equivalent to using the &lt;p&gt; tag).
        /// </summary>
        ExtraStrong
    }
}
