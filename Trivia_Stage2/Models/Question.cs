using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

public partial class Question
{
    [Key]
    public int QuestionId { get; set; }

    public int? PlayerId { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Correct { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Incorrect1 { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Incorrect2 { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Incorrect3 { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? QuestionText { get; set; }

    public int? SubjectId { get; set; }

    public int? StatusId { get; set; }

    [ForeignKey("PlayerId")]
    [InverseProperty("Questions")]
    public virtual Player? Player { get; set; }

    [ForeignKey("StatusId")]
    [InverseProperty("Questions")]
    public virtual QuestionStatus? Status { get; set; }

    [ForeignKey("SubjectId")]
    [InverseProperty("Questions")]
    public virtual Subject? Subject { get; set; }
}
