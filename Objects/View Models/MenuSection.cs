using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Chess_Educational_Project
{
    /// <summary>
    /// The object that represents a complete section in the menu.
    /// </summary>
    public class MenuSection
    {
        /// <summary>
        /// The header title of the section.
        /// </summary>
        public string header { get; set; }

        /// <summary>
        /// All of the buttons in that section.
        /// </summary>
        public List<MenuButton> menuButtons { get; set; }


        /// <summary>
        /// The base constructor.
        /// </summary>
        /// <param name="header"><inheritdoc/></param>
        /// <param name="menuButtons"<inheritdoc/></param>
        public MenuSection(string header, List<MenuButton> menuButtons)
        {
            this.header = header;
            this.menuButtons = menuButtons;
        }
    }
}
