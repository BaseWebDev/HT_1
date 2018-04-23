using System;

namespace HT_1
{
    class SimpleParser  // internal
    {
        /// <summary>
        /// Почему оставил
        /// Рихтер CLR via C# Глава Свойства без параметров стр 269
        /// </summary>
        private string phrase; // парсируемое выражение
        private int curIndex;   // текущий индекс
        private int result;  // результат парсинга/вычислений
        private bool errSt; // есть ли ошибка
        private string mesError;  // ошибки парсирования
        public string Phrase {
            get {
                return phrase;
            }
            set {
                phrase = value;
                curIndex = 0;
                result = 0;
                errSt = false;
                mesError = String.Empty;
            }
        }
        public int CurIndex { get { return curIndex; } }    
        public int Result { get {return result; } }
        public bool ErrSt { get { return errSt; } }
        public string MesError { get { return mesError; } }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public SimpleParser():this(String.Empty) {
        }
        /// <summary>
        /// Конструктор с входной строкой
        /// </summary>
        /// <param name="inPhrase">Парсируемая строка</param>
        public SimpleParser(string inPhrase) {
            Phrase = inPhrase;
        }
        /// <summary>
        /// Вычисление рузльтата, true - если нет ошибок
        /// </summary>
        public bool Try() {
            result = SubOrAdd();
            return !errSt;  
        }
        /// <summary>
        /// Вычисление рузльтата, true - если нет ошибок
        /// </summary>
        public bool Try(string inPhrase) {
            Phrase = inPhrase;
            return Try();
        }
        /// <summary>
        /// Отображение результата или ошибки
        /// </summary>
        public override string ToString() {
           if (!errSt) {
                return phrase + "=" + result;
           } else {
                return "Ошибка: " + mesError + " В выражении: " + phrase + ", в " + curIndex + " символе c 0";
           }            
        }
        /// <summary>
        /// Синтактический разбор выражения
        /// </summary>
        /// <returns></returns>
        private int SubOrAdd() {
            int num = MultOrDiv();
            while (curIndex < phrase.Length) {
                if (phrase[curIndex] == '+') {
                    curIndex++;
                    int b = MultOrDiv();
                    num += b;
                } else if (phrase[curIndex] == '-') {
                    curIndex++;
                    int b = MultOrDiv();
                    num -= b;
                } else {
                    mesError= @"Цифра или оператор не определен!";
                    errSt = true;
                    return 0;
                }
            }
            return num;
        }
        /// <summary>
        /// Синтактический разбор выражения
        /// </summary>
        private int ParseFact() {
            int num = Num();
            while (curIndex < phrase.Length) {
                if (phrase[curIndex] == '!') {
                    curIndex++;
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
        private int Fact(int num) {
            if (num == 1) {
                return 1;
            }
            return num * Fact(num - 1);
        }
        /// <summary>
        /// Произведение и деление
        /// </summary>
        /// <returns></returns>
        private int MultOrDiv() {
            int num = ParseFact();
            while (curIndex < phrase.Length) {
                if (phrase[curIndex] == '*') {
                    curIndex++;
                    int b = ParseFact();
                    num *= b;
                } else if (phrase[curIndex] == '/') {
                    curIndex++;
                    int b = ParseFact();
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
       /// <returns></returns>
        private int Num() {
            string buff = "0";
            for (; curIndex < phrase.Length && char.IsDigit(phrase[curIndex]); curIndex++) {
                buff += phrase[curIndex];
            }
            return int.Parse(buff);//01
        }
    }
}