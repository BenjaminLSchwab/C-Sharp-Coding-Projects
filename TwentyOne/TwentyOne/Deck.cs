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
            Suit[] suits = {Suit.Clubs, Suit.Diamonds, Suit.Hearts, Suit.Spades };
            Face[] faces = {Face.Ace, Face.Two, Face.Three, Face.Four, Face.Five, Face.Six, Face.Seven, Face.Eight, Face.Nine, Face.Ten, Face.Jack, Face.Queen, Face.King};

            Cards = new List<Card>();

            foreach (Suit suit in suits)
            {
                foreach (Face face in faces)
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
