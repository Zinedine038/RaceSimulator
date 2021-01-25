using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Competition
    {
        public List<IParticipant> Participants { get; set; }
        public Queue<Track> Tracks { get; set; }

        public SavedData<ParticipantPointsData> Points { get; set; }

        public SavedData<SectionTimeData> SectionTimes { get; set; }
        public SavedData<BreakdownData> Breakdowns { get; set; }
        public SavedData<OvertakeData> Overtakes { get; set; }

        public Competition()
        {
            Participants = new List<IParticipant>();
            Tracks = new Queue<Track>();
            Points = new SavedData<ParticipantPointsData>();
            Breakdowns = new SavedData<BreakdownData>();
            SectionTimes = new SavedData<SectionTimeData>();
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

        public void LogBreakDown(IParticipant participant, TimeSpan time)
        {
            var breakdownData = new BreakdownData();
            breakdownData.ParticipantName = participant.Name;
            breakdownData.Time = time;
            breakdownData.Vehicle = participant.Equipment;
            Breakdowns.Add(breakdownData);
        }

        public void LogSectionTime(IParticipant participant,TimeSpan time,Section section)
        {
            var sectionTime = new SectionTimeData();
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
            overtakeData.Section = section;
            Overtakes.Add(overtakeData);
        }

        public void DistributePoints(Dictionary<IParticipant, int> endResult)
        {
            int earnedPoints = endResult.Count;
            foreach(KeyValuePair<IParticipant, int> p in endResult)
            {
                var points = new ParticipantPointsData();
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
