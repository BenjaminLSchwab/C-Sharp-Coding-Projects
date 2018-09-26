using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyOne
{
    public class Card
    {
        public Card(string face, string suit)
        {
            Suit = suit;
            Face = face;
        }

        public string Display()
        {
            return Face + " of " + Suit;
        }

        public string Suit { get; set; }
        public string Face { get; set; }

    }
}
