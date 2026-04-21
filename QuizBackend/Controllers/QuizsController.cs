using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizBackend.Models;

namespace QuizBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QuizsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Quizs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizzes()
        {
            return await _context.Quizzes.ToListAsync();
        }

        // GET: api/Quizs/list
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<QuizList>>> GetListQuizzes()
        {
            return await _context.Quizzes
         .Select(q => new QuizList
         {
             QuizId = q.QuizId,
             QuizName = q.QuizName,
         }) 
         .ToListAsync();
        }

        // GET: api/Quizs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Quiz>> GetQuiz(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);

            if (quiz == null)
            {
                return NotFound();
            }

            return quiz;
        }

        // GET: api/Quizs/by-userid
        [HttpGet("by-userid/{id}")]
        public async Task<ActionResult<Quiz>> GetQuizzesName(int id)
        {
            var results = await _context.Quizzes
                .Where(q => q.UserId == id)
                .Select(q => new
                {
                    QuizId = q.QuizId,
                    QuizName = q.QuizName
                }).ToListAsync();

            if (id == 0)
                return NotFound();

            return Ok(results);
        }

        // PUT: api/Quizs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuiz(int id, Quiz quiz)
        {
            if (id != quiz.QuizId)
            {
                return BadRequest();
            }

            _context.Entry(quiz).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Quizs
        [HttpPost]
        public async Task<ActionResult<Quiz>> PostQuiz(Quiz quiz)
        {
            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuiz", new { id = quiz.QuizId }, quiz);
        }

        // DELETE: api/Quizs
        [HttpDelete]
        public async Task<IActionResult> DeleteQuiz(List<int> ids)
        {
            var quizzes = await _context.Quizzes.Where(q => ids.Contains(q.QuizId))
        .ToListAsync();

            if (!quizzes.Any())
            {
                return NotFound();
            }

            _context.Quizzes.RemoveRange(quizzes);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuizExists(int id)
        {
            return _context.Quizzes.Any(e => e.QuizId == id);
        }
    }
}
