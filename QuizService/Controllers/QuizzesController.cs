using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizService.Data;
using QuizService.Models;

namespace QuizService
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        private readonly QuizContext _context;

        public QuizzesController(QuizContext context)
        {
            _context = context;
        }

        // GET: api/Quizzes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizAsync()
        {
            return await _context.Quiz.ToListAsync();
        }

        // GET: api/Quizzes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Quiz>> GetQuizAsync(int id)
        {
            var quiz = await _context.Quiz.FindAsync(id);

            if (quiz == null)
            {
                return NotFound();
            }

            return quiz;
        }

        // GET: api/Quizzes/Random
        [HttpGet]
        [Route("Random")]
        public async Task<ActionResult<Quiz>> GetRandomQuizAsync() {
            var numberOfQuizzes = _context.Quiz.Count();
            if (numberOfQuizzes < 1)
                return NotFound();
            var r = new Random();
            var randomId = r.Next(1, numberOfQuizzes + 1);
            while (QuizExists(randomId) is not true) {
                randomId = r.Next(1, numberOfQuizzes + 1);
            }
            var quiz = await _context.Quiz
                .Include(x => x.Questions)
                .ThenInclude(y => y.Answers)
                .Where(z => z.Id == randomId)
                .FirstOrDefaultAsync();
            if (!quiz.Questions.Any()) {
                return NotFound();
            }

            return quiz;
        }

        // PUT: api/Quizzes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuizAsync(int id, Quiz quiz)
        {
            if (id != quiz.Id)
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

        // POST: api/Quizzes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Quiz>> PostQuizAsync(Quiz quiz)
        {
            _context.Quiz.Add(quiz);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuiz", new { id = quiz.Id }, quiz);
        }

        // DELETE: api/Quizzes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuizAsync(int id)
        {
            var quiz = await _context.Quiz
                .Include(x => x.Questions)
                .ThenInclude(y => y.Answers)
                .Where(z => z.Id == id)
                .FirstOrDefaultAsync();
            if (quiz == null)
            {
                return NotFound();
            }
            
            _context.Quiz.Remove(quiz);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuizExists(int id)
        {
            return _context.Quiz.Any(e => e.Id == id);
        }
    }
}
