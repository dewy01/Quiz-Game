using QuizGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Controller
{
    public class QuizCreator
    {
        public Quiz CreateNewQuiz()
        {
            Quiz newQuiz = new Quiz();
            Console.WriteLine("Podaj pytania dla nowego quizu. Aby zakończyć, naciśnij Enter bez wpisywania treści pytania.");

            while (true)
            {
                Console.WriteLine("Nowe pytanie:");
                string content = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(content))
                    break;

                List<string> options = new List<string>();
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine($"Odpowiedź {i + 1}:");
                    string option = Console.ReadLine();
                    options.Add(option);
                }

                Console.WriteLine("Podaj numer poprawnej odpowiedzi (1-3):");
                int correctOptionIndex;
                while (!int.TryParse(Console.ReadLine(), out correctOptionIndex) || correctOptionIndex < 1 || correctOptionIndex > 3)
                {
                    Console.WriteLine("Nieprawidłowy numer. Podaj ponownie:");
                }

                newQuiz.AddQuestion(new Question(content, options, correctOptionIndex - 1));
            }

            return newQuiz;
        }
    }
}
