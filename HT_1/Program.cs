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
            Console.WriteLine("Введите выражение для парсинга:");
            SimpleParser parser = new SimpleParser(Console.ReadLine());  // Вводим с консоли выражение для парсинга, используем конструктор для ввода выражения
            if (parser.Phrase.Length > 0) {  // Что-то ввели
                var timeStart = DateTime.Now;
                if (parser.TryParse()) {
                    Console.WriteLine(parser.Phrase + "=" + parser.Result);
                    var timeEnd = DateTime.Now;
                    TimeSpan diffTime = timeEnd - timeStart;
                    Console.WriteLine("Выражение проанализировано и вычислено за: " + (diffTime.TotalMilliseconds) + " милисекунд");
                } else {
                    Console.WriteLine("Ошибка: " + parser.MesError + " В выражении: " + parser.Phrase + ", в " + parser.CurIndex + " символе c 0");
                }
            } else {  // Если ничего не ввели, то используем тестовые примеры
                string[] phrases = new string[] { @"1m+2*2", @"2+2*2", @"1-6/2", @"1-6*2+2-3", @"3!*2+2-3", @"3!+2*3", @"2*3!-3", @"10!-3" };
                SimpleParser parserTest = new SimpleParser();  // Используем конструктор по умолчанию
                foreach (string phrase in phrases) {
                    var timeStart = DateTime.Now;                
                    if (parserTest.TryParse(phrase)) {
                        Console.WriteLine(phrase + "=" + parserTest.Result + " за время: " + (DateTime.Now - timeStart).TotalMilliseconds + " милисекунд");
                    } else {
                        Console.WriteLine("Ошибка: " + parserTest.MesError + " В выражении: " + parserTest.Phrase + ", в " + parserTest.CurIndex + " символе c 0");
                    }
                }
            }
        }
        
    }

}

