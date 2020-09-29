using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using static Poker_Judge.PokerEngine.Deck;

namespace Poker_Judge.PokerEngine
{
    public class HandRank
    {        
        public enum HandTypes
        {
            HighCard, Pair, TwoPair, ThreeOfAKind, Straight, Flush, FullHouse,
            FourOfAKind, StraightFlush, RoyalFlush, 
        }

        private Hand _hand;
        public HandRank(Hand hand)
        {
            _hand = hand;
        }
        public int PlayerNumber => _hand.PlayerNumber;

        private bool CheckIfContainsFlush(Hand hand)
        {
            List<int> suitCount = new List<int>() { 0, 0, 0, 0 };

            for (int card = 0; card < hand.Count; card++)
            {
                int suit = (int)hand[card].Suit;
                suitCount[suit]++;
            }

            for (int i = 0; i < suitCount.Count; i++)
            {
                if (suitCount[i] >= 5) { return true; }
            }
            return false;
        }

        public bool ContainsStraight
        {
            get
            {
                List<int> orderedValues = _hand.OrderByDescending(c => c.Value)
                                                .Select(c => c.Value)
                                                .ToList();

                List<int> differentialList = getDifferentialList();

                int counter = 0;
                bool isPossibleStraight = false;

                foreach (int diff in differentialList)
                {
                    if(diff == 1 && isPossibleStraight) { counter++; }
                    else if(diff == 1 && !isPossibleStraight)
                    {
                        counter++;
                        isPossibleStraight = true;
                    }
                    else
                    {
                        counter = 0;
                        isPossibleStraight = false;
                    }
                    if(counter == 4) { return true; }
                }
                return false;

                List<int> getDifferentialList()
                {
                    List<int> diffList = new List<int>();
                    for (int i = 0; i < orderedValues.Count - 1; i++)
                    {
                        int diff = orderedValues[i] - orderedValues[i + 1];
                        if (diff != 0) { diffList.Add(diff); }
                    }
                    return diffList;
                }
            }
        }
        private List<int> _distinctValues => _hand.Select(v => v.Value)
                                            .Distinct()
                                            .ToList();

        public bool ContainsThreeOfAKind
        {
            get
            {
                foreach (int v in _distinctValues)
                {
                    if (_hand.Where(c => c.Value == v).Count() == 3) { return true; }
                }
                return false;
            }
        }

        public bool ContainsFourOfAKind
        {
            get
            {
                foreach (int v in _distinctValues)
                {
                    if (_hand.Where(c => c.Value == v).Count() == 4) { return true; }
                }
                return false;
            }
        }

        public bool ContainsFullHouse
        {
            get
            {
                if (ContainsThreeOfAKind)
                {
                    foreach (int value in _distinctValues)
                    {
                        if(_hand.Where(v => v.Value == value).Count() == 2) { return true; }
                    }                    
                }
                return false;
            }
        }

        public bool ContainsPair
        {
            get
            {
                foreach (int value in _distinctValues)
                {
                    if (_hand.Where(v => v.Value == value).Count() == 2) { return true; }
                }
                return false;
            }
        }

        public bool ContainsTwoPair
        {
            get
            {
                if (ContainsPair)
                {
                    int numberOfPairs = 0;

                    foreach (int value in _distinctValues)
                    {
                        if(_hand.Where(v => v.Value == value).Count() == 2) { numberOfPairs++; }
                    }
                    if(numberOfPairs == 2) { return true; }
                }
                return false;
            }
        }

        public int Ranking
        {
            get
            {
                if(ContainsStraight && CheckIfContainsFlush(_hand) && HighCard == 12) { return (int)HandTypes.RoyalFlush; }
                if(ContainsStraight && CheckIfContainsFlush(_hand)) { return (int)HandTypes.StraightFlush; }
                if(ContainsFourOfAKind) { return (int)HandTypes.FourOfAKind; }
                if(ContainsFullHouse) { return (int)HandTypes.FullHouse; }
                if(CheckIfContainsFlush(_hand)) { return (int)HandTypes.Flush; }
                if(ContainsStraight) { return (int)HandTypes.Straight; }
                if(ContainsThreeOfAKind) { return (int)HandTypes.ThreeOfAKind; }
                if(ContainsTwoPair) { return (int)HandTypes.TwoPair; }
                if(ContainsPair) { return (int)HandTypes.Pair; }

                return (int)HandTypes.HighCard;
            }
        }

        public int HighCard => _hand.OrderByDescending(c => c.Value)
                                    .Select(c => c.Value)
                                    .First();
    }
}
