using System;
using System.Collections;
using System.Collections.Generic;

namespace HT_1 {
    /// <summary>
    /// Пробуем наследование
    /// </summary>
    class SimpleParserTime : SimpleParser,IEnumerable<PhareResTime> {
        private int countOpOk;
        private List<PhareResTime> phrases;
        private TimeSpan sumTime;
        /// <summary>
        /// Время выполнения парсинга
        /// </summary>
        public int CountOpOk { get => countOpOk; private set => countOpOk = value; }

        public TimeSpan SumTime { get => sumTime; private set => sumTime = value; }
      
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
                    Calculate(phrase.Phrase);
                    DateTime timeEnd = DateTime.Now;
                    phrase.Result = Result;
                    phrase.Time = timeEnd - timeStart;
                    ++countOpOk;
                    sumTime += phrase.Time;                  
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