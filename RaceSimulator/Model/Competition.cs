using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Competition
    {
        public List<IParticipant> Participants { get; set; }
        public Queue<Track> Tracks { get; set; }

        public SavedData<ParticipantPoints> Points { get; set; }

        public Competition()
        {
            Participants = new List<IParticipant>();
            Tracks = new Queue<Track>();
        }

        public Competition(List<IParticipant> participants, Queue<Track> tracks)
        {
            Participants = participants;
            Tracks = tracks;
        }

        public void DistributePoints(List<IParticipant> endResult)
        {
            int earnedPoints = endResult.Count;
            foreach(IParticipant p in endResult)
            {
                var points = new ParticipantPoints();
                points.Name = p.Name;
                points.Points = earnedPoints;
                Points.Add(points);
                earnedPoints--;
            }
        }

        public Track NextTrack()
        {
            if(Tracks.Count>0)
                return Tracks.Dequeue();
            else
                return null;
        }
    }
}
