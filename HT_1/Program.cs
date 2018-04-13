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
            Console.WriteLine("Введите выражение для парсинга:");
            string phraseIn = Console.ReadLine();
            if (phraseIn.Length > 0) {
                var timeStart = DateTime.Now;
                Console.WriteLine(phraseIn + "=" + SimpleParse(phraseIn));
                var timeEnd = DateTime.Now;
                TimeSpan diffTime = timeEnd - timeStart;
                Console.WriteLine("Выражение проанализировано и вычислено за: " + (diffTime.TotalMilliseconds) + " милисекунд");
            } else {  // Если ничего не ввели, то используем тестовые примеры
                string[] phrases = new string[] { @"1+2*2", @"2+2*2", @"1-6/2", @"1-6*2+2-3", @"3!*2+2-3", @"3!+2*3", @"2*3!-3", @"10!-3" };
                foreach (string phrase in phrases) {
                    var timeStart = DateTime.Now;
                    Console.WriteLine(phrase + "=" + SimpleParse(phrase)+" за время: "+(DateTime.Now-timeStart).TotalMilliseconds + " милисекунд");
                }
            }
        }
        
    }

}

