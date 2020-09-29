using Poker_Judge.PokerMain;
using System;
using System.Runtime.CompilerServices;

namespace Poker_Judge
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                PokerJudgeMain pokerJudgeMain = new PokerJudgeMain();
            } while (CheckIfRunAgain());
            
        }
        private static bool CheckIfRunAgain()
        {
            Console.WriteLine("Would you like to run again (Y/N)?");
            string response = Console.ReadLine();

            if(response == "Y" || response == "y") { return true; }
            else { return false; }
        }
    }
}
