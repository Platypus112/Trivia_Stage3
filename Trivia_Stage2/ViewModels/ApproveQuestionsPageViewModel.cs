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
    public class ApproveQuestionsPageViewModel:ViewModel
    {
        private Service service;
        private ObservableCollection<Question> pendingQuestions;
        public ObservableCollection<Question> PendingQuestions { get => pendingQuestions; set { pendingQuestions = value; OnPropertyChanged(); } }
        private Question selectedPendingQuestion;
        public Question SelectedPendingQuestion { get => selectedPendingQuestion; set { selectedPendingQuestion = value; OnPropertyChanged(); } }
        private bool isRefreshing;
        public bool IsRefreshing { get => isRefreshing; set { isRefreshing = value; OnPropertyChanged(); } }

       

        public ICommand DeclineQuestionCommand { get; private set; }
        public ICommand ApproveQuestionCommand { get; private set; }
        public ApproveQuestionsPageViewModel(Service service_)
        {
            service = service_;

            PendingQuestions = new ObservableCollection<Question>(service.GetPendingQuestions());
            DeclineQuestionCommand = new Command(async (Object obj) => { await DeclineQuestion(obj); });
            ApproveQuestionCommand = new Command(async (Object obj) => await ApproveQuestion(obj));
        }

        private async Task ApproveQuestion(Object obj)
        {
            
        }
        
        private async Task DeclineQuestion(Object obj)
        {
            PendingQuestions.Remove(((Question)obj));

        }
    }
}
