using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyOne
{
   public class Deck
    {
        public List<Card> Cards { get; set; }

        public Deck()
        {
            string[] suits = {"Spades", "Hearts", "Diamonds", "Clubs" };
            string[] faces = { "Ace", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "King", "Queen", "Jack" };

            Cards = new List<Card>();

            foreach (string suit in suits)
            {
                foreach (string face in faces)
                {
                    Card card = new Card(face, suit);
                    Cards.Add(card);
                }
            }         

        }
    }
}
