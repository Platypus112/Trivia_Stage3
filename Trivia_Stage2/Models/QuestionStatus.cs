using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage3.Models;

[Table("QuestionStatus")]
public partial class QuestionStatus
{
    [Key]
    public int StatusId { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? StatusName { get; set; }

    [InverseProperty("Status")]
    public virtual ICollection<Question> Questions { get; } = new List<Question>();
}
