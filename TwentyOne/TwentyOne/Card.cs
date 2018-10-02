using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyOne
{
    public class Card
    {
        public Card(Face face, Suit suit)
        {
            Suit = suit;
            Face = face;
        }

        public string Display()
        {
            return Face + " of " + Suit;
        }

        public Suit Suit { get; set; }
        public Face Face { get; set; }

    }
    public enum Suit
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades
    }
    public enum Face
    {
        Ace,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }
}
