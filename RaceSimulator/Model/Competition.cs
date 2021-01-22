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

        public SavedData<ParticipantTimeAndSection> SectionTimes { get; set; }
        public SavedData<ParticipantTimeAndSection> Breakdowns { get; set; }
        public SavedData<OvertakeData> Overtakes { get; set; }

        public Competition()
        {
            Participants = new List<IParticipant>();
            Tracks = new Queue<Track>();
            Points = new SavedData<ParticipantPoints>();
            Breakdowns = new SavedData<ParticipantTimeAndSection>();
            SectionTimes = new SavedData<ParticipantTimeAndSection>();
            Overtakes = new SavedData<OvertakeData>();
        }

        public Competition(List<IParticipant> participants, Queue<Track> tracks)
        {
            Participants = participants;
            Tracks = tracks;
        }

        public void RaceFinished(object sender, RaceFinishedEventArgs e)
        {

            DistributePoints(e.Participants);
        }

        public void LogBreakDown(IParticipant participant, TimeSpan time, Section section)
        {
            var breakdownTime = new ParticipantTimeAndSection();
            breakdownTime.ParticipantName = participant.Name;
            breakdownTime.Time = time;
            breakdownTime.Section = section;
            Breakdowns.Add(breakdownTime);
        }

        public void LogSectionTime(IParticipant participant,TimeSpan time,Section section)
        {
            var sectionTime = new ParticipantTimeAndSection();
            sectionTime.ParticipantName = participant.Name;
            sectionTime.Time = time;
            sectionTime.Section = section;
            SectionTimes.Add(sectionTime);
        }

        public void LogOvertake(IParticipant overtaker, IParticipant overtaken, Section section, string trackName)
        {
            var overtakeData = new OvertakeData();
            overtakeData.ParticipantName = overtaker.Name;
            overtakeData.Overtaken = overtaken.Name;
            overtakeData.TrackName = trackName;
            overtakeData.section = section;
            Overtakes.Add(overtakeData);
        }

        public void DistributePoints(Dictionary<IParticipant, int> endResult)
        {
            int earnedPoints = endResult.Count;
            foreach(KeyValuePair<IParticipant, int> p in endResult)
            {
                var points = new ParticipantPoints();
                points.ParticipantName = p.Key.Name;
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
