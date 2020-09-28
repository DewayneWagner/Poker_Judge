using System;
using System.Collections.Generic;
using System.Text;

namespace Poker_Judge.PokerInterface
{
    interface IPokerJudge
    {
        string GetWinner(string fiveCommunityCards, List<string> twoHoleCardsPerPlayer);
    }
}
