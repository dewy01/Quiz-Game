using System;
using System.Collections.Generic;
using System.IO;

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
            string commonPath = "../../Data/";
            string fileName = Path.GetFileNameWithoutExtension(quizName.Substring(commonPath.Length));
            PlayedQuizzes.Add(new PlayedQuiz(fileName, score));
        }

        public void DisplayPlayedQuizzes()
        {
            Console.Clear();
            Program.PrintCentered("╔══════════════════════════════════════════════════════════╗", false);
            Program.PrintCentered($"║     Historia rozegranych quizów dla użytkownika {UserName}     ║", false);
            Program.PrintCentered("╚══════════════════════════════════════════════════════════╝", false);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Program.PrintCentered("Quiz | Wynik",false);
            Console.ResetColor();

            foreach (var playedQuiz in PlayedQuizzes)
            {
                Program.PrintCentered($"{playedQuiz.QuizName,-5} | {playedQuiz.Score}",false);
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