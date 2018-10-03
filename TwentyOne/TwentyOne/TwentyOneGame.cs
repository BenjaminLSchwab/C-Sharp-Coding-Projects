using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyOne
{
    public class TwentyOneGame : Game, IWalkAway
    {
        public TwentyOneDealer Dealer { get; set; }
        public override void Play()
        {
            Dealer = new TwentyOneDealer();
            foreach (Player player in Players)
            {
                player.Hand = new List<Card>();
                player.Stay = false;

            }
            Dealer.Hand = new List<Card>();
            Dealer.Stay = false;
            Dealer.Deck = new Deck();

            Console.WriteLine("Place your bet!");

            foreach (Player player in Players)
            {
                int bet = 0;
                bool validInput = false;
                while (!validInput)
                {
                    try
                    {
                        bet = Convert.ToInt32(Console.ReadLine());
                        validInput = true;
                    }
                    catch
                    {
                        Console.WriteLine("Whole number bets only, please.");
                        validInput = false;
                    }
                }
                bool sucessfullyBet = player.Bet(bet);
                if (!sucessfullyBet)
                {
                    return;
                }
                Bets[player] = bet;
            }



        }

        public override void ListPlayers()
        {
            Console.WriteLine("21 Players:");
            base.ListPlayers();
        }

        public void WalkAway(Player player)
        {
            throw new NotImplementedException();
        }
        
    }
}
