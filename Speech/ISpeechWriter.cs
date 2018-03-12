using System.Xml;

namespace Alexa.Speech
{
    public interface ISpeechWriter
    {
        void Write(XmlWriter writer);
    }
}
