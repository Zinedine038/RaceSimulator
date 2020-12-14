using System;
using System.Collections.Generic;
using System.Text;
 

namespace Model
{
    public class Graphic
    {
        public string[,] graphic { get; set; }
        
        public Graphic(string[,] design)
        {
            graphic = design;
        }
        
    }
}
