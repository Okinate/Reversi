using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    // Właściwości określające szerokość, wysokość planszy oraz numer gracza, który ma teraz ruch
    internal class ReversiEngine
    {
        public int BoardWidth { get; private set; }
        public int BoardHeight { get; private set; }
        public int NextPlayerNumber { get; private set; } = 1;

        private int[,] gameBoard;

        // Konstruktor inicjalizujący silnik gry Reversi
        public ReversiEngine(int startingPlayerNumber, int boardWidth, int boardHeight)
        {
            if (startingPlayerNumber != 1 && startingPlayerNumber != 2)
                throw new Exception("Nieprawidłowy numer gracza rozpoczynającego grę");
            BoardWidth = boardWidth;
            BoardHeight = boardHeight;
            NextPlayerNumber = startingPlayerNumber;
            gameBoard = new int[BoardWidth, BoardHeight];
            clearBoard();
        }

        // Metoda prywatna do wyczyszczenia planszy i ustawienia początkowych pionków
        private void clearBoard()
        {
            for(int i = 0; i < BoardWidth; i++)
                for(int j = 0; j < BoardHeight; j++)
                    gameBoard[i, j] = 0;
            int centerWidth = BoardWidth / 2;
            int centerHeight = BoardHeight / 2;
            gameBoard[centerWidth, centerHeight] = 1;
            gameBoard[centerWidth - 1, centerHeight - 1] = 1;
            gameBoard[centerWidth - 1, centerWidth] = 2;
            gameBoard[centerWidth, centerWidth - 1] = 2;
        }

        // Metoda publiczna zwracająca stan pola na planszy o podanych współrzędnych
        public int GetBoardFieldState (int Width , int Height)
        {
            if (isBoardCoordinateValid(Width, Height))
                throw new Exception("Nieprawidłowe współrzędne pola");

            return gameBoard[Width, Height];
        }

        // Metoda prywatna sprawdzająca poprawność współrzędnych pola na planszy
        private bool isBoardCoordinateValid (int Width, int Height)
        {
            if (Width >= 0 && Height >= 0 && Width < BoardWidth && Height < BoardHeight)
                return true;
                return false;
        }

        // Metoda statyczna zwracająca numer przeciwnika danego gracza
        private static int opponentId (int playerId)
        {
            if (playerId == 1) return 2;
            return 1;
        }
    }
}
