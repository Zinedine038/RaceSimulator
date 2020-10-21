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
        static Graphic empty;
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
            empty = new Graphic(new string[4] { "    ", "    ", "    ", "    ", });
            #endregion

            var sections = new List<SectionType>();
            sections.Add(SectionType.StartGrid);
            sections.Add(SectionType.Straight);
            sections.Add(SectionType.RightCorner);
            sections.Add(SectionType.RightCorner);
            sections.Add(SectionType.Straight);
            sections.Add(SectionType.Straight);
            sections.Add(SectionType.LeftCorner);
            sections.Add(SectionType.LeftCorner);
            sections.Add(SectionType.Straight);
            sections.Add(SectionType.Straight);
            sections.Add(SectionType.RightCorner);
            sections.Add(SectionType.RightCorner);
            sections.Add(SectionType.Straight);
            sections.Add(SectionType.Finish);
            //sections.Add(SectionType.Finish);

            var testTrack = new Track("silverstone", sections.ToArray());
            if(testTrack.visualTrack!=null)
                DrawTrack(testTrack);

        }

        public static void DrawTrack(Track track)
        {
            for(int x = 0; x<track.visualTrack.GetLength(0); x++)
            {
                for(int line  = 0; line<4; line++)
                {
                    for(int y = 0; y < track.visualTrack.GetLength(1); y++)
                    {
                        if(track.visualTrack[x,y]!=null)
                        {
                            Console.Write(GetGraphic(track.visualTrack[x,y].SectionType).graphic[line]);
                        }
                        else
                        {
                            Console.Write(empty.graphic[line]);
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }


        public static Graphic GetGraphic(SectionType type)
        {
            switch (type)
            {
                case SectionType.Straight:
                    return straightHorizontal;
                case SectionType.LeftCorner:
                    return leftCorner;
                case SectionType.RightCorner:
                    return rightCorner;
                case SectionType.Finish:
                    return finishHorizontal;
                case SectionType.StartGrid:
                    return startGridHorizontal;
            }
            return null;
        }
    }
}
