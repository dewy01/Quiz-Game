﻿using QuizGame.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Data
{
    public class QuizDataHandler
    {
        private string filePath;

        public QuizDataHandler(string filePath)
        {
            this.filePath = filePath;
        }

        public List<Question> LoadQuestions()
        {
            List<Question> loadedQuestions = new List<Question>();

            try
            {
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);

                    if (lines.Length % 5 != 0)
                    {
                        Console.WriteLine("Błąd formatu pliku. Nieprawidłowa liczba linii.");
                        return loadedQuestions;
                    }

                    for (int i = 0; i < lines.Length; i += 5)
                    {
                        string content = lines[i];
                        List<string> options = new List<string> { lines[i + 1], lines[i + 2], lines[i + 3] };
                        int correctOptionIndex;

                        if (!int.TryParse(lines[i + 4], out correctOptionIndex) || correctOptionIndex < 1 || correctOptionIndex > 3)
                        {
                            Console.WriteLine("Błąd formatu pliku. Nieprawidłowy indeks poprawnej odpowiedzi.");
                            return loadedQuestions;
                        }

                        loadedQuestions.Add(new Question(content, options, correctOptionIndex - 1));
                    }
                }
                else
                {
                    Console.WriteLine("Plik nie istnieje.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wystąpił błąd podczas odczytu pliku: " + ex.Message);
            }

            return loadedQuestions;
        }

        public void SaveQuestions(List<Question> questions)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    foreach (var question in questions)
                    {
                        sw.WriteLine(question.Content);
                        foreach (var option in question.Options)
                        {
                            sw.WriteLine(option);
                        }
                        sw.WriteLine(question.CorrectOptionIndex + 1);
                    }
                }

                Console.WriteLine("Quiz został zapisany pomyślnie.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wystąpił błąd podczas zapisywania pliku: " + ex.Message);
            }
        }
    }
}
