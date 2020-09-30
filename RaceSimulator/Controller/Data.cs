using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Controller
{
    public static class Data
    {
        public static Competition Competition { get; set; }
        public static Race CurrentRace { get; set; }
        public static void Initialize()
        {
            Competition = new Competition();
            AddParticipants();
            AddTrack();
        }

        public static void AddParticipants()
        {
            Competition.Participants.Add(new Driver("Jeremy Clarkson"));
            Competition.Participants.Add(new Driver("James May"));
            Competition.Participants.Add(new Driver("Richard Hammond"));
            Competition.Participants.Add(new Driver("The Stig"));
        }
        public static void AddTrack()
        {
            Competition.Tracks.Enqueue(new Track("Top Gear testtrack"));
            Competition.Tracks.Enqueue(new Track("Silverstone"));
            Competition.Tracks.Enqueue(new Track("Nordschleife"));

        }

        public static void NextRace()
        {
            if(Competition.NextTrack()!=null)
            {
                CurrentRace = new Race(Competition.NextTrack(), Competition.Participants);
            }
        }
    }
}
