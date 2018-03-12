namespace Alexa.Speech
{
    public interface ISpeech
    {
        ISay Say(string value);

        IPause Pause();

        string Build();
    }
}
