using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

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
            string[] phrases= new string[] { @"-1+2=3", @"2+2*2=6", @"1-6/2m=-2" };
            string pattern = @"^(\-*\d+)([\+\-\*\/])(\d+)([\+\-\*\/])*(\d+)*=(\-*\d+)$"; //  @"^(\-*\d+)" - с начала строки, (\-*\d+)$ - в конце строки 
            foreach (string phrase in phrases) {
                bool notMatch = true;
                foreach (Match match in Regex.Matches(phrase, pattern, RegexOptions.IgnoreCase)) {
                    notMatch = false;
                    Console.WriteLine("Выражение " + phrase + " возможно вычислить");
                   // Console.WriteLine(match.Value +" " + match.Groups[0].Value +" " + match.Groups[2].Value + " "+ match.Index);
                }
                if (notMatch) { // Если нет совпадений
                    Console.WriteLine("Выражение "+ phrase + " не возможно вычислить");
                }
            }
            string phrase1 =  @"1+2+3+4+5+6";
            Console.WriteLine(SimpleParse(phrase1));

            Console.ReadKey();
            // Посимвольный ввод 
            /*  
              char inChar;
              string inStr="";
              while ((inChar = Convert.ToChar(Console.Read())) != '=') {  // Ввод строки
                  inStr += inChar;
              }
            */
        }
        private static int SimpleParse(string inPhrase) {
            Match endNumber = Regex.Match(inPhrase, @"^(\d+)$", RegexOptions.IgnoreCase);  //Если это последнее число в строке
            if (endNumber.Success) { // Удачно 
                return Convert.ToInt32(endNumber.Value);
            } 
            Match startNumber = Regex.Match(inPhrase, @"\d+", RegexOptions.IgnoreCase);  //Берем первое число в строке
            Match startOperator = Regex.Match(inPhrase, @"[\+\-\*\/]", RegexOptions.IgnoreCase);  //Берем первый оператор в строке
            switch (startOperator.Value) {
                case "+":
                    return Convert.ToInt32(startNumber.Value) + SimpleParse(inPhrase.Substring(startNumber.Value.Length+ startOperator.Value.Length));
                case "-":
                    return Convert.ToInt32(startNumber.Value) - SimpleParse(inPhrase.Substring(startNumber.Value.Length + startOperator.Value.Length));
                default:
                    return Convert.ToInt32(startNumber.Value) - SimpleParse(inPhrase.Substring(startNumber.Value.Length + startOperator.Value.Length));

                    /*case "*":
                        return Convert.ToInt32(startNumber.Value) * SimpleParse(inPhrase.Substring(startNumber.Value.Length + startOperator.Value.Length));
                    case "/":
                        return Convert.ToInt32(startNumber.Value) / SimpleParse(inPhrase.Substring(startNumber.Value.Length + startOperator.Value.Length));*/
            }
          //  return 0;
        }
        private static int MultiParse(string inNumber1, string inOperator, string inSubPhrase) {
            Match startNumber = Regex.Match(inSubPhrase, @"\d+", RegexOptions.IgnoreCase);  //Берем первое число в строке
            switch (inOperator) {
                case "*":
                    return Convert.ToInt32(inNumber1) * Convert.ToInt32(startNumber.Value);
                default: // case "/":
                    return Convert.ToInt32(inNumber1) / Convert.ToInt32(startNumber.Value); 
            }
        }

    }
}
