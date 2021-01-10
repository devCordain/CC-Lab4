using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frontend;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fakes
{
    [TestClass]
    public class QuizServiceClientFakeTests
    {
        [TestMethod]
        public async Task Get_quiz_should_return_quiz_with_matching_id()
        {
            // Arrange
            var quizzes = new TestData().GetDefaultFrontendQuizzes(2);
            quizzes[0].Id = 1;
            quizzes[1].Id = 2;
            var fakeClient = new QuizServiceClientFake(quizzes);
            // Act
            var result = await fakeClient.GetQuizAsync(quizzes[0].Id);
            // Assert
            Assert.AreEqual(quizzes[0].Id, result.Id);
        }

        [TestMethod]
        public async Task Get_quiz_should_return_null_If_no_matching_id_was_found()
        {
            // Arrange
            var quizzes = new TestData().GetDefaultFrontendQuizzes(1);
            quizzes[0].Id = 1;
            var fakeClient = new QuizServiceClientFake(quizzes);
            // Act
            var result = await fakeClient.GetQuizAsync(2);
            // Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public async Task Get_random_quiz_should_return_a_quiz()
        {
            // Arrange
            var quizzes = new TestData().GetDefaultFrontendQuizzes(1);
            var fakeClient = new QuizServiceClientFake(quizzes);
            // Act
            var result = await fakeClient.GetRandomQuizAsync();
            // Assert
            Assert.AreEqual(quizzes.First(), result);
        }

        [TestMethod]
        public async Task Get_random_quiz_should_return_null_If_no_quiz_exists()
        {
            // Arrange
            var fakeClient = new QuizServiceClientFake(new List<Quiz>());
            // Act
            var result = await fakeClient.GetRandomQuizAsync();
            // Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public async Task Get_quiz_without_specifying_should_return_all_quizzes()
        {
            // Arrange
            var quizzes = new TestData().GetDefaultFrontendQuizzes(5);
            var fakeClient = new QuizServiceClientFake(quizzes);
            // Act
            var result = await fakeClient.GetQuizAsync();
            // Assert
            Assert.AreEqual(5, result.Count);
        }

        [TestMethod]
        public async Task Create_quiz_should_add_a_quiz()
        {
            // Arrange
            var fakeClient = new QuizServiceClientFake(new List<Quiz>());
            // Act & Assert
            Assert.AreEqual(0, fakeClient._quizzes.Count);
            await fakeClient.CreateQuizAsync(new TestData().GetDefaultFrontendQuiz());
            Assert.AreEqual(1, fakeClient._quizzes.Count);
        }

        [TestMethod]
        public async Task Update_quiz_should_update_a_matching_quiz()
        {
            // Arrange
            var quizzes = new TestData().GetDefaultFrontendQuizzes(1);
            quizzes.First().Id = 1;
            var updatedQuiz = new  TestData().GetDefaultFrontendQuiz();
            updatedQuiz.Id = 1337;
            var fakeClient = new QuizServiceClientFake(quizzes);
            // Act & Assert
            Assert.AreEqual(quizzes.First().Id, fakeClient._quizzes.First().Id);
            await fakeClient.UpdateQuizAsync(quizzes.First().Id, updatedQuiz);
            Assert.AreEqual(updatedQuiz.Id, fakeClient._quizzes.First().Id);
        }

        [TestMethod]
        public async Task Delete_quiz_should_delete_a_matching_quiz()
        {
            // Arrange
            var quizzes = new TestData().GetDefaultFrontendQuizzes(1);
            quizzes.First().Id = 1;
            var fakeClient = new QuizServiceClientFake(quizzes);
            // Act & Assert
            Assert.AreEqual(quizzes.First().Id, fakeClient._quizzes.First().Id);
            await fakeClient.DeleteQuizAsync(quizzes.First().Id);
            Assert.AreEqual(0, fakeClient._quizzes.Count);
        }

    }
}
