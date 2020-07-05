using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    public class Game
    {
        private const string k_Quit = "Q";
        private Player[] m_Players;
        private Board m_BoardGame;
        private bool m_AgainstComputer;
        private List<string> m_ValidMoves;

        public Game(int i_ChosenSize, bool i_IsAgainstComputer, Player[] i_PlayersArray)
        {
            m_Players = i_PlayersArray;
            m_AgainstComputer = i_IsAgainstComputer;
            m_BoardGame = new Board(i_ChosenSize);
            m_ValidMoves = new List<string>();
            UpdateScore();
        }

        public List<string> ValidMoves
        {
            get { return m_ValidMoves; }
            set { m_ValidMoves = value; }
        }

        public Board BoardGame
        {
            get { return m_BoardGame; }
            set { m_BoardGame = value; }
        }

        public Player[] PlayersArray
        {
            get { return m_Players; }
            set { m_Players = value; }
        }

        public void UpdateScore()
        {
            m_Players[0].PlayerScore = m_BoardGame.CountBlack;
            m_Players[1].PlayerScore = m_BoardGame.CountWhite;
        }

        public bool AgainstComputer
        {
            get { return m_AgainstComputer; }
            set { m_AgainstComputer = value; }
        }

        // Save possible valid moves for the current player
        public void GetValidMoves(Player i_CurrentPlayer)
        {
            ValidMoves.Clear();

            for (int i = 0; i < m_BoardGame.ScreenBoard.GetLength(0); i++)
            {
                for (int j = 0; j < m_BoardGame.ScreenBoard.GetLength(0); j++)
                {
                    if (m_BoardGame.IsValidMove(i_CurrentPlayer, i, j))
                    {
                        string RowToConcat = (i + 1).ToString();
                        string ColToConcat = ((char)(j + 'A')).ToString();
                        string ValidMoveToAdd = string.Concat(ColToConcat, RowToConcat);
                        ValidMoves.Add(ValidMoveToAdd);
                    }
                }
            }
        }

        private bool isSyntacticValidation(string i_Move)
        {
            bool isValid = false;

            if (i_Move.Length == 2)
            {
                if (i_Move[0] >= 'A' && i_Move[0] <= Convert.ToChar('A' + BoardGame.ScreenBoard.Length - 1))
                {
                    if (i_Move[0] >= '1' && i_Move[0] <= Convert.ToChar('1' + BoardGame.ScreenBoard.Length - 1))
                    {
                        isValid = true;
                    }
                }
            }

            return isValid;
        }

        private bool isValidMove(Player i_Player, string i_Move)
        {
            bool isValid = false;

            int col = i_Move[0] - 'A';
            int row;
            bool rowIsValid = int.TryParse(i_Move[1].ToString(), out row);

            if (rowIsValid)
            {
                row -= 1;
                if (m_BoardGame.IsValidMove(i_Player, row, col))
                {
                    isValid = true;
                }
            }

            return isValid;
        }

        public bool CheckMove(Player i_Player, string i_Move)
        {
            bool isValid = false;

            if (i_Move == k_Quit)
            {
                isValid = true;
            }
            else
            {
                if (isSyntacticValidation(i_Move) && isValidMove(i_Player, i_Move))
                {
                    isValid = true;
                }
            }

            return isValid;
        }

        public void MakeMove(Player i_Player, string i_MoveToDo)
        {
            int col = i_MoveToDo[0] - 'A';
            int row = int.Parse(i_MoveToDo[1].ToString());
            if (i_MoveToDo.Length > 2)
            {
                row = ((row * 10) + int.Parse(i_MoveToDo[2].ToString())) - 1;
            }
            else
            {
                row -= 1;
            }

            m_BoardGame.UpdateBoardState(i_Player, row, col);
            m_BoardGame.CountColors();
            UpdateScore();
        }

        public bool HasValidMoves(Player i_Player)
        {
            bool isValid = false;

            for (int i = 0; i < m_BoardGame.ScreenBoard.GetLength(0); i++)
            {
                for (int j = 0; j < m_BoardGame.ScreenBoard.GetLength(0); j++)
                {
                    if (m_BoardGame.ScreenBoard[i, j] != null)
                    {
                        if (m_BoardGame.IsValidMove(i_Player, i, j))
                        {
                            isValid = true;
                        }
                    }
                }
            }

            return isValid;
        }
    }
}