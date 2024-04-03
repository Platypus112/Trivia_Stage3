using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Trivia_Stage3.Services;
using Trivia_Stage3.Models;


namespace Trivia_Stage3.ViewModels
{
    public class RegisterPageViewModel:ViewModel
    {
        private Service service;
        private bool logged;
        public bool Logged { get { return logged; } set { logged = value; OnPropertyChanged(); } }
        private string playerName;
        public string PlayerName { get { return playerName; } set { playerName = value; OnPropertyChanged(); ((Command)RegisterCommand).ChangeCanExecute(); ((Command)CancelCommand).ChangeCanExecute(); } }
        private string email;
        public string Email { get { return email; } set { email = value; OnPropertyChanged(); ((Command)RegisterCommand).ChangeCanExecute(); ((Command)CancelCommand).ChangeCanExecute(); } }
        private string password;
        public string Password { get { return password; } set { password = value; OnPropertyChanged(); ((Command)RegisterCommand).ChangeCanExecute(); ((Command)CancelCommand).ChangeCanExecute(); } }
        private string notif;
        public string Notif { get { return notif; } set { notif = value; OnPropertyChanged(); } }
        private Color notifColor;
        public Color NotifColor { get { return notifColor; } set { notifColor = value; OnPropertyChanged(); } }
        public ICommand RegisterCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public ICommand NavigateToLoginPageCommand { get; private set; }
        public RegisterPageViewModel(Service _service)
        {
            service = _service;
            RegisterCommand = new Command(Register, () => !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password)&&!string.IsNullOrEmpty(PlayerName));
            CancelCommand = new Command(Cancel, () => !string.IsNullOrEmpty(Email) || !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(PlayerName));
            if (Logged != true)
            {
                Logged = false;
                AppShell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
            }
            NavigateToLoginPageCommand = new Command(async () => await AppShell.Current.GoToAsync("///LoginPage"));
        }
        private async void Register()
        {
            if (await service.RegisterPlayer(email,playerName, password))
            {
                Notif = "Login succeeded successfully";
                NotifColor = Colors.Green;
                Logged = true;
                AppShell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
                await AppShell.Current.GoToAsync("///ApproveQuestionsPage");
            }
            else
            {
                Notif = "Login failed failfully";
                NotifColor = Colors.Red;
            }
            Email = string.Empty;
            PlayerName= string.Empty;
            Password = string.Empty;
        }
        private async void Cancel()
        {
            Notif = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
        }
    }
}
