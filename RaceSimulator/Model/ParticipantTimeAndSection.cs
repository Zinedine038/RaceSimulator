using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ParticipantTimeAndSection : IParticipantData
    {
        public string ParticipantName { get; set; }
        public TimeSpan Time { get; set; }
        public Section Section { get; set; }

        public void Add(List<IParticipantData> data)
        {

        }
    }
}
