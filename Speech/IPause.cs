using System;

namespace Alexa.Speech
{
    public interface IPause : ISpeech
    {
        IPause For(TimeSpan duration);

        IPause WithStrength(PauseStrength strength);
    }
}
