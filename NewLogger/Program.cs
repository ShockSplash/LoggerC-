using System;
using System.Collections.Generic;

namespace NewLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger lg = new Logger();

            lg.ErrorUnique("nu", new ArgumentNullException());
            lg.ErrorUnique("num", new IndexOutOfRangeException());
            lg.ErrorUnique("nu", new IndexOutOfRangeException());

            lg.Debug("Message for debug");

            lg.ErrorUnique("Hello", new StackOverflowException());


            lg.WarningUnique("Please, update system!");
            lg.WarningUnique("Please, update system!");
        }
    }
}
