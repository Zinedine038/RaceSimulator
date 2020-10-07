using System;
using System.Collections.Generic;
using System.Linq;
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

        public Track(string name, SectionType[] sectionsTypes)
        {

            Name = name;
            Sections = GenerateSectionList(sectionsTypes);
        }

        public LinkedList<Section> GenerateSectionList(SectionType[] sectionTypes)
        {
            var section = new LinkedList<Section>();
            foreach (SectionType s in sectionTypes)
                section.AddLast(new Section(s));
            return section;
        }
       
    }
}
