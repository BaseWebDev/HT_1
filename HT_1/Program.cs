using System;
using System.IO;
using System.Collections.Generic;
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
        static void Main(string[] args) {
            // Выражения для тестирования
            string[] phrases = new string[] { @"1+2*2", @"2+2*2", @"1-6/2", @"9!-6*2+2-3", @"15!+2*3", @"2m*3!-3", @"3!*2+2-3", @"10!-3" };         

            Console.WriteLine("\t Парсер с поддержкой истории");

            SimpleParserTime parserTime = LoadJson();
            if (parserTime.Count > 0) { 
                Console.WriteLine("Загружено:");
                foreach (var parserTimeTest in parserTime) {
                    if (parserTimeTest.ParseException == null) {
                        Console.WriteLine(parserTimeTest.Phrase + "=" + parserTimeTest.Result + ", вычислено за " + parserTimeTest.TimeOperation.TotalMilliseconds + " милисекунд");
                    } else {
                        ShowError(parserTimeTest.ParseException);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("Введите выражение для парсинга или нажмите Enter:");
            string stringForHeir = Console.ReadLine();
           
            
            parserTime.OnCompleted += SaveJson;
            if (!string.IsNullOrEmpty(stringForHeir)) {  // Что-то ввели
                parserTime.Add(stringForHeir);
            } else {
                foreach (string phrase in phrases) {
                    parserTime.Add(phrase);
                }
            }     
            parserTime.Calculate();
            foreach (var parserTimeTest in parserTime) {
                if (parserTimeTest.ParseException == null) {
                    Console.WriteLine(parserTimeTest.Phrase + "=" + parserTimeTest.Result + ", вычислено за " + parserTimeTest.TimeOperation.TotalMilliseconds + " милисекунд");
                }  else {
                    ShowError(parserTimeTest.ParseException);
                }
            }
            Console.WriteLine();
            Console.WriteLine("Проанализировано: " + parserTime.Count + " выражений за " + parserTime.SumTime().TotalMilliseconds + " милисекунд");
        }
        public static void ShowError(NotParseException ex) {
            ConsoleColor curBack = Console.BackgroundColor;
            Console.Write(ex.Message.Substring(0, ex.CurIndex));
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write(ex.Message.Substring(ex.CurIndex, ex.EndIndex - ex.CurIndex));
            Console.BackgroundColor = curBack;
            Console.WriteLine(ex.Message.Substring(ex.EndIndex,ex.Message.Length- ex.EndIndex));
        }
        static void SaveJson(object sender, ParserEventArgs<PhareResTime> eventArgs) {
            var json = JsonConvert.SerializeObject((SimpleParserTime)sender, Formatting.Indented);
            File.WriteAllText(fileName, json);
        }
        static SimpleParserTime LoadJson() {
            if (File.Exists(fileName)){
                var json =  File.ReadAllText(fileName);
                var parserLine = JsonConvert.DeserializeObject<List<PhareResTime>>(json);
                return new SimpleParserTime(parserLine);
            } else {
                return new SimpleParserTime(); // Используем конструктор по умолчанию
            }
        }



    }

}

