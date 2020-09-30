using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        private Random _random;
        private Dictionary<Section, SectionData> _positions;

        public SectionData GetSectionData(Section section)
        {
            if(_positions[section]!=null)
                return _positions[section];
            else
            {
                var temp = new SectionData();
                _positions.Add(section, temp);
                return temp;
            }
        }

        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            StartTime = new DateTime();
            _random = new Random(DateTime.Now.Millisecond);
            RandomizeEquipment();
        }

        public void RandomizeEquipment()
        {
            foreach(IParticipant p in Participants)
            {
                p.Equipment.Quality = _random.Next();
                p.Equipment.Speed = _random.Next();
            }
        }
    }
}
