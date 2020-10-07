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
            Visualization.Initialize();
            for(; ; )
            {
                Thread.Sleep(100);
            }
        }
    }
}
