using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public List<IParticipant> participants;
        public DateTime StartTime;
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
    }
}
