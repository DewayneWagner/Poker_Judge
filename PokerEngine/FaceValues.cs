using System;
using System.Collections.Generic;
using System.Text;

namespace Poker_Judge.PokerEngine
{
    class FaceValues
    {
        private readonly List<string> _faceValues = new List<string>() { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

        public List<string> Values => _faceValues;
        public int IndexOf(string value) => Values.IndexOf(value);
        public string GetFaceValue(int value) => _faceValues[value];
        public int Count => _faceValues.Count;
        public int GetValue(string faceValue) => _faceValues.IndexOf(faceValue); 
    }
}
