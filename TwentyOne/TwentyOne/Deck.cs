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

        public void Shuffle(int times = 1)
        {
            Random random = new Random();
            for (int i = 0; i < times; i++)
            {

                List<Card> tempList = new List<Card>();

                while (Cards.Count > 0)
                {
                    int randomIndex = random.Next(0, Cards.Count);
                    tempList.Add(Cards[randomIndex]);
                    Cards.RemoveAt(randomIndex);
                }

                Cards = tempList;
            }
        }
    }
}
