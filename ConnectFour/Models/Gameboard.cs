using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using ConnectFour.Presentation;
using System.Windows.Media;

namespace ConnectFour.Models
{
    public class Gameboard : ObservableObject
    {
        #region ENUMS

        public enum GameboardState
        {
            NewGame,
            PlayerRedTurn,
            PlayerYellowTurn,
            PlayerRedWin,
            PlayerYellowWin,
            Draw
        }
        #endregion

        #region FIELDS

        private const int MAX_COLUMNS = 7;
        private const int MAX_ROWS = 6;

        private SolidColorBrush[][] _currentBoard;
        private SolidColorBrush _currentPlayer;

        #endregion

        #region PROPERTIES

        public int MaxRows
        {
            get { return MAX_ROWS;  }
        }
        public int MaxColumns
        {
            get { return MAX_COLUMNS;  }
        }
        public SolidColorBrush PLAYER_NONE
        {
            get
            {
                SolidColorBrush playerNone = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                return playerNone;
            }
        }
        public SolidColorBrush PLAYER_RED
        {
            get
            {
                SolidColorBrush playerRed = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                return playerRed;
            }
        }
        public SolidColorBrush PLAYER_YELLOW
        {
            get
            {
                SolidColorBrush playerYellow = new SolidColorBrush(Color.FromArgb(255, 255, 255, 0));
                return playerYellow;
            }
        }
        public SolidColorBrush[][] CurrentBoard
        {
            get { return _currentBoard; }
            set
            {
                _currentBoard = value;
                OnPropertyChanged(nameof(CurrentBoard));
            }
        }
        public SolidColorBrush CurrentPlayer
        {
            get { return _currentPlayer; }
            set
            {
                _currentPlayer = value;
                OnPropertyChanged(nameof(CurrentPlayer));
            }
        }
        public GameboardState CurrentRoundState { get; set; }

        #endregion

        #region CONSTRUCTORS

        public Gameboard()
        {
            CurrentBoard = new SolidColorBrush[6][];
            CurrentBoard[0] = new SolidColorBrush[7];
            CurrentBoard[1] = new SolidColorBrush[7];
            CurrentBoard[2] = new SolidColorBrush[7];
            CurrentBoard[3] = new SolidColorBrush[7];
            CurrentBoard[4] = new SolidColorBrush[7];
            CurrentBoard[5] = new SolidColorBrush[7];

            InitalizeGameboard();
        }

        #endregion

        #region METHODS

        public void InitalizeGameboard()
        {
            CurrentRoundState = GameboardState.NewGame;

            for (int row = 0; row < MAX_ROWS; row++)
            {
                for (int column = 0; column < MAX_COLUMNS; column++)
                {
                    CurrentBoard[row][column] = PLAYER_NONE;
                }
            }
        }
        public bool GameboardPositionAvailable(int column)
        {
            for (int row = 0; row < MAX_ROWS; row++)
            {
                if (CurrentBoard[row][column].Color == PLAYER_NONE.Color) return true;
            }

            return false;
        }
        public bool IsTieGame()
        {
            for (int row = 0; row < MAX_ROWS; row++)
            {
                for (int column = 0; column < MAX_COLUMNS; column++)
                {
                    if (CurrentBoard[row][column].Color == PLAYER_NONE.Color) return false;
                }
            }
            return true;
        }
        public void UpdateGameboardState()
        {
            for (int row = 0; row < MAX_ROWS; row++)
            {
                for (int column = 0; column < MAX_COLUMNS; column++)
                {
                    for (int count = row; count <= (MAX_ROWS - 4); count++)
                    {
                        if ((CurrentBoard[count][column].Color == CurrentBoard[count + 1][column].Color)
                            && (CurrentBoard[count + 1][column].Color == CurrentBoard[count + 2][column].Color)
                            && (CurrentBoard[count + 2][column].Color == CurrentBoard[count + 3][column].Color))
                        {
                            if (CurrentBoard[count][column].Color == PLAYER_RED.Color) CurrentRoundState = GameboardState.PlayerRedWin;
                            else if (CurrentBoard[count][column].Color == PLAYER_YELLOW.Color) CurrentRoundState = GameboardState.PlayerYellowWin;
                        }
                        if (column <= (MAX_COLUMNS - 4))
                        {
                            if ((CurrentBoard[count][column].Color == CurrentBoard[count + 1][column + 1].Color)
                               && (CurrentBoard[count + 1][column + 1].Color == CurrentBoard[count + 2][column + 2].Color)
                               && (CurrentBoard[count + 2][column + 2].Color == CurrentBoard[count + 3][column + 3].Color))
                            {
                                if (CurrentBoard[count][column].Color == PLAYER_RED.Color) CurrentRoundState = GameboardState.PlayerRedWin;
                                else if (CurrentBoard[count][column].Color == PLAYER_YELLOW.Color) CurrentRoundState = GameboardState.PlayerYellowWin;
                            }
                        }
                        if (column >= 3)
                        {
                            if ((CurrentBoard[count][column].Color == CurrentBoard[count + 1][column - 1].Color)
                               && (CurrentBoard[count + 1][column - 1].Color == CurrentBoard[count + 2][column - 2].Color)
                               && (CurrentBoard[count + 2][column - 2].Color == CurrentBoard[count + 3][column - 3].Color))
                            {
                                if (CurrentBoard[count][column].Color == PLAYER_RED.Color) CurrentRoundState = GameboardState.PlayerRedWin;
                                else if (CurrentBoard[count][column].Color == PLAYER_YELLOW.Color) CurrentRoundState = GameboardState.PlayerYellowWin;
                            }
                        }
                    }
                    for (int count = column; count <= (MAX_COLUMNS - 4); count++)
                    {
                        if ((CurrentBoard[row][count].Color == CurrentBoard[row][count + 1].Color)
                            && (CurrentBoard[row][count + 1].Color == CurrentBoard[row][count + 2].Color)
                            && (CurrentBoard[row][count + 2].Color == CurrentBoard[row][count + 3].Color))
                        {
                            if (CurrentBoard[row][count].Color == PLAYER_RED.Color) CurrentRoundState = GameboardState.PlayerRedWin;
                            else if (CurrentBoard[row][count].Color == PLAYER_YELLOW.Color) CurrentRoundState = GameboardState.PlayerYellowWin;
                        }
                    }
                }
            }
            if (IsTieGame()) CurrentRoundState = GameboardState.Draw;
        }
        #endregion
    }
}
