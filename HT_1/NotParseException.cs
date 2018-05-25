using System;

namespace HT_1
{
    [Serializable]
    public class NotParseException : Exception {
        public override string Message { get;}
        public string PhraseEx {get; private set; }
        public int CurIndex { get; private set; }
        public int EndIndex { get; private set; }
        public NotParseException(string message) {
            Message = message;
        }
       public NotParseException(string message, string phrase, int curIndex) {
            Message = "В выражении: " + phrase + " Ошибка: " + message;
            PhraseEx = phrase;
            CurIndex = curIndex + "В выражении: ".Length;
            EndIndex = "В выражении: ".Length + phrase.Length;
       }
    }
}