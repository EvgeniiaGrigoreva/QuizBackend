using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuizBackend.Models;

public partial class Answer
{
    [Key]
    public int AnswerId { get; set; }

    public string AnswerText { get; set; } = null!;

    public bool IsCorrect { get; set; }

    public int QuestionId { get; set; }  

    [ForeignKey("QuestionId")]
    public virtual Question Question { get; set; } = null!;
}
