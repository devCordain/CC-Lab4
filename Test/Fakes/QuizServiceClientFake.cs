using System.Collections.Generic;
using System.Linq;using System.Threading.Tasks;
using Frontend;

namespace Test.Fakes
{
    class QuizServiceClientFake : IQuizServiceClient
    {
        public List<Quiz> _quizzes { get; private set; }

        public QuizServiceClientFake(IEnumerable<Quiz> expectedQuizzes)
        {
            _quizzes = expectedQuizzes?.ToList();
        }

        public async Task<Quiz> GetQuizAsync(int id)
        {
            return await Task.Run(() => _quizzes.FirstOrDefault(x => x.Id == id));
        }

        public async Task<Quiz> GetRandomQuizAsync()
        {
            return await Task.Run(() => _quizzes.FirstOrDefault());
        }

        public async Task<List<Quiz>> GetQuizAsync()
        {
            return await Task.Run(() => _quizzes);
        }

        public async Task CreateQuizAsync(Quiz quiz)
        {
            
            await Task.Run(() => _quizzes.Add(quiz));
        }

        public async Task UpdateQuizAsync(int id, Quiz quiz)
        {
            var localQuiz = _quizzes.FirstOrDefault(x => x.Id == id);
            if (localQuiz is not null)
            {
                await Task.Run(() => _quizzes[_quizzes.IndexOf(localQuiz)] = quiz);
            }
        }

        public async Task DeleteQuizAsync(int id)
        {
            var localQuiz = _quizzes.FirstOrDefault(x => x.Id == id);
            if (localQuiz is not null)
            {
                await Task.Run(() => _quizzes.RemoveAt(_quizzes.IndexOf(localQuiz)));
            }
        }
    }
}
