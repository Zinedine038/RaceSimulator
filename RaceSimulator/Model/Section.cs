using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public enum SectionType
    {
        Straight,
        LeftCorner,
        RightCorner,
        StartGrid,
        Finish
    }

    public class Section
    {
        public SectionType SectionType { get; set; }
    }
}
