using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Trivia_Stage2.Models;
using Trivia_Stage2.Services;
namespace Trivia_Stage2.ViewModels
{
    public class BestScoresPageViewModel : ViewModel
    {
        private Service service;
        public bool IsReloading;
        public ICommand Reload { get; set; }
        public ICommand Search { get; set; }
        public string SearchBar {  get; set; }
        public bool IsOrdered;
        public Player SelectedPlayer { get { return selectedPlayer; } set { selectedPlayer = value; OnPropertyChanged(); } }
        private Player selectedPlayer;
        public ObservableCollection<Player> players { get; private set; }

        public BestScoresPageViewModel(Service service_)
        {
            IsOrdered = false;
            service = service_;
            players = new ObservableCollection<Player>(service.Players);
            IsReloading = false;
            SelectedPlayer=new Player();
            Reload=new Command(async () => await TaskReload());
            Search = new Command(async () => await TaskSearch());
        }
        public async Task TaskReload()
        {
            if (IsReloading) return;
            IsReloading=true;
            players.Clear();
            foreach (Player plr in service.Players)
                players.Add(plr);
            if (IsOrdered)
            {
                players = new ObservableCollection<Player>(players.OrderByDescending(p => p.Points));
                IsOrdered = false;
            }
            else
            {
                players = new ObservableCollection<Player>(players.OrderBy(p => p.Points));
                IsOrdered = true;
            }
            IsReloading = false;

        }
        public async Task TaskSearch()
        {
            int search = SearchBar;
                players = new ObservableCollection<Player>(players.Where(player => player.RankId == search));
            
        }
    }
}
