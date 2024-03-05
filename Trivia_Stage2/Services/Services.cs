using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            AddSubjectsToQuestions();
            AddRanksToPlayers();
            AddStatusesToQuestions();
            AddPlayerToQuestions();
        }

        private async void AddPlayerToQuestions()
        {
            try
            {
                foreach (Question q in Questions)
                {
                    q.Player = Players.Where(x => x.PlayerId == q.PlayerId).FirstOrDefault();
                }
            }
            catch
            { }
        }
        private async void AddStatusesToQuestions()
        {
            foreach (Question q in Questions)
            {
                q.Status = QuestionStatuses.Where(x => x.StatusId == q.StatusId).FirstOrDefault();
            }
        }
        private async void AddRanksToPlayers()
        {
            foreach(Player p in Players)
            {
                p.Rank = Ranks.Where(x => x.RankId == p.RankId).FirstOrDefault();
            }
        }
        private async void AddSubjectsToQuestions()
        {
            foreach (Question q in Questions)
            {
                q.Subject = Subjects.Where(x => x.SubjectId == q.SubjectId).FirstOrDefault();
            }
        }
        public bool AddNewPlayer(string playerName_,string password_,string email_)
        {
            try
            {
                if (!playerService.IsEmailValid(email_)) return false;
                if (!playerService.IsPasswordValid(password_))return false;
                if (!playerService.IsNameValid(playerName_)) return false;
                Players.Add(new Player()
                {
                    PlayerName = playerName_,
                    Password = password_,
                    Email = email_,
                    Points = 0,
                    RankId = 1,
                    Rank= Ranks.Where(x => x.RankId == 1).FirstOrDefault(),
                    PlayerId=Players.OrderByDescending(x => x.PlayerId).FirstOrDefault().PlayerId+1,
                }) ;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ResetPlayerPoints(Player p)
        {
            try
            {
                p.Points = 0;
                return true;
            }
            catch { return false; }
        }
        public bool RemovePlayer(Player p)
        {
            try
            {
                if (p.PlayerId == LoggedPlayer.PlayerId) return false;
                Players.Remove(p);
                return true;
            }
            catch
            {
                return false;
            }
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
        public async void ApproveQuestion(Question q)
        {
            q.Subject.SubjectId = 2;
        }

        public async void DeclineQuestion(Question q)
        {
            q.Subject.SubjectId = 0;
        }

        public List<Question> GetPendingQuestions()
        {
            return Questions.Where(x => x.StatusId == 1).ToList();
        }
        public List<Question> GetQuestionsByPlayer(int id)
        {
            return Questions.Where(x => x.PlayerId == id).ToList();
        }
    }
    internal class PlayerService
    {
        public List<Player> Players { get; set; }
        public PlayerService()
        {
            FillList();
        }
        public bool IsEmailValid(string emailAddress)
        {
            var pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            var regex = new Regex(pattern);
            return regex.IsMatch(emailAddress);
        }
        public bool IsPasswordValid(string password)
        {
            return !string.IsNullOrEmpty(password) && password.Length >= 3 && password.Length <= 20;
        }
        public bool IsNameValid(string name)
        {
            return !string.IsNullOrEmpty(name) && name.Length >= 3 && name.Length <= 30;
        }
        private async void FillList()
        {
            Players = new List<Player>();
            Players.Add(new Player()
            {
                PlayerId = 1,
                Email = "Admin@yahoo.com",
                Password = "1234",
                PlayerName = "Admin",
                RankId = 3,
                Points = 0,
            });
            Players.Add(new Player()
            {
                PlayerId = 2,
                Email = "idancar7@gmail.com",
                Password = "4444",
                PlayerName = "joe4",
                RankId = 1,
                Points = 0,
            });
            Players.Add(new Player()
            {
                PlayerId = 3,
                Email = "idancar7@gmail.com",
                Password = "55555",
                PlayerName = "joe5",
                RankId = 2,
                Points = 50,
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
            Questions.Add(new Question()
            {
                QuestionId = 6,
                PlayerId = 1,
                Correct = "92 minutes",
                Incorrect1 = "43 minutes",
                Incorrect2 = "90 minutes",
                Incorrect3 = "110 minutes",
                QuestionText = "How long is the trolls3 movie?",
                SubjectId = 1,
                StatusId = 1
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
