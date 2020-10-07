using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace View
{
    static class Visualization
    {
        #region graphics
        static string[] finishHorizontal = { "----", "  # ", "  # ", "----" };
        static string[] finishVertical = { "|  |", "|**|", "|  |", "|  |" };
        static string[] straightHorizontal = { "----", "    ", "    ", "----" };
        static string[] straightVertical = { "|  |", "|  |", "|  |", "|  |" };
        static string[] leftCorner = { @"|  |", @"/  |", @"   /", @"--/ " } ;
        static string[] leftCornerMirrored = { @"|  |", @"|  |", @"\    ", @" \--" };
        static string[] rightCorner = { @"--\ ",@"   \",@"\  |",@"|  |"};
        static string[] rightCornerMirrored = { @" /--", @"/   ", @"|   ", @"|  |" };
        static string[] startGrid = { "----", "| | ", " | |", "----" };
        #endregion

        public static void Initialize()
        {
            

        }

        public static void DrawTrack(Track track)
        {

        }
    }
}
