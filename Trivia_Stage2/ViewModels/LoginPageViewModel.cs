using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Trivia_Stage2.Models;
using Trivia_Stage2.Services;
namespace Trivia_Stage2.ViewModels
{
    public class LoginPageViewModel : ViewModel
    {
        private Service service;
        private bool logged;
        public bool Logged { get { return logged; } set {  logged = value; OnPropertyChanged(); } }
        private string password;
        public string Password { get { return password; } set { password = value; OnPropertyChanged(); ((Command)LoginCommand).ChangeCanExecute(); ((Command)CancelCommand).ChangeCanExecute(); } }
        private string notif;
        public string Notif { get { return notif; } set { notif = value; OnPropertyChanged(); } }
        private Color notifColor;
        public Color NotifColor { get { return notifColor; } set { notifColor = value; OnPropertyChanged(); } }
        private string playerName;
        public string PlayerName { get { return playerName; } set { playerName = value; OnPropertyChanged(); ((Command)LoginCommand).ChangeCanExecute(); ((Command)CancelCommand).ChangeCanExecute(); } }
        public ICommand LoginCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public LoginPageViewModel(Service s)
        {
            service = s;
            LoginCommand = new Command(Login, () => !string.IsNullOrEmpty(PlayerName) && !string.IsNullOrEmpty(Password));
            CancelCommand = new Command(Cancel, () => !string.IsNullOrEmpty(PlayerName) || !string.IsNullOrEmpty(Password));
            Logged = false;
        }

        private void Login()
        {
            if (service.LogPlayer(playerName,password))
            {
                Notif = "Login succeeded successfully";
                NotifColor = Colors.Green;
                Logged = true;
                AppShell.Current.GoToAsync("MenuPage");
            }
            else
            {
                Notif = "Login failed failfully";
                NotifColor = Colors.Red;
            }
        }
        private void Cancel()
        {
            Notif = string.Empty;
            PlayerName = string.Empty;
            Password = string.Empty;
        }
    }
}
