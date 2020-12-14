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
            var sectionsNascar = new List<SectionType>();
            var sectionsMonteCarlo = new List<SectionType>();

            sectionsSilverStone.Add(SectionType.StartGrid);
            sectionsSilverStone.Add(SectionType.RightCorner);
            sectionsSilverStone.Add(SectionType.Finish);
            sectionsSilverStone.Add(SectionType.LeftCorner);
            sectionsSilverStone.Add(SectionType.RightCorner);
            sectionsSilverStone.Add(SectionType.RightCorner);
            sectionsSilverStone.Add(SectionType.Straight);
            sectionsSilverStone.Add(SectionType.RightCorner);
            sectionsSilverStone.Add(SectionType.LeftCorner);
            sectionsSilverStone.Add(SectionType.RightCorner);
            sectionsSilverStone.Add(SectionType.Straight);
            sectionsSilverStone.Add(SectionType.RightCorner);

            sectionsNascar.Add(SectionType.StartGrid);
            sectionsNascar.Add(SectionType.Finish);
            sectionsNascar.Add(SectionType.RightCorner);
            sectionsNascar.Add(SectionType.Straight);
            sectionsNascar.Add(SectionType.Straight);
            sectionsNascar.Add(SectionType.RightCorner);
            sectionsNascar.Add(SectionType.Straight);
            sectionsNascar.Add(SectionType.Straight);
            sectionsNascar.Add(SectionType.RightCorner);
            sectionsNascar.Add(SectionType.Straight);
            sectionsNascar.Add(SectionType.Straight);
            sectionsNascar.Add(SectionType.RightCorner);

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

            Competition.Tracks.Enqueue(new Track("Silverstone",sectionsSilverStone.ToArray(),125));
            Competition.Tracks.Enqueue(new Track("Monte Carlo", sectionsMonteCarlo.ToArray(),125));
            Competition.Tracks.Enqueue(new Track("NASCAR",sectionsNascar.ToArray(),125));

        }

        public static void NextRace()
        {
            if(Competition.NextTrack()!=null)
            {
                CurrentRace = new Race(Competition.NextTrack(), Competition.Participants,3);
            }
        }
    }
}
