using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ParticipantPointsData : IParticipantData
    {
        public string ParticipantName { get; set; }
        public int Points { get; set; }

        public void Add(List<IParticipantData> data)
        {
            var participantPoints = data.Cast<ParticipantPointsData>().ToList();
            var participantData = participantPoints.FirstOrDefault(data => data.ParticipantName == ParticipantName);
            if (participantData==null)
                data.Add(this);
            else
                participantData.Points+=Points;
        }

        public string GetBestParticipant(List<IParticipantData> data)
        {
            string best = "";
            int highestPoints= 0;
            foreach(ParticipantPointsData participantData in data)
            {
                if(participantData.Points>highestPoints)
                {
                    highestPoints = participantData.Points;
                    best = participantData.ParticipantName;
                }
            }
            return best;
        }

        //public Dictionary<string,int> GetParticipantPoints(List<IParticipantData> data)
        //{
        //    Dictionary<string, int> points = new Dictionary<string, int>();
        //    var participantPoints = data.Cast<ParticipantPointsData>().ToList();
        //    foreach(ParticipantPointsData participantData in data)
        //    {
        //        points.Add(participantData.ParticipantName, participantData.Points);
        //    }
        //    return points;
        //}

    }
}
