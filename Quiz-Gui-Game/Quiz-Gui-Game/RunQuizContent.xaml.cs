using Quiz_Gui_Game.Controller;
using Quiz_Gui_Game.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Quiz_Gui_Game
{
    public partial class RunQuizContent : Page
    {
        private int selectedFileIndex = 0;
        private int confirmationIndex = 0;
        private List<QuizInfo> quizzes;
        private string folderPath = "../../Data/";

        public RunQuizContent()
        {
            InitializeComponent();
            LoadQuizzes();
            UpdateUI();
        }

        private void LoadQuizzes()
        {
            try
            {
                string[] files = Directory.GetFiles(folderPath);
                quizzes = new List<QuizInfo>();

                foreach (var file in files)
                {
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(file.Substring(folderPath.Length));
                    quizzes.Add(new QuizInfo { QuizName = fileName, FilePath = file });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading quizzes: {ex.Message}");
                quizzes = new List<QuizInfo>();
            }
        }

        private void UpdateUI()
        {
            QuizzesListBox.ItemsSource = null;
            QuizzesListBox.ItemsSource = quizzes;
        }

        private void QuizzesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedFileIndex = QuizzesListBox.SelectedIndex;
        }

        private void StartQuizButton_Click(object sender, RoutedEventArgs e)
        {
            if (confirmationIndex == 0 && selectedFileIndex >= 0 && selectedFileIndex < quizzes.Count)
            {
                LoadAndStartQuiz(quizzes[selectedFileIndex].FilePath);
            }
            else
            {
                MessageBox.Show("Please select a quiz and confirm to start.");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ConfirmationButton_Click(object sender, RoutedEventArgs e)
        {
            confirmationIndex = 1 - confirmationIndex;
            UpdateUI();
        }

        private void LoadAndStartQuiz(string filePath)
        {
            SelectedQuiz selectedQuizPage = new SelectedQuiz();

            selectedQuizPage.SetFilePath(filePath);

            NavigationService.Navigate(selectedQuizPage);
        }
    }

    public class QuizInfo
    {
        public string QuizName { get; set; }
        public string FilePath { get; set; }
    }
}
