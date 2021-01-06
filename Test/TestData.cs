using QuizService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test {
    class TestData {
        public Quiz GetDefaultQuiz() {
            return new Quiz() {
                Questions = new List<Question>() {
                    new Question() {
                        Text = "How many programmers does it take to write a test?",
                        Answers = new List<Answer>() {
                            new Answer() {
                                Text = "1",
                                IsCorrect = false
                            },
                            new Answer() {
                                Text = "2",
                                IsCorrect = false
                            },
                            new Answer() {
                                Text = "3",
                                IsCorrect = false
                            },
                            new Answer() {
                                Text = "Out of range exception",
                                IsCorrect = true
                            }
                        }
                    }
                }
            };
        }

        public List<Quiz> GetDefaultQuizzes(int numberOfQuizzes) {
            var quizzes = new List<Quiz>();
            for (int i = 0; i < numberOfQuizzes; i++) {
                quizzes.Add(GetDefaultQuiz());
            }
            return quizzes;
        }
    }
}