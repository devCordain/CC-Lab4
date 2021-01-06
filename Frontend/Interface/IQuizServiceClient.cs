using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend {
    public interface IQuizServiceClient {
        public Task<Quiz> GetQuizAsync(int id);
        public Task<Quiz> GetRandomQuizAsync();
        public Task<List<Quiz>> GetQuizAsync();
        public Task CreateQuizAsync(Quiz quiz);
        public Task UpdateQuizAsync(int id, Quiz quiz);
        public Task DeleteQuizAsync(int id);
    }
}
