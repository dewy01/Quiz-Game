using Quiz_Gui_Game.Controller;
using Quiz_Gui_Game.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Quiz_Gui_Game
{
    public partial class SelectedQuiz : Page
    {
        private string FilePath;
        private Quiz quiz;
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
            ExitButton.Opacity = 0;
            ExitButton.IsEnabled = false;
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
            string fileName = System.IO.Path.GetFileNameWithoutExtension(FilePath.Substring("../../Data/".Length));
            QuizName.Content = fileName;
            foreach (var question in loadedQuestions)
            {
                quiz.AddQuestion(question);
            }
        }

        private void UpdateUI()
        {
            if (quiz.GetCurrentQuestionIndex() < quiz.GetTotalQuestions())
            {
                TimerLabel.Content = "Pozostały czas: 10s";
                QuestionLabel.Content = quiz.GetQuestionContent(quiz.GetCurrentQuestionIndex());

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

                    ApplyRadioButtonStyle(radioButton);

                    OptionsStackPanel.Children.Add(radioButton);
                }

                StartTimer();
            }
            else
            {
                if (quiz.GetTotalQuestions() == 0)
                {
                    QuestionLabel.Content = "Niepoprawny format pliku";
                    ExitButton.Opacity = 1;
                    ExitButton.IsEnabled = true;
                    return;
                }
                MessageBox.Show($"Quiz zakończony! Wynik: {quiz.GetScore()}");
                user.AddPlayedQuiz(FilePath, quiz.GetScore());
                NavigationService.GoBack();
                NavigationService.GoBack();
            }
        }

        private void ApplyRadioButtonStyle(RadioButton radioButton)
        {
            Style radioButtonStyle = new Style(typeof(RadioButton));

            radioButtonStyle.Setters.Add(new Setter(Control.MarginProperty, new Thickness(0, 5, 0, 0)));
            radioButtonStyle.Setters.Add(new Setter(Control.VerticalAlignmentProperty, VerticalAlignment.Center));

            radioButtonStyle.Setters.Add(new Setter(RadioButton.ForegroundProperty, Brushes.Black));
            radioButtonStyle.Setters.Add(new Setter(RadioButton.FontSizeProperty, 14.0));

            radioButtonStyle.Setters.Add(new Setter(RadioButton.BorderBrushProperty, Brushes.LightSkyBlue));
            radioButtonStyle.Setters.Add(new Setter(RadioButton.BorderThicknessProperty, new Thickness(2)));
            radioButtonStyle.Setters.Add(new Setter(RadioButton.PaddingProperty, new Thickness(15, 2, 15, 2)));

            Trigger mouseOverTrigger = new Trigger
            {
                Property = UIElement.IsMouseOverProperty,
                Value = true
            };

            mouseOverTrigger.Setters.Add(new Setter(RadioButton.BackgroundProperty, Brushes.White));
            mouseOverTrigger.Setters.Add(new Setter(RadioButton.BorderBrushProperty, Brushes.LightSkyBlue));

            radioButtonStyle.Triggers.Add(mouseOverTrigger);

            radioButton.Style = radioButtonStyle;
        }



        private void Timer_Tick(object sender, EventArgs e)
        {
            TimerLabel.Content = $"Pozostały czas: {--timeLimitSeconds}s";

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
            TimerLabel.Content = string.Empty;
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

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
