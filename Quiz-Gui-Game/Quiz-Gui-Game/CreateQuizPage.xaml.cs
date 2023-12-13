using Quiz_Gui_Game.Controller;
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
    /// Interaction logic for CreateQuizPage.xaml
    /// </summary>
    public partial class CreateQuizPage : Page
    {
        private QuizCreator quizCreator;

        public CreateQuizPage()
        {
            InitializeComponent();
            quizCreator = new QuizCreator();
        }

        private void AddQuestionButton_Click(object sender, RoutedEventArgs e)
        {

            string quizname = QuizNameTextBox.Text; 
            CreateQuestionPage createQuestionPage = new CreateQuestionPage(quizname);
            NavigationService.Navigate(createQuestionPage);
        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.GoBack();

        }
    }
}
