using Quiz_Gui_Game.Animations;
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
            PageTransition.FadeIn(this, 0.5);
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
                QuizNameTextBlock.Content = $"{playedQuiz.QuizName}";
                ScoreTextBlock.Content = $"|   {playedQuiz.Score}pkt";

                await Task.Delay(1000);

                currentIndex++;
                DisplayNextQuiz();
            }
            else
            {
                QuizNameTextBlock.Content = $"Brak historii Quizów";
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            PageTransition.FadeOut(this, 0.5);
            NavigationService.GoBack();
        }
    }
}
