using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RaceSimWpf
{
    public class MainDataContext : INotifyPropertyChanged
    {
        public string TrackName { get => $"Current Track: {Data.CurrentRace.Track.Name}"; }
        public event PropertyChangedEventHandler PropertyChanged;


        public MainDataContext()
        {
            Race.RaceStarted += OnRaceStart;
            if(Data.CurrentRace!=null)
            {
                Data.CurrentRace.DriversChanged += OnDriversChanged;
            }
        }

        private void OnDriversChanged(object e, DriversChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }

        private void OnRaceStart(object e, EventArgs args)
        {
            var newRace = (Race)e;
            newRace.DriversChanged += OnDriversChanged;
        }

    }
}
