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
            Visualization.Initialize();
            Visualization.DrawTrack(Data.CurrentRace.Track);
            Data.CurrentRace.Start();
            for (; ; )
            {
                Thread.Sleep(100);
            }
        }
    }
}
