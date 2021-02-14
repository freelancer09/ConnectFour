using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectFour.Presentation;

namespace ConnectFour.Models
{
    public class Player : ObservableObject
    {
        private string _name;
        private int _wins;
        private int _losses;
        private int _ties;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public int Wins
        {
            get { return _wins; }
            set
            {
                _wins = value;
                OnPropertyChanged(nameof(Wins));
            }
        }
        public int Losses
        {
            get { return _losses; }
            set
            {
                _losses = value;
                OnPropertyChanged(nameof(Losses));
            }
        }
        public int Ties
        {
            get { return _ties; }
            set
            {
                _ties = value;
                OnPropertyChanged(nameof(Ties));
            }
        }
        public Player() 
        {
            _wins = 0;
            _losses = 0;
            _ties = 0;
        }
    }
}
