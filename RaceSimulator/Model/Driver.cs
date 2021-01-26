using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Driver : IParticipant
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public IEquipment Equipment { get; set; }
        public TeamColours TeamColour { get; set; }
        public int LapsInCurrentRace { get; set; }
        public bool Finished { get; set; } = false;
        public Section currentSection { get; set; }
        public bool Moved { get; set; }


        public Driver(string name)
        {
            Name = name;
            Points = 0;
            Equipment = new Car();

        }

        public Driver(string name,TeamColours teamColour)
        {
            Name = name;
            Points = 0;
            Equipment = new Car();
            TeamColour = teamColour;

        }

        public Driver(string name, int points, IEquipment equipment, TeamColours tamColour)
        {
            Name = name;
            Points = points;
            Equipment = equipment;
            TeamColour = tamColour;
        }
    }
}
