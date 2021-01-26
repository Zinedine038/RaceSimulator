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

        private Dictionary<string, TimeSpan> _averageTimeDict;

        public List<string> AverageTimes { get; set; }

        public List<string> BreakdownLog { get; set; }

        public string MostRecentOvertake { get; set; }

        public RaceContextData()
        {
            BreakdownLog = new List<string>();
            AverageTimes = new List<string>();
            _averageTimeDict = new Dictionary<string, TimeSpan>();
            Race.RaceStarted += OnRaceStart;
            if (Data.CurrentRace != null)
            {
                Data.CurrentRace.DriversChanged += OnDriversChanged;
            }
        }

        private void OnDriversChanged(object e, DriversChangedEventArgs args)
        {
            RankAverageTimes();
            LogBreakDowns();
            LogMostRecentOvertake();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }

        private void LogMostRecentOvertake()
        {
            var overtakes = Data.Competition.Overtakes.GetData();
            OvertakeData newestOvertake = (OvertakeData) overtakes.Last();
            MostRecentOvertake = $"{newestOvertake.ParticipantName} overtook {newestOvertake.Overtaken}!";
        }

        private void LogBreakDowns()
        {
            BreakdownLog.Clear();
            var breakdowns = Data.Competition.Breakdowns.GetData();
            foreach(BreakdownData data in breakdowns)
            {
                double time = data.Time.TotalSeconds;
                string onedec = time.ToString("0.0");
                BreakdownLog.Add($"{data.ParticipantName} has broken down a total of {data.TimesBrokenDown+1} for a total length of {onedec} seconds");
            }
            BreakdownLog = BreakdownLog.OrderBy(p => p).ToList();
        }

        private void RankAverageTimes()
        {
            _averageTimeDict.Clear();
            AverageTimes.Clear();

            var fastestTimes = Data.Competition.SectionTimes.GetData();

            Dictionary<string, TimeSpan> totalTimes = new Dictionary<string, TimeSpan>();

            foreach (SectionTimeData data in fastestTimes)
            {
                if(totalTimes.ContainsKey(data.ParticipantName))
                {
                    totalTimes[data.ParticipantName] += data.Time;
                }
                else
                {
                    totalTimes.Add(data.ParticipantName,data.Time);
                }
            }

            foreach(KeyValuePair<string,TimeSpan> totalTime in totalTimes )
            {
                int sum = (from x in fastestTimes where x.ParticipantName == totalTime.Key select x).Count();
                _averageTimeDict.Add(totalTime.Key, totalTime.Value / sum);
            }

            var ordered = _averageTimeDict.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            int placement = 1;

            foreach (KeyValuePair<string,TimeSpan> totalTime in ordered)
            {
                double time = totalTime.Value.TotalSeconds;
                string onedec = time.ToString("0.00");
                string name = totalTime.Key;

                AverageTimes.Add($"{placement}: {name} {onedec}");
                placement++;
            }
        }

        private void OnRaceStart(object e, EventArgs args)
        {
            var newRace = (Race)e;
            newRace.DriversChanged += OnDriversChanged;
        }
    }
}
