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
        public bool IsReloading { get; set; }
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
        public async Task TaskReload(){if(IsReloading)return;IsReloading=true;players.Clear();if(IsOrdered){players=new ObservableCollection<Player>(service.Players.OrderByDescending(x=>x.Points));IsOrdered=false;}else if(!IsOrdered){players=new ObservableCollection<Player>(service.Players.OrderBy(x=>x.Points));IsOrdered = true;}IsReloading=false;}
        public async Task TaskSearch()
        {
            
            if (SelectedRank != null)
            {
                try
                {
                     players.Clear();
                    players = new ObservableCollection<Player>(service.Players.Where(x => x.RankId == SelectedRank.RankId));
                }
                catch (Exception ex)
                {
                    players.Clear();
                }
            }
        }
    }
}
