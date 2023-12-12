using QuizGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Controller
{
    public class QuizCreator
    {
        public Quiz CreateNewQuiz(string quizName)
        {
            Quiz newQuiz = new Quiz();

            while (true)
            {
                Console.Clear();
                Program.PrintCentered("╔════════════════════════════════════════════════╗", false);
                Program.PrintCentered($"             Nowy Quiz: {quizName}                ", false);
                Program.PrintCentered("╚════════════════════════════════════════════════╝", false);
                Console.WriteLine();
                Program.PrintCentered("Podaj pytania dla nowego quizu. Aby zakończyć, naciśnij Enter bez wpisywania treści pytania.", false);
                Console.WriteLine();
                string content = Program.ReadCenteredInput("Nowe pytanie: ");
                Console.WriteLine();

                if (string.IsNullOrWhiteSpace(content))
                    break;

                List<string> options = new List<string>();
                for (int i = 0; i < 3; i++)
                {
                    string option = Program.ReadCenteredInput($"Odpowiedź {i + 1}:");
                    Console.WriteLine();
                    options.Add(option);
                }

                Program.PrintCentered("Podaj numer poprawnej odpowiedzi (1-3):", false);
                int correctOptionIndex;
                while (!int.TryParse(Program.ReadCenteredInput(""), out correctOptionIndex) || correctOptionIndex < 1 || correctOptionIndex > 3)
                {
                    Program.PrintCentered("Nieprawidłowy numer. Podaj ponownie:", false);
                }

                newQuiz.AddQuestion(new Question(content, options, correctOptionIndex - 1));
            }

            return newQuiz;
        }
    }
}
