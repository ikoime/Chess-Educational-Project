using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Chess_Educational_Project
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            
            InitializeComponent();
            //Creating a collection which will be under the play heading.
            List<MenuButton> playList = new List<MenuButton>()
            {
                {new MenuButton("Play chess",Chess_Educational_Project.Resources.CreateImageSource("pack://siteoforigin:,,,/Images/cburnett/wP.png"),typeof(ChessGame))},
                {new MenuButton("FEN importing",Chess_Educational_Project.Resources.CreateImageSource("pack://siteoforigin:,,,/Images/cburnett/wK.png"),typeof(FENImporting))},

            };
            //Creating a collection which will be under the learn heading.
            List<MenuButton> learnList = new List<MenuButton>()
            {
                {new MenuButton("Tutorial",Chess_Educational_Project.Resources.CreateImageSource("pack://siteoforigin:,,,/Images/cburnett/bN.png"),typeof(Tutorial))},
            };

            //Creating the collection which will be bound to the ItemsControl.
            List<MenuSection> menuSections = new List<MenuSection>()
            {
                {new MenuSection("Play",playList)},
                {new MenuSection("Learn",learnList) },
            };

            itc_Buttons.ItemsSource = menuSections;
        }
        /// <summary>
        /// The Click event handler for the buttons in the menu. Opens the window linked to that button.
        /// </summary>
        /// <param name="sender">The button that was clicked.</param>
        /// <param name="e">Related information.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Gets the button that is clicked on.
            Button clickedOption = sender as Button;

            //This button is bound to our view model, so I get the instance of MenuButton that it is bound to.
            MenuButton contextInfo = clickedOption.DataContext as MenuButton;

            //I then open the window which is linked to that instance.
            Window windowToOpen = WindowSwitch(contextInfo.targetWindow);
            windowToOpen.Show();
        }
        /// <summary>
        /// Converts a Type into the correct window, and returns an instance of that window.
        /// </summary>
        /// <param name="type">The type to be converted.</param>
        /// <returns>The window instance.</returns>
        public Window WindowSwitch(Type type)
        {
            if (type == typeof(ChessGame))
            {
                return new ChessGame(new Board());
            }
            else if(type == typeof(FENImporting))
            {
                return new FENImporting();
            }
            else
            {
                return new Tutorial();
            }
        }
    }
}
