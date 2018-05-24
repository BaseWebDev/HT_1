using System;
using System.Collections;
using System.Collections.Generic;

namespace HT_1 {
    /// <summary>
    /// Парсинг нескольких выражений
    /// Считаем возникновение ошибки тоже результатом
    /// </summary>
    class SimpleParserTime : SimpleParser,IEnumerable<PhareResTime> {
        private List<PhareResTime> phrases;
        public int Count { get => phrases.Count; }
        /// <summary>
        /// Время выполнения парсинга
        /// </summary>
        public TimeSpan SumTime { get; private set; }
      
        /// <summary>
        /// При вызове конструктора по умолчанию вызываем его из базового класса
        /// </summary>
        public SimpleParserTime(){
            // Поскольку выражения нет, то только List
            phrases = new List<PhareResTime>();
        }
        /// <summary>
        /// При вызове конструктора с параметром, вызывается базовый с параметром
        /// </summary>
        /// <param name="inPhrase">входная строка</param>
        public SimpleParserTime(string inPhrase) : base(inPhrase) {
            phrases = new List<PhareResTime> ();
            Add(inPhrase);
        }
        /// <summary>
        /// Перекрываем базовый метод
        /// </summary>
        public virtual new void Calculate() {
            foreach (var phrase in phrases) {
                DateTime timeStart = DateTime.Now;
                DateTime timeEnd;
                try {
                    Calculate(phrase.Phrase);
                    timeEnd = DateTime.Now;
                    phrase.Result = Result;
                }
                catch (NotParseException ex) {
                    phrase.ParseException = ex;
                    timeEnd = DateTime.Now;
                    phrase.Result = Result;
                }
                catch  {
                    phrase.ParseException= new NotParseException("Неизвестная ошибка");
                    return;
                }                
                phrase.Time = timeEnd - timeStart;
                SumTime += phrase.Time;

            }
        }
        /// <summary>
        /// Добавление выражение в анализ
        /// </summary>
        /// <param name="inPhrase">Входное выражение</param>
        public void Add(string inPhrase) {
             phrases.Add(new PhareResTime (inPhrase));
        }

        public IEnumerator<PhareResTime> GetEnumerator() {
            return ((IEnumerable<PhareResTime>)phrases).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return ((IEnumerable<PhareResTime>)phrases).GetEnumerator();
        }
    }
}