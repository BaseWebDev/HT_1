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
            Console.WriteLine("Введите выражение для парсинга:");
            string phrase = Console.ReadLine();
            if (phrase.Length > 0) {
                var t1 = DateTime.Now;
                Console.WriteLine(phrase + "=" + SimpleParse(phrase));
                var t2 = DateTime.Now;
                Console.WriteLine(phrase + "=" + Parse(phrase));
                var t3 = DateTime.Now;
                TimeSpan Reg = t2 - t1;
                TimeSpan Reg2 = t3 - t2;
                Console.WriteLine("С регулярками: " + (Reg.TotalMilliseconds) + " сек");
                Console.WriteLine("Без регулярок: " + (Reg2.TotalMilliseconds) + " сек");
            } else {  // Если ничего не ввели, то используем тестовые примеры
                string[] phrases = new string[] { @"1+2*2", @"2+2*2", @"1-6/2", @"1-6*2+2-3", @"3!*2+2-3", @"3!+2*3", @"2*3!-3" };
                foreach (string key in phrases) {
                    Console.WriteLine(key + "=" + Parse(key));
                }
            }
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
            string selectionAll = selection.Groups[0].Value;
            string term = selection.Groups[1].Value;
            string operatorSumDiff = selection.Groups[2].Value;
            switch (operatorSumDiff) {
                case "+":
                    return Convert.ToInt32(term) + SimpleParse(inPhrase.Substring(selectionAll.Length));
                case "-":
                    return Convert.ToInt32(term) - SimpleParse(inPhrase.Substring(selectionAll.Length));
                case "*":
                    return SimpleParse(MultiOrDivision(inPhrase));
                case "/":
                    return SimpleParse(MultiOrDivision(inPhrase));
            }
            Console.WriteLine("Error");
            return 0;   // Если нет ошибок, то этот код недостижим
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
            string selectionAll = selection.Groups[0].Value;
            string termFirst = selection.Groups[1].Value;
            string operatorMultDiv = selection.Groups[2].Value;
            string termSecond = selection.Groups[1].Value;
            switch (operatorMultDiv) {
                case "*":
                    return (Convert.ToInt32(termFirst) * Convert.ToInt32(termSecond)).ToString() + inSubPhrase.Substring(selectionAll.Length);
                case "/":
                    return (Convert.ToInt32(termFirst) / Convert.ToInt32(termSecond)).ToString() + inSubPhrase.Substring(selectionAll.Length);
            }
            Console.WriteLine("Error");
            return ""; // Если нет ошибок, то этот код недостижим
        }
        /// <summary>
        /// Синтактический разбор выражения
        /// </summary>
        /// <param name="s">Парсируемая строка</param>
        /// <returns></returns>
        static int Parse(string s) {
            int index = 0;
            int num = MultOrDiv(s, ref index);
            while (index < s.Length) {
                if (s[index] == '+') {
                    index++;
                    int b = MultOrDiv(s, ref index);
                    num += b;          
                } else if (s[index] == '-') {
                    index++;
                    int b = MultOrDiv(s, ref index);
                    num -= b;
                } else {
                    Console.WriteLine("Error");
                    return 0;
                }
            }
            return num;
        }
        /// <summary>
        /// Синтактический разбор выражения
        /// </summary>
        /// <param name="s">Парсируемая строка</param>
        /// <returns></returns>
        static int ParseFact(string s, ref int index) {
            int num = Num(s, ref index);
            while (index < s.Length) {
                if (s[index] == '!') {
                    index++;
                    num = Fact(num); ;
                }  else {
                   return num;
                }
            }
            return num;
        }
        /// <summary>
        /// Факториал
        /// </summary>
        /// <param name="num">кол-во итераций</param>
        /// <returns></returns>
        static int Fact(int num) {
            if (num == 1) {
                return 1;
            }
            return num*Fact(num - 1);
           
        }
        /// <summary>
        /// Произведение и деление
        /// </summary>
        /// <param name="s">Парсируемая строка</param>
        /// <param name="index">Индекс смещения в строке</param>
        /// <returns></returns>
        static int MultOrDiv(string s, ref int index) {
            int num = ParseFact(s, ref index);
            while (index < s.Length) {
                if (s[index] == '*') {
                    index++;
                    int b = ParseFact(s, ref index);
                    num *= b;
                } else if (s[index] == '/') {
                    index++;
                    int b = ParseFact(s, ref index);
                    num /= b;
                } else {  // Если + или -, то
                    return num;
                }
            }

            return num;
        }
        /// <summary>
        /// Разбор числа
        /// </summary>
        /// <param name="s"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        static int Num(string s, ref int i) {
            string buff = "0";
            for (; i < s.Length && char.IsDigit(s[i]); i++) {
                buff += s[i];
            }
            return int.Parse(buff);//01
        }
    }

}

