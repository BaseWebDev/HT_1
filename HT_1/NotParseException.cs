using System;

namespace HT_1
{
    [Serializable]
    public class NotParseException : ApplicationException {
        public string Phrase {get; private set; }
        public int StartIndex { get; private set;}   
        public int CurIndex { get; private set; }
        public NotParseException() { }
        public NotParseException(string message) : base(message) { }
        public NotParseException(string message, Exception inner) : base(message, inner) { }
        public NotParseException(string message, string phrase, int curIndex):this (message) {
            Phrase = phrase;
            CurIndex = curIndex;
        }
        protected NotParseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}