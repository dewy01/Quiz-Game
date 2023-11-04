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

        public void StartGame(int timeLimitSeconds)
        {
            Console.WriteLine("Witaj w Quizie!");

            foreach (var question in questions)
            {
                Console.WriteLine($"Czas na odpowiedź na to pytanie: {timeLimitSeconds} sekundy");

                System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();

                DisplayQuestion(question);

                var inputThread = new Thread(() => GetUserChoiceInBackground(stopwatch,question));
                inputThread.Start();

                if (inputThread.Join(TimeSpan.FromSeconds(timeLimitSeconds)))
                {
                    int userChoice = userChoiceResult;
                    ProcessUserChoice(userChoice, question);
                }
                else
                {
                    Console.WriteLine("\nCzas na odpowiedź minął. Niestety nie udało się odpowiedzieć w określonym czasie.");
                }
                stopwatch.Stop();
                Console.ReadKey();
                Console.Clear();
            }
            if(score/questions.Count == 1)
            {
                Animations.PlayWinAnimation();
            }
            else
            {
                Console.WriteLine($"Koniec quizu. Twój wynik to: {score}/{questions.Count}");
            }
            Console.WriteLine("Naciśnij dowolny klawisz, aby zamknąć program...");
            Console.ReadKey();
        }

        private int userChoiceResult;
        private void GetUserChoiceInBackground(System.Diagnostics.Stopwatch stopwatch, Question question)
        {
            int userChoice = 1; // Domyślna opcja
            int selectedOptionIndex = 0;

            do
            {
                Console.Clear();
                Console.WriteLine($"Pytanie: {question.Content}\n");

                for (int i = 0; i < question.Options.Count; i++)
                {
                    Console.ForegroundColor = (i == selectedOptionIndex) ? ConsoleColor.Gray : ConsoleColor.White;
                    Console.WriteLine($"{i + 1}. {question.Options[i]}");
                    Console.ResetColor();
                }

                ConsoleKeyInfo key = Console.ReadKey(true);
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

        /*
        private void GetUserChoiceInBackground(System.Diagnostics.Stopwatch stopwatch, Question question)
        {
            Console.WriteLine($"Pytanie: {question.Content}\n");

            for (int i = 0; i < question.Options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {question.Options[i]}");
            }

            Console.Write("Wybierz odpowiedź (podaj numer): ");
            int userChoice;
            while (!int.TryParse(Console.ReadLine(), out userChoice) || userChoice < 1 || userChoice > 4)
            {
                Console.WriteLine("Nieprawidłowy wybór. Wybierz ponownie.");
            }

            userChoiceResult = userChoice;
            stopwatch.Stop();
        }

        private void DisplayQuestion(Question question)
        {
            Console.WriteLine($"Pytanie: {question.Content}\n");

            for (int i = 0; i < question.Options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {question.Options[i]}");
            }

            Console.Write("Wybierz odpowiedź (podaj numer): ");
        }
        */

        private void ProcessUserChoice(int userChoice, Question question)
        {
            if (question.IsCorrect(userChoice))
            {
                Console.WriteLine("Poprawna odpowiedź!\n");
                score++;
            }
            else
            {
                Console.WriteLine($"Błędna odpowiedź. Prawidłowa odpowiedź to: {question.Options[question.CorrectOptionIndex]}\n");
            }
        }
    }

}
