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

        public static string CongratsAsciiArt = @"
 _       _                                          _ 
( )  _  ( )                                        ( )
| | ( ) | | _   _    __   _ __   _ _   ___     _ _ | |
| | | | | |( ) ( ) /'_ `\( '__)/'_` )/' _ `\ /'_` )| |
| (_/ \_) || (_) |( (_) || |  ( (_| || ( ) |( (_| || |
`\___x___/'`\__, |`\__  |(_)  `\__,_)(_) (_)`\__,_)(_)
           ( )_| |( )_) |                          (_)
           `\___/' \___/'                             
";

        public static string firstAsciiArt = @" ██████╗ ██╗   ██╗██╗███████╗ ██████╗ ██╗    ██╗ █████╗ ███╗   ██╗██╗███████╗    ██████╗ ██╗   ██╗
██╔═══██╗██║   ██║██║╚══███╔╝██╔═══██╗██║    ██║██╔══██╗████╗  ██║██║██╔════╝    ██╔══██╗╚██╗ ██╔╝
██║   ██║██║   ██║██║  ███╔╝ ██║   ██║██║ █╗ ██║███████║██╔██╗ ██║██║█████╗      ██████╔╝ ╚████╔╝ 
██║▄▄ ██║██║   ██║██║ ███╔╝  ██║   ██║██║███╗██║██╔══██║██║╚██╗██║██║██╔══╝      ██╔══██╗  ╚██╔╝  
╚██████╔╝╚██████╔╝██║███████╗╚██████╔╝╚███╔███╔╝██║  ██║██║ ╚████║██║███████╗    ██████╔╝   ██║   
 ╚══▀▀═╝  ╚═════╝ ╚═╝╚══════╝ ╚═════╝  ╚══╝╚══╝ ╚═╝  ╚═╝╚═╝  ╚═══╝╚═╝╚══════╝    ╚═════╝    ╚═╝   ";

        public static string secondAsciiArt = @"██████╗  █████╗ ██╗    ██╗██╗██████╗                                                              
██╔══██╗██╔══██╗██║    ██║██║██╔══██╗                                                             
██║  ██║███████║██║ █╗ ██║██║██║  ██║                                                             
██║  ██║██╔══██║██║███╗██║██║██║  ██║                                                             
██████╔╝██║  ██║╚███╔███╔╝██║██████╔╝                                                             
╚═════╝ ╚═╝  ╚═╝ ╚══╝╚══╝ ╚═╝╚═════╝                                                              
";

        public static string thirdAsciiArt = @"                                                                                                  
██╗    ██╗ █████╗ ███████╗███████╗██╗  ██╗██╗███████╗██╗    ██╗██╗ ██████╗███████╗                
██║    ██║██╔══██╗██╔════╝╚══███╔╝██║ ██╔╝██║██╔════╝██║    ██║██║██╔════╝╚══███╔╝                
██║ █╗ ██║███████║███████╗  ███╔╝ █████╔╝ ██║█████╗  ██║ █╗ ██║██║██║       ███╔╝                 
██║███╗██║██╔══██║╚════██║ ███╔╝  ██╔═██╗ ██║██╔══╝  ██║███╗██║██║██║      ███╔╝                  
╚███╔███╔╝██║  ██║███████║███████╗██║  ██╗██║███████╗╚███╔███╔╝██║╚██████╗███████╗                
 ╚══╝╚══╝ ╚═╝  ╚═╝╚══════╝╚══════╝╚═╝  ╚═╝╚═╝╚══════╝ ╚══╝╚══╝ ╚═╝ ╚═════╝╚══════╝ ";


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

            var lines = CongratsAsciiArt.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var totalFrames = lines[0].Length;
            var frameDuration = 100;
            for (var currentFrame = 0; currentFrame < totalFrames; currentFrame++)
            {
                UpdateAsciiArtVertical(lines, currentFrame);
                Thread.Sleep(frameDuration);
            }

            Console.ResetColor();
            musicPlayer.Stop();
            Console.Clear();
        }



        public static void StartAnimation()
        {
            var lines = asciiArt.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var totalFrames = lines[0].Length;
            var frameDuration = 30; 
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
                    int currentLineHorizontalSpaces = (Console.WindowWidth - lines[i].Length) / 2;
                    displayText[i + verticalSpaces] = new string(' ', currentLineHorizontalSpaces) + lines[i].Substring(0, currentFrame + 1) + new string(' ', horizontalSpaces);
                }
                else
                {
                    int currentLineHorizontalSpaces = (Console.WindowWidth - lines[i].Length) / 2;
                    displayText[i + verticalSpaces] = new string(' ', currentLineHorizontalSpaces) + lines[i] + new string(' ', horizontalSpaces);
                }
            }

            Console.Clear();
            Console.WriteLine(string.Join(Environment.NewLine, displayText));
        }

        private static void UpdateAsciiArtVertical(string[] lines, int currentFrame)
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
                if (i <= currentFrame)
                {
                    int currentLineHorizontalSpaces = (Console.WindowWidth - lines[i].Length) / 2;
                    displayText[i + verticalSpaces] = new string(' ', currentLineHorizontalSpaces) + lines[i] + new string(' ', horizontalSpaces);
                }
                else
                {
                    displayText[i + verticalSpaces] = new string(' ', Console.WindowWidth);
                }
            }

            Console.Clear();
            Console.WriteLine(string.Join(Environment.NewLine, displayText));
        }

        public static void LoadingAnimation()
        {
            int windowHeight = Console.WindowHeight;
            int middleVertical = windowHeight / 2;

            PrintLoading("LOADING", middleVertical - 2, false);
            DisplayLoadingBar(middleVertical);
        }

        public static void DisplayLoadingBar(int middleVertical)
        {
            int progressBarWidth = 40;
            int width = Console.WindowWidth;

            for (int i = 0; i <= progressBarWidth; i++)
            {
                Console.SetCursorPosition((width - progressBarWidth -4) / 2, middleVertical);

                Console.Write("[");

                for (int j = 0; j < progressBarWidth; j++)
                {
                    Console.Write(j <= i ? "■" : " ");
                }

                Console.Write($"] {i * 100 / progressBarWidth}%");
                Thread.Sleep(40);
            }

            Console.WriteLine();
        }

        public static void PrintLoading(string text, int middleVertical, bool isSelected)
        {
            int width = Console.WindowWidth;
            int leftPadding = (width - text.Length) / 2;

            Console.SetCursorPosition(leftPadding, middleVertical);
            Console.BackgroundColor = isSelected ? ConsoleColor.DarkGray : ConsoleColor.Black;
            Console.Write(text);
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void EndAnimation()
        {
            Console.Clear();
            PrintEnd(firstAsciiArt);
            Thread.Sleep(1000);

            PrintEnd(secondAsciiArt);
            Thread.Sleep(1000);

            PrintEnd(thirdAsciiArt);
            Thread.Sleep(2000);
        }

        static void PrintEnd(string text)
        {
            int width = Console.WindowWidth;
            int leftPadding = Math.Max((width - text.Length) / 2, 0);

            Console.WriteLine(new string(' ', leftPadding));
            Console.WriteLine(text);
        }

    }

}



