using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Trivia_Stage3.Models;

namespace Trivia_Stage3.Services
{
    public class Service
    {
        private readonly string URL = "https://qsc714b9-7128.euw.devtunnels.ms/TriviaApi";
        private HttpClient client;
        private JsonSerializerOptions options;
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
            options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
            client = new HttpClient();
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
        public bool PlayerIsLogged()
        {
            return LoggedPlayer!= null;
        }
        public List<Player> GetPlayersSortedById()
        {
            return Players.OrderBy(x => x.PlayerId).ToList();
        }
        public List<Player> GetPlayersSortedByRank()
        {
            return Players.OrderBy(x=> -x.RankId).ToList();
        }
        public async void SaveEditedChanges(Question question, string subject, string text, string c, string inc1, string inc2, string inc3)
        {
            int playerId = (int)question.PlayerId;
            int index = question.QuestionId - 1;
            Questions.RemoveAt(index);
            question = new Question();
            question.PlayerId = playerId;
            question.Player=Players.Where(x => x.PlayerId == playerId).FirstOrDefault();
            question.QuestionId = index+1;
            question.Subject = Subjects.Where(s => s.SubjectName.ToLower() == subject.ToLower()).FirstOrDefault();
            question.SubjectId = question.Subject.SubjectId;
            question.QuestionText = text;
            question.Correct = c;
            question.Incorrect1 = inc1;
            question.Incorrect2 = inc2;
            question.Incorrect3 = inc3;
            question.Status = QuestionStatuses.Where(s => s.StatusId == 1).FirstOrDefault();
            question.StatusId = question.Status.StatusId;
            Questions.Insert(index, question);
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
        public bool SubjectIsExist(string subject)
        {
            return (Subjects.Where(s => s.SubjectName.ToLower() == subject.ToLower()).FirstOrDefault() != null);
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
        public async Task<bool> RegisterPlayer(string email,string name, string password)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true };

                string jsonString = JsonSerializer.Serialize(new OtherPlayerForLogin() { 
                    playerEmail = email,
                    playerName=name,
                    playerPassword=password,
                }, options);
                HttpResponseMessage message = await client.PostAsync(URL + "/RegisterPlayer", new StringContent(jsonString, Encoding.UTF8, "application/json"));
                if (message.IsSuccessStatusCode)
                {
                    LoggedPlayer = playerService.ConvertLoginToNormal(JsonSerializer.Deserialize<OtherPlayerForLogin>(await message.Content.ReadAsStringAsync()));
                }
                return LoggedPlayer != null;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> LogPlayer(string email,string password)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true };
                
                string jsonString = JsonSerializer.Serialize(new PlayerForLogin() { Email=email,Password=password}, options);
                HttpResponseMessage message = await client.PostAsync(URL + "/Login",new StringContent(jsonString,Encoding.UTF8, "application/json"));
                if (message.IsSuccessStatusCode)
                {
                    OtherPlayerForLogin p = JsonSerializer.Deserialize<OtherPlayerForLogin>(await message.Content.ReadAsStringAsync());
                    LoggedPlayer = this.Players.Where(x=>x.PlayerId==p.playerId).FirstOrDefault();
                    if(LoggedPlayer == null&&p!=null)
                    {
                        LoggedPlayer = playerService.ConvertLoginToNormal(p);
                        AddRanksToPlayers();
                        this.Players.Add(LoggedPlayer);
                    }
                }
                return LoggedPlayer != null;
            }
            catch
            {
                return false;
            }
        }
        public async void ApproveQuestion(Question q)
        {
            q.StatusId = 2;
            q.Status =QuestionStatuses.Where(x=>x.StatusId==q.StatusId).FirstOrDefault();
        }

        public async void DeclineQuestion(Question q)
        {
            q.StatusId = 3;
            q.Status = QuestionStatuses.Where(x => x.StatusId == q.StatusId).FirstOrDefault();
        }
        public List<Question> GetQuestionsByStatusName(string status)
        {
            return Questions.Where(x => x.Status.StatusName == status).ToList();
        }

        public List<Question> GetPendingQuestionsBySubjectName(string subject)
        {
            return Questions.Where(x => x.Subject.SubjectName.Contains(subject)&& x.StatusId==1).ToList();
        }
        public List<Question> GetPendingQuestions()
        {
            return Questions.Where(x => x.StatusId == 1).ToList();
        }
        public List<Question> GetLoggedQuestions()
        {
            return Questions.Where(x => x.PlayerId == LoggedPlayer.PlayerId).ToList();
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
        public Player ConvertRegisterToNormal(ResultFromRegisterPlayer p)
        {
            return new Player()
            {
                PlayerId = p.playerId,
                Email = p.playerEmail,
                Password = p.playerPassword,
                PlayerName = p.playerName,
                Points = p.playerScore,
                //RankId = p.playerRank.rankId,
            };
        }
        public Player ConvertLoginToNormal(OtherPlayerForLogin p)
        {
            return new Player()
            {
                PlayerId = p.playerId,
                Email = p.playerEmail,
                Password = p.playerPassword,
                PlayerName = p.playerName,
                Points = p.playerScore,
                RankId= p.playerRank.rankId,
            };
        }
        public OtherPlayerForLogin ConvertNormalToLogin(Player player)
        {
            OtherPlayerForLogin result=new OtherPlayerForLogin()
            {
                playerEmail = player.Email,
                playerId = player.PlayerId,
                playerName = player.PlayerName,
                playerPassword = player.Password,
                playerScore = player.Points,
                questions = new Question2[0],
                playerRank = new Playerrank2()
                {
                    rankId = 0,
                    rankName = "????"
                }
            };
            return result;
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
