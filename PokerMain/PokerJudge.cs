using Poker_Judge.PokerInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poker_Judge.PokerMain
{
    class PokerJudge : IPokerJudge
    {
        public string GetWinner(string fiveCommunityCards, List<string> twoHoleCardsPerPlayer)
        {
            return "Player1";
        }
    }
}
