using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace TwentyOne
{
    public class TwentyOneGame : Game, IWalkAway
    {

        public TwentyOneDealer Dealer { get; set; }

        public override void Play()
        {            
            Console.Clear();
            Dealer = new TwentyOneDealer();
            foreach (Player player in Players)
            {
                player.Hand = new List<Card>();
                player.Stay = false;

            }
            Dealer.Hand = new List<Card>();
            Dealer.Stay = false;
            Dealer.Deck = new Deck();
            Dealer.Deck.Shuffle();


            foreach (Player player in Players)
            {
                Console.WriteLine("{0} has a balance of {1}.", player.Name, player.Balance);
                Console.WriteLine("{0}, place your bet!", player.Name);
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

            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine("Dealing...");
                foreach (Player player in Players)
                {
                    Console.Write("{0}: ", player.Name);
                    Dealer.Deal(player.Hand);
                    if (i == 1)
                    {
                        bool blackJack = TwentyOneRules.CheckForBlackJack(player.Hand);
                        if (blackJack)
                        {
                            Console.WriteLine("BlackJack! {0} wins {1}", player.Name, Convert.ToInt32((float)Bets[player] * 1.5));
                            player.Balance = Convert.ToInt32(((float)Bets[player] * 1.5) + Bets[player]);
                            Dealer.Balance -= Convert.ToInt32(Bets[player] * 1.5);
                            PlayAgainPrompt(player);
                            return;
                        }
                    }
                }

                if (i == 1)
                {
                Console.Write("Dealer: ");
                }
                else
                {
                    Console.Write("Dealer: Hidden Card\n");
                }

                Dealer.Deal(Dealer.Hand, (i == 1)? false : true); //dealers first card is hidden
                if (i == 1)
                {
                    bool blackJack = TwentyOneRules.CheckForBlackJack(Dealer.Hand);
                    if (blackJack)
                    {
                        Console.WriteLine("Dealer got BlackJack! Everyone loses.");
                        foreach (KeyValuePair<Player, int> entry in Bets)
                        {
                            Dealer.Balance += entry.Value;
                        }
                        foreach (Player player in Players)
                        {
                            PlayAgainPrompt(player);
                        }
                        return;
                    }
                }
            }

            foreach (Player player in Players)
            {
                Console.WriteLine("\nDealer's top card:");
                Console.Write(Dealer.Hand.Last().ToString());
                
                while (!player.Stay)
                {
                    Console.WriteLine("\n{0}'s cards:", player.Name);
                    foreach (Card card in player.Hand)
                    {
                        Console.Write("{0}, ", card.ToString());
                    }
                    Console.WriteLine("\n\n Hit or Stay?");
                    string answer = Console.ReadLine().ToLower();
                    if (answer.Contains("s"))
                    {
                        player.Stay = true;
                        break;
                    }
                    else if (answer.Contains("h"))
                    {
                        Dealer.Deal(player.Hand);
                    }

                    bool busted = TwentyOneRules.IsBusted(player.Hand);
                    if (busted)
                    {
                        Dealer.Balance += Bets[player];
                        Console.WriteLine("{0} busted! You lose your bet of {1}. Your balance is now {2}", player.Name, Bets[player], player.Balance);
                        Dealer.Balance += Bets[player];
                        Bets.Remove(player);
                        PlayAgainPrompt(player);
                        return;
                    }

                }
            }

            Dealer.IsBusted = TwentyOneRules.IsBusted(Dealer.Hand);
            Dealer.Stay = TwentyOneRules.ShouldDealerStay(Dealer.Hand);
            Console.WriteLine("Dealer reveals his bottom card:{0}", Dealer.Hand.First().ToString());
            while (!Dealer.Stay && !Dealer.IsBusted)
            {
                Console.WriteLine("Dealer is hitting...");
                Dealer.Deal(Dealer.Hand);
                Dealer.IsBusted = TwentyOneRules.IsBusted(Dealer.Hand);
                Dealer.Stay = TwentyOneRules.ShouldDealerStay(Dealer.Hand);
            }
            if (Dealer.IsBusted)
            {
                Console.WriteLine("Dealer busted!");
                foreach (KeyValuePair<Player, int> entry in Bets)
                {
                    Console.WriteLine("{0} won {1}!", entry.Key.Name, entry.Value);
                    entry.Key.Balance += 2 * entry.Value;
                    Dealer.Balance -= entry.Value;
                }
                foreach (Player player in Players)
                {
                    PlayAgainPrompt(player);
                }
                return;
            }
            else if (Dealer.Stay) Console.WriteLine("Dealer is staying");

            foreach (Player player in Players)
            {
                bool? playerWon = TwentyOneRules.CompareHands(player.Hand, Dealer.Hand);
                if (playerWon == null)
                {
                    Console.WriteLine("Push! returning bet...");
                    player.Balance += Bets[player];
                    Bets.Remove(player);
                }
                else if (playerWon == true)
                {
                    Console.WriteLine("{0} won {1}!", player.Name, Bets[player]);
                    player.Balance += Bets[player] * 2;
                    Dealer.Balance -= Bets[player];
                    Bets.Remove(player);
                }
                else
                {
                    Console.WriteLine("{0} lost their bet of {1}.", player.Name, Bets[player]);
                    Dealer.Balance += Bets[player];
                    Bets.Remove(player);

                }
                Console.WriteLine("{0}'s balance is now {1}.", player.Name, player.Balance);
                PlayAgainPrompt(player);
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

        void PlayAgainPrompt(Player player)
        {
            Console.WriteLine("Play again?");
            string answer = Console.ReadLine().ToLower();
            if (answer.Contains("y")) player.IsActivleyPlaying = true;

            else player.IsActivleyPlaying = false;
        }


        
    }
}
