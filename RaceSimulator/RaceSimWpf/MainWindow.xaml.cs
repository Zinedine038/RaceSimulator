using Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RaceSimWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Data.Initialize();
            Data.NextRace();
            Data.CurrentRace.Start();
            Data.CurrentRace.RaceFinished += NextRace;
            Data.CurrentRace.DriversChanged += OnDriversChanged;
        }

        private void OnDriversChanged(object sender, EventArgs e)
        {
            this.Track.Dispatcher.BeginInvoke(
                DispatcherPriority.Render,
                new Action(() =>
                {
                    this.Track.Source = null;
                    this.Track.Source = VisualisationWPF.DrawTrack(Data.CurrentRace.Track);
                }));
        }

        private void NextRace(object sender, EventArgs e)
        {

            if (Data.Competition.Tracks.Count > 0)
            {
                //Visualization.DrawEndOfRaceScreen();

                //Puur voor mooier maken
                Thread.Sleep(3000);

                Console.Clear();
                Data.CurrentRace.RaceFinished -= NextRace;
                Data.NextRace();
                //Visualization.Initialize();
                //Visualization.DrawTrack(Data.CurrentRace.Track);
                Data.CurrentRace.RaceFinished += NextRace;
                Data.CurrentRace.DriversChanged += OnDriversChanged;
                Data.CurrentRace.Start();
            }
            else
            {
                //Visualization.DrawEndOfCompetitionScreen();
            }
        }
    }


}
