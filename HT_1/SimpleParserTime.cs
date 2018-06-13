using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace HT_1 {

    [Serializable]
    /// <summary>
    /// Парсинг нескольких выражений
    /// </summary>
    class SimpleParserTime : SimpleParser,IEnumerable<PhareResTime> {
        public event EventHandler<ParserEventArgs<PhareResTime>> OnCompleted;
        private List<PhareResTime> phrases;
        public int Count { get => phrases.Count; }
      
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
        public SimpleParserTime(List<PhareResTime> inPhrase) {
            phrases = inPhrase;
         }
        /// <summary>
        /// Перекрываем базовый метод
        /// </summary>
        public virtual new void Calculate() {
            foreach (var phrase in phrases.Where(x=>!x.Parse)) {
                DateTime timeStart = DateTime.Now;
                DateTime timeEnd;
                try {
                    Calculate(phrase.Phrase);
                    phrase.Parse = true;
                    timeEnd = DateTime.Now;
                    phrase.Result = Result;
                }
                catch (NotParseException ex) {
                    phrase.Parse = true;
                    phrase.ParseException = ex;
                    timeEnd = DateTime.Now;
                    phrase.Result = null;
                }
                catch  {
                    phrase.Parse = true;
                    phrase.ParseException= new NotParseException("Неизвестная ошибка");
                    timeEnd = DateTime.Now;
                    phrase.Result = null;
                    return;
                }
                phrase.DateTimeOperation = timeStart;
                phrase.TimeOperation = timeEnd - timeStart;

                if (OnCompleted != null) {
                    var eventArgs = new ParserEventArgs<PhareResTime>(phrase);
                    OnCompleted(this, eventArgs);
                    if (eventArgs.Cancel) {
                        return;
                    }
                }
            }
        }
        /// <summary>
        /// Время выполнения парсинга
        /// </summary>
        public TimeSpan SumTime() {
            TimeSpan temp;
            foreach (var phrase in phrases) {
                temp += phrase.TimeOperation;
            }
            return temp;
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