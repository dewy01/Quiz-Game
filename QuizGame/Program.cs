using QuizGame.Controller;
using QuizGame.Data;
using QuizGame.Model;
using QuizGame.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace QuizGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Animations.StartAnimation();
            string folderPath = "../../Data/";
            var fileList = new List<string>();
            User user = new User("Gracz");
            int selectedOptionIndex = 0;

            do
            {
                Console.Clear();
                PrintCentered("╔═══════════════════════════════════╗", selectedOptionIndex == -1);
                PrintCentered("║            Wybierz opcję:         ║", selectedOptionIndex == -1);
                PrintCentered("║                                   ║", selectedOptionIndex == -1);
                PrintCentered($"║  {((selectedOptionIndex == 0) ? ">" : " ")} 1. Zagraj                      ║", selectedOptionIndex == 0);
                PrintCentered($"║  {((selectedOptionIndex == 1) ? ">" : " ")} 2. Zrób nowy quiz              ║", selectedOptionIndex == 1);
                PrintCentered($"║  {((selectedOptionIndex == 2) ? ">" : " ")} 3. Zagraj w losowy quiz        ║", selectedOptionIndex == 2);
                PrintCentered($"║  {((selectedOptionIndex == 3) ? ">" : " ")} 4. Usuń quiz                   ║", selectedOptionIndex == 3);
                PrintCentered($"║  {((selectedOptionIndex == 4) ? ">" : " ")} 5. Wyświetl historię           ║", selectedOptionIndex == 4);
                PrintCentered($"║  {((selectedOptionIndex == 5) ? ">" : " ")} 6. Wyjdź                       ║", selectedOptionIndex == 5);
                PrintCentered("║                                   ║", selectedOptionIndex == -1);
                PrintCentered("╚═══════════════════════════════════╝", selectedOptionIndex == -1);

                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedOptionIndex = (selectedOptionIndex > 0) ? selectedOptionIndex - 1 : 5;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedOptionIndex = (selectedOptionIndex < 5) ? selectedOptionIndex + 1 : 0;
                        break;
                    case ConsoleKey.Enter:
                        if (selectedOptionIndex == 0)
                        {
                            RunQuiz(folderPath, user);
                        }
                        else if (selectedOptionIndex == 1)
                        {
                            CreateQuiz(folderPath);
                        }
                        else if (selectedOptionIndex == 2)
                        {
                            RunRandomQuiz(folderPath, user);
                        }
                        else if (selectedOptionIndex == 3)
                        {
                            DeleteQuiz(folderPath);
                        }
                        else if (selectedOptionIndex == 4)
                        {
                            DisplayHistory(user);
                        }
                        else if (selectedOptionIndex == 5)
                        {
                            Animations.EndAnimation();
                            Environment.Exit(0);
                        }
                        break;
                }

            } while (true);
        }

        static void RunQuiz(string folderPath, User user)
        {
            var fileList = new List<string>();
            int selectedFileIndex = 0;
            int confirmationIndex = 0; // 0 - Tak, 1 - Nie
            string commonPath = "../../Data/";

            try
            {
                string[] files = Directory.GetFiles(folderPath);
                Console.Clear();

                do
                {
                    Console.Clear();
                    PrintCentered("╔═══════════════════════════════════╗", false);
                    PrintCentered("║            Lista plików:          ║", false);
                    PrintCentered("╚═══════════════════════════════════╝", false);

                    for (int i = 0; i < files.Length; i++)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(files[i].Substring(commonPath.Length));
                        PrintCentered($"{((i == selectedFileIndex) ? ">" : " ")} Plik nr. {i + 1}. {fileName}", selectedFileIndex==i);
                    }

                    PrintCentered("╔═══════════════════════════════════╗", false);
                    PrintCentered($"║   Czy na pewno chcesz rozpocząć?  ║", false);
                    PrintCentered($"║            {((confirmationIndex == 0) ? "[Tak]  Nie" : "Tak  [Nie]")}             ║", false);
                    PrintCentered("╚═══════════════════════════════════╝", false);

                    ConsoleKeyInfo key = Console.ReadKey();
                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            selectedFileIndex = Math.Max(0, selectedFileIndex - 1);
                            break;
                        case ConsoleKey.DownArrow:
                            selectedFileIndex = Math.Min(files.Length - 1, selectedFileIndex + 1);
                            break;
                        case ConsoleKey.RightArrow:
                            confirmationIndex = 1;
                            break;
                        case ConsoleKey.LeftArrow:
                            confirmationIndex = 0;
                            break;
                        case ConsoleKey.Enter:
                            if (confirmationIndex == 0)
                            {
                                string score = LoadAndStartQuiz(files[selectedFileIndex]);
                                user.AddPlayedQuiz(files[selectedFileIndex], score);
                                return;
                            }
                            return;
                    }

                } while (true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                return;
            }
        }

        static void RunRandomQuiz(string folderPath, User user)
        {
            try
            {
                string[] files = Directory.GetFiles(folderPath);

                if (files.Length > 0)
                {
                    int randomIndex = new Random().Next(0, files.Length);
                    Console.Clear();
                    Animations.LoadingAnimation();
                    string score = LoadAndStartQuiz(files[randomIndex]);
                    user.AddPlayedQuiz(files[randomIndex], score);
                }
                else
                {
                    Console.WriteLine("Brak dostępnych quizów do rozegrania.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
            }
        }


        static string LoadAndStartQuiz(string filePath)
        {
            QuizDataHandler dataHandler = new QuizDataHandler();
            List<Question> loadedQuestions = dataHandler.LoadQuestions(filePath);
            Quiz quiz = new Quiz();

            foreach (var question in loadedQuestions)
            {
                quiz.AddQuestion(question);
            }

            return quiz.StartGame(5);
        }


        static void CreateQuiz(string folderPath)
        {
            QuizDataHandler dataHandler = new QuizDataHandler();
            QuizCreator quizCreator = new QuizCreator();

            Console.Clear();
            Program.PrintCentered("╔════════════════════════════════════════════════╗", false);
            Program.PrintCentered("║           Tworzenie Nowego Quizu               ║", false);
            Program.PrintCentered("╚════════════════════════════════════════════════╝", false);

            string quizName = ReadCenteredInput("Podaj nazwę nowego Quizu:");
            string quizPath = folderPath + quizName + ".txt";

            dataHandler.SaveQuestions(quizCreator.CreateNewQuiz(quizName), quizPath);
        }

        static void DeleteQuiz(string folderPath)
        {
            var fileList = new List<string>();
            int selectedFileIndex = 0;
            int confirmationIndex = 0; // 0 - Tak, 1 - Nie
            string commonPath = "../../Data/";

            try
            {
                string[] files = Directory.GetFiles(folderPath);
                Console.Clear();

                do
                {
                    Console.Clear();
                    PrintCentered("╔═══════════════════════════════════╗", false);
                    PrintCentered("║            Lista plików:          ║", false);
                    PrintCentered("╚═══════════════════════════════════╝", false);

                    for (int i = 0; i < files.Length; i++)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(files[i].Substring(commonPath.Length));
                        PrintCentered($"{((i == selectedFileIndex) ? ">" : " ")} Plik nr. {i + 1}. {fileName}", selectedFileIndex == i);
                    }

                    PrintCentered("╔═══════════════════════════════════╗", false);
                    PrintCentered($"║  Czy na pewno chcesz usunąć plik? ║", false);
                    PrintCentered($"║            {((confirmationIndex == 0) ? "[Tak]  Nie" : "Tak  [Nie]")}             ║", false);
                    PrintCentered("╚═══════════════════════════════════╝", false);

                    ConsoleKeyInfo key = Console.ReadKey();
                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            selectedFileIndex = Math.Max(0, selectedFileIndex - 1);
                            break;
                        case ConsoleKey.DownArrow:
                            selectedFileIndex = Math.Min(files.Length - 1, selectedFileIndex + 1);
                            break;
                        case ConsoleKey.RightArrow:
                            confirmationIndex = 1;
                            break;
                        case ConsoleKey.LeftArrow:
                            confirmationIndex = 0;
                            break;
                        case ConsoleKey.Enter:
                            if (confirmationIndex == 0)
                            {
                                DeleteSelectedQuiz(files[selectedFileIndex]);
                            }
                            return;
                    }

                } while (true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                return;
            }
        }


        static void DeleteSelectedQuiz(string filePath)
        {
            try
            {
                File.Delete(filePath);
                Console.WriteLine($"Quiz został usunięty: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd podczas usuwania quizu: {ex.Message}");
            }
        }

        static void DisplayHistory(User user)
        {
            DisplayHistoryStarWars(user);
            Console.Clear();
            user.DisplayPlayedQuizzes();

            PrintCentered("Naciśnij dowolny klawisz, aby wrócić do menu...", false);
            Console.ReadKey();
        }

        static void DisplayHistoryStarWars(User user)
        {

            var quizzes = user.PlayedQuizzes;

            for (int i = quizzes.Count - 1; i >= 0; i--)
            {
                Console.Clear();
                
                PrintCentered("╔══════════════════════════════════════╗", false);
                PrintCentered("║           Historia Quizów            ║", false);
                PrintCentered("╚══════════════════════════════════════╝", false);

                
                PrintCentered($"Quiz: {quizzes[i].QuizName}",false);
                PrintCentered($"Wynik: {quizzes[i].Score}", false);
                Console.ResetColor();

                Thread.Sleep(1000); // Czas trwania pojedynczego wpisu w sekundach
            }
        }


        public static void PrintCentered(string text, bool isSelected)
        {
            int width = Console.WindowWidth;
            int leftPadding = (width - text.Length) / 2;

            Console.Write(new string(' ', leftPadding));
            Console.BackgroundColor = isSelected ? ConsoleColor.DarkGray : ConsoleColor.Black;
            Console.Write(text);
            Console.ResetColor();
            Console.WriteLine();
        }

        public static string ReadCenteredInput(string prompt)
        {
            Program.PrintCentered(prompt, false);
            int leftPadding = (Console.WindowWidth - prompt.Length) / 2;
            Console.SetCursorPosition(leftPadding, Console.CursorTop);
            return Console.ReadLine();
        }


    }
}
