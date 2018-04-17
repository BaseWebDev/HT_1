using System;

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
            string[] phrases = new string[] { @"1m+2*2", @"2+2*2", @"1-6/2", @"1-6*2+2-3", @"3!*2+2-3", @"3!+2*3", @"2*3!-3", @"10!-3" };

            Console.WriteLine("\tИспользуем класс SimpleParser");
            Console.WriteLine("Введите выражение для парсинга или нажмите Enter:");
            SimpleParser parser = new SimpleParser(Console.ReadLine());  // Вводим с консоли выражение для парсинга, используем конструктор для ввода выражения
            if (parser.Phrase.Length > 0) {  // Что-то ввели
                var timeStart = DateTime.Now;
                if (parser.Try()) {
                    parser.ShowResult();
                    var timeEnd = DateTime.Now;
                    TimeSpan diffTime = timeEnd - timeStart;
                    Console.WriteLine(", выражение проанализировано и вычислено за: " + (diffTime.TotalMilliseconds) + " милисекунд");
                } else {
                    parser.ShowResult();
                }
            } else {  // Если ничего не ввели, то используем тестовые примеры
                
                SimpleParser parserTest = new SimpleParser();  // Используем конструктор по умолчанию
                foreach (string phrase in phrases) {
                    var timeStart = DateTime.Now;                
                    if (parserTest.Try(phrase)) {
                        parserTest.ShowResult();
                        Console.WriteLine(", вычислено за " + (DateTime.Now - timeStart).TotalMilliseconds + " милисекунд");
                    } else {
                        parserTest.ShowResult();
                    }
                }
            }
            Console.WriteLine();

            Console.WriteLine("\tИспользуем класс-наследник SimpleParserTime");
            Console.WriteLine("Введите выражение для парсинга или нажмите Enter:");
            SimpleParserTime parserTime = new SimpleParserTime(Console.ReadLine());  // Вводим с консоли выражение для парсинга, используем конструктор для ввода выражения
            if (parserTime.Phrase.Length > 0) {  // Что-то ввели
                parserTime.ShowResult();
            } else {  // Если ничего не ввели, то используем тестовые примеры
                SimpleParserTime parserTimeTest = new SimpleParserTime();  // Используем конструктор по умолчанию
                foreach (string phrase in phrases) {
                    parserTimeTest.Add(phrase);
                }
                parserTimeTest.ShowResult();
                Console.WriteLine("Выполнено удачных: "+parserTimeTest.CountOpOk + " и " +parserTimeTest.CountOpEr +" с ошибкой");
            }
        }
        
    }

}

