using System.Collections.Generic;
using System.Threading.Tasks;
using Frontend;
using Frontend.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Fakes;

namespace Test.FrontendTests
{
     [TestClass]
    public class QuizControllerTests
    {
        [TestMethod]
        public void Index_Should_return_expected_view()
        {
            var controller = new QuizController( null);
            var result = controller.Index() as ViewResult;
            // If fetching a page based on the Action name, I.E "Index", thus not using any arguments within the "View()" method
            // the ViewName property in the ViewResult will be null
            Assert.AreEqual(null, result?.ViewName);
        }

        [TestMethod]
        public async Task Random_Should_return_expected_view_and_data()
        {

            var quiz = new TestData().GetDefaultFrontendQuiz();
            var controller = new QuizController(new QuizServiceClientFake(new List<Quiz>()
            {
                quiz
            }));
            var result = await controller.Random() as ViewResult;
            // If fetching a page based on the Action name, I.E "Index", thus not using any arguments within the "View()" method
            // the ViewName property in the ViewResult will be null
            Assert.AreEqual(null, result?.ViewName);
            Assert.AreEqual(quiz, result?.Model);
        }

        [TestMethod]
        public void Admin_Should_return_expected_view()
        {
            var controller = new QuizController( null);
            var result = controller.Admin() as ViewResult;
            // If fetching a page based on the Action name, I.E "Index", thus not using any arguments within the "View()" method
            // the ViewName property in the ViewResult will be null
            Assert.AreEqual(null, result?.ViewName);
        }

        [TestMethod]
        public void Privacy_Should_return_expected_view()
        {
            var controller = new QuizController( null);
            var result = controller.Privacy() as ViewResult;
            // If fetching a page based on the Action name, I.E "Index", thus not using any arguments within the "View()" method
            // the ViewName property in the ViewResult will be null
            Assert.AreEqual(null, result?.ViewName);
        }
    }
}
