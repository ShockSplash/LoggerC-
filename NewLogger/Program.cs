using System;

namespace NewLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger lg = new Logger();
            lg.Debug("Dim22a");
            lg.Error("New Error Dim22a", new IndexOutOfRangeException());
        }
    }
}
