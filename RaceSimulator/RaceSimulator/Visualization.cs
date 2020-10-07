using Model;
using System;
using System.Collections.Generic;
using System.Text;
using Model;
using System.Linq;

namespace View
{
    static class Visualization
    {
        #region graphics
        static Graphic finishHorizontal;
        static Graphic finishVertical;
        static Graphic straightHorizontal;
        static Graphic straightVertical;
        static Graphic leftCorner;
        static Graphic leftCornerMirrored;
        static Graphic rightCorner;
        static Graphic rightCornerMirrored;
        static Graphic startGridHorizontal;
        static Graphic startGridVertical;
        #endregion

        public static void Initialize()
        {
            #region graphic declaration
            finishHorizontal = new Graphic(new string[4] { "----", "  # ", "  # ", "----" });
            finishVertical = new Graphic(new string[4] { "|  |", "|**|", "|  |", "|  |" });
            straightHorizontal = new Graphic(new string[4] { "----", "    ", "    ", "----" });
            straightVertical = new Graphic(new string[4] { "|  |", "|  |", "|  |", "|  |" });
            leftCorner = new Graphic(new string[4] { @"|  |", @"/  |", @"   /", @"--/ " });
            leftCornerMirrored = new Graphic(new string[4] { @"|  |", @"|  |", @"\    ", @" \--" });
            rightCorner = new Graphic(new string[4] { @"--\ ", @"   \", @"\  |", @"|  |" });
            rightCornerMirrored = new Graphic(new string[4] { @" /--", @"/   ", @"|   ", @"|  |" });
            startGridHorizontal = new Graphic(new string[4] { "----", "| | ", " | |", "----" });
            startGridVertical = new Graphic(new string[4] { "|- |", "| -|", "|- |", "| -|" });
            #endregion

            var sections = new List<SectionType>();
            sections.Add(SectionType.StartGrid);
            sections.Add(SectionType.Straight);
            sections.Add(SectionType.Straight);
            sections.Add(SectionType.RightCorner);
            sections.Add(SectionType.LeftCorner);
            sections.Add(SectionType.Straight);
            sections.Add(SectionType.Straight);
            sections.Add(SectionType.RightCorner);
            sections.Add(SectionType.LeftCorner);
            sections.Add(SectionType.Straight);
            sections.Add(SectionType.Straight);
            sections.Add(SectionType.RightCorner);
            sections.Add(SectionType.LeftCorner);
            sections.Add(SectionType.Straight);
            sections.Add(SectionType.Straight);
            sections.Add(SectionType.Finish);

            var testTrack = new Track("silverstone", sections.ToArray());
            Console.WriteLine(testTrack.Name);
            DrawTrack(testTrack);

        }

        public static void DrawTrack(Track track)
        {
            Grid grid = new Grid(4, 4, track);
            for (int x = 0; x < 4; x++)
            {
                for (int line = 0; line < 4; line++)
                {
                    for (int y = 0; y < 4; y++)
                    {
                        Console.Write(GetGraphic(grid.trackGrid[x, y].SectionType, line) + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

        public Graphic[,] GetGraphicsGrid(LinkedList<Section> sections, int x, int y)
        {
            Direction dir = Direction.East;
            var sectionArray = sections.ToList();
            var graphicsPackage = new Graphic[x, y];
            int teller = 0;
            for(int i=0; i<x; i++)
            {
                for (int i2 = 0; i2 < y; i2++)
                {
                    switch(sectionArray[teller].SectionType)
                    {
                        case SectionType.Straight:
                        case SectionType.StartGrid:
                        case SectionType.Finish:
                            break;
                        case SectionType.LeftCorner:
                            GetCorrectGraphic(dir, SectionType.LeftCorner);
                            GetNewDirection(dir, true);
                            break;
                        case SectionType.RightCorner:
                            GetCorrectGraphic(dir, SectionType.RightCorner);
                            GetNewDirection(dir, false);
                            break;

                    }

                    teller++;
                }
            }
        }

        public static Direction GetNewDirection(Direction dir, bool left)
        {
            switch(dir)
            {
                case Direction.East:
                    if (left)
                        return Direction.North;
                    else
                        return Direction.South;
                case Direction.South:
                    if (left)
                        return Direction.East;
                    else
                        return Direction.West;
                case Direction.West:
                    if (left)
                        return Direction.South;
                    else
                        return Direction.North;
                case Direction.North:
                    if (left)
                        return Direction.West;
                    else
                        return Direction.East;
            }
            return Direction.North;
        }

        public static Graphic GetCorrectGraphic(Direction dir, SectionType type)
        {
            switch(type)
            {
                case SectionType.LeftCorner:
                    switch(dir)
                    {
                        case Direction.East:
                            return leftCorner;
                        case Direction.South:
                            return leftCornerMirrored;
                        case Direction.West:
                            return rightCornerMirrored;
                        case Direction.North:
                            return rightCorner;
                    }
                    break;
                case SectionType.RightCorner:
                    switch(dir)
                    {
                        case Direction.East:
                            return rightCorner;
                        case Direction.South:
                            return rightCornerMirrored;
                        case Direction.West:
                            return leftCornerMirrored;
                        case Direction.North:
                            return leftCorner;
                    }
                    break;
            }
            return null;
        }
        public static string GetGraphic(SectionType type, int line)
        {
            switch (type)
            {
                case SectionType.Straight:
                    return straightHorizontal[line];
                case SectionType.LeftCorner:
                    return leftCorner[line];
                case SectionType.RightCorner:
                    return rightCorner[line];
                case SectionType.Finish:
                    return finishHorizontal[line];
                case SectionType.StartGrid:
                    return startGrid[line];
            }
            return "";
        }
    }
}
