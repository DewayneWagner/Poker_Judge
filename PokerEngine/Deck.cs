using System;
using System.Collections.Generic;
using System.Text;

namespace Poker_Judge.PokerEngine
{
    public class Deck : List<Card>
    {
        public enum Suits { C, D, H, S, Total }
        private FaceValues _faceValues;
        System.Random rnd = new System.Random();

        public const int NumberOfCommunityCards = 5;
        public const int NumberOfHoleCardsPerPlayer = 2;

        public Deck()
        {
            _faceValues = new FaceValues();
            ShuffleDeck();
        }
        private void ShuffleDeck()
        {
            for (int v = 0; v < _faceValues.Count; v++)
            {
                for (int s = 0; s < (int)Suits.Total; s++)
                {
                    this.Add(new Card(v, (Suits)s));
                }
            }
        }
        public string GetRandomCommunityCards()
        {
            string communityCards = null;

            for (int i = 0; i < NumberOfCommunityCards; i++)
            {
                int randomCardIndex = rnd.Next(0, this.Count);
                communityCards += this[randomCardIndex].FaceValue;
                if(i != NumberOfCommunityCards - 1) { communityCards += " "; }
                this.RemoveAt(randomCardIndex);
            }

            return communityCards;
        }
        public string GetPlayerHoleCards()
        {
            string holeCards = null;

            for (int c = 0; c < NumberOfHoleCardsPerPlayer; c++)
            {
                int randomCardIndex = rnd.Next(0, this.Count);
                holeCards += this[randomCardIndex].FaceValue;
                if(c != NumberOfHoleCardsPerPlayer - 1) { holeCards += " "; }
                this.RemoveAt(randomCardIndex);
            }

            return holeCards;
        }
    }
}
