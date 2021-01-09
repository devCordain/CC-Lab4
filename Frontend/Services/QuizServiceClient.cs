using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend {
    public class QuizServiceClient : IQuizServiceClient {
        private readonly HttpClient httpClient;
        private string quizServiceController = "api/Quizzes/";

        public QuizServiceClient(IConfiguration config, HttpClient httpClient) {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri(config.GetValue<string>("QuizServiceClientUrl"));
        }

        public async Task CreateQuizAsync(Quiz quiz) {
            string jsonQuiz;
            HttpResponseMessage result;
            try {
                jsonQuiz = JsonConvert.SerializeObject(quiz);
                result = await httpClient.PostAsync(quizServiceController, new StringContent(jsonQuiz));
            }
            catch (Exception e) {
                // Do we need this? The "IsSuccessful" check below should handle most errors
                throw new Exception("Post to QuizService failed", e);
            }
            if (!result.IsSuccessStatusCode) {
                throw new HttpRequestException($"Create Quiz HttpRequest failed with code {result.StatusCode}");
            }
        }

        public async Task DeleteQuizAsync(int id) {
            HttpResponseMessage result;
            try {
                result = await httpClient.DeleteAsync(quizServiceController + id);
            } 
            catch(Exception e) {
                throw new Exception("Delete quiz failed", e);
            }
            if (!result.IsSuccessStatusCode) {
                throw new HttpRequestException($"Delete Quiz HttpRequest failed with code {result.StatusCode}");
            }
        }

        public async Task<Quiz> GetQuizAsync(int id) {
            HttpResponseMessage result;
            Quiz quiz;
            try {
                result = await httpClient.GetAsync(quizServiceController + id);
            }
            catch (Exception e) {
                throw new Exception($"Get quiz failed", e);
            }
            if (!result.IsSuccessStatusCode) {
                throw new HttpRequestException($"Get quiz HttpRequest failed with code {result.StatusCode}");
            }
            try {
                var content = await result.Content.ReadAsStringAsync();
                quiz = JsonConvert.DeserializeObject<Quiz>(content);
            }
            catch (Exception e) {
                throw new Exception("Deserialize from Json to Quiz failed", e);
            }
            return quiz;
        }

        public async Task<Quiz> GetRandomQuizAsync() {
            HttpResponseMessage result;
            Quiz quiz;
            try {
                result = await httpClient.GetAsync(quizServiceController + "Random/");
            }
            catch (Exception e) {
                throw new Exception($"Get quiz failed", e);
            }
            if (!result.IsSuccessStatusCode) {
                throw new HttpRequestException($"Get quiz HttpRequest failed with code {result.StatusCode}");
            }
            try {
                var content = await result.Content.ReadAsStringAsync();
                quiz = JsonConvert.DeserializeObject<Quiz>(content);
            }
            catch (Exception e) {
                throw new Exception("Deserialize from Json to Quiz failed", e);
            }
            return quiz;
        }

        public async Task<List<Quiz>> GetQuizAsync() {
            HttpResponseMessage result;
            List<Quiz> quiz;
            try {
                result = await httpClient.GetAsync(quizServiceController);
            }
            catch (Exception e) {
                throw new Exception($"Get quiz failed", e);
            }
            if (!result.IsSuccessStatusCode) {
                throw new HttpRequestException($"Get quiz HttpRequest failed with code {result.StatusCode}");
            }
            try {
                var content = await result.Content.ReadAsStringAsync();
                quiz = JsonConvert.DeserializeObject<List<Quiz>>(content);
            }
            catch (Exception e) {
                throw new Exception("Deserialize from Json to Quiz failed", e);
            }
            return quiz;
        }

        public async Task UpdateQuizAsync(int id, Quiz quiz) {
            string jsonQuiz;
            HttpResponseMessage result;
            try {
                jsonQuiz = JsonConvert.SerializeObject(quiz);
                result = await httpClient.PutAsync(quizServiceController + id, new StringContent(jsonQuiz));
            }
            catch (Exception e) {
                throw new Exception("Put to QuizService failed", e);
            }
            if (!result.IsSuccessStatusCode) {
                throw new HttpRequestException($"Update Quiz HttpRequest failed with code {result.StatusCode}");
            }
        }
    }
}
