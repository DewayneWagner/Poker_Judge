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

        private bool _containsFlush;
        private bool _containsStraight;
        private List<int> _distinctValues;
        private bool _containsThreeOfAKind;
        private bool _containsPair;

        public int PlayerNumber { get; set; }
        public HandTypes Ranking { get; set; }
        public int HighCard { get; set; }

        public void Run(Hand hand)
        {
            PlayerNumber = hand.PlayerNumber;
            HighCard = GetHighCard(hand);
            _distinctValues = GetListOfDistinctValues(hand);
            _containsFlush = CheckIfContainsFlush(hand);
            _containsStraight = CheckIfContainsStraight(hand);
            _containsThreeOfAKind = CheckIfContainsThreeOfAKind(hand);
            _containsPair = CheckIfContainsPair(hand);
            Ranking = GetHandType(hand);
        }

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

        private bool CheckIfContainsStraight(Hand hand)
        {
            List<int> orderedValues = hand.OrderByDescending(c => c.Value)
                                                .Select(c => c.Value)
                                                .ToList();

            List<int> differentialList = getDifferentialList();

            int counter = 0;
            bool isPossibleStraight = false;

            foreach (int diff in differentialList)
            {
                if (diff == 1 && isPossibleStraight) { counter++; }
                else if (diff == 1 && !isPossibleStraight)
                {
                    counter++;
                    isPossibleStraight = true;
                }
                else
                {
                    counter = 0;
                    isPossibleStraight = false;
                }
                if (counter == 4) { return true; }
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

        private List<int> GetListOfDistinctValues(Hand hand) => hand.Select(v => v.Value)
                                                                    .Distinct()
                                                                    .ToList();

        private bool CheckIfContainsThreeOfAKind(Hand hand)
        {
            foreach (int v in _distinctValues)
            {
                if (hand.Where(c => c.Value == v).Count() == 3) { return true; }
            }
            return false;
        }

        private bool ContainsFourOfAKind(Hand hand)
        {
            foreach (int v in _distinctValues)
            {
                if (hand.Where(c => c.Value == v).Count() == 4) { return true; }
            }
            return false;
        }

        private bool ContainsFullHouse(Hand hand)
        {
            if (_containsThreeOfAKind)
            {
                foreach (int value in _distinctValues)
                {
                    if (hand.Where(v => v.Value == value).Count() == 2) { return true; }
                }
            }
            return false;
        }

        private bool CheckIfContainsPair(Hand hand)
        {
            foreach (int value in _distinctValues)
            {
                if (hand.Where(v => v.Value == value).Count() == 2) { return true; }
            }
            return false;
        }

        private bool ContainsTwoPair(Hand hand)
        {
            if (_containsPair)
            {
                int numberOfPairs = 0;

                foreach (int value in _distinctValues)
                {
                    if(hand.Where(v => v.Value == value).Count() == 2) { numberOfPairs++; }
                }
                    
                if (numberOfPairs == 2) { return true; }
            }
            return false;
        }
        private int GetHighCard(Hand hand)
        {
            return hand.OrderByDescending(c => c.Value)
                                    .Select(c => c.Value)
                                    .First();
        }

        private HandTypes GetHandType(Hand hand)
        {
            if (_containsStraight && _containsFlush && HighCard == 12) { return HandTypes.RoyalFlush; }
            if (_containsStraight && _containsFlush) { return HandTypes.StraightFlush; }
            if (ContainsFourOfAKind(hand)) { return HandTypes.FourOfAKind; }
            if (ContainsFullHouse(hand)) { return HandTypes.FullHouse; }
            if (CheckIfContainsFlush(hand)) { return HandTypes.Flush; }
            if (_containsStraight) { return HandTypes.Straight; }
            if (_containsThreeOfAKind) { return HandTypes.ThreeOfAKind; }
            if (ContainsTwoPair(hand)) { return HandTypes.TwoPair; }
            if (_containsPair) { return HandTypes.Pair; }

            return HandTypes.HighCard;
        }
    }
}
