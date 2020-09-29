using Poker_Judge.PokerEngine;
using Poker_Judge.PokerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

    }
}
