using System;
using System.Collections.Generic;

namespace HT_1
{
    /// <summary>
    /// Пробуем наследование
    /// </summary>
    class SimpleParserTime:SimpleParser
    {
        private DateTime timeStart;  
        private DateTime timeEnd;
        private List<string> list;
        /// <summary>
        /// Время выполнения парсинга
        /// </summary>
        public double DiffTime { get { return (timeEnd - timeStart).TotalMilliseconds; } }
        /// <summary>
        /// При вызове конструктора по умолчанию вызываем его из базового класса
        /// </summary>
        public SimpleParserTime():this (String.Empty) {}
        /// <summary>
        /// При вызове конструктора с параметром, вызывается базовый с параметром
        /// </summary>
        /// <param name="inPhrase">входная строка</param>
        public SimpleParserTime(string inPhrase) : base(inPhrase) {
            list = new List<string> ();
            Add(inPhrase);
        }
        /// <summary>
        /// Отображение результата или ошибки
        /// </summary>
        public void ShowResult() {
            foreach (string phrase in list) {
                timeStart = DateTime.Now;
                if (Try(phrase)) {
                    timeEnd = DateTime.Now;
                    Console.WriteLine(Phrase + "=" + Result + ", вычислено за " + DiffTime.ToString() + " милисекунд");
                } else {
                    Console.WriteLine("Ошибка: " + MesError + " В выражении: " + Phrase + ", в " + CurIndex + " символе c 0");
                }
            }
        }
        /// <summary>
        /// Добавление выражение в анализ
        /// </summary>
        /// <param name="inPhrase">Входное выражение</param>
        public void Add(string inPhrase) {
             if (inPhrase!=String.Empty)list.Add(inPhrase);
        }     
    }
}