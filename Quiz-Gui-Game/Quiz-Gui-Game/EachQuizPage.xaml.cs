using Quiz_Gui_Game.Model;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Quiz_Gui_Game
{
    /// <summary>
    /// Interaction logic for EachQuizPage.xaml
    /// </summary>
    public partial class EachQuizPage : Page
    {
        private User user;
        private int currentIndex;

        public EachQuizPage(User user)
        {
            this.user = user;
            InitializeComponent();
            currentIndex = 0;
            DisplayNextQuiz();
        }

        private async void DisplayNextQuiz()
        {
            if (currentIndex < user.PlayedQuizzes.Count)
            {
                var playedQuiz = user.PlayedQuizzes[currentIndex];
                QuizNameTextBlock.Text = $"{playedQuiz.QuizName}";
                ScoreTextBlock.Text = $"wynik: {playedQuiz.Score.ToString()}pkt";

                // Delay for a second (1000 milliseconds)
                await Task.Delay(1000);

                currentIndex++;
                DisplayNextQuiz();
            }
            else
            {
                // Navigate to AllQuizzesPage after displaying all quizzes
                if(user.PlayedQuizzes.Count > 0)
                {
                    AllQuizzesPage allQuizzesPage = new AllQuizzesPage(user);
                    NavigationService.Navigate(allQuizzesPage);
                }
                QuizNameTextBlock.Text = $"No quizzes to show";
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
