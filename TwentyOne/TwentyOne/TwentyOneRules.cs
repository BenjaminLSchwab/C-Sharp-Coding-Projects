using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyOne
{
    class TwentyOneRules
    {
        private static Dictionary<Face, int> _cardValues = new Dictionary<Face, int>()
        {
            [Face.Two] = 2,
            [Face.Three] = 3,
            [Face.Four] = 4,
            [Face.Five] = 5,
            [Face.Six] = 6,
            [Face.Seven] = 7,
            [Face.Eight] = 8,
            [Face.Nine] = 9,
            [Face.Ten] = 10,
            [Face.King] = 10,
            [Face.Queen] = 10,
            [Face.Jack] = 10,
            [Face.Ace] = 1
        };

        private static int[] GetAllPossibleHandValues(List<Card> hand)
        {
            int aceCount = hand.Count(x => x.Face == Face.Ace);
            int[] result = new int[aceCount + 1];
            int value = hand.Sum(x => _cardValues[x.Face]);
            result[0] = value;
            if (result.Length == 1) return result;
            for (int i = 1; i < result.Length; i++)
            {
                value += (i * 10);
                result[i] = value;
            }
            return result;
        }

        public static bool CheckForBlackJack(List<Card> hand)
        {
            int[] possibleValues = GetAllPossibleHandValues(hand);
            int value = possibleValues.Max();
            return value == 21;
        }

        public static bool IsBusted(List<Card> hand)
        {
            int value = GetAllPossibleHandValues(hand).Min();
            return value > 21;
        }

        public static bool ShouldDealerStay(List<Card> hand)
        {
            int[] possibleValues = GetAllPossibleHandValues(hand);
            foreach (int value in possibleValues)
            {
                if (value < 17)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool? CompareHands(List<Card> playerHand, List<Card> dealerHand)
        {
            int[] playerResults = GetAllPossibleHandValues(playerHand);
            int[] dealerResults = GetAllPossibleHandValues(dealerHand);

            int playerBestValue = playerResults.Where(x => x < 22).Max();
            int dealerBestValue = dealerResults.Where(x => x < 22).Max();

            if (playerBestValue > dealerBestValue) return true;
            if (playerBestValue < dealerBestValue) return false;
            return null;
        }
    }
}
