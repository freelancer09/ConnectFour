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
using System.Windows.Shapes;
using ConnectFour.Models;

namespace ConnectFour.Presentation
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : Window
    {
        GameViewModel _gameViewModel;

        public GameView()
        {
            _gameViewModel = new GameViewModel();

            DataContext = _gameViewModel;

            InitializeComponent();
        }

        private void Gameboard_Enter(object sender, MouseEventArgs e)
        {
            Ellipse boardPositionButton = sender as Ellipse;
            boardPositionButton.Fill = _gameViewModel.CurrentPlayer;
        }

        private void Gameboard_Leave(object sender, MouseEventArgs e)
        {
            Ellipse boardPositionButton = sender as Ellipse;
            boardPositionButton.Fill = _gameViewModel.PlayerNone;
        }
        private void Gameboard_Click(object sender, MouseButtonEventArgs e)
        {
            Ellipse boardPositionButton = sender as Ellipse;
            int column = int.Parse(boardPositionButton.Tag.ToString());
            _gameViewModel.PlayerMove(column);
            boardPositionButton.Fill = _gameViewModel.PlayerNone;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button windowButton = sender as Button;

            switch (windowButton.Name)
            {
                case "NewGame":
                case "ResetGame":
                    _gameViewModel.GameCommand(windowButton.Name);
                    break;
                case "Help":
                    HelpView helpView = new HelpView();
                    helpView.ShowDialog();
                    break;
                case "Quit":
                    Close();
                    break;
            }
        }
    }
}
