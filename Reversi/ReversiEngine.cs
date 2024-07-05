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

        // Metoda chroniona, która umieszcza pionek na planszy
        // Jeśli parametr test jest true, metoda symuluje ruch bez faktycznego umieszczania pionka
        protected int PutStone(int width, int height, bool test)
        {
            int capturedFieldsCount = 0;

            // Sprawdzenie czy pole jest puste

            if (gameBoard[width, height] != 0) return -1;

            // Przechodzenie przez wszystkie kierunki dookoła pionka

            for (int isHorizontalDirection = -1; isHorizontalDirection <= 1; isHorizontalDirection++)
                for(int isVerticalDirection = -1; isVerticalDirection <= 1; isVerticalDirection++)
                {
                    if(isHorizontalDirection == 0 && isVerticalDirection == 0) continue;

                    // Inicjalizacja zmiennych do sprawdzania kierunku

                    bool hasReachedBoardEdge = false;
                    bool foundEmptyField = false;
                    bool foundPlayerStone = false;
                    bool foundEnemyStone = false;
                    int i = width;
                    int j = height;

                    // Przesuwanie w kierunku, aż do napotkania krawędzi planszy lub innego pionka

                    do
                    {
                        i += isHorizontalDirection;
                        j += isVerticalDirection;
                        if (!isBoardCoordinateValid(i, j))
                            hasReachedBoardEdge = true;

                        if (!hasReachedBoardEdge)
                        {
                            if (gameBoard[i, j] == NextPlayerNumber)
                                foundPlayerStone = true;
                            if (gameBoard[i, j] == opponentId(NextPlayerNumber))
                                foundEnemyStone = true;
                            if (gameBoard[i, j] == 0)
                                foundEmptyField = true;
                        }
                    }
                    while (!(hasReachedBoardEdge || foundEmptyField || foundPlayerStone));

                    // Sprawdzenie czy można umieścić pionek

                    bool canPlaceStone = foundEnemyStone && foundPlayerStone != foundEmptyField;

                    if (canPlaceStone)
                    {
                        int maxIndex = Math.Max(Math.Abs(i - width), Math.Abs(j - height));

                        // Faktyczne umieszczenie pionka, jeśli test jest false

                        if (!test)
                        {
                            for (int index = 0; index < maxIndex; index++)
                            gameBoard[width + index * isHorizontalDirection, height + index * isVerticalDirection] = NextPlayerNumber;
                        }   
                        
                        capturedFieldsCount = capturedFieldsCount + maxIndex - 1;
                    }
                }

            // Zmiana gracza, jeśli pionek został umieszczony i to nie był test

            if (capturedFieldsCount > 0 && !test)
                changePlayer();

            return capturedFieldsCount;
        }

        // Publiczna metoda do umieszczania pionka, wywołuje metodę chronioną z test = false

        public bool PutStone(int width, int height)
        {
            return PutStone(width, height, false) > 0;
        }

        // Prywatna metoda zmieniająca gracza
        private void changePlayer()
        {
            NextPlayerNumber = opponentId(NextPlayerNumber);
        }
    }
}
