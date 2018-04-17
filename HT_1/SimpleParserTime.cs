using System;
using System.Collections.Generic;

namespace HT_1 {
    /// <summary>
    /// Пробуем наследование
    /// </summary>
    class SimpleParserTime : SimpleParser {
        private DateTime timeStart;
        private DateTime timeEnd;
        private int countOpOk;
        private int countOpEr;
        private List<string> phrases;
        /// <summary>
        /// Время выполнения парсинга
        /// </summary>
        public double DiffTime { get { return (timeEnd - timeStart).TotalMilliseconds; } }
        public int CountOpOk { get { return countOpOk; } }
        public int CountOpEr { get { return countOpEr; } }
        /// <summary>
        /// При вызове конструктора по умолчанию вызываем его из базового класса
        /// </summary>
        public SimpleParserTime():this (String.Empty) {}
        /// <summary>
        /// При вызове конструктора с параметром, вызывается базовый с параметром
        /// </summary>
        /// <param name="inPhrase">входная строка</param>
        public SimpleParserTime(string inPhrase) : base(inPhrase) {
            phrases = new List<string> ();
            Add(inPhrase);
        }
        /// <summary>
        /// Отображение результата или ошибки
        /// </summary>
        public override void ShowResult() {
            foreach (string phrase in phrases) {
                timeStart = DateTime.Now;
                if (Try(phrase)) {
                    timeEnd = DateTime.Now;
                    Console.WriteLine(Phrase + "=" + Result + ", вычислено за " + DiffTime.ToString() + " милисекунд");
                    ++countOpOk;
                } else {
                    ConsoleColor curBack = Console.BackgroundColor;
                    Console.Write("В выражении: " + Phrase.Substring(0, CurIndex));
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write(Phrase.Substring(CurIndex, 1));
                    Console.BackgroundColor = curBack;
                    Console.Write(Phrase.Substring(CurIndex+1));
                    Console.WriteLine(" Ошибка: " + MesError);
                    ++countOpEr;
                }
            }
        }
        /// <summary>
        /// Добавление выражение в анализ
        /// </summary>
        /// <param name="inPhrase">Входное выражение</param>
        public void Add(string inPhrase) {
             if (inPhrase!=String.Empty)phrases.Add(inPhrase);
        }     
    }
}