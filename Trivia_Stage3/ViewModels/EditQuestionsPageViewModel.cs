using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trivia_Stage3.Models;
using Trivia_Stage3.Services;
using System.Windows.Input;

namespace Trivia_Stage3.ViewModels
{
    [QueryProperty(nameof(Question), "Question")]
    public class EditQuestionsPageViewModel : ViewModel
    {
        private Question question;
        public Question Question { get => question; set { question = value; OnPropertyChanged(); UpdateFields(); } }
        private Service service;
        private void UpdateFields()
        {
            if (Question != null)
            {
                QuestionId = Question.QuestionId;
                PlayerId = (int)Question.PlayerId;
                Correct = Question.Correct;
                Incorrect1 = Question.Incorrect1;
                Incorrect2 = Question.Incorrect2;
                Incorrect3 = Question.Incorrect3;
                QuestionText = Question.QuestionText;
                SubjectId = (int)Question.SubjectId;
                StatusId = (int)Question.StatusId;
                StatusName = Question.Status.StatusName;
                SubjectName = Question.Subject.SubjectName;
            }
        }
        private int questionId;
        public int QuestionId { get => questionId; set { questionId = value; OnPropertyChanged(); } }
        private int playerid;
        public int PlayerId { get => playerid; set { playerid = value; OnPropertyChanged(); } }
        private string correct;
        public string Correct { get => correct; set { correct = value; OnPropertyChanged(); } }
        private string incorrect1;
        public string Incorrect1 { get => incorrect1; set { incorrect1 = value; OnPropertyChanged(); } }
        private string incorrect2;
        public string Incorrect2 { get => incorrect2; set { incorrect2 = value; OnPropertyChanged(); } }
        private string incorrect3;
        public string Incorrect3 { get => incorrect3; set { incorrect3 = value; OnPropertyChanged(); } }
        private string questiontext;
        public string QuestionText { get => questiontext; set { questiontext = value; OnPropertyChanged(); } }
        private int subjectid;
        public int SubjectId { get => subjectid; set { subjectid = value; OnPropertyChanged(); } }
        private int statusid;
        public int StatusId { get => statusid; set { statusid = value; OnPropertyChanged(); } }
        private string statusName;
        public string StatusName { get => statusName; set { statusName = value; OnPropertyChanged(); } }
        private string subjectName;
        public string SubjectName { get => subjectName; set { subjectName = value; OnPropertyChanged(); } }
        private string subjectEntry;
        public string SubjectEntry { get => subjectEntry; set { subjectEntry = value; OnPropertyChanged(); } }
        private string textEntry;
        public string TextEntry { get => textEntry; set { textEntry = value; OnPropertyChanged(); } }
        private string correctEntry;
        public string CorrectEntry { get => correctEntry; set { correctEntry = value; OnPropertyChanged(); } }
        private string incorrect1Entry;
        public string Incorrect1Entry { get => incorrect1Entry; set { incorrect1Entry = value; OnPropertyChanged(); } }
        private string incorrect2Entry;
        public string Incorrect2Entry { get => incorrect2Entry; set { incorrect2Entry = value; OnPropertyChanged(); } }
        private string incorrect3Entry;
        public string Incorrect3Entry { get => incorrect3Entry; set { incorrect3Entry = value; OnPropertyChanged(); } }
        private string alertText;
        public string AlertText { get => alertText; set { alertText = value; OnPropertyChanged(); } }
        public ICommand SaveChangesCommand { get; private set; }
        public ICommand ResetChangesCommand { get; private set; }
        public ICommand UpdateSubjectCommand { get; private set; }
        public ICommand UpdateQuestionTextCommand { get; private set; }
        public ICommand UpdateCorrectCommand { get; private set; }
        public ICommand UpdateIncorrect1Command { get; private set; }
        public ICommand UpdateIncorrect2Command { get; private set; }
        public ICommand UpdateIncorrect3Command { get; private set; }
        public EditQuestionsPageViewModel(Service service)
        {
            this.service = service;
            UpdateFields();
            SaveChangesCommand = new Command(async () => await SaveChanges());
            ResetChangesCommand = new Command(async () => await ResetChanges());
            UpdateSubjectCommand = new Command<string>((x) => SubjectCommand(x));
            UpdateQuestionTextCommand = new Command<string>((x) => QuestionTextCommand(x));
            UpdateCorrectCommand = new Command<string>((x) => CorrectCommand(x));
            UpdateIncorrect1Command = new Command<string>((x) => Incorrect1Command(x));
            UpdateIncorrect2Command = new Command<string>((x) => Incorrect2Command(x));
            UpdateIncorrect3Command = new Command<string>((x) => Incorrect3Command(x));
        }
        private async Task SaveChanges()
        {
            if (await AppShell.Current.DisplayAlert("Save Changes?", "Are you sure you want to save the changes? The question will be changed forever and set to pending.", "Accept", "Cancel"))
            {
                service.SaveEditedChanges(Question, SubjectName, QuestionText, Correct, Incorrect1, Incorrect2, Incorrect3);
                await AppShell.Current.GoToAsync("///UserQuestionsPage");
            }
        }
        private async Task ResetChanges()
        {
            UpdateFields();
            AlertText = string.Empty;
        }
        private void SubjectCommand(string entry)
        {
            if (entry != null)
            {
                if (service.SubjectIsExist(entry))
                {
                    SubjectName = entry;
                    AlertText = string.Empty;
                    SubjectEntry = string.Empty;
                }
                else
                {
                    SubjectEntry = string.Empty;
                    AlertText = " Invalid Subject";
                }
            }
        }
        private void QuestionTextCommand(string entry)
        {
            if (entry != null)
            {
                QuestionText = entry;
                TextEntry = string.Empty;
            }
        }
        private void CorrectCommand(string entry)
        {
            if (entry != null)
            {
                Correct = entry;
                CorrectEntry = string.Empty;
            }
        }
        private void Incorrect1Command(string entry)
        {
            if (entry != null)
            {
                Incorrect1 = entry;
                Incorrect1Entry = string.Empty;
            }
        }
        private void Incorrect2Command(string entry)
        {
            if (entry != null)
            {
                Incorrect2 = entry;
                Incorrect2Entry = string.Empty;
            }
        }
        private void Incorrect3Command(string entry)
        {
            if (entry != null)
            {
                Incorrect3 = entry;
                Incorrect3Entry = string.Empty;
            }
        }
    }
}
