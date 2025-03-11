using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Chess_Educational_Project
{
    /// <summary>
    /// An object for the ViewModel in the main menu. Each instance of this object will be bound to a button.
    /// </summary>
    public class MenuButton
    {
        /// <summary>
        /// The text to be displayed on the button.
        /// </summary>
        public string buttonText { get; set; }

        /// <summary>
        /// The image to be displayed on the button.
        /// </summary>
        public BitmapImage image { get; set; }
        /// <summary>
        /// The type of the window to open when the button is pressed.
        /// </summary>
        public Type targetWindow{ get; set; }

        /// <summary>
        /// The base constructor.
        /// </summary>
        /// <param name="buttonText"><inheritdoc/></param>
        /// <param name="image"><inheritdoc/></param>
        /// <param name="targetWindow"><inheritdoc/></param>
        public MenuButton(string buttonText, BitmapImage image, Type targetWindow)
        {
            this.buttonText = buttonText;
            this.image = image;
            this.targetWindow = targetWindow;
        }
    }
}
