using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace TwentyOne
{
    public class Dealer
    {
        public string Name { get; set; }
        public Deck Deck { get; set; }
        public int Balance { get; set; }

        public void Deal(List<Card> Hand)
        {
            Hand.Add(Deck.Cards.First());
            string card = String.Format(Deck.Cards.First().ToString() + "\n");
            Thread.Sleep(500);
            Console.WriteLine(card);
            using (StreamWriter file = new StreamWriter(@"C:\Users\DwarfKhan\Documents\work\TechAcademyLocalRepos\C-Sharp-Coding-Projects\TwentyOne\logs\cardlog.txt", true))
            {
                file.WriteLine(DateTime.Now);
                file.WriteLine(card);
            }
                Deck.Cards.RemoveAt(0);
        }
    }
}
