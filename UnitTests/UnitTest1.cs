using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using System.Net.Security;
using static Poker_Judge.PokerEngine.Deck;
using Poker_Judge.PokerEngine;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Poker_Judge.PokerMain;
using static Poker_Judge.PokerEngine.HandRank;
using System.Text;
using System;
using System.IO;

namespace UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void parse_aceofhearts_aceofheartscard()
        {
            int value = 12;
            Suits suit = Suits.H;

            Card card = new Card(value, suit);

            Assert.That(card.Value, Is.EqualTo(12));
            Assert.That(card.Suit, Is.EqualTo(Suits.H));
            Assert.That(card.FaceValue, Is.EqualTo("AH"));
        }
        [Test]
        public void parse_twoofdiamonds_twoofdiamondscard()
        {
            int value = 0;
            Suits suit = Suits.D;

            Card card = new Card(value, suit);

            Assert.That(card.Value, Is.EqualTo(0));
            Assert.That(card.Suit, Is.EqualTo(Suits.D));
            Assert.That(card.FaceValue, Is.EqualTo("2D"));
        }
        [Test]
        public void count_deckis52cards_52cards()
        {
            Deck deck = new Deck();

            Assert.That(deck.Count, Is.EqualTo(52));
        }
        [Test]
        public void parse_jackofclubs_jackofclubscard()
        {
            string fv = "JC";
            Card card = new Card(fv);

            Assert.That(card.FaceValue, Is.EqualTo("JC"));
            Assert.That(card.Suit, Is.EqualTo(Suits.C));
            Assert.That(card.Value, Is.EqualTo(9));
        }
        [Test]
        public void create_sevenfacevalues_hand()
        {
            List<string> faceValues = new List<string>() { "AH", "KH", "QH", "JH", "10H", "9H", "8H" };
            string communitycards = "AH KH QH JH 10H";
            string holeCards = "9H 8H";

            int numberOfCards = faceValues.Count;

            Hand hand = new Hand(communitycards, holeCards);

            for (int c = 0; c < numberOfCards; c++)
            {
                Assert.That(hand[c].FaceValue, Is.EqualTo(faceValues[c]));
            }
        }
        [Test]
        public void create_sevenfacevalues_hand2()
        {
            List<string> faceValues = new List<string>() { "3S", "4C", "5D", "KD", "QS", "6D", "7D" };
            List<int> values = new List<int>() { 1, 2, 3, 11, 10, 4, 5 };
            string communityCards = "3S 4C 5D KD QS";
            string holeCards = "6D 7D";

            int numberOfCards = faceValues.Count;

            Hand hand = new Hand(communityCards, holeCards);

            for (int c = 0; c < numberOfCards; c++)
            {
                Assert.That(hand[c].FaceValue, Is.EqualTo(faceValues[c]));
                Assert.That(hand[c].Value, Is.EqualTo(values[c]));
            }
        }
        [Test]
        public void handranker_testhands_isflush()
        {
            HandIdentifierTests hit = new HandIdentifierTests();

            for (int i = 0; i < hit.NumberOfTestHands; i++)
            {
                Hand hand = hit.TestHands[i];
                HandRank hr = new HandRank(hand);
                Assert.That(hr.GetContainsFlush(hand), Is.EqualTo(hit.ContainsFlushKey[i]));
            }
        }
        [Test]
        public void handranker_testhands_isstraight()
        {
            HandIdentifierTests hit = new HandIdentifierTests();

            for (int i = 0; i < hit.NumberOfTestHands; i++)
            {
                Hand hand = hit.TestHands[i];
                HandRank hr = new HandRank(hand);
                Assert.That(hr.ContainsStraight, Is.EqualTo(hit.ContainsStraightKey[i]));
            }
        }
        [Test]
        public void handranker_testhands_highcard()
        {
            HandIdentifierTests hit = new HandIdentifierTests();

            for (int i = 0; i < hit.NumberOfTestHands; i++)
            {
                Hand hand = hit.TestHands[i];
                HandRank hr = new HandRank(hand);
                Assert.That(hr.HighCard, Is.EqualTo(hit.HightCardKey[i]));
            }
        }
        [Test]
        public void handranker_testhands_threeofakind()
        {
            HandIdentifierTests hit = new HandIdentifierTests();

            for (int h = 0; h < hit.NumberOfTestHands; h++)
            {
                Hand hand = hit.TestHands[h];
                HandRank hr = new HandRank(hand);
                Assert.That(hr.ContainsThreeOfAKind, Is.EqualTo(hit.ContainsThreeOfAKindKey[h]));
            }
        }
        [Test]
        public void handranker_testhands_fourofakind()
        {
            HandIdentifierTests hit = new HandIdentifierTests();

            for (int h = 0; h < hit.NumberOfTestHands; h++)
            {
                Hand hand = hit.TestHands[h];
                HandRank hr = new HandRank(hand);
                Assert.That(hr.ContainsFourOfAKind, Is.EqualTo(hit.ContainsFourOfAKindKey[h]));
            }
        }
        [Test]
        public void handranker_testhands_fullhouse()
        {
            HandIdentifierTests hit = new HandIdentifierTests();

            for (int h = 0; h < hit.NumberOfTestHands; h++)
            {
                Hand hand = hit.TestHands[h];
                HandRank hr = new HandRank(hand);
                Assert.That(hr.ContainsFullHouse, Is.EqualTo(hit.ContainsFullHouseKey[h]));
            }
        }
        [Test]
        public void handranker_testhands_pair()
        {
            HandIdentifierTests hit = new HandIdentifierTests();

            for (int h = 0; h < hit.NumberOfTestHands; h++)
            {
                Hand hand = hit.TestHands[h];
                HandRank hr = new HandRank(hand);
                Assert.That(hr.ContainsPair, Is.EqualTo(hit.ContainsPairKey[h]));
            }
        }
        [Test]
        public void handranker_testhands_twopair()
        {
            HandIdentifierTests hit = new HandIdentifierTests();

            for (int h = 0; h < hit.NumberOfTestHands; h++)
            {
                Hand hand = hit.TestHands[h];
                HandRank hr = new HandRank(hand);
                Assert.That(hr.ContainsTwoPair, Is.EqualTo(hit.ContainsTwoPairKey[h]));
            }
        }
        [Test]
        [TestCase("AH KH QH JH 8S", "10H 7D", HandTypes.RoyalFlush)]
        public void handranker_testhands_ranking(string communityCards, string playerCards, HandTypes ranking)
        {
            Hand hand = new Hand(communityCards, playerCards);
            HandRank handRanker = new HandRank(hand);

            Assert.That(handRanker.Ranking, Is.EqualTo((int)ranking));
            Assert.That(handRanker.GetContainsFlush(hand), Is.EqualTo(true));
            Assert.That(handRanker.ContainsStraight, Is.EqualTo(true));


            //HandIdentifierTests hit = new HandIdentifierTests();

            //for (int h = 0; h < hit.NumberOfTestHands; h++)
            //{
            //    Hand hand = hit.TestHands[h];
            //    HandRanker hr = new HandRanker(hand);
            //    Assert.That(hr.Ranking, Is.EqualTo(hit.RankingKey[h]));
            //}
        }
        [Test]
        public void pokerjudge_choosewinner_player1()
        {
            string communityCards = "4H 5H 6H QD 9C";
            List<string> playerCards = new List<string>()
            {
                "7H 3H",
                "KC 2D",
                "AS 3D",
            };

            string correctWinner = "Player1";

            PokerJudge pj = new PokerJudge();
            string chosenWinner = pj.GetWinner(communityCards, playerCards);
            Assert.That(chosenWinner, Is.EqualTo(correctWinner));
        }
        [Test]
        public void pokerjudge_choosewinner_player2()
        {
            string communityCards = "2H 4D 6C 9S KH";
            List<string> playerCards = new List<string>()
            {
                "7H 3H",
                "KD KC",
                "AS 3D",
            };

            string correctWinner = "Player2";

            PokerJudge pj = new PokerJudge();
            string chosenWinner = pj.GetWinner(communityCards, playerCards);
            Assert.That(chosenWinner, Is.EqualTo(correctWinner));
        }
        [Test]
        public void pokerjudge_choosewinner_player3()
        {
            string communityCards = "8D 9C 9H 2S 5D";
            List<string> playerCards = new List<string>()
            {
                "7H 3H",
                "8S KC",
                "AS 8C",
            };

            string correctWinner = "Player3";

            PokerJudge pj = new PokerJudge();
            string chosenWinner = pj.GetWinner(communityCards, playerCards);
            Assert.That(chosenWinner, Is.EqualTo(correctWinner));
        }

        [Test]
        public void run_withInput_returnsExpectedOutput()
        {
            StringWriter output = new StringWriter();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("community cards");
            sb.AppendLine("player1 1 2");
            sb.AppendLine("player1 1 2");
            sb.AppendLine("player1 1 2");
            sb.AppendLine("player1 1 2");
            StringBuilder expectedOutput = new StringBuilder();
            expectedOutput.AppendLine("Player1");

            new PokerJudgeMain().Run(sb.ToString(), output);

            Assert.That(output.ToString(), Is.EqualTo(expectedOutput.ToString()));
        }

        [Test]
        public void run_withMultiGameInput_returnsExpectedOutput()
        {
            StringWriter output = new StringWriter();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("community cards");
            sb.AppendLine("player1 1 2");
            sb.AppendLine("player1 1 2");
            sb.AppendLine("player1 1 2");
            sb.AppendLine("player1 1 2");
            sb.AppendLine();
            sb.AppendLine("community cards");
            sb.AppendLine("player1 1 2");
            sb.AppendLine("player1 1 2");
            sb.AppendLine("player1 1 2");
            sb.AppendLine("player1 1 2");
            StringBuilder expectedOutput = new StringBuilder();
            expectedOutput.AppendLine("Player1");
            expectedOutput.AppendLine("Player2");

            new PokerJudgeMain().Run(sb.ToString(), output);

            Assert.That(output.ToString(), Is.EqualTo(expectedOutput.ToString()));

        }
    }
}