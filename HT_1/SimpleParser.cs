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
       public string Phrase {
            get {
                return phrase;
            }
            set {
                phrase = value;
                curIndex = 0;
                result = 0;
            }
        }
        public int CurIndex { get { return curIndex; } }    
        public int Result { get {return result; } }
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
        /// Вычисление рузльтата
        /// </summary>
        public void Calculate() {                  
            if (!string.IsNullOrEmpty(Phrase)) {
                result = SubOrAdd();
            } else {
                NotParseException ex = new NotParseException(@"Пустая строка!", @"..." ,0);
                throw ex;
            }        
        }
        /// <summary>
        /// Вычисление рузльтата
        /// </summary>
        public void Calculate(string inPhrase) {
            Phrase = inPhrase;
            Calculate();
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
                    NotParseException ex = new NotParseException(@"Цифра или оператор не определен!",phrase,curIndex);
                    throw ex;
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
                    try {
                        num = Fact(num);
                    }
                    catch (OverflowException) {
                        NotParseException ex = new NotParseException(@"Большое значение факториала!", phrase, curIndex);
                        throw ex;
                    }
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
            if (num == 2) {
                return 2;
            }
            return checked (num * Fact(num - 1));
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
            int tempIndex = curIndex; 
            string buff = "0";
            for (; curIndex < phrase.Length && char.IsDigit(phrase[curIndex]); curIndex++) {
                buff += phrase[curIndex];
            }
            int iBuff = 0;
            if (int.TryParse(buff, out iBuff)) {
                return iBuff;//01
            } 
                NotParseException ex = new NotParseException(@"Невозможно преобразовать в Integer!", phrase, tempIndex);
                throw ex;        
        }
    }
}