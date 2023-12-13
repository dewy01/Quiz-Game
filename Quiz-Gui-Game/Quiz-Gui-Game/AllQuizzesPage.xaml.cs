using Quiz_Gui_Game.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Quiz_Gui_Game
{
    public partial class AllQuizzesPage : Page
    {
        private ObservableCollection<PlayedQuiz> allQuizzes;

        public AllQuizzesPage(User user)
        {
            InitializeComponent();
            allQuizzes = new ObservableCollection<PlayedQuiz>(user.PlayedQuizzes);

            if (allQuizzes.Count > 0)
            {
                QuizzesListBox.ItemsSource = allQuizzes;
            }
            else
            {
                MessageBox.Show("No quizzes to show");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            NavigationService.GoBack();
        }
    }
}
