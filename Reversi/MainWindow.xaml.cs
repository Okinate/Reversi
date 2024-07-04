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
        private Button[,] GameBoard;
        public MainWindow()
        {
            InitializeComponent();

                //GameBoard Draw
            for (int i = 0; i < 8; i++)
                GridBoard.ColumnDefinitions.Add(new ColumnDefinition());
            for (int i = 0; i < 8; i++)
                GridBoard.RowDefinitions.Add(new RowDefinition());
            GameBoard = new Button[8, 8];

            for (int i = 0; i < 8; i++)
                for(int j = 0; j < 8; j++)
                {
                    Button button = new Button();
                    button.Margin = new Thickness(0);
                    GridBoard.Children.Add(button);
                    Grid.SetColumn(button, i);
                    Grid.SetRow(button, j);
                    GameBoard[i,j] = button;
                }
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