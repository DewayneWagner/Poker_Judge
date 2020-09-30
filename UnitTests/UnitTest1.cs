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
        [TestCase()]
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
        [TestCase("10S 8S 7D KH 2S", "3S 5S", HandTypes.Flush)]
        public void handranker_testhands_isflush(string communityCards, string playerCards, HandTypes ranking)
        {
            Hand hand = new Hand(communityCards, playerCards);
            HandRank handRanker = new HandRank();
            handRanker.Run(hand);

            Assert.That(handRanker.Ranking, Is.EqualTo(ranking));
        }
        [Test]
        [TestCase("3S 4C 5D KD QS", "6D 7D", HandTypes.Straight)]
        public void handranker_testhands_isstraight(string communityCards, string playerCards, HandTypes ranking)
        {
            Hand hand = new Hand(communityCards, playerCards);
            HandRank handRanker = new HandRank();
            handRanker.Run(hand);

            Assert.That(handRanker.Ranking, Is.EqualTo(ranking));
        }
        [Test]
        [TestCase("2H 5D 9C KD 10S", "8S 3D", HandTypes.HighCard)]
        public void handranker_testhands_highcard(string communityCards, string playerCards, HandTypes ranking)
        {
            Hand hand = new Hand(communityCards, playerCards);
            HandRank handRanker = new HandRank();
            handRanker.Run(hand);

            Assert.That(handRanker.Ranking, Is.EqualTo(ranking));
        }
        [Test]
        [TestCase("5S 5D 5H 8C QD", "2C AH", HandTypes.ThreeOfAKind)]
        public void handranker_testhands_threeofakind(string communityCards, string playerCards, HandTypes ranking)
        {
            Hand hand = new Hand(communityCards, playerCards);
            HandRank handRanker = new HandRank();
            handRanker.Run(hand);

            Assert.That(handRanker.Ranking, Is.EqualTo(ranking));
        }
        [Test]
        [TestCase("JH JC JS 9D 2S", "JD 4H", HandTypes.FourOfAKind)]
        public void handranker_testhands_fourofakind(string communityCards, string playerCards, HandTypes ranking)
        {
            Hand hand = new Hand(communityCards, playerCards);
            HandRank handRanker = new HandRank();
            handRanker.Run(hand);

            Assert.That(handRanker.Ranking, Is.EqualTo(ranking));
        }
        [Test]
        [TestCase("5S 5H 5D 2C QD", "JD JC", HandTypes.FullHouse)]
        public void handranker_testhands_fullhouse(string communityCards, string playerCards, HandTypes ranking)
        {
            Hand hand = new Hand(communityCards, playerCards);
            HandRank handRanker = new HandRank();
            handRanker.Run(hand);

            Assert.That(handRanker.Ranking, Is.EqualTo(ranking));
        }
        [Test]
        [TestCase("8D 9C QH 2S 5D", "8H 10D", HandTypes.Pair)]
        public void handranker_testhands_pair(string communityCards, string playerCards, HandTypes ranking)
        {
            Hand hand = new Hand(communityCards, playerCards);
            HandRank handRanker = new HandRank();
            handRanker.Run(hand);

            Assert.That(handRanker.Ranking, Is.EqualTo(ranking));
        }
        [Test]
        [TestCase("8D 9C 9H 2S 5D", "8H 10D", HandTypes.TwoPair)]
        public void handranker_testhands_twopair(string communityCards, string playerCards, HandTypes ranking)
        {
            Hand hand = new Hand(communityCards, playerCards);
            HandRank handRanker = new HandRank();
            handRanker.Run(hand);

            Assert.That(handRanker.Ranking, Is.EqualTo(HandTypes.TwoPair));
        }
        [Test]
        [TestCase("AH KH QH JH 8S", "10H 7D", HandTypes.RoyalFlush)]
        public void handranker_testhands_ranking(string communityCards, string playerCards, HandTypes ranking)
        {
            Hand hand = new Hand(communityCards, playerCards);
            HandRank handRanker = new HandRank();
            handRanker.Run(hand);

            Assert.That(handRanker.Ranking, Is.EqualTo(ranking));
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
            sb.AppendLine("5H 6H 9H 2D QC");
            sb.AppendLine("Player1 10H KH");
            sb.AppendLine("Player2 4C JS");
            sb.AppendLine("Player3 4D 8D");
            sb.AppendLine("Player4 10D 8C");

            InputParser inputParser = new InputParser(sb.ToString());
            string communityCardsExpectedResult = "5H 6H 9H 2D QC";
            string result = inputParser.GetTestString();
            Assert.That(inputParser.CommunityCards, Is.EqualTo(communityCardsExpectedResult));

            List<string> expectedPlayerHands = new List<string>() { "10H KH", "4C JS", "4D 8D", "10D 8C" };

            for (int p = 0; p < inputParser.PlayerCards.Count; p++)
            {
                string playerResult = inputParser.PlayerCards[p];
                Assert.That(playerResult, Is.EqualTo(expectedPlayerHands[p]));
            }

            PokerJudge pokerJudge = new PokerJudge();
            string winner = pokerJudge.GetWinner(inputParser.CommunityCards, inputParser.PlayerCards);
            string expectedWinner = "Player1";
            Assert.That(winner, Is.EqualTo(expectedWinner));
        }

        [Test]
        public void run_withMultiGameInput_returnsExpectedOutput()
        {
            StringWriter output = new StringWriter();
            StringBuilder sb = new StringBuilder();

            // tested this and it worked
            //sb.AppendLine("3C 4C 5C JH KH");
            //sb.AppendLine("Player1 6C 7C");
            //sb.AppendLine("Player2 2H 9H");
            //sb.AppendLine("Player3 2S 9D");
            //sb.AppendLine("Player4 AD 8S");
            //sb.AppendLine();

            sb.AppendLine("7H 7D 7C 9S QS");
            sb.AppendLine("Player1 JD JH");
            sb.AppendLine("Player2 QD QH");
            sb.AppendLine("Player3 8S 2H");
            sb.AppendLine("Player4 2S JC");

            InputParser inputParser = new InputParser(sb.ToString());
            string communityCardsExpectedResult = "7H 7D 7C 9S QS";
            string result = inputParser.GetTestString();
            Assert.That(inputParser.CommunityCards, Is.EqualTo(communityCardsExpectedResult));

            List<string> expectedPlayerHands = new List<string>() { "JD JH", "QD QH", "8S 2H", "2S JC" };

            for (int p = 0; p < inputParser.PlayerCards.Count; p++)
            {
                string playerResult = inputParser.PlayerCards[p];
                Assert.That(playerResult, Is.EqualTo(expectedPlayerHands[p]));
            }

            PokerJudge pokerJudge = new PokerJudge();
            string winner = pokerJudge.GetWinner(inputParser.CommunityCards, inputParser.PlayerCards);
            string expectedWinner = "Player1";
            Assert.That(winner, Is.EqualTo(expectedWinner));



            //StringBuilder expectedOutput = new StringBuilder();

            //expectedOutput.AppendLine("Player1");
            //expectedOutput.AppendLine("Player2");

            //new PokerJudgeMain().Run(sb.ToString(), output);

            //Assert.That(output.ToString(), Is.EqualTo(expectedOutput.ToString()));
        }
        [Test]
        public void run_withappendedinput_returnsplayer1()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("5H 6H 9H 2D QC");
            sb.AppendLine("Player1 10H KH");
            sb.AppendLine("Player2 4C JS");
            sb.AppendLine("Player3 4D 8D");
            sb.AppendLine("Player4 10D 8C");

            InputParser inputParser = new InputParser(sb.ToString());
            string communityCardsExpectedResult = "5H 6H 9H 2D QC";
            string result = inputParser.GetTestString();
            Assert.That(inputParser.CommunityCards, Is.EqualTo(communityCardsExpectedResult));

            List<string> expectedPlayerHands = new List<string>() { "10H KH", "4C JS", "4D 8D", "10D 8C" };

            for (int p = 0; p < inputParser.PlayerCards.Count; p++)
            {
                string playerResult = inputParser.PlayerCards[p];
                Assert.That(playerResult, Is.EqualTo(expectedPlayerHands[p]));
            }

            PokerJudge pokerJudge = new PokerJudge();
            string winner = pokerJudge.GetWinner(inputParser.CommunityCards, inputParser.PlayerCards);
            string expectedWinner = "Player1";
            Assert.That(winner, Is.EqualTo(expectedWinner));
        }
    }
}