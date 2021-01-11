// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function setAnswer(id, isTrue) {
    var answer = document.getElementById(id);
    var answerContainer = answer.parentNode;
    var answers = answerContainer.children;
    if (isTrue === "True") {
        answer.classList.add("correct");
        answer.style.background = "green";
    }
    else {
        answer.classList.add("incorrect");
        answer.style.background = "red";
    }
    for (var i = 0; i < answers.length; i++) {
        answers[i].removeAttribute("onclick");
    }
}