using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuizGame.View
{
    public class Animations
    {
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
            Console.Clear();
        }
    }
}
