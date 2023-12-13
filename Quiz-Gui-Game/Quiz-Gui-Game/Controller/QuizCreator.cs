using Quiz_Gui_Game.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Gui_Game.Controller
{
    public class QuizCreator
    {

        private Quiz newQuiz;

        public QuizCreator()
        {
            newQuiz = new Quiz();
        }

        public void AddQuestion(string content, List<string> options, int correctOptionIndex)
        {
            // Add logic to create and add questions
            newQuiz.AddQuestion(new Question(content, options, correctOptionIndex));
        }

    }
}
