using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace tictactoe_windowsapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members

        /// <summary>
        /// Hold Current results of cells
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// True if is Player 1 turn 
        /// </summary>
        private bool mPlayer1Turn;
        /// <summary>
        /// True if game ended
        /// </summary>
        private bool GameEnded;

        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }


        #endregion
        /// <summary>
        /// starts a new game
        /// </summary>
        private void NewGame()
        {
            ///Creates new blank array of free cells
            mResults = new MarkType[9];
            for(var i=0; i < mResults.Length; i++)
            {
                mResults[i] = MarkType.Free;
            }
            //make sure Player1 Starts the game
            mPlayer1Turn = true;

            //interate every button of the grid
            Container.Children.Cast<Button>().ToList().ForEach(button => 
            {
                //changes content and colours to default
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            //makes sure game is not ended
            GameEnded = false;
        }
        /// <summary>
        /// Handles a new click event
        /// </summary>
        /// <param name="sender">the button that was clicked</param>
        /// <param name="e">The event that object clicked</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //when game ended and clicked anywhere it resets the game
            if (GameEnded)
            {
                NewGame();
                return;
            }
            //cast sender to a button
            var Button = (Button)sender;
            //find buttons in aray
            var column = Grid.GetColumn(Button);
            var row = Grid.GetRow(Button);

            //var index = (column+1) * 3 + row;
            var index = column + ( row * 3 );

            //if it has value in it do nothing
            if (mResults[index] != MarkType.Free)
                return;

            //set the cell value based on witch player move it is
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;
            Button.Content = mPlayer1Turn ? "X" : "O";

            //changes nords to green
            if (!mPlayer1Turn)
            {
                Button.Foreground = Brushes.Red;
            }

            //changes players turn
            mPlayer1Turn ^= true;

            //Check for a winner
            CheckForAWinner();
        }
        /// <summary>
        /// Checks if there is a winner 
        /// </summary>
        private void CheckForAWinner()
        {
            #region horizontal win
            //check for horizontal wins
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
                {
                    GameEnded = true;
                //highlight winning cells in green;
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;

                }
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                GameEnded = true;
                //highlight winning cells in green;
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;

            }
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                GameEnded = true;
                //highlight winning cells in green;
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;

            }
            #endregion
            #region Vertical win
            //vertical wins
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                GameEnded = true;
                //highlight winning cells in green;
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;

            }
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                GameEnded = true;
                //highlight winning cells in green;
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;

            }
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                GameEnded = true;
                //highlight winning cells in green;
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;

            }
            #endregion

            #region Diagonal Win
            //check diagonal wins

            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                GameEnded = true;
                //highlight winning cells in green;
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;

            }

            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                GameEnded = true;
                //highlight winning cells in green;
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;

            }
            #endregion

            #region Full board no winners
            //Check for no winner and full board
            if (!mResults.Any(result => result == MarkType.Free))
            {
                //Game ended turn all cells orange
                GameEnded = true;
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    //changes content and colours to default
                    button.Background = Brushes.Orange;
                });
            }
            #endregion
        }
    }
}
