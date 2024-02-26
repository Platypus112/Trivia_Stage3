using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trivia_Stage1.Models;
using Xamarin.Google.Crypto.Tink.Signature;

namespace Trivia_Stage2.Services
{
    public class Service
    {
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
    }
    internal class PlayerService
    {
        public List<Player> Players { get; set; }
        public PlayerService() { }
    }
    internal class QuestionService
    {
        public List<Question> Questions { get; set; }
        public QuestionService() { }
    }
    internal class RankService
    {
        public List<Rank> Ranks { get; set; }
        public RankService() { }
    }
    internal class QuestionStatusService
    {
        public List<QuestionStatus> QuestionStatuses { get; set; }
        public QuestionStatusService() { }
    }
    internal class SubjectService
    {
        public List<Subject> Subjects { get; set; }
        public SubjectService() { }
    }
}
