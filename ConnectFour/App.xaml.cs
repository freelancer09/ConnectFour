using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ConnectFour.Presentation;

namespace ConnectFour
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public GameViewModel GameViewModel { get; private set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {

            GameView gameView = new GameView();
            gameView.Show();
        }
    }
}
