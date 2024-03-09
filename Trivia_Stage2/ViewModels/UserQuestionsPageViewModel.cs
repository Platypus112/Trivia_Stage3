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
    public class UserQuestionsPageViewModel : ViewModel
    {
        private Service service;
        private List<Question> questionKeeper;
        private ObservableCollection<Question> questions;
        public ObservableCollection<Question> Questions { get => questions; set { questions = value; OnPropertyChanged(); } }
        private Question selectedQuestion;
        public Question SelectedQuestion { get => selectedQuestion; set { selectedQuestion = value; OnPropertyChanged(); } }
        private string text;
        public string Text { get => text; set {  text = value; OnPropertyChanged(); } }
        private bool isRefreshing;
        public bool IsRefreshing { get => isRefreshing; set { isRefreshing = value; OnPropertyChanged(); } }
        private string filterEntry;
        public string FilterEntry { get => filterEntry; set { filterEntry = value; OnPropertyChanged(); } }
        public ICommand FilterByStatusCommand { get; private set; }
        public ICommand LoadQuestionsCommand { get; private set; }
        public ICommand NavigateEditCommand { get; private set; }

        public UserQuestionsPageViewModel(Service service_)
        {
            IsRefreshing = false;
            service = service_;
            Questions = new ObservableCollection<Question>(service.GetLoggedQuestions());
            FilterByStatusCommand = new Command<string>((x) => FilterStatus(x));
            LoadQuestionsCommand = new Command(async () => await LoadQuestions());
            NavigateEditCommand = new Command(async () => await NavToEditPage());
            LoadQuestions();
        }
        private async Task NavToEditPage()
        {
            if (SelectedQuestion != null)
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("Question", SelectedQuestion);
                await AppShell.Current.GoToAsync("EditQuestionsPage", data);
                SelectedQuestion = null;
            }
        }
        private void FilterStatus(string filter)
        {
            Questions.Clear();
            foreach (Question question in questionKeeper)
            {
                Questions.Add(question);
            }
            List<Question> filtered = new List<Question>();
            filter.ToLower();
            foreach (Question q in Questions)
            {
                if (q.Status.StatusName.ToLower() == filter.ToLower()) filtered.Add(q);
            }
            Questions.Clear();
            foreach (Question q in filtered)
            {
                Questions.Add(q);
            }
        }
        private async Task LoadQuestions()
        {
            IsRefreshing = true;
            if(service.PlayerIsLogged())questionKeeper = service.GetLoggedQuestions();
            Questions.Clear();
            FilterEntry = string.Empty;
            foreach (Question q in questionKeeper)
            {
                Questions.Add(q);
            }
            IsRefreshing = false;
        }
    }
}
