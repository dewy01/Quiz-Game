using QuizGame.Data;
using QuizGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "../../Data/quiz.txt";
            QuizDataHandler dataHandler = new QuizDataHandler(filePath);
            List<Question> loadedQuestions = dataHandler.LoadQuestions();
            Quiz quiz = new Quiz();
            foreach (var question in loadedQuestions)
            {
                quiz.AddQuestion(question);
                Console.WriteLine(question);
            }
            if (loadedQuestions.Count <= 0)
            {
                quiz.AddQuestion(new Question("Ile to 2+2?", new List<string> { "A. 1", "B. 2", "C.4" }, 2));
                quiz.AddQuestion(new Question("Gdzie mieszka Jasper?", new List<string> { "A. w Łodzi", "B. w Pabianicach", "C. w Warszawie" }, 1));
            }

     
            quiz.StartGame(5);
            dataHandler.SaveQuestions(quiz.questions);
        }
    }
}
