using Poker_Judge.PokerEngine;
using Poker_Judge.PokerInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Poker_Judge.PokerMain
{
    public class PokerJudgeMain
    {
        private bool _manuallyEnteredVsRandomGenerated;
        private int _numberOfPlayers;
        private string _fiveCommunityCards;
        private List<string> _twoHoleCardsPerPlayer = new List<string>();
                
        public void Run()
        {
            GetNumberOfPlayers();
            GetManualEntryVSRandomlyGeneratedHands();
            if (_manuallyEnteredVsRandomGenerated) { GetManuallyEnteredCards(); }
            else { GetRandomGeneratedCards(); }
            DisplayWinner();
        }       

        public void Run(string input, TextWriter output)
        {
            StringReader reader = new StringReader(input);

            string appendedInput = null;
            string line = reader.ReadLine();

            while (line != null)
            {
                appendedInput += line;

                if(line == string.Empty)
                {
                    InputParser inputParser = new InputParser(appendedInput);
                    PokerJudge pokerJudge = new PokerJudge();
                    string winner = pokerJudge.GetWinner(inputParser.CommunityCards, inputParser.PlayerCards);
                    output.Write(winner);
                    return;
                }
                line = reader.ReadLine();
            }
        }

        public void DisplayWinner()
        {
            PokerJudge pokerJudge = new PokerJudge();
            string winner = pokerJudge.GetWinner(_fiveCommunityCards, _twoHoleCardsPerPlayer);
            Console.WriteLine();
            Console.WriteLine("WINNER IS: " + winner + "with " + pokerJudge.WinningHandDisplayString);
        }

        private void GetNumberOfPlayers()
        {
            Console.WriteLine("Enter Number of Players (Max 6):");
            string response = Console.ReadLine();
            _numberOfPlayers = Convert.ToInt32(response);

        }
        private void GetManualEntryVSRandomlyGeneratedHands()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Enter scenario type: ");
            sb.AppendLine("1. Manually Enter Hands");
            sb.AppendLine("2. Randomly Generate Hands");

            Console.WriteLine(sb.ToString());
            string response = Console.ReadLine();
            if(response == "1") { _manuallyEnteredVsRandomGenerated = true; }
            else { _manuallyEnteredVsRandomGenerated = false; }
        }
        private void GetManuallyEnteredCards()
        {
            Console.WriteLine("Enter 5 Community Cards (format:  AC 8D 5H QS JD):");
            _fiveCommunityCards = Console.ReadLine();

            for (int i = 1; i <= _numberOfPlayers; i++)
            {
                Console.WriteLine("Enter 2 cards for Player" + i.ToString() + ":");
                string response = Console.ReadLine();
                _twoHoleCardsPerPlayer.Add(response);
            }
        }
        private void GetRandomGeneratedCards()
        {
            Deck deck = new Deck();
            _fiveCommunityCards = deck.GetRandomCommunityCards();

            Console.WriteLine("Community Cards: " + _fiveCommunityCards);

            for (int p = 0; p < _numberOfPlayers; p++)
            {
                string playerCards = deck.GetPlayerHoleCards();
                _twoHoleCardsPerPlayer.Add(playerCards);
                Console.WriteLine("Player" + (p + 1).ToString() + ": " + playerCards);
            }
        }
    }
}
