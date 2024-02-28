using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Trivia_Stage2.Services;

namespace Trivia_Stage2.ViewModels
{
    public class MenuPageViewModel : ViewModel
    {
        private Service service;
        public ICommand NavigateToApproveQuestionsPageCommand { get; private set; }
        public ICommand NavigateToBestScoresPageCommand { get; private set; }
        public ICommand NavigateToUserAdminPageCommand { get; private set; }
        public ICommand NavigateToUserQuestionsPageCommand { get; private set; }
        public MenuPageViewModel(Service service_)
        {
            service = service_;

            NavigateToApproveQuestionsPageCommand = new Command(NavigateToApproveQuestionsPage);
            NavigateToBestScoresPageCommand=new Command(NavigateToBestScoresPage);
            NavigateToUserAdminPageCommand= new Command(NavigateToUserAdminPage);
            NavigateToUserQuestionsPageCommand = new Command(NavigateToUserQuestionPage);
        }
        private async void NavigateToApproveQuestionsPage()
        {
            await AppShell.Current.GoToAsync("ApproveQuestionsPage");
        }
        private async void NavigateToBestScoresPage()
        {
            await AppShell.Current.GoToAsync("BestScoresPage");
        }
        private async void NavigateToUserAdminPage()
        {
            await AppShell.Current.GoToAsync("UserAdminPage");
        }
        private async void NavigateToUserQuestionPage()
        {
            await AppShell.Current.GoToAsync("UserQuestionsPage");
        }
    }
}
