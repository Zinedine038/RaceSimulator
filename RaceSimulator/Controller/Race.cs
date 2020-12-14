using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Timers;

namespace Controller
{
    public class Race
    {
        public int lapsToFinish;
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public List<IParticipant> FinishedParticipants { get; set; }
        public DateTime StartTime { get; set; }
        private Random _random;
        private Dictionary<Section, SectionData> _positions;
        private System.Timers.Timer timer;
        public ElapsedEventHandler OnTimedEvent;
        public event EventHandler<DriversChangedEventArgs> DriversChanged;

        public SectionData GetSectionData(Section section)
        {
            if (_positions.ContainsKey(section))
                return _positions[section];
            else
            {
                var temp = new SectionData();
                _positions.Add(section, temp);
                return temp;
            }
        }

        public Race(Track track, List<IParticipant> participants, int laps)
        {
            Track = track;
            FinishedParticipants = new List<IParticipant>();
            Participants = participants;
            StartTime = new DateTime();
            lapsToFinish = laps;
            //_random = new Random(DateTime.Now.Millisecond);
            //RandomizeEquipment();
            _positions = new Dictionary<Section, SectionData>();
            PlaceParticipantsStart(track, participants);
            timer = new System.Timers.Timer(500);
            timer.Elapsed += MoveDrivers;
        }

        public void Start()
        {
            timer.Start();
        }

        public void MoveDrivers(object sender, EventArgs e)
        {
            var finishedDriversThisLap = new List<IParticipant>();
            foreach(IParticipant p in Participants)
            {
                GetSectionData(p.currentSection).ParticipantsOnSection.Remove(p);
                p.DistanceDrivenInCurrentRace = p.DistanceDrivenInCurrentRace + (p.Equipment.Speed * p.Equipment.Performance);
                if((p.DistanceDrivenInCurrentRace / (Track.sectionLength*Track.Sections.Count))>=lapsToFinish)
                {
                    finishedDriversThisLap.Add(p);
                }
                int sectionToGoTo = (p.DistanceDrivenInCurrentRace / Track.sectionLength) % Track.Sections.Count;
                var nextSection = GetNextSectionInRace(sectionToGoTo);
                var nextSectionData = GetSectionData(nextSection);
                int placeOnSection = 0;
                for (int i = 0; i<nextSectionData.ParticipantsOnSection.Count; i++)
                {
                    if (nextSectionData.ParticipantsOnSection[i].DistanceDrivenInCurrentRace > p.DistanceDrivenInCurrentRace)
                        placeOnSection++;
                    else
                        break;
                }
                if (placeOnSection >= nextSectionData.ParticipantsOnSection.Count)
                    nextSectionData.ParticipantsOnSection.Add(p);
                else
                    nextSectionData.ParticipantsOnSection.Insert(placeOnSection, p);
                p.currentSection = nextSection;
            }
            DriversChanged.Invoke(this, new DriversChangedEventArgs(Track));
            RemoveFromRace(finishedDriversThisLap);
        }

        public void RemoveFromRace(List<IParticipant> finished)
        {
            foreach(IParticipant p in finished)
            {
                Participants.Remove(p);
                FinishedParticipants.Add(p);
                var sectionData = GetSectionData(p.currentSection);
                sectionData.ParticipantsOnSection.Remove(p);
                if (sectionData.RightParticipant == p)
                    sectionData.RightParticipant = null;
                else if(sectionData.LeftParticipant== p)
                    sectionData.LeftParticipant = null;
            }
        }


        public Section GetNextSectionInRace(int amount)
        {
            var next = Track.Sections.First;
            for (int i = 0; i < amount; i++)
            {
                if (next != Track.Sections.Last)
                {
                    next = next.Next;

                }
                else
                {
                    next = Track.Sections.First;
                }
            }
            return next.Value;
        }

        public void RandomizeEquipment()
        {
            foreach (IParticipant p in Participants)
            {
                p.Equipment.Quality = _random.Next();
                p.Equipment.Speed = _random.Next();
                p.Equipment.Performance = _random.Next();
            }
        }

        public void PlaceParticipantsStart(Track track, List<IParticipant> participant)
        {
            var start = track.Sections.First.Value;
            var last = track.Sections.Last.Value;
            GetSectionData(start).RightParticipant = participant[0];
            participant[0].DistanceDrivenInCurrentRace = 0;
            participant[0].currentSection = start;
            GetSectionData(start).LeftParticipant = participant[1];
            participant[1].DistanceDrivenInCurrentRace = 0;
            participant[1].currentSection = start;
            GetSectionData(last).RightParticipant = participant[2];
            GetSectionData(last).LeftParticipant = participant[3];
            participant[2].DistanceDrivenInCurrentRace = 0;
            participant[2].currentSection = last;
            participant[3].DistanceDrivenInCurrentRace = 0;
            participant[3].currentSection = last;


            participant[0].Equipment.Speed = 120;
            participant[1].Equipment.Speed = 50;
            participant[2].Equipment.Speed = 155;
            participant[3].Equipment.Speed = 120;
            participant[0].Equipment.Performance = 1;
            participant[1].Equipment.Performance = 1;
            participant[2].Equipment.Performance = 1;
            participant[3].Equipment.Performance = 1;

        }

    }
}
