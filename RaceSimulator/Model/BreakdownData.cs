using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class BreakdownData : IParticipantData
    {
        public string ParticipantName { get; set; }
        public TimeSpan Time { get; set; }
        public IEquipment Vehicle { get; set; }
        public int TimesBrokenDown { get; set; }

        public void Add(List<IParticipantData> data)
        {
            var participantSectionTime = data.Cast<BreakdownData>().ToList();
            var participantData = participantSectionTime.FirstOrDefault(data => data.ParticipantName == ParticipantName);

            if (participantData==null)
            {
                data.Add(this);
            }
            else
            {
                participantData.TimesBrokenDown++;
                participantData.Time += Time;
            }
        }

        public string GetBestParticipant(List<IParticipantData> data)
        { 
            string best = "";
            TimeSpan leastTimeBrokenDown = TimeSpan.MaxValue;
            foreach (BreakdownData participantData in data)
            {
                if (participantData.Time<leastTimeBrokenDown)
                {
                    leastTimeBrokenDown = participantData.Time;
                    best = participantData.ParticipantName;
                }
            }
            return best;
        }
    }
}
