using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Trivia_Stage3.Models;
using Trivia_Stage3.Services;
namespace Trivia_Stage3.ViewModels
{
    public class BestScoresPageViewModel : ViewModel
    {
        private Service service;
        public bool IsReloading { get { return isReloading; } set{isReloading=value; OnPropertyChanged(); } }
        private bool isReloading;
        public ICommand Reload { get; set; }
        public ICommand Search { get; set; }
        public bool IsOrdered;
        public ObservableCollection<Rank> Ranks { get; set; }
        public Player SelectedPlayer { get { return selectedPlayer; } set { selectedPlayer = value; OnPropertyChanged(); } }
        private Player selectedPlayer;
        public ObservableCollection<Player> players { get; private set; }
        public Rank SelectedRank { get {return selectedRank; } set { selectedRank = value; OnPropertyChanged(); } }
        private Rank selectedRank;
        public BestScoresPageViewModel(Service service_)
        {
            IsOrdered = false;
            service = service_;
            players = new ObservableCollection<Player>(service.Players);
            IsReloading = false;
            SelectedPlayer=new Player();
            Reload=new Command(async () => await TaskReload());
            Search = new Command(async () => await TaskSearch());
            Ranks = new ObservableCollection<Rank>(service.Ranks);
        }
        public async Task TaskReload() 
        { 
            
            IsReloading = true; 
            players.Clear(); 
            if (IsOrdered) 
            { 
               foreach(Player p in service.Players.OrderByDescending(x => x.Points))
                {
                    players.Add(p);
                }
                IsOrdered = false; 
            }
            else if (!IsOrdered) 
            { 
                foreach(Player p in service.Players.OrderBy(x=>x.Points))
                {
                    players.Add(p);
                }
                IsOrdered = true;
            } 
            IsReloading = false; 
        }
        public async Task TaskSearch()
        {
            
            if (SelectedRank != null)
            {
                try
                {
                    players.Clear();
                    foreach (Player p in service.Players.Where(x => x.RankId == SelectedRank.RankId))
                    {
                        players.Add(p);
                    }
                }
                catch (Exception ex)
                {
                    players.Clear();
                }
            }
        }
    }
}
