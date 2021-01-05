using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuizService.Data;
using QuizService;
using QuizService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Test {
    [TestClass]
    public class UnitTest1 {
        //DeleteQuiz(id)

        private QuizContext CreateContextWithData(IEnumerable<Quiz> quizzes = null) {
            var options = new DbContextOptionsBuilder<QuizContext>()
                .UseInMemoryDatabase(databaseName: "MockQuizDatabase")
                .Options;
            var quizContext = new QuizContext(options);
            if (quizzes is not null) {
                quizContext.Quiz.AddRange(quizzes);
                quizContext.SaveChanges();
            }
            return quizContext;
        }

        [TestMethod]
        public async Task Posting_quizzes_should_save_to_database() {
            using var context = CreateContextWithData();
            var quizzesController = new QuizzesController(context);
            var actionResult = await quizzesController.PostQuiz(new Quiz() {
                Questions = new List<Question>() {
                        new Question() {
                            Text = "How many programmers does it take to write a test?",
                            Answers = new List<Answer>() {
                                new Answer(){
                                    Text = "1",
                                    IsCorrect = false
                                },
                                new Answer(){
                                    Text = "2",
                                    IsCorrect = false
                                },
                                new Answer(){
                                    Text = "3",
                                    IsCorrect = false
                                },
                                new Answer(){
                                    Text = "Out of range exception",
                                    IsCorrect = true
                                }
                            }
                        }
                    }
            });
            Assert.AreEqual(201, (actionResult.Result as CreatedAtActionResult).StatusCode);
            await context.Database.EnsureDeletedAsync();
        }

        [TestMethod]
        public async Task Get_quiz_should_return_a_list_from_database() {
            var quiz = new List<Quiz>() {
                new Quiz() {
                    Questions = new List<Question>() {
                        new Question() {
                            Text = "How many programmers does it take to write a test?",
                            Answers = new List<Answer>() {
                                new Answer(){
                                    Text = "1",
                                    IsCorrect = false
                                },
                                new Answer(){
                                    Text = "2",
                                    IsCorrect = false
                                },
                                new Answer(){
                                    Text = "3",
                                    IsCorrect = false
                                },
                                new Answer(){
                                    Text = "Out of range exception",
                                    IsCorrect = true
                                }
                            }
                        }
                    }
                },
                new Quiz() {
                    Questions = new List<Question>() {
                        new Question() {
                            Text = "How many programmers does it take to write a test?",
                            Answers = new List<Answer>() {
                                new Answer(){
                                    Text = "1",
                                    IsCorrect = false
                                },
                                new Answer(){
                                    Text = "2",
                                    IsCorrect = false
                                },
                                new Answer(){
                                    Text = "3",
                                    IsCorrect = false
                                },
                                new Answer(){
                                    Text = "Out of range exception",
                                    IsCorrect = true
                                }
                            }
                        }
                    }
                }
            };
            using var context = CreateContextWithData(quiz);
            var quizzesController = new QuizzesController(context);
            var quizzes = await quizzesController.GetQuiz();
            Assert.AreEqual(2, (quizzes.Value as List<Quiz>).Count);
            await context.Database.EnsureDeletedAsync();
        }

        [TestMethod]
        public async Task Get_quiz_with_id_should_return_the_specified_quiz_from_database() {
            var quiz = new List<Quiz>() {
                new Quiz() {
                    Questions = new List<Question>() {
                        new Question() {
                            Text = "How many programmers does it take to write a test?",
                            Answers = new List<Answer>() {
                                new Answer(){
                                    Text = "1",
                                    IsCorrect = false
                                },
                                new Answer(){
                                    Text = "2",
                                    IsCorrect = false
                                },
                                new Answer(){
                                    Text = "3",
                                    IsCorrect = false
                                },
                                new Answer(){
                                    Text = "Out of range exception",
                                    IsCorrect = true
                                }
                            }
                        }
                    }
                },
                new Quiz() {
                    Questions = new List<Question>() {
                        new Question() {
                            Text = "How many programmers does it take to write a test?",
                            Answers = new List<Answer>() {
                                new Answer(){
                                    Text = "1",
                                    IsCorrect = false
                                },
                                new Answer(){
                                    Text = "2",
                                    IsCorrect = false
                                },
                                new Answer(){
                                    Text = "3",
                                    IsCorrect = false
                                },
                                new Answer(){
                                    Text = "Out of range exception",
                                    IsCorrect = true
                                }
                            }
                        }
                    }
                }
            };
            using var context = CreateContextWithData(quiz);
            var quizzesController = new QuizzesController(context);
            var quizzes = await quizzesController.GetQuiz(2);
            Assert.AreEqual(2, quizzes.Value.Id);
            await context.Database.EnsureDeletedAsync();
        }

        [TestMethod]
        public async Task Get_quiz_with_nonexistant_id_should_return_expected_result() {
            using var context = CreateContextWithData();
            var quizzesController = new QuizzesController(context);
            var quizzes = await quizzesController.GetQuiz(2);
            Assert.AreEqual(404, (quizzes.Result as NotFoundResult).StatusCode);
            await context.Database.EnsureDeletedAsync();
        }

        [TestMethod]
        public async Task Updating_a_quiz_should_return_expected_result() {
            var quiz = new List<Quiz>() {
                new Quiz() {
                    Questions = new List<Question>() {
                        new Question() {
                            Text = "How many programmers does it take to write a test?",
                            Answers = new List<Answer>() {
                                new Answer(){
                                    Text = "1",
                                    IsCorrect = false
                                },
                                new Answer(){
                                    Text = "2",
                                    IsCorrect = false
                                },
                                new Answer(){
                                    Text = "3",
                                    IsCorrect = false
                                },
                                new Answer(){
                                    Text = "Out of range exception",
                                    IsCorrect = true
                                }
                            }
                        }
                    }
                },
                new Quiz() {
                    Questions = new List<Question>() {
                        new Question() {
                            Text = "How many programmers does it take to write a test?",
                            Answers = new List<Answer>() {
                                new Answer(){
                                    Text = "1",
                                    IsCorrect = false
                                },
                                new Answer(){
                                    Text = "2",
                                    IsCorrect = false
                                },
                                new Answer(){
                                    Text = "3",
                                    IsCorrect = false
                                },
                                new Answer(){
                                    Text = "Out of range exception",
                                    IsCorrect = true
                                }
                            }
                        }
                    }
                }
            };
            using var context = CreateContextWithData(quiz);
            var quizzesController = new QuizzesController(context);
            quiz[1].Questions[0].Text = "How many Trumpets does it take to blow the vote blue in USA?";
            var quizzes = await quizzesController.PutQuiz(2, quiz[1]);
            var badResultQuizzes = await quizzesController.PutQuiz(3, quiz[1]);
            var notFoundQuizzes = await quizzesController.PutQuiz(12, quiz[1]);
            Assert.AreEqual(204, (quizzes as NoContentResult).StatusCode);
            Assert.AreEqual(400, (badResultQuizzes as BadRequestResult).StatusCode);
            // We would like to test DBUpdateConcurrencyException but cannot find a feasable way to test this within scope of this assignment (need to install separate testing framework)
            await context.Database.EnsureDeletedAsync();
        }

        [TestMethod]
        public async Task Delete_quiz_should_return_expected_result() {
            var quiz = new List<Quiz>() {
                new Quiz() {
                    Questions = new List<Question>() {
                        new Question() {
                            Text = "How many programmers does it take to write a test?",
                            Answers = new List<Answer>() {
                                new Answer(){
                                    Text = "1",
                                    IsCorrect = false
                                },
                                new Answer(){
                                    Text = "2",
                                    IsCorrect = false
                                },
                                new Answer(){
                                    Text = "3",
                                    IsCorrect = false
                                },
                                new Answer(){
                                    Text = "Out of range exception",
                                    IsCorrect = true
                                }
                            }
                        }
                    }
                },
                new Quiz() {
                    Questions = new List<Question>() {
                        new Question() {
                            Text = "How many programmers does it take to write a test?",
                            Answers = new List<Answer>() {
                                new Answer(){
                                    Text = "1",
                                    IsCorrect = false
                                },
                                new Answer(){
                                    Text = "2",
                                    IsCorrect = false
                                },
                                new Answer(){
                                    Text = "3",
                                    IsCorrect = false
                                },
                                new Answer(){
                                    Text = "Out of range exception",
                                    IsCorrect = true
                                }
                            }
                        }
                    }
                }
            };
            using var context = CreateContextWithData(quiz);
            var quizzesController = new QuizzesController(context);
            var actualSuccess = await quizzesController.DeleteQuiz(2);
            var actualFail = await quizzesController.DeleteQuiz(2);
            Assert.AreEqual(204, (actualSuccess as NoContentResult).StatusCode);
            Assert.AreEqual(404, (actualFail as NotFoundResult).StatusCode);
            await context.Database.EnsureDeletedAsync();
        }
    }
}
