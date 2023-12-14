using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Quiz_Gui_Game.Model
{
    public class QuizSet
    {
        public List<Quiz> Quizzes { get; set; }

        public QuizSet()
        {
            Quizzes = new List<Quiz>();
        }

        public void AddQuiz(Quiz quiz)
        {
            Quizzes.Add(quiz);
        }

        public void RemoveQuiz(int quizIndex)
        {
            if (quizIndex >= 0 && quizIndex < Quizzes.Count)
            {
                Quizzes.RemoveAt(quizIndex);
            }
            else
            {
                MessageBox.Show("Błąd usuwania");
            }
        }


    }
}
