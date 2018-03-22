using Alexa.Speech.Writers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Alexa.Speech
{
    public class Speech : ISpeech
    {
        private readonly List<ISpeechWriter> _says = new List<ISpeechWriter>();
        private static readonly List<string> _sentenceStops = new List<string>
        {
            ".", "!", "?"
        };
        
        public ISay Say(string value)
        {
            SayWriter say = new SayWriter(this, value);
            _says.Add(say);
            return say;
        }
        
        public ISayAsNumber Say(int number)
        {
            SayAsNumberWriter say = new SayAsNumberWriter(this, number);
            _says.Add(say);
            return say;
        }

        public ISayAsDate Say(DateTime date)
        {
            SayAsDateWriter say = new SayAsDateWriter(this, date);
            _says.Add(say);
            return say;
        }

        public IPause Pause()
        {
            PauseWriter pause = new PauseWriter(this);
            _says.Add(pause);
            return pause;
        }

        public string Build()
        {
            StringBuilder speech = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                OmitXmlDeclaration = true,
                NamespaceHandling = NamespaceHandling.OmitDuplicates
            };

            using (XmlWriter writer = XmlWriter.Create(speech, settings))
            {
                Write(writer);
                writer.Flush();
                return PolishOutput(speech.ToString());
            }
        }

        public void Write(XmlWriter writer)
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("speak");

            for (var index = 0; index < _says.Count; index++)
            {
                ISpeechWriter say = _says[index];
                say.Write(writer);
                if (index != _says.Count - 1)
                {
                    writer.WriteString(" ");
                }
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
        }

        private static string PolishOutput(string output)
        {
            foreach (string sign in _sentenceStops)
            {
                output = output.Replace($" {sign}", sign);
            }
            output = output.Replace("> <", "><");
            // Also get rid of the namespaces...
            output = output.Replace(" xmlns:amazon=\"amazon\"", "");
            return output;
        }
    }
}
