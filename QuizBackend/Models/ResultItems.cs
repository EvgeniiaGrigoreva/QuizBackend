namespace QuizBackend.Models
{
    public class ResultItems
    {
        public int Id { get; set; }
        public int CorAnswer { get; set; }
        public DateOnly Date { get; set; }
        public string QuizName { get; set; }
    }
}
