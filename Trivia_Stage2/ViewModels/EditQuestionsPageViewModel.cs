using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trivia_Stage2.Models;
using Trivia_Stage2.Services;

namespace Trivia_Stage2.ViewModels
{
    [QueryProperty(nameof(Question), "Question")]
    public class EditQuestionsPageViewModel:ViewModel
    {
        private Question question;
        public Question Question { get => question; set { question = value; UpdateFields(); } }
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
                Player = Question.Player;
                Status = Question.Status;
                Subject= Question.Subject;
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
        private Player player;
        public Player Player { get => player; set { player = value; OnPropertyChanged(); } }
        private QuestionStatus status;
        public QuestionStatus Status { get => status; set { status = value; OnPropertyChanged(); } }
        private Subject subject;
        public Subject Subject { get => subject; set { subject = value; OnPropertyChanged(); } }
        public EditQuestionsPageViewModel(Service service)
        {
            this.service = service;
            UpdateFields();
        }
    }
}
