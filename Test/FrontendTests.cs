using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Frontend;
using Frontend.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Test.Fakes;
using Test.Helpers;

namespace Test
{
    [TestClass]
    public class FrontendTests
    {
        [TestMethod]
        public void Index_Should_return_expected_view()
        {
            var controller = new QuizController(null, null);
            var result = controller.Index() as ViewResult;
            // If fetching a page based on the Action name, I.E "Index", thus not using any arguments within the "View()" method
            // the ViewName property in the ViewResult will be null
            Assert.AreEqual(null, result.ViewName);
        }

        [TestMethod]
        public async Task Random_Should_return_expected_view_and_data()
        {

            var quiz = new TestData().GetDefaultFrontendQuiz();
            var controller = new QuizController(null, new QuizServiceClientFake(new List<Quiz>()
            {
                quiz
            }));
            var result = await controller.Random() as ViewResult;
            // If fetching a page based on the Action name, I.E "Index", thus not using any arguments within the "View()" method
            // the ViewName property in the ViewResult will be null
            Assert.AreEqual(null, result.ViewName);
            Assert.AreEqual(quiz, result.Model);
        }

        [TestMethod]
        public void Admin_Should_return_expected_view()
        {
            var controller = new QuizController(null, null);
            var result = controller.Admin() as ViewResult;
            // If fetching a page based on the Action name, I.E "Index", thus not using any arguments within the "View()" method
            // the ViewName property in the ViewResult will be null
            Assert.AreEqual(null, result.ViewName);
        }

        [TestMethod]
        public void Privacy_Should_return_expected_view()
        {
            var controller = new QuizController(null, null);
            var result = controller.Privacy() as ViewResult;
            // If fetching a page based on the Action name, I.E "Index", thus not using any arguments within the "View()" method
            // the ViewName property in the ViewResult will be null
            Assert.AreEqual(null, result.ViewName);
        }

        // QuizServiceClient Tests
        [TestMethod]
        public async Task Create_quiz_Should_succeed()
        {
            var baseUri = "http://localhost:60479/";
            var url = "api/Quizzes/";
            var quiz = new TestData().GetDefaultFrontendQuiz();
            var jsonString = JsonConvert.SerializeObject(quiz);
            var client = CreateTestClient(baseUri, url, 
                new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.Created,
                    Content = new StringContent(jsonString)
                });
            
            await new QuizServiceClient(GetDefaultConfiguration(), client).CreateQuizAsync(quiz);
        }

        [TestMethod]
        public async Task Create_quiz_Should_throw_HttpRequestException_If_client_returns_non_successful_statuscode()
        {
            var baseUri = "http://localhost:60479/";
            var url = "api/Quizzes/";
            var quiz = new TestData().GetDefaultFrontendQuiz();
            var client = CreateTestClient(baseUri, url, 
                new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError
                });
            
            await Assert.ThrowsExceptionAsync<HttpRequestException>(
                () => new QuizServiceClient(GetDefaultConfiguration(), client)
                    .CreateQuizAsync(quiz));
        }

        [TestMethod]
        public async Task Delete_quiz_Should_succeed()
        {
            var baseUri = "http://localhost:60479/";
            var id = 1;
            var url = "api/Quizzes/" + id;
            var client = CreateTestClient(baseUri, url, 
                new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.NoContent
                });
            
            await new QuizServiceClient(GetDefaultConfiguration(), client).DeleteQuizAsync(id);
        }

        [TestMethod]
        public async Task Delete_quiz_Should_throw_HttpRequestException_If_client_returns_non_successful_statuscode()
        {
            var baseUri = "http://localhost:60479/";
            var id = 1;
            var url = "api/Quizzes/" + id;
            var client = CreateTestClient(baseUri, url, 
                new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError
                });
            
            await Assert.ThrowsExceptionAsync<HttpRequestException>(
                () => new QuizServiceClient(GetDefaultConfiguration(), client)
                    .DeleteQuizAsync(id));
        }

        [TestMethod]
        public async Task Get_quiz_Should_succeed()
        {
            var baseUri = "http://localhost:60479/";
            var id = 1;
            var url = "api/Quizzes/" + id;
            var quiz = new TestData().GetDefaultFrontendQuiz();
            var jsonString = JsonConvert.SerializeObject(quiz);
            var client = CreateTestClient(baseUri, url, 
                new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonString)
                });
            var result = await new QuizServiceClient(GetDefaultConfiguration(), client).GetQuizAsync(id);
            Assert.AreEqual(jsonString, JsonConvert.SerializeObject(result));
        }

        [TestMethod]
        public async Task Get_quiz_Should_throw_HttpRequestException_If_client_returns_non_successful_statuscode()
        {
            var baseUri = "http://localhost:60479/";
            var id = 1;
            var url = "api/Quizzes/" + id;
            var client = CreateTestClient(baseUri, url, 
                new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("BAD CONTENT")
                });
            
            await Assert.ThrowsExceptionAsync<Exception>(
                () => new QuizServiceClient(GetDefaultConfiguration(), client)
                    .GetQuizAsync(id));
        }

        [TestMethod]
        public async Task Get_quiz_Should_throw_Exception_If_response_content_cannot_be_deserialized()
        {
            var baseUri = "http://localhost:60479/";
            var id = 1;
            var url = "api/Quizzes/" + id;
            var client = CreateTestClient(baseUri, url, 
                new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                });
            
            await Assert.ThrowsExceptionAsync<HttpRequestException>(
                () => new QuizServiceClient(GetDefaultConfiguration(), client)
                    .GetQuizAsync(id));
        }

        [TestMethod]
        public async Task Get_random_quiz_Should_succeed()
        {
            var baseUri = "http://localhost:60479/";
            var url = "api/Quizzes/Random/";
            var quiz = new TestData().GetDefaultFrontendQuiz();
            var jsonString = JsonConvert.SerializeObject(quiz);
            var client = CreateTestClient(baseUri, url, 
                new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonString)
                });
            var result = await new QuizServiceClient(GetDefaultConfiguration(), client).GetRandomQuizAsync();
            Assert.AreEqual(jsonString, JsonConvert.SerializeObject(result));
        }

        [TestMethod]
        public async Task Get_random_quiz_Should_throw_HttpRequestException_If_client_returns_non_successful_statuscode()
        {
            var baseUri = "http://localhost:60479/";
            var url = "api/Quizzes/Random/";
            var client = CreateTestClient(baseUri, url, 
                new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("BAD CONTENT")
                });
            
            await Assert.ThrowsExceptionAsync<Exception>(
                () => new QuizServiceClient(GetDefaultConfiguration(), client)
                    .GetRandomQuizAsync());
        }

        [TestMethod]
        public async Task Get_random_quiz_Should_throw_Exception_If_response_content_cannot_be_deserialized()
        {
            var baseUri = "http://localhost:60479/";
            var url = "api/Quizzes/Random/";
            var client = CreateTestClient(baseUri, url, 
                new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                });
            
            await Assert.ThrowsExceptionAsync<HttpRequestException>(
                () => new QuizServiceClient(GetDefaultConfiguration(), client)
                    .GetRandomQuizAsync());
        }

        [TestMethod]
        public async Task Get_all_quizzes_Should_succeed()
        {
            var baseUri = "http://localhost:60479/";
            var url = "api/Quizzes/";
            var quizzes = new TestData().GetDefaultFrontendQuizzes(2);
            var jsonString = JsonConvert.SerializeObject(quizzes);
            var client = CreateTestClient(baseUri, url, 
                new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonString)
                });
            var result = await new QuizServiceClient(GetDefaultConfiguration(), client).GetQuizAsync();
            Assert.AreEqual(jsonString, JsonConvert.SerializeObject(result));
        }

        [TestMethod]
        public async Task Get_all_quizzes_Should_throw_HttpRequestException_If_client_returns_non_successful_statuscode()
        {
            var baseUri = "http://localhost:60479/";
            var url = "api/Quizzes/";
            var client = CreateTestClient(baseUri, url, 
                new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("BAD CONTENT")
                });
            
            await Assert.ThrowsExceptionAsync<Exception>(
                () => new QuizServiceClient(GetDefaultConfiguration(), client)
                    .GetQuizAsync());
        }

        [TestMethod]
        public async Task Get_all_quizzes_Should_throw_Exception_If_response_content_cannot_be_deserialized()
        {
            var baseUri = "http://localhost:60479/";
            var url = "api/Quizzes/";
            var client = CreateTestClient(baseUri, url, 
                new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("BAD CONTENT")
                });
            
            await Assert.ThrowsExceptionAsync<HttpRequestException>(
                () => new QuizServiceClient(GetDefaultConfiguration(), client)
                    .GetQuizAsync());
        }

        [TestMethod]
        public async Task Update_quiz_Should_succeed()
        {
            var baseUri = "http://localhost:60479/";
            var id = 1;
            var url = "api/Quizzes/" + id;
            var quiz = new TestData().GetDefaultFrontendQuiz();
            var jsonString = JsonConvert.SerializeObject(quiz);
            var client = CreateTestClient(baseUri, url, 
                new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.NoContent,
                    Content = new StringContent(jsonString)
                });
            await new QuizServiceClient(GetDefaultConfiguration(), client).UpdateQuizAsync(id, quiz);
        }

        [TestMethod]
        public async Task Update_quiz_Should_throw_HttpRequestException_If_client_returns_non_successful_statuscode()
        {
            var baseUri = "http://localhost:60479/";
            var id = 1;
            var url = "api/Quizzes/" + id;
            var quiz = new TestData().GetDefaultFrontendQuiz();
            var jsonString = JsonConvert.SerializeObject(quiz);
            var client = CreateTestClient(baseUri, url, 
                new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(jsonString)
                });
            await Assert.ThrowsExceptionAsync<HttpRequestException>(
                () => new QuizServiceClient(GetDefaultConfiguration(), client)
                    .UpdateQuizAsync(id, quiz));
        }

        private HttpClient CreateTestClient(string baseUri, string url, HttpResponseMessage httpResponseMessage = null)
        {
            var requests = new Dictionary<string, HttpResponseMessage>
            {
                {
                    baseUri + url, 
                    httpResponseMessage
                }
            };
            var client = new HttpClient(new TestHttpMessageHandler(requests));
            return client;
        }

        private IConfiguration GetDefaultConfiguration()
        {
            var myConfiguration = new Dictionary<string, string>
            {
                {"QuizServiceClientUrl", "http://localhost:60479/"}
            };

            return new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();
        }
    }
}
