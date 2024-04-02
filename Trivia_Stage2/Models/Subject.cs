using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage3.Models;

public partial class Subject
{
    [Key]
    public int SubjectId { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? SubjectName { get; set; }

    [InverseProperty("Subject")]
    public virtual ICollection<Question> Questions { get; } = new List<Question>();
}
