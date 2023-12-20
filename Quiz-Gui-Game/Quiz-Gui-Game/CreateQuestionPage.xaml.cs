using Quiz_Gui_Game.Animations;
using Quiz_Gui_Game.Controller;
using Quiz_Gui_Game.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Quiz_Gui_Game
{
    /// <summary>
    /// Interaction logic for CreateQuestionPage.xaml
    /// </summary>
    public partial class CreateQuestionPage : Page
    {
        private string quizName;
        Quiz newQuiz = new Quiz();

        public CreateQuestionPage(string quizName)
        {
            InitializeComponent();
            this.quizName = quizName;
        }

        private async void AddQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            await PageTransition.SlideOutAsync(ContentGrid, 0.3, false);
            string questionContent = QuestionTextBox.Text;
            List<string> answers = new List<string>
            {
                Answer1TextBox.Text,
                Answer2TextBox.Text,
                Answer3TextBox.Text
            };
            int correctAnswerIndex = CorrectAnswerComboBox.SelectedIndex;
            newQuiz.AddQuestion(new Question(questionContent, answers, correctAnswerIndex));

            QuestionTextBox.Clear();
            Answer1TextBox.Clear();
            Answer2TextBox.Clear();
            Answer3TextBox.Clear();
            CorrectAnswerComboBox.SelectedIndex = -1;
            await PageTransition.SlideInAsync(ContentGrid, 0.3, true);
        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            QuizDataHandler dataHandler = new QuizDataHandler();
            string quizPath = "../../Data/" + quizName + ".txt";
            dataHandler.SaveQuestions(newQuiz, quizPath);
            NavigationService.GoBack();
            NavigationService.GoBack();
        }
    }
}
