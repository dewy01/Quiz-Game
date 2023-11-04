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
        static void Main(string[] args) { 
        
            Animations.Intro();
            string folderPath = "../../Data/";
            var fileList = new List<string>();
            int selectedOptionIndex = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("Wybierz opcję:");
                Console.BackgroundColor = (selectedOptionIndex == 0) ? ConsoleColor.DarkGray : ConsoleColor.Black;
                Console.WriteLine("1. Zagraj");
                Console.ResetColor();
                Console.BackgroundColor = (selectedOptionIndex == 1) ? ConsoleColor.DarkGray : ConsoleColor.Black;
                Console.WriteLine("2. Zrób nowy quiz");
                Console.ResetColor();
                Console.BackgroundColor = (selectedOptionIndex == 2) ? ConsoleColor.DarkGray : ConsoleColor.Black;
                Console.WriteLine("3. Wyjdź");
                Console.ResetColor();

                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedOptionIndex = (selectedOptionIndex > 0) ? selectedOptionIndex - 1 : 2;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedOptionIndex = (selectedOptionIndex < 2) ? selectedOptionIndex + 1 : 0;
                        break;
                    case ConsoleKey.Enter:
                        if (selectedOptionIndex == 0)
                        {
                            RunQuiz(folderPath);
                        }
                        else if (selectedOptionIndex == 1)
                        {
                            CreateQuiz(folderPath);
                        }
                        else if (selectedOptionIndex == 2)
                        {
                            Environment.Exit(0);
                        }
                        break;
                }

            } while (true);
        }

        static void RunQuiz(string folderPath)
        {
            var fileList = new List<string>();
            int selectedFileIndex = 0;

            try
            {
                string[] files = Directory.GetFiles(folderPath);
                Console.Clear();
                Console.WriteLine("Lista plików:");
                int i = 1;
                foreach (var file in files)
                {
                    Console.WriteLine($"Plik nr. {i}. {file}");
                    i++;
                }
                fileList = files.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                return;
            }

            Console.WriteLine("Poruszaj się strzałkami (góra/dół), Enter aby wybrać quiz:");

            do
            {
                ConsoleKeyInfo key = Console.ReadKey();
                Console.Clear();

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedFileIndex = Math.Max(0, selectedFileIndex - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        selectedFileIndex = Math.Min(fileList.Count - 1, selectedFileIndex + 1);
                        break;
                    case ConsoleKey.Enter:
                        LoadAndStartQuiz(fileList.ElementAt(selectedFileIndex));
                        return;
                }

                Console.WriteLine("Lista plików:");
                for (int i = 0; i < fileList.Count; i++)
                {
                    if (i == selectedFileIndex)
                        Console.BackgroundColor = ConsoleColor.DarkGray;

                    Console.WriteLine($"Plik nr. {i + 1}. {fileList[i]}");
                    Console.ResetColor();
                }

                Console.WriteLine("Poruszaj się strzałkami (góra/dół), Enter aby wybrać quiz:");
            } while (true);
        }

        static void LoadAndStartQuiz(string filePath)
        {
            QuizDataHandler dataHandler = new QuizDataHandler();
            List<Question> loadedQuestions = dataHandler.LoadQuestions(filePath);
            Quiz quiz = new Quiz();

            foreach (var question in loadedQuestions)
            {
                quiz.AddQuestion(question);
            }

            quiz.StartGame(5);
        }


        static void CreateQuiz(string folderPath)
        {
            QuizDataHandler dataHandler = new QuizDataHandler();
            QuizCreator quizCreator = new QuizCreator();

            Console.WriteLine("Podaj nazwę nowego Quizu");
            string quizpath = folderPath + Console.ReadLine() + ".txt";
            dataHandler.SaveQuestions(quizCreator.CreateNewQuiz(), quizpath);
        }

    }
}
