
using Poker_Judge.PokerEngine;
using System;
using System.Collections.Generic;
using System.Text;
using static Poker_Judge.PokerEngine.Deck;
using HT = Poker_Judge.PokerEngine.HandRank.HandTypes;

namespace UnitTests
{
    class HandIdentifierTests
    {
        private List<Hand> _testHands = new List<Hand>()
        {
            new Hand("AH KH QH JH 8S", "10H 7D"), // Royal Flush
            new Hand("10S 8S 7D KH 2S", "3S 5S"), // Flush
            new Hand("3S 4S 5S 6S 8D", "7S 9C"), // Straight Flush
            new Hand("3S 4C 5D KD QS", "6D 7D"), // Straight
            new Hand("5H 6H 9H 2D QC", "10H KH"), // Flush
            new Hand("4D 8C 6D 8D KH", "QD 2H"), // Pair
            new Hand("5S 5D 5H 8C QD", "2C AH"), // Three of a kind
            new Hand("2H 4D 6C 9S KH", "KC KD"), // Three of a kind
            new Hand("JH JC JS 9D 2S", "JD 4H"), // Four of a kind
            new Hand("4S 4D 2S KH JD", "4H 4C"), // Four of a kind
            new Hand("5S 5H 5D 2C QD", "JD JC"), // Full House
            new Hand("7D 7C QC AD 2S", "7H AC"), // Full House
            new Hand("8D 9C QH 2S 5D", "8H 10D"), // Pair
            new Hand("8D 9C 9H 2S 5D", "8H 10D"), // Two Pair
        };

        private List<bool> _containsFlushKey = new List<bool>() { true, true, true, false, true, false, false, false, false, false, false, false, false, false };
        private List<int> _highCardKey = new List<int>() { 12, 11, 7, 11, 11, 11, 12, 11, 9, 11, 10, 12, 10, 8 };
        private List<bool> _containsStraightKey = new List<bool>() { true, false, true, true, false, false, false, false, false, false, false, false, false, false };
        private List<bool> _containsThreeOfAKindKey = new List<bool>() { false, false, false, false, false, false, true, true, false, false, true, true, false, false };
        private List<bool> _containsFourOfAKindKey = new List<bool>() { false, false, false, false, false, false, false, false, true, true, false, false, false, false };
        private List<bool> _containsFullHouseKey = new List<bool>() { false, false, false, false, false, false, false, false, false, false, true, true, false, false };
        private List<bool> _containsPairKey = new List<bool>() { false, false, false, false, false, true, false, false, false, false, true, true, true, true };
        private List<int> _rankingKey = new List<int>() { (int)HT.RoyalFlush, (int)HT.Flush, (int)HT.StraightFlush, (int)HT.Straight, (int)HT.Flush, (int)HT.Pair, (int)HT.ThreeOfAKind,
                                                            (int)HT.ThreeOfAKind, (int)HT.FourOfAKind, (int)HT.FourOfAKind, (int)HT.FullHouse, (int)HT.FullHouse, (int)HT.Pair, (int)HT.TwoPair };
        private List<bool> _containsTwoPairKey = new List<bool>() { false, false, false, false, false, false, false, false, false, false, false, false, false, true };

        public List<Hand> TestHands => _testHands;
        public List<bool> ContainsFlushKey => _containsFlushKey;
        public List<int> HightCardKey => _highCardKey;
        public List<bool> ContainsStraightKey => _containsStraightKey;
        public List<bool> ContainsThreeOfAKindKey => _containsThreeOfAKindKey;
        public List<bool> ContainsFourOfAKindKey => _containsFourOfAKindKey;
        public List<bool> ContainsFullHouseKey => _containsFullHouseKey;
        public List<bool> ContainsPairKey => _containsPairKey;
        public List<bool> ContainsTwoPairKey => _containsTwoPairKey;
        public List<int> RankingKey => _rankingKey;
        public int NumberOfTestHands => TestHands.Count;


    }
}
