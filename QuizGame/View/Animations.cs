using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Media;
using System.IO;
using static System.Net.WebRequestMethods;

namespace QuizGame.View
{
    public class Animations
    {

        public static string asciiArt = @"
$$$$$$$\  $$\   $$\ $$$$$$\ $$$$$$$$\  $$$$$$\  $$\      $$\  $$$$$$\  $$\   $$\ $$$$$$\ $$$$$$$$\ 
$$  __$$\ $$ |  $$ |\_$$  _|\____$$  |$$  __$$\ $$ | $\  $$ |$$  __$$\ $$$\  $$ |\_$$  _|$$  _____|
$$ /  $$ |$$ |  $$ |  $$ |      $$  / $$ /  $$ |$$ |$$$\ $$ |$$ /  $$ |$$$$\ $$ |  $$ |  $$ |      
$$ |  $$ |$$ |  $$ |  $$ |     $$  /  $$ |  $$ |$$ $$ $$\$$ |$$$$$$$$ |$$ $$\$$ |  $$ |  $$$$$\    
$$ |  $$ |$$ |  $$ |  $$ |    $$  /   $$ |  $$ |$$$$  _$$$$ |$$  __$$ |$$ \$$$$ |  $$ |  $$  __|   
$$ $$\$$ |$$ |  $$ |  $$ |   $$  /    $$ |  $$ |$$$  / \$$$ |$$ |  $$ |$$ |\$$$ |  $$ |  $$ |      
\$$$$$$ / \$$$$$$  |$$$$$$\ $$$$$$$$\  $$$$$$  |$$  /   \$$ |$$ |  $$ |$$ | \$$ |$$$$$$\ $$$$$$$$\ 
 \___$$$\  \______/ \______|\________| \______/ \__/     \__|\__|  \__|\__|  \__|\______|\________|
     \___|                                                                                         
";

        public static void Intro()
        {
            int screenWidth = Console.WindowWidth;
            int headerPosition = 1;
            int headerLength = "Dawid Waszkiewicz".Length;

            int position = 0;
            int direction = 1;

            DateTime startTime = DateTime.Now;

            while (true)
            {
                Console.SetCursorPosition((screenWidth - headerLength) / 2, headerPosition);
                Console.Write("Dawid Waszkiewicz");

                Console.SetCursorPosition(position, Console.WindowHeight / 2);
                Console.Write("QUIZZZOWANIE");

                Thread.Sleep(10);
                if ((DateTime.Now - startTime).TotalSeconds >= 2.63)
                {
                    Console.SetCursorPosition((screenWidth - headerLength) / 2, headerPosition + 1);
                    Console.Write("Zaprasza do gry");
                }
                if ((DateTime.Now - startTime).TotalSeconds >= 2.63)
                {
                    Console.ReadLine();
                    break;
                }
                Console.SetCursorPosition(position, Console.WindowHeight / 2);
                Console.Write(new string(' ', "QUIZZZOWANIE".Length));

                position += direction;

                if (position <= 0 || position + "QUIZZZOWANIE".Length >= screenWidth)
                {
                    direction *= -1;
                }
            }
            Console.Clear();
        }

        public static void PlayWinAnimation()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            string musicFile = "../../Sound/victory2.wav";
            SoundPlayer musicPlayer = new SoundPlayer();
            musicPlayer.SoundLocation = musicFile;
            musicPlayer.Play();

            for (int i = 0; i < 5; i++)
            {
                Console.Clear();
                Console.WriteLine("   *   ");
                Console.WriteLine("  ***  ");
                Console.WriteLine(" ***** ");
                Console.WriteLine("*******");
                Console.WriteLine(" ***** ");
                Console.WriteLine("  ***  ");
                Console.WriteLine("   *   ");
                Thread.Sleep(200);

                Console.Clear();
                Console.WriteLine("*  *  *");
                Console.WriteLine("  ***  ");
                Console.WriteLine(" ***** ");
                Console.WriteLine("*******");
                Console.WriteLine(" ***** ");
                Console.WriteLine("  ***  ");
                Console.WriteLine("*  *  *");
                Thread.Sleep(200);

                Console.Clear();
                Console.WriteLine("*  *  *");
                Console.WriteLine("* *** *");
                Console.WriteLine(" ***** ");
                Console.WriteLine("**   **");
                Console.WriteLine(" ***** ");
                Console.WriteLine("* *** *");
                Console.WriteLine("*  *  *");
                Thread.Sleep(200);

                Console.Clear();
                Console.WriteLine("  ***  ");
                Console.WriteLine("* *** *");
                Console.WriteLine("  ***  ");
                Console.WriteLine("** * **");
                Console.WriteLine("  ***  ");
                Console.WriteLine("* *** *");
                Console.WriteLine("  ***  ");
                Thread.Sleep(200);
            }

            Console.Clear();
            Console.WriteLine("Gratulacje! Wygrałeś!");
            Thread.Sleep(1500);

            Console.ResetColor();
            musicPlayer.Stop();
            Console.Clear();
        }



        public static void StartAnimation()
        {
            var lines = asciiArt.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var totalFrames = lines[0].Length;
            var frameDuration = 50; 
            for (var currentFrame = 0; currentFrame < totalFrames; currentFrame++)
            {
                UpdateAsciiArt(lines, currentFrame);
                Thread.Sleep(frameDuration);
            }
            Thread.Sleep(1000);
            Console.Clear();
        }

        private static void UpdateAsciiArt(string[] lines, int currentFrame)
        {
            var horizontalSpaces = (Console.WindowWidth - lines[0].Length) / 2;

            var verticalSpaces = (Console.WindowHeight - lines.Length) / 2;

            var displayText = new string[lines.Length + verticalSpaces];

            for (int i = 0; i < verticalSpaces; i++)
            {
                displayText[i] = new string(' ', Console.WindowWidth);
            }
            for (var i = 0; i < lines.Length; i++)
            {
                if (currentFrame < lines[i].Length)
                {
                    displayText[i + verticalSpaces] = new string(' ', horizontalSpaces) + lines[i].Substring(0, currentFrame + 1);
                }
                else
                {
                    displayText[i + verticalSpaces] = new string(' ', horizontalSpaces) + lines[i];
                }
            }
            Console.Clear();
            Console.WriteLine(string.Join(Environment.NewLine, displayText));
        }
    }
}


