using System;
using System.IO;
using System.Linq;

namespace Bowling
{
    public class Program
    {
        private static string QuitCommand = "Q";

        static void Main(string[] args)
        {
            Game game = new Game();

            string entry = string.Empty;

            do
            {
                Console.WriteLine("Enter pins knocked down. (Q) for quit.");
                entry = Console.ReadLine();

                if(entry.All(char.IsDigit) && entry != QuitCommand)
                {
                    game.Roll(int.Parse(entry));
                }

            } while (entry.ToUpper() != QuitCommand);

            Console.WriteLine($"Score is: {game.Score()}");
            StreamWriter sw = new StreamWriter("C:\\katas\\score.txt");
            sw.WriteLine($"Score is: {game.Score()}");
            sw.Close();

            Game fileGame = new Game();

            try
            {
                StreamReader sr = new StreamReader("C:\\katas\\input.txt");
                string readPin = string.Empty;

                do
                {
                    readPin = sr.ReadLine();
                    bool isReadPin = int.TryParse(readPin, out int pin);
                    if (isReadPin) { fileGame.Roll(pin); }

                } while (readPin != null);

                Console.WriteLine($"Score from input file is: {fileGame.Score()}");
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}