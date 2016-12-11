using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PushdownAutomata
{
    public class ConfSet
    {
        //a
        public char ASame { get; set; }
        public char AOther { get; set; }
        public char AEndStack { get; set; }
        //b
        public char BSame { get; set; }
        public char BOther { get; set; }
        public char BEndStack { get; set; }

        public int TmieStep { get; set; }

        public ConfSet()
        {
        }

        public ConfSet(char aSame, char aOther, char aEndStack, char bSame, char bOther, char bEndStack, int timeStep)
        {
            ASame = aSame;
            AOther = AOther;
            AEndStack = aEndStack;

            BSame = bSame;
            BOther = bOther;
            BEndStack = bEndStack;

            TmieStep = timeStep;
        }
    }
}
