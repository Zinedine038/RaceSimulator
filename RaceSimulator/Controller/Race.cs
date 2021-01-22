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

        public Dictionary<IParticipant, int> FinishedParticipants { get; set; }
        public DateTime StartTime { get; set; }
        private Random _random;

        private Dictionary<Section, SectionData> _positions;
        private Dictionary<IParticipant, TimeSpan> _previousSectionTimes;
        private Dictionary<IParticipant, DateTime> _currentBreakdowns;

        private System.Timers.Timer _timer;




        public event EventHandler<DriversChangedEventArgs> DriversChanged;
        public event EventHandler<RaceFinishedEventArgs> RaceFinished;

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
            Participants = participants;
            _previousSectionTimes = new Dictionary<IParticipant, TimeSpan>();
            _currentBreakdowns = new Dictionary<IParticipant, DateTime>();

            foreach (IParticipant p in participants)
            {
                p.LapsInCurrentRace = 0;
                _previousSectionTimes.Add(p, TimeSpan.Zero);
            }
            StartTime = DateTime.Now;
            lapsToFinish = laps;


            _random = new Random(DateTime.Now.Millisecond);
            //RandomizeEquipment();
            _positions = new Dictionary<Section, SectionData>();
            PlaceParticipantsStart(track, participants);
            _timer = new System.Timers.Timer(500);
            _timer.Elapsed += OnTimedEvent;
                        

            RaceFinished += Data.Competition.RaceFinished;
            FinishedParticipants = new Dictionary<IParticipant, int>();
        }

        public void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            MoveDrivers(e.SignalTime);
            SimulateEquipment(e.SignalTime);
        }

        public void Start()
        {
            _timer.Start();
        }

        private void SimulateEquipment(DateTime time)
        {
            foreach(IParticipant p in Participants)
            {
                if(p.Equipment.IsBroken)
                {
                    var chanceToFix = 20;
                    if(_random.Next(0,100)<chanceToFix)
                    {
                        var consuquenceRandom = 50;
                        if(_random.Next(1,100)< consuquenceRandom || p.Equipment.Quality==1)
                        {
                            p.Equipment.Speed = (int) Math.Round(p.Equipment.Speed * 0.9f);
                        }
                        else
                        {
                            if(p.Equipment.Quality>1)
                                p.Equipment.Quality--;
                        }
                        LogBreakdown(p, _currentBreakdowns[p] - time);
                        _currentBreakdowns.Remove(p);
                        p.Equipment.IsBroken = false;
                    }
                }
                else if(p.Finished!=true)
                {
                    var chanceToBreak = (float) 100 / (p.Equipment.Quality*10);
                    if (_random.Next(0, 100) < chanceToBreak)
                    {
                        p.Equipment.IsBroken = true;
                        _currentBreakdowns.Add(p, time);
                        DriversChanged.Invoke(this, new DriversChangedEventArgs(Track));
                    }
                }
            }

        }

        public void MoveDrivers(DateTime time)
        {
            var finishedDriversThisUpdate = new List<IParticipant>();
            foreach(IParticipant p in Participants)
                p.Moved = false;

            foreach(Section section in Track.Sections.Reverse())
            {

                var data = GetSectionData(section);
                Section previousSection = GetPreviousSection(section);

                var previousSectionData = GetSectionData(previousSection);


                if (data.LeftParticipant == null || data.RightParticipant == null)
                {
                    if (previousSectionData.LeftParticipant != null && previousSectionData.LeftParticipant.Moved == false && previousSectionData.LeftParticipant.Equipment.IsBroken == false)
                    {
                        var participant = previousSectionData.LeftParticipant;
                        int amountToMove = participant.Equipment.Speed * participant.Equipment.Performance;
                        if (amountToMove + previousSectionData.DistanceLeft > Track.sectionLength)
                        {
                            PlaceParticipantCorrectly(section, data, participant, (previousSectionData.DistanceLeft + amountToMove) % Track.sectionLength);
                            if(previousSectionData.RightParticipant!=null)
                            {
                                previousSectionData.LeftParticipant = previousSectionData.RightParticipant;
                                previousSectionData.DistanceLeft = previousSectionData.DistanceRight;

                                previousSectionData.RightParticipant = null;
                                previousSectionData.DistanceRight = 0;
                            }
                            else
                            {
                                previousSectionData.LeftParticipant = null;
                                previousSectionData.DistanceLeft = 0;
                            }
                            if (IsFinishSection(section))
                            {
                                participant.LapsInCurrentRace++;
                                if(participant.LapsInCurrentRace>lapsToFinish)
                                {
                                    finishedDriversThisUpdate.Add(participant);
                                }
                            }
                            LogSectionTime(participant, time, previousSection);
                            DriversChanged.Invoke(this, new DriversChangedEventArgs(Track));
                        }
                        if (amountToMove+previousSectionData.DistanceLeft<=Track.sectionLength)
                        {
                            previousSectionData.DistanceLeft += amountToMove;
                        }
                        participant.Moved = true;
                    }
                    if (previousSectionData.RightParticipant!=null && previousSectionData.RightParticipant.Moved == false && previousSectionData.RightParticipant.Equipment.IsBroken == false)
                    {
                        var participant = previousSectionData.RightParticipant;
                        int amountToMove = participant.Equipment.Speed * participant.Equipment.Performance;
                        if (amountToMove + previousSectionData.DistanceRight > Track.sectionLength)
                        {
                            PlaceParticipantCorrectly(section, data, participant, (previousSectionData.DistanceRight + amountToMove) % Track.sectionLength);
                            previousSectionData.RightParticipant = null;
                            previousSectionData.DistanceRight = 0;
                            if (IsFinishSection(section))
                            {
                                participant.LapsInCurrentRace++;
                                if (participant.LapsInCurrentRace > lapsToFinish)
                                {
                                    finishedDriversThisUpdate.Add(participant);
                                }
                            }
                            LogSectionTime(participant, time, previousSection);
                            DriversChanged.Invoke(this, new DriversChangedEventArgs(Track));
                        }
                        if (amountToMove + previousSectionData.DistanceRight <= Track.sectionLength)
                        {
                            previousSectionData.DistanceRight += amountToMove;
                            if (previousSectionData.DistanceRight > previousSectionData.DistanceLeft)
                                Overtake(previousSectionData);
                        }
                        participant.Moved = true;
                    }
                }
                else if(data.LeftParticipant!=null && data.RightParticipant!=null)
                {
                    if(previousSectionData.LeftParticipant!=null && previousSectionData.LeftParticipant.Moved == false && previousSectionData.LeftParticipant.Equipment.IsBroken == false)
                    {
                        var participant = previousSectionData.LeftParticipant;
                        int amountToMove = participant.Equipment.Speed * participant.Equipment.Performance;
                        if (amountToMove + previousSectionData.DistanceLeft <= Track.sectionLength)
                        {
                            previousSectionData.DistanceLeft += amountToMove;
                        }
                        else
                        {
                            previousSectionData.DistanceRight = Track.sectionLength;
                        }
                        participant.Moved = true;

                    }
                    if (previousSectionData.RightParticipant!=null && previousSectionData.RightParticipant.Moved == false && previousSectionData.RightParticipant.Equipment.IsBroken == false)
                    {
                        var participant = previousSectionData.RightParticipant;
                        int amountToMove = participant.Equipment.Speed * participant.Equipment.Performance;
                        if (amountToMove + previousSectionData.DistanceRight < Track.sectionLength)
                        {
                            previousSectionData.DistanceRight += amountToMove;
                            if (previousSectionData.DistanceRight > previousSectionData.DistanceLeft)
                                Overtake(previousSectionData);
                        }
                        else
                        {
                            previousSectionData.DistanceRight = Track.sectionLength - 1;
                        }
                        participant.Moved = true;
                    }
                }
                RemoveFromRace(finishedDriversThisUpdate);
            }
        }

        private void LogSectionTime(IParticipant participant,DateTime time,Section section)
        {
            TimeSpan span = time - StartTime -_previousSectionTimes[participant];
            _previousSectionTimes[participant] += span;
            Data.Competition.LogSectionTime(participant, span, section);
        }

        private void LogBreakdown(IParticipant participant, TimeSpan time)
        {
            Data.Competition.LogBreakDown(participant, time, participant.currentSection);
        }

        private void LogOvertake(IParticipant overtaker, IParticipant overtaken, Section section)
        {
            Data.Competition.LogOvertake(overtaker, overtaken, section, Track.Name);
        }



        private void PlaceParticipantCorrectly(Section section, SectionData data, IParticipant newParticipant, int distanceRemaining)
        {
            newParticipant.currentSection = section;
            if (data.LeftParticipant == null)
            {
                data.LeftParticipant = newParticipant;
                data.DistanceLeft = distanceRemaining;
            }
            else if (data.LeftParticipant!=null && distanceRemaining>data.DistanceLeft)
            {
                data.RightParticipant = data.LeftParticipant;
                data.DistanceRight = data.DistanceLeft;

                data.LeftParticipant = newParticipant;
                data.DistanceLeft = distanceRemaining;
            }
            else if(data.LeftParticipant!=null)
            {
                data.RightParticipant = newParticipant;
                data.DistanceRight = distanceRemaining;
            }
        }

        private void Overtake(SectionData data)
        {
            var overTaker = data.RightParticipant;
            var overTakerDistance = data.DistanceRight;

            data.RightParticipant = data.LeftParticipant;
            data.DistanceRight = data.DistanceLeft;

            data.LeftParticipant = overTaker;
            data.DistanceLeft = overTakerDistance;

            LogOvertake(overTaker, data.RightParticipant, overTaker.currentSection);
            DriversChanged.Invoke(this, new DriversChangedEventArgs(Track));
        }

        private Section GetPreviousSection(Section current)
        {
            Section previousSection;
            if (current == Track.Sections.First.Value)
            {
                previousSection = Track.Sections.Last.Value;
            }
            else
            {
                previousSection = Track.Sections.Find(current).Previous.Value;
            }
            return previousSection;
        }

        private bool IsFinishSection(Section section)
        {
            if (section.SectionType == SectionType.Finish)
                return true;
            else
                return false;
        }

        public void RemoveFromRace(List<IParticipant> finished)
        {
            foreach(IParticipant p in finished)
            {
                p.Finished = true;
                if (!FinishedParticipants.ContainsKey(p))
                    FinishedParticipants.Add(p, FinishedParticipants.Count + 1);

                var sectionData = GetSectionData(p.currentSection);
                if (sectionData.RightParticipant == p)
                    sectionData.RightParticipant = null;
                else if(sectionData.LeftParticipant== p)
                    sectionData.LeftParticipant = null;
            }
            if(CheckFinished())
            {
                FinishRace();
            }
        }

        private bool CheckFinished()
        {
            return FinishedParticipants.Count == Data.Competition.Participants.Count;
        }

        private void FinishRace()
        {
            _timer.Stop();
            _timer.Elapsed -= OnTimedEvent;
            RaceFinished?.Invoke(this, new RaceFinishedEventArgs(GetFinishedPlacements()));
            if(DriversChanged!=null)
            {
                foreach (Delegate d in DriversChanged.GetInvocationList())
                {
                    DriversChanged -= (EventHandler<DriversChangedEventArgs>)d;
                }
            }

        }

        public Dictionary<IParticipant,int> GetFinishedPlacements()
        {
            return FinishedParticipants;
        }

        public void RandomizeEquipment()
        {
            foreach (IParticipant p in Participants)
            {
                p.Equipment.Quality = _random.Next(1,100);
                p.Equipment.Speed = _random.Next(50,150);
                p.Equipment.Performance = _random.Next(1);
            }
        }

        public void PlaceParticipantsStart(Track track, List<IParticipant> participant)
        {
            var start = track.Sections.First.Value;
            var last = track.Sections.Last.Value;
            GetSectionData(start).RightParticipant = participant[0];
            participant[0].LapsInCurrentRace = 0;
            participant[0].currentSection = start;
            GetSectionData(start).LeftParticipant = participant[1];
            participant[1].LapsInCurrentRace = 0;
            participant[1].currentSection = start;
            GetSectionData(last).RightParticipant = participant[2];
            GetSectionData(last).LeftParticipant = participant[3];
            participant[2].LapsInCurrentRace = 0;
            participant[2].currentSection = last;
            participant[3].LapsInCurrentRace = 0;
            participant[3].currentSection = last;


            participant[0].Equipment.Speed = 120;
            participant[1].Equipment.Speed = 75;
            participant[2].Equipment.Speed = 115;
            participant[3].Equipment.Speed = 105;
            participant[0].Equipment.Performance = 1;
            participant[1].Equipment.Performance = 1;
            participant[2].Equipment.Performance = 1;
            participant[3].Equipment.Performance = 1;
            participant[0].Equipment.Quality = 5;
            participant[1].Equipment.Quality= 5;
            participant[2].Equipment.Quality = 5;
            participant[3].Equipment.Quality = 5;
            foreach (IParticipant p in participant)
                p.Finished = false;

        }

    }
}
