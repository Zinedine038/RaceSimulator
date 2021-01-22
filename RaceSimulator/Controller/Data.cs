using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace Controller
{
    public static class Data
    {
        public static Competition Competition { get; set; }
        public static Race CurrentRace { get; set; }
        public static void Initialize()
        {
            Competition = new Competition();
            AddParticipants();
            AddTrack();
        }

        public static void AddParticipants()
        {
            Competition.Participants.Add(new Driver("Clarkson"));
            Competition.Participants.Add(new Driver("May"));
            Competition.Participants.Add(new Driver("Hammond"));
            Competition.Participants.Add(new Driver("Stig"));
        }
        public static void AddTrack()
        {
            var sectionsSilverStone = new List<SectionType>();
            var sectionsMonteCarlo = new List<SectionType>();

            sectionsMonteCarlo.Add(SectionType.StartGrid);
            sectionsMonteCarlo.Add(SectionType.Finish);
            sectionsMonteCarlo.Add(SectionType.RightCorner);
            sectionsMonteCarlo.Add(SectionType.RightCorner);
            sectionsMonteCarlo.Add(SectionType.Straight);
            sectionsMonteCarlo.Add(SectionType.LeftCorner);
            sectionsMonteCarlo.Add(SectionType.LeftCorner);
            sectionsMonteCarlo.Add(SectionType.Straight);
            sectionsMonteCarlo.Add(SectionType.RightCorner);
            sectionsMonteCarlo.Add(SectionType.RightCorner);
            sectionsMonteCarlo.Add(SectionType.Straight);
            sectionsMonteCarlo.Add(SectionType.Straight);
            sectionsMonteCarlo.Add(SectionType.RightCorner);
            sectionsMonteCarlo.Add(SectionType.Straight);
            sectionsMonteCarlo.Add(SectionType.Straight);
            sectionsMonteCarlo.Add(SectionType.RightCorner);

            sectionsSilverStone.Add(SectionType.StartGrid);
            sectionsSilverStone.Add(SectionType.Finish);
            sectionsSilverStone.Add(SectionType.RightCorner);
            sectionsSilverStone.Add(SectionType.Straight);
            sectionsSilverStone.Add(SectionType.Straight);
            sectionsSilverStone.Add(SectionType.RightCorner);
            sectionsSilverStone.Add(SectionType.RightCorner);
            sectionsSilverStone.Add(SectionType.Straight);
            sectionsSilverStone.Add(SectionType.LeftCorner);
            sectionsSilverStone.Add(SectionType.LeftCorner);
            sectionsSilverStone.Add(SectionType.Straight);
            sectionsSilverStone.Add(SectionType.RightCorner);
            sectionsSilverStone.Add(SectionType.RightCorner);
            sectionsSilverStone.Add(SectionType.Straight);
            sectionsSilverStone.Add(SectionType.Straight);
            sectionsSilverStone.Add(SectionType.RightCorner);

            Competition.Tracks.Enqueue(new Track("Monte Carlo", sectionsMonteCarlo.ToArray(), 125));
            Competition.Tracks.Enqueue(new Track("Silverstone", sectionsSilverStone.ToArray(), 125));
            Competition.Tracks.Enqueue(new Track("Monte Carlo Round 2", sectionsMonteCarlo.ToArray(), 125));

        }

        public static void NextRace()
        {
            var nextTrack = Competition.NextTrack();
            if(nextTrack != null)
            {
                CurrentRace = new Race(nextTrack, Competition.Participants,1);
            }
        }
    }
}
