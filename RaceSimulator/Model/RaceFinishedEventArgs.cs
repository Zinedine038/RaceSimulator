using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class RaceFinishedEventArgs : EventArgs
    {
        public Dictionary<IParticipant, int> Participants { get; set; }
        public RaceFinishedEventArgs(Dictionary<IParticipant,int> participants)
        {
            Participants = participants;
        }
    }
}
