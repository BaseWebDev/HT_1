using Newtonsoft.Json;
using System;

namespace HT_1
{
    [Serializable]
    class PhareResTime
    {
        public bool Parse { get; set; }
        public string Phrase { get; set; }
        public int? Result { get; set; }
        public DateTime DateTimeOperation { get; set; }
        public TimeSpan TimeOperation { get; set; }
        /// <summary>
        /// В случае невозможности парсинга ошибка, считаем тоже результатом
        /// </summary>
        [JsonIgnore]
        public NotParseException ParseException { get; set; }
        public PhareResTime() : this(false, "", null, DateTime.MinValue, TimeSpan.MinValue, null) {
        }
        /// <param name="phare"></param>
        public PhareResTime(string phare) : this(false,phare, null, DateTime.MinValue, TimeSpan.MinValue, null) {
        }
        public PhareResTime(bool parse, string phrase, 
           int? result, DateTime dateTimeOperation, TimeSpan time, NotParseException parseException) {
            Parse = parse;
            Phrase = phrase;
            Result = result;
            DateTimeOperation = dateTimeOperation;
            TimeOperation = time;
            ParseException = parseException;
        }
    }
}