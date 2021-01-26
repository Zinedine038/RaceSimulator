using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;

namespace RaceSimWpf
{
    public class RaceContextData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<string> Participantstandings { get; set; }
        public string ParticipantMostOvertakes { get => $"Most overtakes this competition by: {Data.Competition.Overtakes.GetBestParticipant()}"; }
        //public SavedData<OvertakeData> Overtakes { get => $"Current Track: {Data.CurrentRace.Track.Name}"; }
        public RaceContextData()
        {
            Participantstandings = new List<string>();
            Race.RaceStarted += OnRaceStart;
            if (Data.CurrentRace != null)
            {
                Data.CurrentRace.DriversChanged += OnDriversChanged;
                Data.CurrentRace.RaceFinished += OnRaceFinished;
            }
        }

        private void OnDriversChanged(object e, DriversChangedEventArgs args)
        {
            MakeRankings();
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(""));
        }

        private void MakeRankings()
        {
            Participantstandings.Clear();
            int placement = 1;
            var fastestTimes = Data.Competition.SectionTimes.GetData();
            
            foreach (SectionTimeData data in fastestTimes)
            {
                Participantstandings.Add($"{placement}: {data.ParticipantName} fastests time was {data.Time.TotalSeconds.ToString()} on a{data.Section.SectionType}");
            }

        }

        private void OnRaceStart(object e, EventArgs args)
        {
            var newRace = (Race)e;
            newRace.DriversChanged += OnDriversChanged;
            newRace.RaceFinished += OnRaceFinished;
        }
    }
}
