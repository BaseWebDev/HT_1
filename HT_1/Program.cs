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
            string phrase1 =  @"-1*2+3+4*6*6/1";
            Console.WriteLine(phrase1+"="+SimpleParse(phrase1));
            
        }
        /// <summary>
        /// Рекурсия с определением сложения и вычитания
        /// </summary>
        /// <param name="inPhrase">Входная строка выражения</param>
        /// <returns></returns>
        private static int SimpleParse(string inPhrase) {
            Match endNumber = Regex.Match(inPhrase, @"^(\d+)$", RegexOptions.IgnoreCase);  //Если это последнее число в строке
            if (endNumber.Success) { // Удачно 
                return Convert.ToInt32(endNumber.Value);
            }
            string pattern = @"(\-*\d+)([\+\-\*\/])";
            Match selection = Regex.Match(inPhrase,pattern,RegexOptions.IgnoreCase);
           // Match startNumber = Regex.Match(inPhrase, @"\-*\d+", RegexOptions.IgnoreCase);  //Берем первое число в строке
           // Match startOperator = Regex.Match(inPhrase, @"[\+\-\*\/]", RegexOptions.IgnoreCase);  //Берем первый оператор в строке
            switch (selection.Groups[2].Value) {
                case "+":
                    return Convert.ToInt32(selection.Groups[1].Value) + SimpleParse(inPhrase.Substring(selection.Groups[0].Value.Length));
                case "-":
                    return Convert.ToInt32(selection.Groups[1].Value) - SimpleParse(inPhrase.Substring(selection.Groups[0].Value.Length));
                default:
                    return SimpleParse(MultiOrDivision(inPhrase));
            }
        }
        /// <summary>
        /// Преобразование умножения в сложение
        /// возращаем в строке произведение или частное
        /// </summary>
        /// <param name="inSubPhrase">входная подстрока</param>
        /// <returns></returns>
        private static string MultiOrDivision(string inSubPhrase) {
            string pattern = @"(\-*\d+)([\*\/])(\-*\d+)";
            Match selection = Regex.Match(inSubPhrase, pattern, RegexOptions.IgnoreCase);
            switch (selection.Groups[2].Value) {
                case "*":
                    return (Convert.ToInt32(selection.Groups[1].Value) * Convert.ToInt32(selection.Groups[3].Value)).ToString() + inSubPhrase.Substring(selection.Groups[0].Value.Length);
                default:
                   return (Convert.ToInt32(selection.Groups[1].Value) / Convert.ToInt32(selection.Groups[3].Value)).ToString() + inSubPhrase.Substring(selection.Groups[0].Value.Length);
            }
        }

    }
}
