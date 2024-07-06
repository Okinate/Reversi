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

namespace Reversi
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ReversiEngine engine = new ReversiEngine(1);

        private SolidColorBrush[] colors = { Brushes.Beige, Brushes.Green, Brushes.Brown };

        string[] playerNames = { "","Zielony", "Brązowy" };

        private Button[,] gameBoard;

        private struct FieldCoordinates
        {
            public int Width, Height;
        }
        private void syncBoardContent()
        {
            for(int i = 0; i < engine.BoardWidth; i++)
                for (int j = 0; j < engine.BoardHeight; j++)
                {
                    gameBoard[i, j].Background = colors[engine.GetBoardFieldState(i,j)];
                    gameBoard[i, j].Content = engine.GetBoardFieldState(i, j).ToString();
                }
            ButtonPlayerColor.Background = colors[engine.NextPlayerNumber];
            GreenPlayerCount.Text = engine.fieldCountPlayer1.ToString();
            BrownPlayerCount.Text = engine.fieldCountPlayer2.ToString();
        }

        private static string fieldSymbol(int Width, int Height)
        {
            if(Width > 25 ||  Height > 8)
                return "(" + Width.ToString() + "," + Height.ToString() + ")";
            return "" + "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Width] + "123456789"[Height];
        }

        //przekazywanie właściwości klikniętego pola
        void clickField(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            FieldCoordinates coordinates = (FieldCoordinates)clickedButton.Tag;

            int savedPlayerNumber = engine.NextPlayerNumber;

            if (engine.PutStone(coordinates.Width, coordinates.Height))
            { 
                syncBoardContent();
                if (savedPlayerNumber == 1)
                    GreenMoveList.Items.Add(fieldSymbol(coordinates.Width, coordinates.Height));
                if (savedPlayerNumber == 2)
                    BrownMoveList.Items.Add(fieldSymbol(coordinates.Width, coordinates.Height));
            }
        }

        public MainWindow()
        {
            InitializeComponent();

                //GameBoard Draw
            for (int i = 0; i < engine.BoardWidth; i++)
                GridBoard.ColumnDefinitions.Add(new ColumnDefinition());
            for (int i = 0; i < engine.BoardHeight; i++)
                GridBoard.RowDefinitions.Add(new RowDefinition());
            gameBoard = new Button[engine.BoardWidth, engine.BoardHeight];

            for (int i = 0; i < engine.BoardWidth; i++)
                for(int j = 0; j < engine.BoardHeight; j++)
                {
                    Button button = new Button();
                    button.Margin = new Thickness(0);
                    GridBoard.Children.Add(button);
                    Grid.SetColumn(button, i);
                    Grid.SetRow(button, j);
                    //Przypisanie koordynantów pola
                    button.Tag = new FieldCoordinates { Width = i , Height = j };
                    button.Click += new RoutedEventHandler(clickField);
                    gameBoard[i,j] = button;
                }
            //engine.PutStone(3, 5);
            //engine.PutStone(2, 3);
            syncBoardContent();
                      
        }
        #region Menu Methods
        private void StartGameWithBronzePlayer(object sender, RoutedEventArgs e)
        {

        }

        private void StartGameWithGreenPlayer(object sender, RoutedEventArgs e)
        {

        }

        private void StartGameWithBothPlayers(object sender, RoutedEventArgs e)
        {

        }

        private void CloseGame(object sender, RoutedEventArgs e)
        {

        }

        private void ShowMoveHint(object sender, RoutedEventArgs e)
        {

        }

        private void ShowLastOpponentMove(object sender, RoutedEventArgs e)
        {

        }

        private void ShowGameRules(object sender, RoutedEventArgs e)
        {

        }

        private void ShowComputerStrategy(object sender, RoutedEventArgs e)
        {

        }

        private void ShowAbout(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}