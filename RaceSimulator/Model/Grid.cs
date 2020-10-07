using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Grid
    {
        public Section[,] trackGrid;
        public Grid (int x, int y, Track track)
        {
            trackGrid= new Section[x, y];
            for (int i = 0; i < x; i++)
            {
                for(int i2 = 0; i2<y; i2++)
                {
                    trackGrid[i,i2]=track.Sections.First.Value;
                    track.Sections.RemoveFirst();
                }
            }
        }
    }
}
