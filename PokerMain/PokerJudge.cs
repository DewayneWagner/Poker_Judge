﻿using Poker_Judge.PokerEngine;
using Poker_Judge.PokerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Poker_Judge.PokerEngine.HandRanker;

namespace Poker_Judge.PokerMain
{
    public class PokerJudge : IPokerJudge
    {
        List<Hand> _hands = new List<Hand>();
        List<HandRanker> _rankings = new List<HandRanker>();
        public string GetWinner(string fiveCommunityCards, List<string> twoHoleCardsPerPlayer)
        {
            SetHands(fiveCommunityCards, twoHoleCardsPerPlayer);
            SetRankings();

            string winner = IsTie ? "Tie!" : "Player" + (_rankings[0].PlayerNumber + 1).ToString();
            winner += " with " + WinningHandDisplayString;
            return winner;
        }
        private void SetHands(string fiveCommunityCards, List<string> twoHoleCardsPerPlayer)
        {
            for (int h = 0; h < twoHoleCardsPerPlayer.Count; h++)
            {
                _hands.Add(new Hand(fiveCommunityCards, twoHoleCardsPerPlayer[h], h));
            }
        }
        private void SetRankings()
        {
            List<HandRanker> rankings = new List<HandRanker>();
            foreach (Hand hand in _hands)
            {
                rankings.Add(new HandRanker(hand));
            }
            _rankings = rankings.OrderByDescending(r => r.Ranking)
                           .ThenByDescending(r => r.HighCard)
                           .ToList();
        }
        private bool IsTie
        {
            get
            {
                if(_rankings[0].Ranking == _rankings[1].Ranking && _rankings[0].HighCard == _rankings[1].HighCard) { return true; }
                else { return false; }
            }
        }
        private string WinningHandDisplayString
        {
            get
            {
                HandTypes winningHand = (HandTypes)_rankings[0].Ranking;
                switch (winningHand)
                {
                    case HandTypes.HighCard:
                        return "High Card";
                    case HandTypes.Pair:
                        return "a Pair";
                    case HandTypes.TwoPair:
                        return "Two Pair";
                    case HandTypes.ThreeOfAKind:
                        return "Three of a kind";
                    case HandTypes.Straight:
                        return "a Straight";
                    case HandTypes.Flush:
                        return "a Flush";
                    case HandTypes.FullHouse:
                        return "a Full House";
                    case HandTypes.FourOfAKind:
                        return "Four of a kind";
                    case HandTypes.StraightFlush:
                        return "a Straight Flush";
                    case HandTypes.RoyalFlush:
                        return "a Royal Flush";
                    default:
                        return null;
                }

            }
        }
    }
}
