using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public enum TeamColours
    {
        Red,
        Green,
        Yellow,
        Grey,
        Blue
    }
    public interface IParticipant
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public IEquipment Equipment { get; set; }
        public TeamColours TamColour { get; set; }
        public int DistanceDrivenInCurrentRace { get; set; }
        public Section currentSection { get; set; }
    }
}
