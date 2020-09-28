using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using static Poker_Judge.PokerEngine.Deck;

namespace Poker_Judge.PokerEngine
{
    public class HandRanker
    {        
        private enum HandTypes
        {
            HighCard, Pair, TwoPair, ThreeOfAKind, Straight, Flush, FullHouse,
            FourOfAKind, StraightFlush, RoyalFlush, 
        }

        private Hand _hand;
        public HandRanker(Hand hand)
        {
            _hand = hand;
        }
        public bool ContainsFlush
        {
            get
            {
                List<int> suitCount = new List<int>() { 0, 0, 0, 0 };
                
                for (int card = 0; card < _hand.Count; card++)
                {
                    int suit = (int)_hand[card].Suit;
                    suitCount[suit]++;
                }

                for (int i = 0; i < suitCount.Count; i++)
                {
                    if(suitCount[i] >= 5) { return true; }
                }
                return false;
            }
        }
        public bool ContainsStraight { get; set; }
        public bool ContainsThreeOfAKind { get; set; }

        public int HighCard { get; set; }
        public int Ranking { get; set; }
    }
}
