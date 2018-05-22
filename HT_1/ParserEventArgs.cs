using System;

namespace HT_1 {
    [Serializable]
    class ParserEventArgs:EventArgs
    {
        static int nomer;
        public int Nomer { get; private set; }
        public string NameOperation { get; set; }
        public int Operand1 { get; set; }
        public int Operand2 { get; set; }
        public int Result { get; set; }
        public DateTime DateTimeRequest { get; set; }
        public TimeSpan TimeExecution { get; set; }
        public ParserEventArgs() {
            Nomer = nomer++;
        }
        public override string ToString() {
            return "\t № "+ Nomer.ToString() +" Name " + NameOperation + " Operand1 " + Operand1 + " Operand2 " + Operand2 + " Result " + Result;
        }
    }
}