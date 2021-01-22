using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class OvertakeData : IParticipantData
    {
        public string ParticipantName { get; set; }
        public string Overtaken { get; set; }
        public string TrackName { get; set; }
        public Section section;

        public void Add(List<IParticipantData> data)
        {

        }
    }
}
