using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public interface IParticipantData
    {
        public string ParticipantName { get; set; }
        public void Add(List<IParticipantData> data);
    }
}
