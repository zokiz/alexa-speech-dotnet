using System;
using System.Collections.Generic;
using System.Xml;

namespace Alexa.Speech.Writers
{
    public class SayAsDateWriter : BaseSpeach, ISayAsDate, ISpeechWriter
    {
        private SayAsWriter _writer;
        private readonly DateTime _date;
        private DateFormat _format;
        private static readonly IReadOnlyDictionary<DateFormat, string> DateFormatAttributeValueMap =
            new Dictionary<DateFormat, string>()
            {
                {DateFormat.NotSet, null},
                {DateFormat.MonthDayYear, "mdy"},
                {DateFormat.DayMonthYear, "dmy"},
                {DateFormat.YearMonthDay, "ymd"},
                {DateFormat.MonthDay, "md"},
                {DateFormat.DayMonth, "dm"},
                {DateFormat.YearMonth, "ym"},
                {DateFormat.MonthYear, "my"},
                {DateFormat.Day, "d"},
                {DateFormat.Month, "m"},
                {DateFormat.Year, "y"}
            };
        private static readonly IReadOnlyDictionary<DateFormat, string> DateFormatToDateTimeFormatString =
            new Dictionary<DateFormat, string>()
            {
                {DateFormat.NotSet, "yyyyMMdd"},
                {DateFormat.MonthDayYear, "MMddyyyy"},
                {DateFormat.DayMonthYear, "ddMMyyyy"},
                {DateFormat.YearMonthDay, "yyyyMMdd"},
                {DateFormat.MonthDay, "MMdd"},
                {DateFormat.DayMonth, "ddMM"},
                {DateFormat.YearMonth, "yyyyMM"},
                {DateFormat.MonthYear, "MMyyyy"},
                {DateFormat.Day, "dd"},
                {DateFormat.Month, "MM"},
                {DateFormat.Year, "yyyy"}
            };

        public SayAsDateWriter(ISpeech speech, DateTime date)
            : base(speech)
        {
            _date = date;
        }

        public ISpeech As(DateFormat format)
        {
            _format = format;
            return this;
        }

        public void Write(XmlWriter writer)
        {
            string format = DateFormatAttributeValueMap[_format];
            string date = _date.ToString(DateFormatToDateTimeFormatString[_format]);

            _writer = new SayAsWriter("date", format, date);
            _writer.Write(writer);
        }
    }
}
