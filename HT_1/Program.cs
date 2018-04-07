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
            Console.WriteLine(phrase + "=" +Parse(phrase));
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
        /// Решение с урока
        /// </summary>
        /// <param name="s">Парсируемая строка</param>
        /// <returns></returns>
        static int Parse(string s) {
            int index = 0;
            int num = Num(s, ref index);
            while (index < s.Length) {
                if (s[index] == '+') {
                    index++;
                    int indNew = index;
                    int b = Num(s, ref index);
                    if ((index < s.Length)&&(s[index] == '*' || (s[index] == '/')) ) {
                        index = indNew;
                        num += MulDiv(s, ref index);
                    } else {
                        num += b;
                    }
                } else if (s[index] == '-') {
                    index++;
                    int indNew = index;
                    int b = Num(s, ref index);
                    if ((index < s.Length) && (s[index] == '*' || (s[index] == '/'))) {
                        index = indNew;
                        num -= MulDiv(s, ref index);
                    } else {
                        num -= b;
                    }
                } else if (s[index] == '*') {
                    index++;
                    int b = Num(s, ref index);
                    num *= b;
                } else if (s[index] == '/') {
                    index++;
                    int b = Num(s, ref index);
                    num /= b;
                } else {
                    Console.WriteLine("Error");
                    return 0;
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
        /// <summary>
        /// Для умножения и деления
        /// </summary>
        /// <returns></returns>
        static int MulDiv(string s, ref int i) {
             int inum = Num(s, ref i);

            while ( i < s.Length && (s[i]=='*'|| s[i] == '/')) {
                if (s[i] == '*') {
                    i++;
                    int ib = Num(s, ref i);
                    inum *= ib;
                } else if (s[i] == '/') {
                    i++;
                    int ib = Num(s, ref i);
                    inum /= ib;
                }
            }
            return inum;
        }

    }

}

