using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace RaceSimWpf
{
    public static class VisualisationWPF
    {
        public const string Finish = ".\\Images\\Track\\Finish.png";
        public const string StraightVertical = ".\\Images\\Track\\StraightVer.png";
        public const string StraightHorizontal = ".\\Images\\Track\\StraightHor.png";
        public const string CornerLeft = ".\\Images\\Track\\CornerLeft.png";
        public const string CornerLeftMirrored = ".\\Images\\Track\\CornerLeftMirrored.png";
        public const string CornerRight = ".\\Images\\Track\\CornerRight.png";
        public const string CornerRightMirrored = ".\\Images\\Track\\CornerRightMirrored.png";
        public const string StartGrid = ".\\Images\\Track\\StartGrid.png";

        public static BitmapSource DrawTrack(Track track)
        {
            return Caching.CreateBitmapSourceFromGdiBitmap(Caching.GetTrackSize(4,4));
        }
    }
}
