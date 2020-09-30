using Poker_Judge.PokerEngine;
using Poker_Judge.PokerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Poker_Judge.PokerEngine.HandRank;

namespace Poker_Judge.PokerMain
{
    public class PokerJudge : IPokerJudge
    {
        List<Hand> _hands = new List<Hand>();
        List<HandRank> _rankings = new List<HandRank>();
        public string GetWinner(string fiveCommunityCards, List<string> twoPlayerCards)
        {
            SetHands(fiveCommunityCards, twoPlayerCards);
            SetRankings();

            string winner = IsTie ? "Tie!" : "Player" + (_rankings[0].PlayerNumber + 1).ToString();
            return winner;
        }
        private void SetHands(string fiveCommunityCards, List<string> twoHoleCardsPerPlayer)
        {
            if (textIncludePlayerNumber())
            {
                List<string> playerCards = getCardsWithoutPlayerText();

                for (int h = 0; h < playerCards.Count; h++)
                {
                    _hands.Add(new Hand(fiveCommunityCards, playerCards[h]));
                }

                List<string> getCardsWithoutPlayerText()
                {
                    List<string> cardsWithoutPlayerText = new List<string>();
                    foreach (string cards in twoHoleCardsPerPlayer)
                    {
                        string[] dataSplit = cards.Split(" ");
                        cardsWithoutPlayerText.Add(dataSplit[1] + " " + dataSplit[2]);
                    }
                    return cardsWithoutPlayerText;
                }
            }
            else
            {
                for (int h = 0; h < twoHoleCardsPerPlayer.Count; h++)
                {
                    _hands.Add(new Hand(fiveCommunityCards, twoHoleCardsPerPlayer[h], h));
                }
            }
                      
            bool textIncludePlayerNumber()
            {
                if(twoHoleCardsPerPlayer[0].Length > 6) { return true; }
                else { return false; }
            }
        }
        private void SetRankings()
        {
            List<HandRank> rankings = new List<HandRank>();
            foreach (Hand hand in _hands)
            {
                HandRank handRank = new HandRank();
                handRank.Run(hand);
                rankings.Add(handRank);
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
        public string WinningHandDisplayString
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
