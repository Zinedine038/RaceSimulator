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
            Data.CurrentRace.RaceFinished += NextRace;
            for (; ; )
            {
                Thread.Sleep(100);
            }
        }

        private static void NextRace(object sender, EventArgs e)
        {

            if (Data.Competition.Tracks.Count > 0)
            {
                Visualization.DrawEndOfRaceScreen();

                //Puur voor mooier maken
                Thread.Sleep(3000);

                Console.Clear();
                Data.CurrentRace.RaceFinished -= NextRace;
                Data.NextRace();
                Visualization.Initialize();
                Visualization.DrawTrack(Data.CurrentRace.Track);
                Data.CurrentRace.RaceFinished += NextRace;
                Data.CurrentRace.Start();
            }
            else
            {
                Visualization.DrawEndOfCompetitionScreen();
            }
        }
    }
}
