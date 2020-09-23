using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Track
    {
        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }

        public Track(string name)
        {
            Name = name;
        }

        public Track(string name, SectionType[] sections)
        {


        }
       
    }
}
