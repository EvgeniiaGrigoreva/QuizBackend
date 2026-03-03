using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuizBackend.Models;

public partial class Quiz
{
    [Key]
    public int QuizId { get; set; }

    public int UserId { get; set; }

    public string? QuizName { get; set; }
     
    [InverseProperty("Quiz")]
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    [InverseProperty("Quiz")]
    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    [ForeignKey("UserId")]
    [InverseProperty("Quizzes")]
    public virtual User User { get; set; } = null!;
}
