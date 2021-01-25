using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Model
{
    public class OvertakeData : IParticipantData
    {
        public string ParticipantName { get; set; }
        public string Overtaken { get; set; }
        public string TrackName { get; set; }
        public Section Section { get; set; }


        public void Add(List<IParticipantData> data)
        {
            data.Add(this);
        }

        public string GetBestParticipant(List<IParticipantData> data)
        {
            Dictionary<string, int> totalOvertakes = new Dictionary<string, int>();
            foreach(OvertakeData overtake in data)
            {
                if (totalOvertakes.ContainsKey(overtake.ParticipantName))
                    totalOvertakes[overtake.ParticipantName]++;
                else
                    totalOvertakes.Add(overtake.ParticipantName, 1);
            }

            string mostOvertakes = "";
            int overtakes = 0;
            foreach(KeyValuePair<string,int> participant in totalOvertakes)
            {
                if(participant.Value>overtakes)
                {
                    mostOvertakes = participant.Key;
                }
            }
            return mostOvertakes;
        }
    }
}
