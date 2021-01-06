using Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.Controllers {
    public class QuizController : Controller {
        private readonly ILogger<QuizController> _logger;
        private readonly IQuizServiceClient _quizServiceClient;

        public QuizController(ILogger<QuizController> logger, IQuizServiceClient quizServiceClient) {
            _logger = logger;
            _quizServiceClient = quizServiceClient;
        }

        public IActionResult Index() {
            return View();
        }

        public async Task<IActionResult> Random() {
            var quiz = await _quizServiceClient.GetRandomQuizAsync();
            return View(quiz);
        }

        public IActionResult Admin() {
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
