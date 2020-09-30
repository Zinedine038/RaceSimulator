using System;
using Model;
using Controller;
using System.Threading;

namespace View
{
    class Program
    {
        static void Main(string[] args)
        {
            Data.Initialize();
            Data.NextRace();
            Console.WriteLine(Data.CurrentRace.Track.Name);

            for(; ; )
            {
                Thread.Sleep(100);
            }
        }
    }
}
