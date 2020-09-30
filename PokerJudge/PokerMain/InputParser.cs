using Poker_Judge.PokerEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker_Judge.PokerMain
{
    public class InputParser
    {
        private const int NumberOfCommunityCards = 5;
        private const int NumberOfCardsPerPlayer = 2;

        public InputParser(string appendedInput)
        {
            SplitStrings(appendedInput);
        }

        public string CommunityCards { get; set; }
        public List<string> PlayerCards { get; set; } = new List<string>();

        public string GetTestString()
        {
            string concatenatedString = null;

            concatenatedString += CommunityCards + " ";

            for (int c = 0; c < PlayerCards.Count; c++)
            {
                concatenatedString += PlayerCards[c];
                if(c != PlayerCards.Count - 1) { concatenatedString += " "; }
            }

            return concatenatedString;
        }

        private void SplitStrings(string appendedInput)
        {
            Deck deck = new Deck();
            char[] charArray = appendedInput.ToCharArray();
            List<string> cards = getCards();
            string _communityCards = null;
            SetCommunityCards();
            SetPlayerCards();
            
            void SetCommunityCards()
            {
                for (int c = 0; c < NumberOfCommunityCards; c++)
                {
                    _communityCards += cards[c];
                    if(c != NumberOfCommunityCards - 1) { _communityCards += " "; }
                }
                CommunityCards = _communityCards;
            }
            void SetPlayerCards()
            {
                for (int c = NumberOfCommunityCards; c < cards.Count; c++)
                {
                    string pCard = null;

                    for (int p = 0; p < NumberOfCardsPerPlayer; p++)
                    {
                        pCard += cards[c + p];
                        if(p != NumberOfCardsPerPlayer - 1) { pCard += " "; }
                    }
                    c++;
                    PlayerCards.Add(pCard);
                }
            }
            List<string> getCards()
            {
                List<string> c = new List<string>();
                for (int i = 0; i < charArray.Length - 1; i++)
                {
                    string sequence = charArray[i].ToString() + charArray[i + 1].ToString();
                    if (deck.Any(c => c.FaceValue == sequence))
                    {
                        c.Add(sequence);
                        i++;
                    }
                    else if (sequence == "10")
                    {
                        c.Add(sequence + charArray[i + 2].ToString());
                        i += 2;
                    }
                }
                return c;
            }
        }
    }
}
