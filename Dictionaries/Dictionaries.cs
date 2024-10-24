using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Educational_Project
{
    public enum SquareColour
    {
        White = 0,
        Black = 1
    }


    public static class Dictionaries
    {
        public static Dictionary<SquareColour, string> squareColourToImage = new Dictionary<SquareColour, string>()
        {
            { SquareColour.White, "Images/White_Square.png" },
            { SquareColour.Black,"Images/Black_Square.png" }
        };
    }
}
