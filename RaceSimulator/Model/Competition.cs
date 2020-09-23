using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Competition
    {
        public List<IParticipant> Participants { get; set; }
        public Queue<Track> tracks { get; set; }

        public Competition()
        {

        }

        public Competition(List<IParticipant> participants, Queue<Track> tracks)
        {
            Participants = participants;
            this.tracks = tracks;
        }

        public Track NextTrack()
        {
            return null;
        }
    }
}
