using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HT_1
{
    class SimpleParser  // internal
    {
        private string phrase; // парсируемое выражение
        private int index;   // текущий индекс
        private int result;  // результат парсинга/вычислений
        /// <summary>
        /// Синтактический разбор выражения
        /// </summary>
        /// <param name="s">Парсируемая строка</param>
        /// <returns></returns>
        static int SimpleParse(string s) {
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
                } else {
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
            return num * Fact(num - 1);

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