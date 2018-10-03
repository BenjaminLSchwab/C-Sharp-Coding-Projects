using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyOne
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Welcome to back alley no-limit to-the-death black jack. What is your name?");
            string playerName = Console.ReadLine();

            Console.WriteLine("How much money do you have? (honor system)");
            bool validInput = false;
            int bank = 0;
            while (!validInput)
            {
                try
                {
                    bank = Convert.ToInt32(Console.ReadLine());
                    validInput = true;
                }
                catch
                {
                    Console.WriteLine("Please input a whole number. This is the big leagues we dont care about cents!");
                    validInput = false;
                }
            }

            Console.WriteLine("Well, {0}, are you ready to begin?", playerName);
            string input = Console.ReadLine().ToLower();

            if (!input.Contains("y"))
            {
                Console.WriteLine("Have a nice day!");
                Console.ReadLine();
                return;
            }

            Player player = new Player(playerName, bank);
            Game game = new TwentyOneGame();
            game += player;
            player.IsActivleyPlaying = true;

            while (player.IsActivleyPlaying && player.Balance > 0)
            {
                game.Play();
            }

            game -= player;
            Console.WriteLine("Thanks for playing!");
            Console.ReadLine();

        }

        
    }
}
