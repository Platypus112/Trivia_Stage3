using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace Trivia_Stage3.ViewModels
{
    public class RegisterPageViewModel:ViewModel
    {
        private Service service
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
        public RegisterPageViewModel(Service)
        {

        }
    }
}
