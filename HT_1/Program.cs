﻿using System;
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
            Console.WriteLine(phrase +"="+ SimpleParse(phrase));
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
