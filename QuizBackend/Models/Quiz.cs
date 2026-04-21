using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizBackend.Models;

public partial class Quiz
{
    [Key]
    public int QuizId { get; set; }

    public int UserId { get; set; }

    [StringLength(200)]
    public string QuizName { get; set; } = null!;

    [InverseProperty("Quiz")]
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    [InverseProperty("Quiz")]
    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    [ForeignKey("UserId")]
    [InverseProperty("Quizzes")]
    public virtual User User { get; set; } = null!;
}
