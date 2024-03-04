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
        private List<Question> questionkeeper;
        private ObservableCollection<Question> questions;
        public ObservableCollection<Question> Questions { get => questions; set { questions = value; OnPropertyChanged(); } }
        private Question selectedquestion;
        public Question SelectedQuestion { get => selectedquestion; set { selectedquestion = value; OnPropertyChanged(); } }
        private string text;
        public string Text { get => text; set {  text = value; OnPropertyChanged(); } }
        private bool isRefreshing;
        public bool IsRefreshing { get => isRefreshing; set { isRefreshing = value; OnPropertyChanged(); } }
        public ICommand FilterByStatus { get; private set; }
        public ICommand LoadQuestionsCommand { get; private set; }

        public UserQuestionsPageViewModel(Service service_)
        {
            IsRefreshing = false;
            service = service_;
            Questions = new ObservableCollection<Question>(service.GetQuestionsByPlayer(service.LoggedPlayer.PlayerId));
            FilterByStatus = new Command<string>((x) => FilterStatus(x));
            LoadQuestionsCommand = new Command(async () => await LoadQuestions());
            LoadQuestions();
        }
        private void FilterStatus(string filter)
        {
            Questions.Clear();
            foreach (Question question in questionkeeper)
            {
                Questions.Add(question);
            }
            filter = filter.ToLower();
            List<Question> filtered = new List<Question>();
            foreach (Question q in Questions)
            {
                if (q.Status.StatusName.ToLower() == filter)
                {
                    filtered.Add(q);
                }
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
            questionkeeper = service.Questions.Where(x => x.Player == service.LoggedPlayer).ToList();
            Questions.Clear();
            foreach (Question q in questionkeeper)
            {
                Questions.Add(q);
            }
            IsRefreshing = false;
        }
    }
}
