﻿using System;

namespace HT_1
{ 
    /// <summary>
    /// HT_1. Написать простой парсер математических выражений:
    /// 1+2=3
    /// 2+2*2=6
    /// 1-6/2=-2
    /// </summary>
    class Program
    {
        
        static void Main(string[] args)
        {
            // Выражения для тестирования
            string[] phrases = new string[] { @"1+2*2", @"2+2*2", @"1-6/2", @"1-6*2+2-3", @"3!*2+2-3", @"3!+2*3", @"2*3!-3", @"10!-3" };

            Console.WriteLine("\tИспользуем класс SimpleParser");
            Console.WriteLine("Введите выражение для парсинга или нажмите Enter:");
            string stringForBase = Console.ReadLine();
            if (!string.IsNullOrEmpty(stringForBase)) {  // Что-то ввели              
                try {
                    SimpleParser parser = new SimpleParser(stringForBase);
                    var timeStart = DateTime.Now;
                    parser.Calculate();
                    Console.WriteLine(parser.Phrase+"="+parser.Result+", выражение проанализировано и вычислено за: " + ((DateTime.Now - timeStart).TotalMilliseconds) + " милисекунд");
                }
                catch (NotParseException ex) {
                    ShowError(ex.Message, ex.Phrase, ex.CurIndex);
                }
                catch (Exception ex) {
                    Console.WriteLine(ex);
                }               
            } else {  // Если ничего не ввели, то используем тестовые примеры
                try {
                    SimpleParser parserTest = new SimpleParser();  // Используем конструктор по умолчанию
                    foreach (string phrase in phrases) {
                        var timeStart = DateTime.Now;
                        parserTest.Calculate(phrase);
                        Console.WriteLine(parserTest.Phrase + "=" + parserTest.Result + ", вычислено за " + (DateTime.Now - timeStart).TotalMilliseconds + " милисекунд");
                    }
                }
                catch (NotParseException ex) {
                    ShowError(ex.Message, ex.Phrase, ex.CurIndex);
                }
                catch (Exception ex) {
                    Console.WriteLine(ex);
                }
            }
            Console.WriteLine();
            
            Console.WriteLine("\tИспользуем класс-наследник SimpleParserTime");
            Console.WriteLine("Введите выражение для парсинга или нажмите Enter:");
            string stringForHeir = Console.ReadLine();
            if (!string.IsNullOrEmpty(stringForHeir)) {  // Что-то ввели
                SimpleParserTime parserTime = new SimpleParserTime(stringForHeir);  // Вводим с консоли выражение для парсинга, используем конструктор для ввода выражения
                try {
                    parserTime.Calculate();
                    Console.WriteLine(parserTime.Phrase + "=" + parserTime.Result + ", вычислено за " + parserTime.SumTime.TotalMilliseconds + " милисекунд");
                }
                catch (NotParseException ex) {
                    ShowError(ex.Message, ex.Phrase, ex.CurIndex);
                }
                catch (Exception ex) {
                    Console.WriteLine(ex);
                }
            } else {  // Если ничего не ввели, то используем тестовые примеры
                SimpleParserTime parserTimeTests = new SimpleParserTime();  // Используем конструктор по умолчанию
                foreach (string phrase in phrases) {
                    parserTimeTests.Add(phrase);
                }
                try {
                    parserTimeTests.Calculate();
                }
                catch (NotParseException ex) {
                    ShowError(ex.Message, ex.Phrase, ex.CurIndex);
                }
                catch (Exception ex) {
                    Console.WriteLine(ex);
                }
                foreach (var parserTimeTest in parserTimeTests) {
                    Console.WriteLine(parserTimeTest.Phrase + "=" + parserTimeTest.Result + ", вычислено за " + parserTimeTest.Time.TotalMilliseconds + " милисекунд");
                }
                Console.WriteLine("Проанализировано: "+ parserTimeTests.CountOpOk + " выражений и за " +parserTimeTests.SumTime.TotalMilliseconds +" милисекунд");
            }
        }
        public static void ShowError(string mesError, string phrase, int curIndex) {
            ConsoleColor curBack = Console.BackgroundColor;
            Console.Write("В выражении: " + phrase.Substring(0, curIndex));
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write(phrase.Substring(curIndex, phrase.Length- curIndex));
            Console.BackgroundColor = curBack;
            Console.WriteLine(" Ошибка: " + mesError);
        }      

    }

}

