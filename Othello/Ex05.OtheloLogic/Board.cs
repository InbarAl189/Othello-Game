using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    public class Board
    {
        private const string k_Empty = " ";
        private const string k_Black = "X";
        private const string k_White = "O";

        private string[,] m_Board;
        private int m_CountBlack;
        private int m_CountWhite;
        private int m_CountEmpty;

        public Board(int i_Size)
        {
            m_CountWhite = 2;
            m_CountBlack = 2;
            m_CountEmpty = (i_Size * i_Size) - (m_CountBlack + m_CountWhite);
            m_Board = new string[i_Size, i_Size];

            for (int i = 0; i < i_Size; i++)
            {
                for (int j = 0; j < i_Size; j++)
                {
                    m_Board[i, j] = k_Empty;
                }
            }

            m_Board[((i_Size / 2) - 1), ((i_Size / 2) - 1)] = k_White;
            m_Board[((i_Size / 2) - 1), (i_Size / 2)] = k_Black;
            m_Board[(i_Size / 2), ((i_Size / 2) - 1)] = k_Black;
            m_Board[(i_Size / 2), (i_Size / 2)] = k_White;
        }

        public int CountBlack
        {
            get { return m_CountBlack; }
            set { m_CountBlack = value; }
        } // count current number of blacks on the board

        public int CountWhite
        {
            get { return m_CountWhite; }
            set { m_CountWhite = value; }
        } // count current number of whites on the board

        public int CountEmpty
        {
            get { return m_CountEmpty; }
            set { m_CountWhite = value; }
        }

        public string[,] ScreenBoard
        {
            get { return m_Board; }
            set { m_Board = value; }
        }

        public bool IsValidMove(Player i_CurrentPlayer, int i_Row, int i_Col)
        {
            bool isValid = false;

            if (this.m_Board[i_Row, i_Col] != k_Empty)
            {
                isValid = false;
            }
            else
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (!((i == 0) && (j == 0)) && hasPossibleToFlip(i_CurrentPlayer, i_Row, i_Col, i, j))
                        {
                            isValid = true;
                        }
                    }
                }
            }

            return isValid;
        }

        // Check if any opponent discs on a given path
        private bool hasPossibleToFlip(Player i_Player, int i_Row, int i_Col, int i_DirectionRow, int i_DirectionCol)
        {
            bool isPossible = true;

            int rowToCheck = i_Row + i_DirectionRow;
            int colToCheck = i_Col + i_DirectionCol;

            while (rowToCheck >= 0 && rowToCheck < this.m_Board.GetLength(0) && colToCheck >= 0 &&
                colToCheck < this.m_Board.GetLength(0) && (this.m_Board[rowToCheck, colToCheck] == i_Player.GetOpponentSign()))
            {
                rowToCheck += i_DirectionRow;
                colToCheck += i_DirectionCol;
            }

            if (rowToCheck < 0 || rowToCheck > this.m_Board.GetLength(0) - 1 || colToCheck < 0 ||
                colToCheck > this.m_Board.GetLength(0) - 1 || (rowToCheck - i_DirectionRow == i_Row && colToCheck - i_DirectionCol == i_Col) ||
                this.m_Board[rowToCheck, colToCheck] != i_Player.Sign)
            {
                isPossible = false;
            }

            return isPossible;
        }

        public void UpdateBoardState(Player i_Player, int i_Row, int i_Col)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (hasPossibleToFlip(i_Player, i_Row, i_Col, i, j))
                    {
                        this.m_Board[i_Row, i_Col] = i_Player.Sign;
                        int rowToChange = i_Row + i;
                        int colToChange = i_Col + j;

                        while (this.m_Board[rowToChange, colToChange] != i_Player.Sign)
                        {
                            this.m_Board[rowToChange, colToChange] = i_Player.Sign;
                            rowToChange += i;
                            colToChange += j;
                        }
                    }
                }
            }
        }

        public void CountColors()
        {
            m_CountBlack = 0;
            m_CountEmpty = 0;
            m_CountWhite = 0;

            for (int i = 0; i < m_Board.GetLength(0); i++)
            {
                for (int j = 0; j < m_Board.GetLength(0); j++)
                {
                    if (m_Board[i, j] == k_Black)
                    {
                        m_CountBlack++;
                    }
                    else
                    {
                        if (m_Board[i, j] == k_White)
                        {
                            m_CountWhite++;
                        }
                        else
                        {
                            if (m_Board[i, j] == k_Empty)
                            {
                                m_CountEmpty++;
                            }
                        }
                    }
                }
            }
        }
    }
}