using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage3.Models;

public partial class Player
{
    [Key]
    public int PlayerId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Email { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Password { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? PlayerName { get; set; }

    public int? RankId { get; set; }

    public int Points { get; set; }

    [InverseProperty("Player")]
    public virtual ICollection<Question> Questions { get; } = new List<Question>();

    [ForeignKey("RankId")]
    [InverseProperty("Players")]
    public virtual Rank? Rank { get; set; }
}

public class PlayerForLogin
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class OtherPlayerForLogin
{
    public int playerId { get; set; }
    public string playerEmail { get; set; }
    public string playerPassword { get; set; }
    public string playerName { get; set; }
    public int playerScore { get; set; }
    public Playerrank2 playerRank { get; set; }
    public Question2[] questions { get; set; }
}

public class Playerrank2
{
    public int rankId { get; set; }
    public string rankName { get; set; }
}

public class Question2
{
    public int questionId { get; set; }
    public Questiontopic2 questionTopic { get; set; }
    public int questionPlayerId { get; set; }
    public string questionText { get; set; }
    public string questionAnswerText { get; set; }
    public string questionWrongText1 { get; set; }
    public string questionWrongText2 { get; set; }
    public string questionWrongText3 { get; set; }
    public int questionStatusId { get; set; }
}

public class Questiontopic2
{
    public int topicId { get; set; }
    public string topicName { get; set; }
}

public class ResultFromRegisterPlayer
{
    public string id { get; set; }
    public int playerId { get; set; }
    public string playerEmail { get; set; }
    public string playerPassword { get; set; }
    public string playerName { get; set; }
    public int playerScore { get; set; }
    public object playerRank { get; set; }
    public Questions questions { get; set; }
}

public class Questions
{
    public string id { get; set; }
    public object[] values { get; set; }
}



