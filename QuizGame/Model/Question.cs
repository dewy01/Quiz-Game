using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Model
{
    public class Question
    {
        public string Content { get; set; }
        public List<string> Options { get; set; }
        public int CorrectOptionIndex { get; set; }

        public Question(string content, List<string> options, int correctOptionIndex)
        {
            Content = content;
            Options = options;
            CorrectOptionIndex = correctOptionIndex;
        }

        public void DisplayQuestion()
        {
            Console.WriteLine(Content);

            for (int i = 0; i < Options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Options[i]}");
            }
        }

        public bool IsCorrect(int userChoice)
        {
            return userChoice - 1 == CorrectOptionIndex;
        }
    }

}
