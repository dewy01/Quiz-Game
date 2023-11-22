using QuizGame.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuizGame.Model
{
    public class Quiz
    {
        public List<Question> questions;
        private int score;
        private bool stopUserChoiceThread = false;

        public Quiz()
        {
            questions = new List<Question>();
            score = 0;
        }

        public void AddQuestion(Question question)
        {
            questions.Add(question);
        }

        public void RemoveQuestion(int questionIndex)
        {
            if (questionIndex >= 0 && questionIndex < questions.Count)
            {
                questions.RemoveAt(questionIndex);
            }
            else
            {
                Console.WriteLine("Nieprawidłowy indeks pytania.");
            }
        }

 public string StartGame(int timeLimitSeconds)
{
    Console.Clear();
            Program.PrintCentered("╔══════════════════════════════════════════════════╗", false);
            Program.PrintCentered("║                  Witaj w Quizie!                 ║", false);
            Program.PrintCentered("╚══════════════════════════════════════════════════╝", false);
            Thread.Sleep(1000);
    foreach (var question in questions)
    {
                stopUserChoiceThread = false;
                System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();

        var inputThread = new Thread(() => GetUserChoiceInBackground(stopwatch, question));
        inputThread.Start();

        if (inputThread.Join(TimeSpan.FromSeconds(timeLimitSeconds)))
        {
            int userChoice = userChoiceResult;
            ProcessUserChoice(userChoice, question);
            Console.ReadKey();
        }
        else
        {
                    stopUserChoiceThread = true;
                    Program.PrintCentered("╔══════════════════════════════════════════════════╗", false);
                    Program.PrintCentered("║              Czas na odpowiedź minął.            ║", false);
                    Program.PrintCentered("║          Kliknij 2xENTER aby przejść dalej       ║", false);
                    Program.PrintCentered("╚══════════════════════════════════════════════════╝", false);
                    stopwatch.Stop();
                    Console.ReadKey();
        }

    }

    Console.Clear();
    if (questions.Count != 0 && (score / questions.Count) == 1)
    {
        Animations.PlayWinAnimation();
    }
            else if (questions.Count != 0)
            {
                Console.Clear();
                Program.PrintCentered("╔════════════════════════════════════════════╗", false);
                Program.PrintCentered("║            Koniec quizu. Wyniki:           ║", false);
                Program.PrintCentered($"║             Twój wynik to: {score}/{questions.Count}             ║", false);
                Program.PrintCentered("╚════════════════════════════════════════════╝", false);
            }

            else
            {
                Program.PrintCentered("Błąd podczas ładowania quizu", false);
    }

            Program.PrintCentered("Naciśnij dowolny klawisz, aby przejść dalej...", false);
    Console.ReadKey();
    return $"{score}/{questions.Count}";
}

private int userChoiceResult;
private void GetUserChoiceInBackground(System.Diagnostics.Stopwatch stopwatch, Question question)
{
    int userChoice = 1; // Domyślna opcja
    int selectedOptionIndex = 0;

    do
    {
        Console.Clear();
        Program.PrintCentered("╔══════════════════════════════════════════════════╗", false);
                Program.PrintCentered($"                 {question.Content}               ", false);
                Program.PrintCentered($"     Czas na odpowiedź na to pytanie: {5 - stopwatch.Elapsed.TotalSeconds:F0} sekundy     ", false);
                Program.PrintCentered("╚══════════════════════════════════════════════════╝", false);
        Console.WriteLine();

        for (int i = 0; i < question.Options.Count; i++)
        {
            
                    Program.PrintCentered($"{question.Options[i]}", selectedOptionIndex == i);
            
        }

        ConsoleKeyInfo key = Console.ReadKey(true);
                if (stopUserChoiceThread)
                {
                    return;
                }
                switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                selectedOptionIndex = (selectedOptionIndex > 0) ? selectedOptionIndex - 1 : question.Options.Count - 1;
                break;
            case ConsoleKey.DownArrow:
                selectedOptionIndex = (selectedOptionIndex < question.Options.Count - 1) ? selectedOptionIndex + 1 : 0;
                break;
            case ConsoleKey.Enter:
                userChoice = selectedOptionIndex + 1;
                userChoiceResult = userChoice;
                stopwatch.Stop();
                return;
        }

    } while (true);
}

        private void ProcessUserChoice(int userChoice, Question question)
        {
            if (question.IsCorrect(userChoice))
            {
                Program.PrintCentered("Poprawna odpowiedź!\n",false);
                score++;
            }
            else
            {
                Program.PrintCentered($"Błędna odpowiedź. Prawidłowa odpowiedź to: {question.Options[question.CorrectOptionIndex]}\n",false);
            }
        }
    }

}
