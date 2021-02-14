using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectFour.Models;
using System.Windows.Media;

namespace ConnectFour.Presentation
{
    public class GameViewModel : ObservableObject
    {
        private Gameboard _gameboard;
        private Player _playerRed = new Player();
        private Player _playerYellow = new Player();
        private string _statusMessage;

        public Gameboard Gameboard
        {
            get { return _gameboard; }
            set
            {
                _gameboard = value;
                OnPropertyChanged(nameof(Gameboard));
            }
        }
        public Player PlayerRed
        {
            get { return _playerRed; }
            set
            {
                _playerRed = value;
                OnPropertyChanged(nameof(PlayerRed));
            }
        }
        public Player PlayerYellow
        {
            get { return _playerYellow; }
            set
            {
                _playerYellow = value;
                OnPropertyChanged(nameof(PlayerYellow));
            }
        }
        public string StatusMessage
        {
            get { return _statusMessage; }
            set
            {
                _statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
            }
        }
        public GameViewModel()
        {
            InitializeGame();
        }
        private void InitializeGame()
        {
            _playerRed.Name = "Player Red";
            _playerYellow.Name = "Player Yellow";
            _statusMessage = PlayerRed.Name + " turn";
            
            _gameboard = new Gameboard();

            _gameboard.CurrentRoundState = Gameboard.GameboardState.PlayerRedTurn;
            _gameboard.CurrentPlayer = _gameboard.PLAYER_RED;

        }
        public SolidColorBrush CurrentPlayer
        {
            get { return Gameboard.CurrentPlayer; }
        }
        public SolidColorBrush PlayerNone
        {
            get { return Gameboard.PLAYER_NONE; }
        }
        public void PlayerMove(int column)
        {
            if (_gameboard.GameboardPositionAvailable(column))
            {
                if ((_gameboard.CurrentRoundState == Gameboard.GameboardState.PlayerRedTurn) || (_gameboard.CurrentRoundState == Gameboard.GameboardState.PlayerYellowTurn))
                {
                    int row = (Gameboard.MaxRows - 1);
                    while (Gameboard.CurrentBoard[row][column].Color != Gameboard.PLAYER_NONE.Color)
                    {
                        row--;
                    }
                    Gameboard.CurrentBoard[row][column] = Gameboard.CurrentPlayer;
                    OnPropertyChanged(nameof(Gameboard));
                    if (_gameboard.CurrentRoundState == Gameboard.GameboardState.PlayerRedTurn)
                    {
                        _gameboard.CurrentRoundState = Gameboard.GameboardState.PlayerYellowTurn;
                        Gameboard.CurrentPlayer = Gameboard.PLAYER_YELLOW;
                        OnPropertyChanged(nameof(CurrentPlayer));
                        _statusMessage = PlayerYellow.Name + " turn";
                        OnPropertyChanged(nameof(StatusMessage));
                    }
                    else if (_gameboard.CurrentRoundState == Gameboard.GameboardState.PlayerYellowTurn)
                    {
                        _gameboard.CurrentRoundState = Gameboard.GameboardState.PlayerRedTurn;
                        Gameboard.CurrentPlayer = Gameboard.PLAYER_RED;
                        OnPropertyChanged(nameof(CurrentPlayer));
                        _statusMessage = PlayerRed.Name + " turn";
                        OnPropertyChanged(nameof(StatusMessage));
                    }
                    UpdateCurrentRoundState();
                }
            }
        }
        internal void GameCommand(string commandName)
        {
            switch (commandName)
            {
                case "NewGame":
                    _gameboard.InitalizeGameboard();
                    OnPropertyChanged(nameof(Gameboard));
                    _gameboard.CurrentRoundState = Gameboard.GameboardState.PlayerRedTurn;
                    _gameboard.CurrentPlayer = _gameboard.PLAYER_RED;
                    _statusMessage = PlayerRed.Name + " turn";
                    OnPropertyChanged(nameof(StatusMessage));
                    break;
                case "ResetGame":
                    PlayerRed.Wins = 0;
                    PlayerRed.Losses = 0;
                    PlayerRed.Ties = 0;
                    PlayerYellow.Wins = 0;
                    PlayerYellow.Losses = 0;
                    PlayerYellow.Ties = 0;
                    break;
            }
        }
        public void UpdateCurrentRoundState()
        {
            _gameboard.UpdateGameboardState();
            if (_gameboard.CurrentRoundState == Gameboard.GameboardState.PlayerRedWin)
            {
                PlayerRed.Wins++;
                PlayerYellow.Losses++;
                _gameboard.CurrentPlayer = PlayerNone;
                _statusMessage = PlayerRed.Name + " wins!";
                OnPropertyChanged(nameof(StatusMessage));
            }
            else if (_gameboard.CurrentRoundState == Gameboard.GameboardState.PlayerYellowWin)
            {
                PlayerYellow.Wins++;
                PlayerRed.Losses++;
                _gameboard.CurrentPlayer = PlayerNone;
                _statusMessage = PlayerYellow.Name + " wins!";
                OnPropertyChanged(nameof(StatusMessage));
            }
            else if (_gameboard.CurrentRoundState == Gameboard.GameboardState.Draw)
            {
                PlayerYellow.Ties++;
                PlayerRed.Ties++;
                _gameboard.CurrentPlayer = PlayerNone; 
                _statusMessage = "Tie game!";
                OnPropertyChanged(nameof(StatusMessage));
            }
        }
    }
}
