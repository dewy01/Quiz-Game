using Quiz_Gui_Game.Animations;
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
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        static public User user = new User("Gracz");
        public MainMenu()
        {
            PageTransition.FadeIn(this, 0.5);
            InitializeComponent();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            RunQuizContent runQuizContent = new RunQuizContent(false , user);
            PageTransition.FadeOut(this, 0.5);
            NavigationService.Navigate(runQuizContent);
        }
        private void RandomButton_Click(object sender, RoutedEventArgs e)
        {
            RunQuizContent runQuizContent = new RunQuizContent(true , user);
            PageTransition.FadeOut(this, 0.5);
            NavigationService.Navigate(runQuizContent);
        }

        private void CreateQuizButton_Click(object sender, RoutedEventArgs e)
        {
            CreateQuizPage createQuizPage = new CreateQuizPage();
            PageTransition.FadeOut(this, 0.5);
            NavigationService.Navigate(createQuizPage);
        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            EachQuizPage eachQuizContent = new EachQuizPage(user);
            PageTransition.FadeOut(this, 0.5);
            NavigationService.Navigate(eachQuizContent);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteQuizContent deleteQuizContent = new DeleteQuizContent();
            PageTransition.FadeOut(this, 0.5);
            NavigationService.Navigate(deleteQuizContent);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
