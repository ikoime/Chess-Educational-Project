using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Educational_Project
{
    /// <summary>
    /// The object representing each individual tutorial.
    /// </summary>
    public class TutorialInfo
    {
        /// <summary>
        /// The FEN information for the starting board state of that tutorial.
        /// </summary>
        public string? fen { get; set; }
        /// <summary>
        /// The text that will be displayed to explain the tutorial.
        /// </summary>
        public string? text { get; set; }
    }
}
