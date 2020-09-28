using Poker_Judge.PokerEngine;
using Poker_Judge.PokerInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poker_Judge.PokerMain
{
    class PokerJudge : IPokerJudge
    {
        List<Hand> _hands = new List<Hand>();
        public string GetWinner(string fiveCommunityCards, List<string> twoHoleCardsPerPlayer)
        {
            SetHands(fiveCommunityCards, twoHoleCardsPerPlayer);



            return "Player1";
        }
        private void SetHands(string fiveCommunityCards, List<string> twoHoleCardsPerPlayer)
        {
            for (int h = 0; h < twoHoleCardsPerPlayer.Count; h++)
            {
                _hands.Add(new Hand(fiveCommunityCards, twoHoleCardsPerPlayer[h]));
            }
        }
    }
}
