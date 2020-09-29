using System;
using System.Collections.Generic;
using System.Text;

namespace Poker_Judge.PokerEngine
{
    public class Hand : List<Card>
    {
        public Hand(string communityCards, string holeCards, int playerNumber = -1)
        {
            PlayerNumber = playerNumber;
            AddCommunityCards(communityCards);
            AddHoleCards(holeCards);
        }

        public int PlayerNumber { get; set; }

        private void AddCommunityCards(string communityCards)
        {
            string[] cards = communityCards.Split(" ");
            for (int c = 0; c < Deck.NumberOfCommunityCards; c++)
            {
                this.Add(new Card(cards[c]));
            }
        }
        private void AddHoleCards(string holeCards)
        {
            string[] cards = holeCards.Split(" ");
            for (int c = 0; c < cards.Length; c++)
            {
                this.Add(new Card(cards[c]));
            }
        }
    }
}
