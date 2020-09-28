using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using static Poker_Judge.PokerEngine.Deck;

namespace Poker_Judge.PokerEngine
{
    public class Card
    {
        private static FaceValues _faceValues;
        public Card(int value, Suits suit)
        {
            if(_faceValues == null) { _faceValues = new FaceValues(); }
            Value = value;
            Suit = suit;
            FaceValue = GetFaceValue();
        }

        public Card(string faceValue)
        {
            FaceValue = faceValue;
            char[] cA = faceValue.ToCharArray();
            
            if(cA.Length == 2)
            {
                Suit = GetSuit(cA[1]);
                Value = _faceValues.GetValue(cA[0].ToString());
            }
            else
            {
                Suit = GetSuit(cA[2]);
                string fv = cA[0].ToString() + cA[1].ToString();
                Value = _faceValues.GetValue(fv);
            }
        }

        public int Value { get; set; }
        public Suits Suit { get; set; }                
        public string FaceValue { get; set; }
        
        private string GetFaceValue() => _faceValues.GetFaceValue(Value) + Convert.ToString(Suit);

        private Suits GetSuit(char suit)
        {
            string s = suit.ToString();
            switch (s)
            {
                case "C":
                    return Suits.C;
                case "D":
                    return Suits.D;
                case "H":
                    return Suits.H;
                case "S":
                    return Suits.S;
                default:
                    return Suits.C;
            }
        }
    }
}
