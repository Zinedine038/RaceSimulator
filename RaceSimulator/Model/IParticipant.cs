using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public enum TeamColours
    {
        Red,
        White,
        Yellow,
        Blue
    }
    public interface IParticipant
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public IEquipment Equipment { get; set; }
        public TeamColours TeamColour { get; set; }
        public int LapsInCurrentRace { get; set; }
        public bool Finished { get; set; }
        public Section currentSection { get; set; }
        public bool Moved { get; set; }
    }
}
