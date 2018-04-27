using System;

namespace HT_1
{
    [Serializable]
    public class NotParseException : ApplicationException {
        public string Phrase {get; private set; }
        public int CurIndex { get; private set; }
        public int EndIndex { get; private set; }

        public NotParseException(string message) : base(message) { }
       public NotParseException(string message, string phrase, int curIndex):base("В выражении: " + phrase + " Ошибка: " + message) {
            Phrase = phrase;
            CurIndex = curIndex + "В выражении: ".Length;
            EndIndex = "В выражении: ".Length + phrase.Length;
       }     
    }
}