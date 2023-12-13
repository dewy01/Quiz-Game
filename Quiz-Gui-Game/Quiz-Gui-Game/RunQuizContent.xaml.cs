using Quiz_Gui_Game.Controller;
using Quiz_Gui_Game.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static System.Net.WebRequestMethods;

namespace Quiz_Gui_Game
{
    public partial class RunQuizContent : Page
    {
        private int selectedFileIndex = -1;
        private List<QuizInfo> quizzes;
        private string folderPath = "../../Data/";
        public string[] files;
        public bool isRandom;
        public User user;

        public RunQuizContent(bool isRandom, User user)
        {
            InitializeComponent();
            LoadQuizzes();
            this.isRandom = isRandom;
            this.user = user;
            if (isRandom && files.Length > 0)
            {
                selectedFileIndex = new Random().Next(0, files.Length);
            }
            UpdateUI();
        }

        private void LoadQuizzes()
        {
            try
            {
                files = Directory.GetFiles(folderPath);
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

            if (isRandom)
            {
                QuizzesListBox.ItemsSource = QuizzesListBox.ItemsSource = new List<QuizInfo> { quizzes[selectedFileIndex] };
                QuizzesListBox.IsEnabled = false;

            }
            else
            {
                QuizzesListBox.ItemsSource = quizzes;
                QuizzesListBox.IsEnabled = true;
            }
        }


        private void QuizzesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedFileIndex = QuizzesListBox.SelectedIndex;
        }

        private void StartQuizButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedFileIndex >= 0 && selectedFileIndex < quizzes.Count)
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

        private void LoadAndStartQuiz(string filePath)
        {
            SelectedQuiz selectedQuizPage = new SelectedQuiz(user);

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
