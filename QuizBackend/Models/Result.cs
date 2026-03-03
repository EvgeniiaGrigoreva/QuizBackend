using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuizBackend.Models;

public partial class Result
{
    [Key]
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public int CorAnswer { get; set; }

    public int? QuizId { get; set; }

    [ForeignKey("QuizId")]
    [InverseProperty("Results")]
    public virtual Quiz? Quiz { get; set; }
}
