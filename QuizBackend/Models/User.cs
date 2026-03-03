using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuizBackend.Models;

public partial class User
{
    [Key]
    public int UserId { get; set; }

    [StringLength(100)]
    public string Login { get; set; } = null!;

    [StringLength(100)]
    public string Password { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
}
