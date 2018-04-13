using System;

namespace HT_1
{
    class SimpleParser  // internal
    {
        private string phrase; // парсируемое выражение
        private int curIndex;   // текущий индекс
        private int result;  // результат парсинга/вычислений
        private string mesError;  // ошибки парсирования
        public string Phrase {
            get {
                return phrase;
            }
            set {
                phrase = value;
                curIndex = 0;
                result = 0;
                mesError = String.Empty;
            }
        }
        public int CurIndex { get { return curIndex; } }    
        public int Result { get {return result; } }
        public string MesError { get { return mesError; } }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public SimpleParser() {
            phrase = String.Empty;
            curIndex = 0;
            result = 0;
            mesError = String.Empty;
        }
        /// <summary>
        /// Конструктор с входной строкой
        /// </summary>
        /// <param name="inPhrase">Парсируемая строка</param>
        public SimpleParser(string inPhrase) {
            phrase = inPhrase;
            curIndex = 0;
            result = 0;
            mesError = String.Empty;
        }
        /// <summary>
        /// Вычисление рузльтата, true - если нет ошибок
        /// </summary>
        public bool Try() {
            result = Parse(phrase, ref curIndex,ref mesError);
            return mesError == String.Empty;
        }
        /// <summary>
        /// Вычисление рузльтата, true - если нет ошибок
        /// </summary>
        public bool Try(string inPhrase) {
            phrase = inPhrase;
            curIndex = 0;
            result = 0;
            mesError = String.Empty;
            return Try();
        }

        /// <summary>
        /// Синтактический разбор выражения
        /// </summary>
        /// <param name="s">Парсируемая строка</param>
        /// <returns></returns>
        public static int Parse(string s, ref int index,ref string mesError) {
            index = 0;
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
                    mesError = "Оператор или цифра не определена!";
                    // Console.WriteLine("Error");
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
        public static int Parse(string s) {
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
        public static int ParseFact(string s, ref int index) {
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
        public static int Fact(int num) {
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
        public static int MultOrDiv(string s, ref int index) {
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
        public static int Num(string s, ref int i) {
            string buff = "0";
            for (; i < s.Length && char.IsDigit(s[i]); i++) {
                buff += s[i];
            }
            return int.Parse(buff);//01
        }
    }
}