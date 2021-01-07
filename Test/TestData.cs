using QuizService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test {
    class TestData {
        public Quiz GetDefaultBackendQuiz() {
            return new QuizService.Models.Quiz() {
                Questions = new List<QuizService.Models.Question>() {
                    new QuizService.Models.Question() {
                        Text = "How many programmers does it take to write a test?",
                        Answers = new List<QuizService.Models.Answer>() {
                            new QuizService.Models.Answer() {
                                Text = "1",
                                IsCorrect = false
                            },
                            new QuizService.Models.Answer() {
                                Text = "2",
                                IsCorrect = false
                            },
                            new QuizService.Models.Answer() {
                                Text = "3",
                                IsCorrect = false
                            },
                            new QuizService.Models.Answer() {
                                Text = "Out of range exception",
                                IsCorrect = true
                            }
                        }
                    }
                }
            };
        }

        public List<Quiz> GetDefaultBackendQuizzes(int numberOfQuizzes) {
            var quizzes = new List<QuizService.Models.Quiz>();
            for (int i = 0; i < numberOfQuizzes; i++) {
                quizzes.Add(GetDefaultBackendQuiz());
            }
            return quizzes;
        }

        public Frontend.Quiz GetDefaultFrontendQuiz() {
            return new Frontend.Quiz() {
                Questions = new List<Frontend.Question>() {
                    new Frontend.Question() {
                        Text = "How many programmers does it take to write a test?",
                        Answers = new List<Frontend.Answer>() {
                            new Frontend.Answer() {
                                Text = "1",
                                IsCorrect = false
                            },
                            new Frontend.Answer() {
                                Text = "2",
                                IsCorrect = false
                            },
                            new Frontend.Answer() {
                                Text = "3",
                                IsCorrect = false
                            },
                            new Frontend.Answer() {
                                Text = "Out of range exception",
                                IsCorrect = true
                            }
                        }
                    }
                }
            };
        }

        public List<Frontend.Quiz> GetDefaultFrontendQuizzes(int numberOfQuizzes) {
            var quizzes = new List<Frontend.Quiz>();
            for (int i = 0; i < numberOfQuizzes; i++) {
                quizzes.Add(GetDefaultFrontendQuiz());
            }
            return quizzes;
        }
    }
}