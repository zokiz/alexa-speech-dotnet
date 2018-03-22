using System;

namespace Alexa.Speech
{
    public interface ISpeech
    {
        ISay Say(string value);

        ISayAsNumber Say(int number);

        ISayAsDate Say(DateTime date);

        IPause Pause();

        string Build();
    }
}
