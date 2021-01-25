using Model;
using System;
using System.Collections.Generic;
using System.Text;
using Model;
using System.Linq;
using Controller;

namespace View
{
    static class Visualization
    {
        #region graphics
        static Graphic finish;
        static Graphic straight;
        static Graphic leftCorner;
        static Graphic rightCorner;
        static Graphic startGrid;
        static Graphic empty;
        #endregion
        const string buffer = "                                       ";
        public static void OnDriversChanged(object sender, DriversChangedEventArgs e)
        {
            DrawTrack(e.Track);
        }

        public static void Initialize()
        {
            Data.CurrentRace.DriversChanged += OnDriversChanged;
            #region graphic declaration
            finish = new Graphic(new string[4, 4] { { "────", "  2║", "  1║", "────" },
                                                    { "|  |", "| |", "|12|", "|══|"},
                                                    { "────", "║1  ", "║2  ", "────" },
                                                    { "|══|", "|21|", "|  |", "|  |"}});
            straight = new Graphic(new string[4,4] { { "────", "   2", "   1", "────" },
                                                     { "|  |", "|  |", "|12|", "|  |" },
                                                     { "────", "1   ", "2   ", "────" },
                                                     { "|  |", "|21|", "|  |", "|  |" }});

            leftCorner = new Graphic(new string[4,4] { { @"┘  |", @" 2 |", @"  1|", @"───┘" },
                                                       { @"|  └", @"| 2 ", @"|1  ", @"└───" },
                                                       { @"┌───", @"|1  ", @"| 2 ", @"|  ┌" } ,
                                                       { @"───┐", @"  1|", @" 2 |", @"┐  |" }});
            rightCorner = new Graphic(new string[4,4] { { @"───┐", @"  2|", @" 1 |", @"┐  |" }, 
                                                        { @"┘  |", @" 1 |", @"  2|", @"───┘" }, 
                                                        { @"|  └", @"| 1 ", @"|2  ", @"└───" }, 
                                                        { @"┌───", @"|2  ", @"| 1 ", @"|  ┌" } });
            startGrid = new Graphic(new string[2, 4] { {"────", "|2| ", " |1|", "────" } ,
                                                                { "|- |", "| -|", "|- |", "| -|" }});
            empty = new Graphic(new string[1,4] { { "    ", "    ", "    ", "    " } });
            #endregion

        }

        public static void DrawTrack(Track track)
        {
            Console.SetCursorPosition(0,0);
            Console.WriteLine(track.Name);
            for(int x = 0; x<track.visualTrack.GetLength(0); x++)
            {
                for(int line  = 0; line<4; line++)
                {
                    for(int y = 0; y < track.visualTrack.GetLength(1); y++)
                    {
                        if(track.visualTrack[x,y]!=null)
                        {
                            int dir = (int)track.visualTrack[x, y].dir;
                            string graphic = DrawParticipants(GetGraphic(track.visualTrack[x, y].SectionType).graphic[dir, line],
                                                              Data.CurrentRace.GetSectionData(track.visualTrack[x, y]).RightParticipant,
                                                              Data.CurrentRace.GetSectionData(track.visualTrack[x, y]).LeftParticipant);
                            Console.Write(graphic);
                        }
                        else
                        {
                            Console.Write(empty.graphic[0,line]);
                        }
                    }
                    Console.WriteLine();
                }
            }
            PrintRaceInfo();

        }

        private static void PrintRaceInfo()
        {
            foreach (IParticipant p in Data.CurrentRace.Participants)
            {
                if (p.LapsInCurrentRace <= Data.CurrentRace.lapsToFinish)
                    Console.WriteLine($"{p.Name} is on lap {p.LapsInCurrentRace}/{Data.CurrentRace.lapsToFinish}");
                else
                    Console.WriteLine($"{p.Name} has finished!");
            }
            Console.WriteLine($"Currently the most overtakes are done by: { Data.Competition.Overtakes.GetBestParticipant() }" + buffer);
        }

        public static void DrawEndOfRaceScreen()
        {
            DrawTrack(Data.CurrentRace.Track);
            Console.WriteLine($"Fastest section in this competition was driven by: { Data.Competition.Overtakes.GetBestParticipant() }" + buffer);
            Console.WriteLine("Next Race Starting soon..");
        }

        public static void DrawEndOfCompetitionScreen()
        {
            Console.Clear();
            Console.WriteLine("Competition Finished!");
            Console.WriteLine($"The Winner is: { Data.Competition.Points.GetBestParticipant() }" );
        }


        private static string DrawParticipants(string graphic, IParticipant right, IParticipant left)
        {
            if(right!=null)
            {
                if(right.Equipment.IsBroken)
                    graphic = graphic.Replace('1', '!');
                else
                    graphic = graphic.Replace('1', right.Name.ToCharArray()[0]);
            }
            else
                graphic = graphic.Replace('1', ' ');
            if(left!=null)
            {
                if (left.Equipment.IsBroken)
                    graphic = graphic.Replace('2', '!');
                else
                    graphic = graphic.Replace('2', left.Name.ToCharArray()[0]);
            }
            else
                graphic = graphic.Replace('2', ' ');
            return graphic;
        }


        private static Graphic GetGraphic(SectionType type)
        {
            switch (type)
            {
                case SectionType.Straight:
                    return straight;
                case SectionType.LeftCorner:
                    return leftCorner;
                case SectionType.RightCorner:
                    return rightCorner;
                case SectionType.Finish:
                    return finish;
                case SectionType.StartGrid:
                    return startGrid;
            }
            return null;
        }
    }
}
