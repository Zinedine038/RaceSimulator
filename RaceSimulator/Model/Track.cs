using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Model
{
    public class Track
    {
        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }
        public Section[,] visualTrack { get; set; }


        public Track(string name)
        {
            Name = name;
        }

        public Track(string name, SectionType[] sectionsTypes)
        {
            Name = name;
            Sections = GenerateSectionList(sectionsTypes);
            GenerateVisualTrack(Sections);
        }

        public void GenerateVisualTrack(LinkedList<Section> sections)
        {
            Direction dir = Direction.East;
            visualTrack = new Section[4,4];
            int x = 0;
            int y = 0;
            foreach(Section s in sections)
            {
                AddToVisualTrack(x, y, s);
                switch(dir)
                {
                    case Direction.East:
                        if (s.SectionType == SectionType.LeftCorner)
                        {
                            dir = Direction.North;
                            x -= 1;
                            break;
                        }
                        else if (s.SectionType == SectionType.RightCorner)
                        {
                            dir = Direction.South;
                            x += 1;
                            break;
                        }
                        y += 1;
                        break;
                    case Direction.West:
                        if (s.SectionType == SectionType.LeftCorner)
                        {
                            dir = Direction.South;
                            x += 1;
                            break;
                        }
                        else if (s.SectionType == SectionType.RightCorner)
                        {
                            dir = Direction.North;
                            x -= 1;
                            break;
                        }
                        y -= 1;
                        break;
                    case Direction.South:
                        if (s.SectionType == SectionType.LeftCorner)
                        {
                            dir = Direction.East;
                            y += 1;
                            break;
                        }
                        else if (s.SectionType == SectionType.RightCorner)
                        {
                            dir = Direction.West;
                            y -= 1;
                            break;
                        }
                        x += 1;
                        break;
                    case Direction.North:
                        if (s.SectionType == SectionType.LeftCorner)
                        {
                            dir = Direction.West;
                            y -= 1;
                            break;
                        }
                        else if (s.SectionType == SectionType.RightCorner)
                        {
                            dir = Direction.East;
                            y += 1;
                            break;
                        }
                        x -= 1;
                        break;
                }
                Console.WriteLine(x);
                Console.WriteLine(y);
                Console.WriteLine(s.SectionType);
                if (x-1< visualTrack.GetLength(0) && y-1< visualTrack.GetLength(1))
                {
                    visualTrack[x, y] = s;
                }
                else
                {
                    Console.WriteLine("invalid track");
                    visualTrack = null;
                }
            }
        }

        public void AddToVisualTrack(int x, int y, Section section)
        {
            Console.WriteLine(visualTrack);
            visualTrack[x, y] = section;
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
