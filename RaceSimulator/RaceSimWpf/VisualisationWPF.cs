using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Media.Imaging;

namespace RaceSimWpf
{
    public static class VisualisationWPF
    {
        public const string Finish = "Images\\Track\\Finish.png";
        public const string StraightVertical = "Images\\Track\\StraightVer.png";
        public const string StraightHorizontal = "Images\\Track\\StraightHor.png";
        public const string CornerLeft = "Images\\Track\\CornerLeft.png";
        public const string CornerLeftMirrored = "Images\\Track\\CornerLeftMirrored.png";
        public const string CornerRight = "Images\\Track\\CornerRight.png";
        public const string CornerRightMirrored = "Images\\Track\\CornerRightMirrored.png";
        public const string StartGrid = "Images\\Track\\StartGrid.png";
        public const string Empty = "Images\\Track\\Empty.png";
        public const string Cars = "Images\\Cars\\";
        public const string Broken = "Images\\Cars\\Equipment_Broke.png";
        public const int sectionWidth = 256;

        public static BitmapSource DrawTrack(Track track)
        {
            string root = System.AppDomain.CurrentDomain.BaseDirectory;
            var empty = Caching.GetTrackSize(1024, 1024);
            int xCord = 0;
            int yCord = 0;

            for (int height = 0; height < track.visualTrack.GetLength(0); height++)
            {
                for (int width = 0; width < track.visualTrack.GetLength(1); width ++)
                {
                    Section current = track.visualTrack[height, width];
                    if (current != null)
                    {
                        SectionData currentData = Data.CurrentRace.GetSectionData(current);
                        Direction dir = track.visualTrack[height, width].dir;
                        using (Graphics gfx = Graphics.FromImage(empty))
                        {
                            gfx.DrawImage(Caching.GetBitMap(root + GetGraphic(current.SectionType,dir)), xCord,yCord,256,256);
                        }
                        DrawParticipants(xCord, yCord, currentData, dir,empty);
                    }
                    else
                    {
                        using (Graphics gfx = Graphics.FromImage(empty))
                        {
                            gfx.DrawImage(Caching.GetBitMap(root + Empty), xCord, yCord, 256, 256);
                        }
                    }
                    xCord += 256;
                }
                xCord = 0;
                yCord += 256;
            }
            
            return Caching.CreateBitmapSourceFromGdiBitmap(empty);

        }

        private static void DrawParticipants(int x, int y, SectionData sectiondata, Direction dir,Bitmap empty)
        {
            if(sectiondata.LeftParticipant!=null)
            {
                using (Graphics gfx = Graphics.FromImage(empty))
                {
                    gfx.DrawImage(Caching.GetBitMap(GetDriverImage(sectiondata.LeftParticipant,dir)), x+128, y+40, 64, 64);
                    if(sectiondata.LeftParticipant.Equipment.IsBroken)
                        gfx.DrawImage(Caching.GetBitMap(System.AppDomain.CurrentDomain.BaseDirectory+Broken), x + 128, y + 40, 64, 64);
                }
            }
            if(sectiondata.RightParticipant!=null)
            {
                using (Graphics gfx = Graphics.FromImage(empty))
                {
                    gfx.DrawImage(Caching.GetBitMap(GetDriverImage(sectiondata.RightParticipant, dir)), x+128, y+128, 64, 64);
                    if (sectiondata.RightParticipant.Equipment.IsBroken)
                        gfx.DrawImage(Caching.GetBitMap(System.AppDomain.CurrentDomain.BaseDirectory + Broken), x + 128, y + 128, 64, 64);
                }
            }
        }

        private static string GetDriverImage(IParticipant participant, Direction dir)
        {
            string filename = $"{System.AppDomain.CurrentDomain.BaseDirectory}{Cars}{participant.TeamColour.ToString()}_{dir.ToString()}.png";
            return filename;
        }

        private static string GetGraphic(SectionType type, Direction dir)
        {

            if (type == SectionType.Straight)
            {
                if (dir == Direction.North || dir == Direction.South)
                    return StraightVertical;
                else
                    return StraightHorizontal;
            }
            if(type == SectionType.LeftCorner)
            {
                if (dir == Direction.North)
                    return CornerLeft;
                if (dir == Direction.East)
                    return CornerLeftMirrored;
                if (dir == Direction.South)
                    return CornerRightMirrored;
                if (dir == Direction.West)
                    return CornerRight;
            }
            if (type == SectionType.RightCorner)
            {
                if (dir == Direction.North)
                    return CornerRight;
                if (dir == Direction.East)
                    return CornerLeft;
                if (dir == Direction.South)
                    return CornerLeftMirrored;
                if (dir == Direction.West)
                    return CornerRightMirrored;
            }
            if(type==SectionType.Finish)
            {
                return Finish;
            }
            if (type == SectionType.StartGrid)
            {
                return StartGrid;
            }
            return Empty;
        }
    }

}
