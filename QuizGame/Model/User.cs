using System;
using System.Collections.Generic;

namespace QuizGame.Model
{
    public class User
    {
        public string UserName { get; }
        public List<PlayedQuiz> PlayedQuizzes { get; }

        public User(string userName)
        {
            UserName = userName;
            PlayedQuizzes = new List<PlayedQuiz>();
        }

        public void AddPlayedQuiz(string quizName, string score)
        {
            PlayedQuizzes.Add(new PlayedQuiz(quizName, score));
        }

        public void DisplayPlayedQuizzes()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Historia rozegranych quizów dla użytkownika {UserName}:");
            Console.ResetColor();

            foreach (var playedQuiz in PlayedQuizzes)
            {
                Console.WriteLine($"Quiz: {playedQuiz.QuizName}, Wynik: {playedQuiz.Score}");
            }
        }
    }

    public class PlayedQuiz
    {
        public string QuizName { get; }
        public string Score { get; }

        public PlayedQuiz(string quizName, string score)
        {
            QuizName = quizName;
            Score = score;
        }
    }
}