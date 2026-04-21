using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizBackend.Models;

public partial class Result
{
    [Key]
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public int CorAnswer { get; set; }

    public int? QuizId { get; set; }

    [ForeignKey("QuizId")]
    [InverseProperty("Results")]
    public virtual Quiz? Quiz { get; set; }
}
