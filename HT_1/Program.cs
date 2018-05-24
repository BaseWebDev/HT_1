using System;
using System.IO;
using Newtonsoft.Json;

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
        const string fileName = "historyParser.json";
        static void Main(string[] args)
        {
            // Выражения для тестирования
            string[] phrases = new string[] { @"1+2*2", @"2+2*2", @"1-6/2", @"9!-6*2+2-3", @"3!*2+2-3", @"15!+2*3", @"2m*3!-3", @"10!-3"};          
            
            Console.WriteLine("\t Парсер с поддержкой истории");
            Console.WriteLine("Введите выражение для парсинга или нажмите Enter:");
            string stringForHeir = Console.ReadLine();
           
            SimpleParserTime parserTimeTests = new SimpleParserTime();  // Используем конструктор по умолчанию
            if (!string.IsNullOrEmpty(stringForHeir)) {  // Что-то ввели
                parserTimeTests.Add(stringForHeir);
            } else {
                foreach (string phrase in phrases) {
                    parserTimeTests.Add(phrase);
                }
            }     
            parserTimeTests.Calculate();
            foreach (var parserTimeTest in parserTimeTests) {
                if (parserTimeTest.ParseException == null) {
                    Console.WriteLine(parserTimeTest.Phrase + "=" + parserTimeTest.Result + ", вычислено за " + parserTimeTest.Time.TotalMilliseconds + " милисекунд");
                }  else {
                    ShowError(parserTimeTest.ParseException);
                }
            }
            Console.WriteLine("Проанализировано: " + parserTimeTests.Count + " выражений за " + parserTimeTests.SumTime.TotalMilliseconds + " милисекунд");
        }
        public static void ShowError(NotParseException ex) {
            ConsoleColor curBack = Console.BackgroundColor;
            Console.Write(ex.Message.Substring(0, ex.CurIndex));
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write(ex.Message.Substring(ex.CurIndex, ex.EndIndex - ex.CurIndex));
            Console.BackgroundColor = curBack;
            Console.WriteLine(ex.Message.Substring(ex.EndIndex,ex.Message.Length- ex.EndIndex));
        }

        static void OutConsole(object sender, ParserEventArgs eventArgs) {
            Console.WriteLine(eventArgs.ToString());
        }
        static void OutJson(object sender, ParserEventArgs eventArgs) {
            var json = JsonConvert.SerializeObject(eventArgs, Formatting.Indented);
            File.AppendAllText(fileName, json);
        }


    }

}

