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
        public int sectionLength { get; set; }


        public Track(string name)
        {
            Name = name;
        }

        public Track(string name, SectionType[] sectionsTypes, int length)
        {
            Name = name;
            Sections = GenerateSectionList(sectionsTypes);
            sectionLength = length;
            GenerateVisualTrack(Sections);
        }

        public void GenerateVisualTrack(LinkedList<Section> sections)
        {
            Direction dir = Direction.East;
            visualTrack = new Section[4,4];
            int x = 0;
            int y = 1;
            foreach(Section s in sections)
            {
                if(!AddToVisualTrack(x, y, s, dir))
                {
                    s.dir = dir;
                    Console.WriteLine("invalid track, track overlaps");
                    visualTrack = null;
                    return;
                }
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
                if (x>3 || y>3 || x<0 || y<0)
                {
                    Console.WriteLine("invalid track, track out of bounds");
                    visualTrack = null;
                    return;
                }
            }
        }

        public bool AddToVisualTrack(int x, int y, Section section,Direction dir)
        {
            if (visualTrack[x, y] == null)
            {
                section.dir = dir;
                visualTrack[x, y] = section;
                return true;
            }
            else
            {
                return false;
            }

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
