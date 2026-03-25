using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuizBackend.Models;

public partial class Question
{
    [Key]
    public int QuestionId { get; set; }

    [StringLength(500)]
    public string QuestionText { get; set; } = null!;

    public int? QuizId { get; set; }

    [InverseProperty("Question")]
    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    [ForeignKey("QuizId")]
    [InverseProperty("Questions")]
    public virtual Quiz? Quiz { get; set; }
}
