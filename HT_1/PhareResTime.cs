using System;

namespace HT_1
{
    class PhareResTime
    {
        public string Phrase { get; private set; }
        public int? Result { get; set; }
        public TimeSpan Time { get; set; }
        /// <summary>
        /// В случае невозможности парсинга ошибка, считаем тоже результатом
        /// </summary>
        public NotParseException ParseException { get; set; }
        /// <param name="phare"></param>
        public PhareResTime(string phare) : this(phare, 0, TimeSpan.MinValue) {
        }
        public PhareResTime(string phrase, int result, TimeSpan time) {
            Phrase = phrase;
            Result = result;
            Time = time;
        }
    }
}