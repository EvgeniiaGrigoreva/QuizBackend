using Microsoft.AspNetCore.Mvc;
using QuizBackend.Models;

namespace QuizBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Endpoints : ControllerBase
    {
        private readonly AppDbContext _context;

        public Endpoints(AppDbContext context)
        {
            _context = context;
        }

        public class CreateQuiz
        {
            public string CreatorLogin { get; set; }
            public string QName { get; set; }

            public int UserID { get; set; }
            public List<NewQuiz> NewQuiz { get; set; }
        }

        public class NewQuiz
        {
            public string Text { get; set; }
            public string AnsTrue { get; set; }
            public string AnsFalse1 { get; set; }
            public string AnsFalse2 { get; set; }
        }


        [HttpPost("savelist")]
        public async Task<IActionResult> SaveList([FromBody] CreateQuiz items)
        {
            if (items == null || items.NewQuiz == null || items.NewQuiz.Count == 0)
            {
                return BadRequest("The list is empty.");
            }


            var quiz = new Quiz
            {
                UserId = items.UserID,
                QuizName = items.QName
            };

            foreach (var q in items.NewQuiz)
            {
                var question = new Question
                {
                    QuestionText = q.Text,

                };

                quiz.Questions.Add(question);
                question.Answers.Add(new Answer { AnswerText = q.AnsTrue, IsCorrect = true });
                question.Answers.Add(new Answer { AnswerText = q.AnsFalse1, IsCorrect = false });
                question.Answers.Add(new Answer { AnswerText = q.AnsFalse2, IsCorrect = false });
            }

            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync();

            return Ok("Quiz received correctly");
        }
    }

}
