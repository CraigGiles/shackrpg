using System;
using System.Collections.Generic;
using System.Text;

namespace DummyProject.HelperClasses
{
    class RandomHelper
    {
        static public int GetRandomInt()
        {
            Random rNum = new Random();
            int randomNumber = rNum.Next();
            return randomNumber;
        }//end GetRandomInt

        static public int GetRandomInt(int min, int max)
        {
            Random rNum = new Random();
            int randomNumber = rNum.Next(min, max);
            return randomNumber;
        }//end GetRandomInt(min,max)

    }//end random helper
}//end namespace
