using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    // $G$ DSN-999 (-3) Better use a struct here
    public class Player
    {
        private const string k_Black = "X";
        private const string k_White = "O";
        private string m_Name;
        private int m_Score;
        private string m_Sign;

        public Player(string i_Name, string i_Sign)
        {
            m_Name = i_Name;
            m_Sign = i_Sign;
        }

        public string Sign
        {
            get { return m_Sign; }
            set { m_Sign = value; }
        }

        public string PlayerName
        {
            get { return m_Name; }
        }

        public int PlayerScore
        {
            get { return m_Score; }
            set { m_Score = value; }
        }

        public string GetOpponentSign()
        {
            string opponentSign;
            if (this.m_Sign == k_White)
            {
                opponentSign = k_Black;
            }
            else
            {
                opponentSign = k_White;
            }

            return opponentSign;
        }
    }
}