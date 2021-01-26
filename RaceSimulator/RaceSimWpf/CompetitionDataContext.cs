using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;

namespace RaceSimWpf
{
    public class CompetitionDataContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<string> Overtakes { get; set; }
        public string ParticipantsHighestPoints { get => $"Leading driver: {Data.Competition.Points.GetBestParticipant()}"; }
        public CompetitionDataContext()
        {
            Overtakes = new List<string>();
            Race.RaceStarted += OnRaceStart;
            if (Data.CurrentRace != null)
            {
                Data.CurrentRace.DriversChanged += OnDriversChanged;
            }
        }

        private void OnDriversChanged(object e, DriversChangedEventArgs args)
        {
            MakeOvertakeList();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }

        private void MakeOvertakeList()
        {
            Overtakes.Clear();
            foreach(OvertakeData overtake in Data.Competition.Overtakes.GetData())
            {
                Overtakes.Add($"{overtake.ParticipantName} overtook {overtake.Overtaken} on a {overtake.Section.SectionType.ToString()} on {overtake.TrackName}");
            }
            Overtakes = Overtakes.OrderBy(p => p).ToList();

        }

        private void OnRaceStart(object e, EventArgs args)
        {
            var newRace = (Race)e;
            newRace.DriversChanged += OnDriversChanged;
        }
    }
}
