using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino
{
    public struct Card
    {
        public Card(Face face, Suit suit)
        {
            Suit = suit;
            Face = face;
        }

        public override string ToString()
        {
            return String.Format("{0} of {1}", Face, Suit);
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
