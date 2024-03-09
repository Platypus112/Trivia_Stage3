using Microsoft.IdentityModel.Tokens;
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
    public class UserAdminPageViewModel : ViewModel
    {
        private Service service;
        private ObservableCollection<Player> players;
        public ObservableCollection<Player> Players { get { return players; }set { players = value;OnPropertyChanged(); } }
        private bool isRefreshing;
        public bool IsRefreshing { get { return isRefreshing; } set { isRefreshing = value; OnPropertyChanged(); } }
        private Color errorColor;
        public Color ErrorColor { get { return errorColor; } set { errorColor = value;OnPropertyChanged(); } }
        private string errorMsg;
        public string ErrorMsg { get { return errorMsg; } set { this.errorMsg = value;OnPropertyChanged(); } }
        private string emailEntry;
        public string EmailEntry { get { return emailEntry; } set { this.emailEntry = value;OnPropertyChanged(); ((Command)AddPlayerCommand).ChangeCanExecute(); } }
        private string passwordEntry;
        public string PasswordEntry { get { return passwordEntry; } set { this.passwordEntry = value; OnPropertyChanged(); ((Command)AddPlayerCommand).ChangeCanExecute(); } }
        private string playerNameEntry;
        public string PlayerNameEntry { get { return playerNameEntry; } set { this.playerNameEntry = value; OnPropertyChanged();((Command)AddPlayerCommand).ChangeCanExecute(); } }
        public ICommand AddPlayerCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand SortByRankCommand {  get; private set; }
        public ICommand ResetPlayerPointsCommand { get; private set; }
        public ICommand RemovePlayerCommand {  get; private set; }
        
        public UserAdminPageViewModel(Service service_)
        {
            service = service_;
            IsRefreshing = false;
            AddPlayerCommand = new Command(async ()=>AddPlayer(),()=>!EmailEntry.IsNullOrEmpty()&&!PasswordEntry.IsNullOrEmpty()&&!PlayerNameEntry.IsNullOrEmpty());
            RefreshCommand = new Command(async () => Refresh());
            SortByRankCommand = new Command(async ()=>SortByRank());
            ResetPlayerPointsCommand = new Command(async (Object obj) => ResetPlayerPoints(obj));
            RemovePlayerCommand = new Command(async (Object obj) => RemovePlayer(obj));
            Refresh();
        }
        private async void RemovePlayer(Object obj)
        {
            try
            {
                if (await AppShell.Current.DisplayAlert("Delete player?","Are you sure you want to delete this player?", "No", "Yes"))
                {
                    ErrorMsg = "Deletion Cancelled";
                    ErrorColor = Color.Parse("Red");
                    return;
                }
                if (service.PlayerIsLogged()&&((Player)obj).PlayerId == service.LoggedPlayer.PlayerId)
                {
                    ErrorMsg = "Can't delete yourself";
                    ErrorColor = Color.Parse("Red");
                }
                else if (service.RemovePlayer((Player)obj))
                {
                    ErrorMsg = "Player deleted successfuly";
                    ErrorColor = Color.Parse("Green");
                }
                else
                {
                    ErrorMsg = "Deleting player didn't work";
                    ErrorColor = Color.Parse("Red");
                }
                Refresh();
            }
            catch
            {
                ErrorMsg = "Deleting player didn't work";
                ErrorColor = Color.Parse("Red");
            }
        }
        private async void ResetPlayerPoints(Object obj)
        {
            
            if (await AppShell.Current.DisplayAlert("Reset player points?", "Are you sure you want to reset this player's points?","No","Yes"))
            {
                ErrorMsg = "Reset Cancelled";
                ErrorColor = Color.Parse("Red");
                return;
            }
            try
            {
                if(service.ResetPlayerPoints((Player)obj))
                {
                    ErrorMsg = "Player points reset successfuly";
                    ErrorColor = Color.Parse("Green");
                }
                else
                {
                    ErrorMsg = "Reseting points didn't work";
                    ErrorColor = Color.Parse("Red");
                }
                Refresh();
            }
            catch
            {
                ErrorMsg = "Reseting points didn't work";
                ErrorColor = Color.Parse("Red");
            }
        }
        private async void SortByRank()
        {
            Players = new ObservableCollection<Player>(service.GetPlayersSortedByRank());
        }
        private async void AddPlayer()
        {
            if (service.AddNewPlayer(PlayerNameEntry, PasswordEntry, EmailEntry))
            {
                ErrorMsg = "Player added successfuly";
                ErrorColor = Color.Parse("Green");
                Refresh();
            }
            else
            {
                ErrorMsg = "Error adding player";
                ErrorColor = Color.Parse("Red");
                Refresh();
            }
        }
        public async void Refresh()
        {
            IsRefreshing=true;
            PlayerNameEntry = "";
            EmailEntry = "";
            PasswordEntry = "";
            Players = new ObservableCollection<Player>(service.GetPlayersSortedById());
            IsRefreshing = false;
        }
    }
}
