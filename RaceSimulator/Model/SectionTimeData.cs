using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class SectionTimeData : IParticipantData
    {
        public string ParticipantName { get; set; }
        public TimeSpan Time { get; set; }
        public Section Section { get; set; }

        public void Add(List<IParticipantData> data)
        {
            var participantSectionTime = data.Cast<SectionTimeData>().ToList();
            var participantData = participantSectionTime.FirstOrDefault(data => data.ParticipantName == ParticipantName && data.Section==Section);

            if (participantData==null)
                data.Add(this);
            else
                participantData.Time = Time;
        }

        public string GetBestParticipant(List<IParticipantData> data)
        {
            string best = "";
            TimeSpan fastest = TimeSpan.Zero;
            foreach (SectionTimeData participantData in data)
            {
                if (participantData.Time < fastest)
                {
                    fastest = participantData.Time;
                    best = participantData.ParticipantName;
                }
            }
            return best;
        }
    }
}
