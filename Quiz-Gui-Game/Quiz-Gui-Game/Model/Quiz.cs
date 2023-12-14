using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Quiz_Gui_Game.Model
{
    public class Quiz
    {
        public List<Question> questions;
        private int score;
        private int currentQuestionIndex;
        private bool stopUserChoiceThread = false;

        public Quiz()
        {
            questions = new List<Question>();
            score = 0;
            currentQuestionIndex = 0;
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

            }
        }

        public async Task<List<QuestionViewModel>> StartGameAsync(int timeLimitSeconds)
        {
            List<QuestionViewModel> questionViewModels = new List<QuestionViewModel>();

            foreach (var question in questions)
            {
                stopUserChoiceThread = false;
                System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();

                await Task.Delay(timeLimitSeconds * 1000);

                if (stopUserChoiceThread)
                {

                    stopwatch.Stop();
                }
                else
                {

                    stopwatch.Stop();
                    MessageBox.Show($"Koniec czasu! Przejdź dalej");
                }

                questionViewModels.Add(new QuestionViewModel
                {
                    Content = question.Content,
                    Options = question.Options,
                    CorrectOptionIndex = question.CorrectOptionIndex
                });

                IncrementCurrentQuestionIndex();
            }

            return questionViewModels;
        }

        public void ProcessUserChoice(StackPanel optionsStackPanel)
        {
            var question = questions[currentQuestionIndex];
            int userChoice = FindSelectedRadioButton(optionsStackPanel);
            if (userChoice != -1)
            {
                if (!stopUserChoiceThread)
                {
                    if (question.IsCorrect(userChoice))
                    {
                        MessageBox.Show("Poprawnie!");
                        score++;
                    }
                    else
                    {
                        MessageBox.Show($"Niepoprawna odpowiedź!");
                    }
                }
            }
        }

        private int FindSelectedRadioButton(StackPanel optionsStackPanel)
        {
            foreach (var radioButton in optionsStackPanel.Children)
            {
                if (radioButton is RadioButton && ((RadioButton)radioButton).IsChecked == true)
                {
                    return Convert.ToInt32(((RadioButton)radioButton).Tag);
                }
            }
            return -1;
        }

        public int GetCurrentQuestionIndex()
        {
            return currentQuestionIndex;
        }

        public string GetQuestionContent(int index)
        {
            return questions[index].Content;
        }

        public List<String> GetQuestionOptions(int index)
        {
            return questions[index].Options;
        }

        public int GetTotalQuestions()
        {
            return questions.Count;
        }

        public int GetScore()
        {
            return score;
        }

        public void IncrementCurrentQuestionIndex()
        {
            currentQuestionIndex++;
        }
    }

    public class QuestionViewModel
    {
        public string Content { get; set; }
        public List<string> Options { get; set; }
        public int CorrectOptionIndex { get; set; }
    }
}
