using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class SectionData
    {
        public IParticipant left { get; set; }
        public int DistanceLeft { get; set; }
        public IParticipant RightParticipant { get; set; }
        public int DistanceRight { get; set; }

        public SectionData()
        {
        }

        public SectionData(IParticipant left, int distanceLeft, IParticipant rightParticipant, int distanceRight)
        {
            this.left = left;
            DistanceLeft = distanceLeft;
            RightParticipant = rightParticipant;
            DistanceRight = distanceRight;
        }
    }
}
