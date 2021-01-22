using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ParticipantPoints : IParticipantData
    {
        public string ParticipantName { get; set; }
        public int Points { get; set; }

        public void Add(List<IParticipantData> data)
        {
            foreach(IParticipantData participantData in data)
            {
                var participantPoints = (ParticipantPoints)participantData;
                bool exists = false;

                foreach(IParticipantData name in data)
                {
                    if (name.ParticipantName == ParticipantName)
                        exists = true;                    
                }

                if(!exists)
                {
                    data.Add(this);
                }
                else
                {
                    participantPoints.Points+=Points;
                }
            }
        }        
    }
}
