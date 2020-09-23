using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Controller
{
    static class Data
    {
        public static Competition Competition { get; set; }
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
            Competition.tracks.Enqueue(new Track("Top Gear testtrack"));
            Competition.tracks.Enqueue(new Track("Silverstone"));
            Competition.tracks.Enqueue(new Track("Nordschleife"));

        }
    }
}
