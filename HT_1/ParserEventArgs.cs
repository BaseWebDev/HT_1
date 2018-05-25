using System;
namespace HT_1 {
    internal class ParserEventArgs<T>: EventArgs where T: PhareResTime{
        public T Value { get; private set; }
        public bool Cancel { get; set; }
        public  ParserEventArgs(T value) {
            this.Value = value;
        }
    }
}