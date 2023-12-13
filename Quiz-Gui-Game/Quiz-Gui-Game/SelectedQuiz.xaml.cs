using Quiz_Gui_Game.Controller;
using Quiz_Gui_Game.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Quiz_Gui_Game
{
    public partial class SelectedQuiz : Page
    {
        private string FilePath;
        private Quiz quiz;
        private int currentQuestionIndex = 0;
        private int score = 0;
        public User user;

        private DispatcherTimer timer;
        private int timeLimitSeconds = 10; 

        public SelectedQuiz(User user)
        {
            this.user = user;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            InitializeComponent();
        }

        public void SetFilePath(string filePath)
        {
            this.FilePath = filePath;
            LoadAndStartQuiz();
            UpdateUI();
        }

        private void LoadAndStartQuiz()
        {
            QuizDataHandler dataHandler = new QuizDataHandler();
            List<Question> loadedQuestions = dataHandler.LoadQuestions(FilePath);

            quiz = new Quiz();
            foreach (var question in loadedQuestions)
            {
                quiz.AddQuestion(question);
            }
        }

        private void UpdateUI()
        {
            if (quiz.GetCurrentQuestionIndex() < quiz.GetTotalQuestions())
              {
                QuestionLabel.Text = quiz.GetQuestionContent(quiz.GetCurrentQuestionIndex());

                    OptionsStackPanel.Children.Clear();
                    foreach (var option in quiz.GetQuestionOptions(quiz.GetCurrentQuestionIndex()))
                    {
                        RadioButton radioButton = new RadioButton
                        {
                            Content = option,
                            GroupName = "OptionsGroup",
                            Margin = new Thickness(0, 5, 0, 0),
                            Tag = quiz.GetQuestionOptions(quiz.GetCurrentQuestionIndex()).IndexOf(option) + 1
                        };
                        OptionsStackPanel.Children.Add(radioButton);
                    }
                    StartTimer();
              }
            else
            {
                if (quiz.GetTotalQuestions() == 0)
                {
                    NavigationService.GoBack();
                }
                MessageBox.Show($"Quiz completed! Score: {quiz.GetScore()}");
                user.AddPlayedQuiz(FilePath, quiz.GetScore());
                NavigationService.GoBack();
                NavigationService.GoBack();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimerLabel.Text = $"Time Remaining: {timeLimitSeconds}s";
            timeLimitSeconds--;

            if (timeLimitSeconds < 0)
            {
                StopTimer();
                ProcessUserChoice();
                quiz.IncrementCurrentQuestionIndex();
                UpdateUI();
            }
        }

        private void StartTimer()
        {
            timeLimitSeconds = 10;
            timer.Start();
        }

        private void StopTimer()
        {
            timer.Stop();
            TimerLabel.Text = string.Empty;
        }

        private void ProcessUserChoice()
        {
            quiz.ProcessUserChoice(OptionsStackPanel);

        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {

            if (quiz.GetCurrentQuestionIndex() < quiz.GetTotalQuestions())
            {

                StopTimer();


                ProcessUserChoice();
                quiz.IncrementCurrentQuestionIndex();

                UpdateUI();
            }
        }
    }
}
