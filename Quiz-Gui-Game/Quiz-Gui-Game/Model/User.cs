using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Gui_Game.Model
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

        public void AddPlayedQuiz(string quizName, int score)
        {
            string commonPath = "../../Data/";
            string fileName = System.IO.Path.GetFileNameWithoutExtension(quizName.Substring(commonPath.Length));
            PlayedQuizzes.Add(new PlayedQuiz(fileName, score));
        }
    }

    public class PlayedQuiz
    {
        public string QuizName { get; }
        public int Score { get; }

        public PlayedQuiz(string quizName, int score)
        {
            QuizName = quizName;
            Score = score;
        }
    }
}
