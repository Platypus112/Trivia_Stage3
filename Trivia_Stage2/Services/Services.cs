using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trivia_Stage2.Models;

namespace Trivia_Stage2.Services
{
    public class Service
    {
        public Player LoggedPlayer { get; private set; }
        public List<Player> Players { get { return playerService.Players; } private set { this.playerService.Players = value; } }
        private PlayerService playerService;
        public List<Question> Questions { get { return questionService.Questions; } private set { this.questionService.Questions = value; } }
        private QuestionService questionService;
        public List<QuestionStatus> QuestionStatuses { get { return questionStatusService.QuestionStatuses; } private set { this.questionStatusService.QuestionStatuses = value; } }
        private QuestionStatusService questionStatusService;
        public List<Rank> Ranks { get { return rankService.Ranks; } private set { this.rankService.Ranks = value; } }
        private RankService rankService;
        public List<Subject> Subjects { get { return subjectService.Subjects; } private set { this.subjectService.Subjects = value; } }
        private SubjectService subjectService;
        public Service()
        {
            playerService = new PlayerService();
            questionService = new QuestionService();
            questionStatusService = new QuestionStatusService();
            rankService = new RankService();
            subjectService = new SubjectService();
        }
        public bool LogPlayer(string playerName,string password)
        {
            try
            {
                LoggedPlayer = Players.Where(x => x.PlayerName == playerName && x.Password == password).FirstOrDefault();
                return LoggedPlayer != null;
            }
            catch
            {
                return false;
            }
        }
    

        public List<Question> GetPendingQuestions()
        {
            return Questions.Where(x => x.StatusId == 1).ToList();
        }
    }
    internal class PlayerService
    {
        public List<Player> Players { get; set; }
        public PlayerService()
        {
            FillList();
        }
        private async void FillList()
        {
            Players = new List<Player>();
            Players.Add(new Player()
            {
                Email = "Admin@yahoo.com",
                Password = "1234",
                PlayerName = "Admin",
                RankId = 3,
                Points = 0,
            });
            Players.Add(new Player()
            {
                Email = "idancar7@gmail.com",
                Password = "4444",
                PlayerName = "joe4",
                RankId = 1,
                Points = 0,
            });
        }
    }
    internal class QuestionService
    {
        public List<Question> Questions { get; set; }
        public QuestionService()
        {
            FillList();
        }
        private async void FillList()
        {
            Questions = new List<Question>();
            Questions.Add(new Question()
            {
                QuestionId=1,
                PlayerId=1, 
                Correct="90 minutes",
                Incorrect1="43 minutes",
                Incorrect2="80 minutes",
                Incorrect3="110 minutes",
                QuestionText="How long is a soccer game?",
                SubjectId =1,
                StatusId = 2
            });
            Questions.Add(new Question()
            {
                QuestionId = 2,
                PlayerId = 1,
                Correct = "Barack Hussein Obama",
                Incorrect1 = "Obama Care",
                Incorrect2 = "Donald Johnathan Trump",
                Incorrect3 = "George Herbert Walker Bush",
                QuestionText = "Who was the 44th PotUS",
                SubjectId = 2,
                StatusId = 2
            });
            Questions.Add(new Question()
            {
                QuestionId = 3,
                PlayerId = 1,
                Correct = "THE DUTCH",
                Incorrect1 = "THE BRITS",
                Incorrect2 = "THE GERMANS",
                Incorrect3 = "THE MURICANS",
                QuestionText = "Who started the slave trade?",
                SubjectId = 3,
                StatusId = 2
            });
            Questions.Add(new Question()
            {
                QuestionId = 4,
                PlayerId = 1,
                Correct = "299 792 458 m/s",
                Incorrect1 = "3x10^8 m/s",
                Incorrect2 = "9x10^23",
                Incorrect3 = "1",
                QuestionText = "What is the speed of light?",
                SubjectId = 4,
                StatusId = 2
            });
            Questions.Add(new Question()
            {
                QuestionId = 5,
                PlayerId = 1,
                Correct = "2005",
                Incorrect1 = "2003",
                Incorrect2 = "2010",
                Incorrect3 = "1995",
                QuestionText = "When was Ramon high school established?",
                SubjectId = 5,
                StatusId = 2
            });
        }

       
    }
    internal class RankService
    {
        public List<Rank> Ranks { get; set; }
        public RankService()
        {
            FillList();
        }
        private async void FillList()
        {
            Ranks = new List<Rank>();
            Ranks.Add(new Rank()
            {
                RankId=1,
                RankName="Trainee"
            });
            Ranks.Add(new Rank()
            {
                RankId = 2,
                RankName = "Master"
            });
            Ranks.Add(new Rank()
            {
                RankId = 3,
                RankName = "Admin"
            });
        }
    }
    internal class QuestionStatusService
    {
        public List<QuestionStatus> QuestionStatuses { get; set; }
        public QuestionStatusService()
        {
            FillList();
        }
        private async void FillList()
        {
            QuestionStatuses = new List<QuestionStatus>();
            QuestionStatuses.Add(new QuestionStatus()
            {
                StatusId = 1,
                StatusName = "Pending"
            });
            QuestionStatuses.Add(new QuestionStatus()
            {
                StatusId = 2,
                StatusName = "Approved"
            });
            QuestionStatuses.Add(new QuestionStatus()
            {
                StatusId = 3,
                StatusName = "Declined"
            });
        }
    }
    internal class SubjectService
    {
        public List<Subject> Subjects { get; set; }
        public SubjectService() 
        {
            FillList();
        }
        private async void FillList()
        {
            Subjects = new List<Subject>();
            Subjects.Add(new Subject()
            {
                SubjectId = 1,
                SubjectName = "Sports"
            });
            Subjects.Add(new Subject()
            {
                SubjectId = 2,
                SubjectName = "Politics"
            });
            Subjects.Add(new Subject()
            {
                SubjectId = 3,
                SubjectName = "History"
            });
            Subjects.Add(new Subject()
            {
                SubjectId = 4,
                SubjectName = "Science"
            });
            Subjects.Add(new Subject()
            {
                SubjectId = 5,
                SubjectName = "Ramon"
            });
        }

        
    }
}
