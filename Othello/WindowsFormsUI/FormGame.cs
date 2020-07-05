using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Ex02_Othelo;

namespace Ex05.WindowsFormsUI
{
    public partial class FormGame : Form
    {
        private const string k_Black = "X";
        private const string k_White = "O";
        private const int k_PictureBoxSize = 50;

        private readonly Image r_RedCoin = Properties.Resources.CoinRed;
        private readonly Image r_YellowCoin = Properties.Resources.CoinYellow;

        private readonly int r_GameBoardSize;

        private int m_BlackNumOfWins;
        private int m_WhiteNumOfWins;
        private bool m_IsAgainstComputer;

        private Game m_Game;
        private PictureBoxBoard[,] m_PictureBoxBoard;
        private Player m_CurrentPlayer;
        private Random m_ComputerRandomMove;

        public FormGame(int i_GameBoardSize, bool i_IsAgainstComputer)
        {
            InitializeComponent();
            r_GameBoardSize = i_GameBoardSize;
            m_IsAgainstComputer = i_IsAgainstComputer;
            m_BlackNumOfWins = 0;
            m_WhiteNumOfWins = 0;
            m_ComputerRandomMove = new Random();
            buildPictureBoxBoard();
            setFormSize();
            startNewGame();
        }

        private void setFormSize()
        {
            this.Height = (r_GameBoardSize * k_PictureBoxSize) + 70;
            this.Width = (r_GameBoardSize * k_PictureBoxSize) + 50;
        }

        private void startNewGame()
        {
            Player[] playersArray = new Player[2];

            playersArray[0] = new Player("Red", k_Black);
            if (m_IsAgainstComputer)
            {
                playersArray[1] = new Player("Computer", k_White);
            }
            else
            {
                playersArray[1] = new Player("Yellow", k_White);
            }

            m_CurrentPlayer = playersArray[0];
            m_Game = new Game(r_GameBoardSize, m_IsAgainstComputer, playersArray);
            printGameBoard();
            playGame();
        }

        // $G$ CSS-999 (-3) instead of using strings in a if statement use constants 
        private void playGame()
        {
            setValidMoves();
            this.Text = string.Format("Othello - {0}'s turn", m_CurrentPlayer.PlayerName);
            if (m_Game.HasValidMoves(m_Game.PlayersArray[0]) || m_Game.HasValidMoves(m_Game.PlayersArray[1]))
            {
                if (hasValidMoveForCurrentPlayer())
                {
                    if (m_CurrentPlayer.PlayerName == "Computer")
                    {
                        printGameBoard();
                        makeComputerMove();
                    }
                }
                else
                {
                    setValidMoves();
                }
            }
            else
            {
                getWinner();
            }
        }

        private void buttonPictureBox_Click(object sender, EventArgs e)
        {
            PictureBoxBoard selectedMove = sender as PictureBoxBoard;
            makePlayerMove(selectedMove);
        }

        private void makePlayerMove(PictureBoxBoard i_PictureBoxSelected)
        {
            string move = string.Empty;
            move += Convert.ToChar(i_PictureBoxSelected.Col + 'A');
            move += (i_PictureBoxSelected.Row + 1).ToString();
            m_Game.MakeMove(m_CurrentPlayer, move);
            switchPlayer();
            playGame();
        }

        private void makeComputerMove()
        {
            Update();
            Thread.Sleep(1000);
            m_Game.GetValidMoves(m_CurrentPlayer);
            int randomIndex = m_ComputerRandomMove.Next(0, m_Game.ValidMoves.Count);
            m_Game.MakeMove(m_Game.PlayersArray[1], m_Game.ValidMoves[randomIndex]);
            switchPlayer();
            playGame();
        }

        private void switchPlayer()
        {
            if (m_CurrentPlayer.PlayerName == "Red")
            {
                m_CurrentPlayer = m_Game.PlayersArray[1];
            }
            else
            {
                m_CurrentPlayer = m_Game.PlayersArray[0];
            }
        }

        private void buildPictureBoxBoard()
        {
            int distanceFromLeft = 15;
            int distanceFromTop = 15;
            m_PictureBoxBoard = new PictureBoxBoard[r_GameBoardSize, r_GameBoardSize];
            for (int i = 0; i < r_GameBoardSize; i++)
            {
                for (int j = 0; j < r_GameBoardSize; j++)
                {
                    m_PictureBoxBoard[i, j] = createPictureBox(i, j, distanceFromLeft, distanceFromTop);
                    this.Controls.Add(m_PictureBoxBoard[i, j]);
                    distanceFromLeft += k_PictureBoxSize;
                }

                distanceFromTop += k_PictureBoxSize;
                distanceFromLeft = 15;
            }
        }

        private PictureBoxBoard createPictureBox(int i_Row, int i_Col, int i_Left, int i_Top)
        {
            PictureBoxBoard pictureBoxButton = new PictureBoxBoard();
            pictureBoxButton.Height = k_PictureBoxSize;
            pictureBoxButton.Width = k_PictureBoxSize;
            pictureBoxButton.Left = i_Left;
            pictureBoxButton.Top = i_Top;
            pictureBoxButton.Row = i_Row;
            pictureBoxButton.Col = i_Col;
            pictureBoxButton.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxButton.BorderStyle = BorderStyle.Fixed3D;
            return pictureBoxButton;
        }

        private void printGameBoard()
        {
            clearGameBoard();
            for (int i = 0; i < r_GameBoardSize; i++)
            {
                for (int j = 0; j < r_GameBoardSize; j++)
                {
                    if (m_Game.BoardGame.ScreenBoard[i, j] == k_Black)
                    {
                        m_PictureBoxBoard[i, j].Image = r_RedCoin;
                    }
                    else if (m_Game.BoardGame.ScreenBoard[i, j] == k_White)
                    {
                        m_PictureBoxBoard[i, j].Image = r_YellowCoin;
                    }
                    else
                    {
                        continue;
                    }

                    m_PictureBoxBoard[i, j].Enabled = true;
                }
            }
        }

        private void resetClicksOnBoard()
        {
            for (int i = 0; i < r_GameBoardSize; i++)
            {
                for (int j = 0; j < r_GameBoardSize; j++)
                {
                    m_PictureBoxBoard[i, j].Click -= new EventHandler(this.buttonPictureBox_Click);
                }
            }
        }

        private void setValidMoves()
        {
            m_Game.GetValidMoves(m_CurrentPlayer);
            resetClicksOnBoard();
            printGameBoard();
            foreach (string str in m_Game.ValidMoves)
            {
                int i = int.Parse(str[1].ToString());
                if (str.Length > 2)
                {
                    i = ((i * 10) + int.Parse(str[2].ToString())) - 1;
                }
                else
                {
                    i = i - 1;
                }

                int j = str[0] - 'A';
                m_PictureBoxBoard[i, j].Enabled = true;
                m_PictureBoxBoard[i, j].BackColor = Color.MediumSeaGreen;
                m_PictureBoxBoard[i, j].Click += new EventHandler(this.buttonPictureBox_Click);
            }
        }

        private void clearGameBoard()
        {
            for (int i = 0; i < r_GameBoardSize; i++)
            {
                for (int j = 0; j < r_GameBoardSize; j++)
                {
                    m_PictureBoxBoard[i, j].Enabled = false;
                    m_PictureBoxBoard[i, j].BackColor = Color.LightGray;
                    m_PictureBoxBoard[i, j].Image = null;
                }
            }
        }

        private bool hasValidMoveForCurrentPlayer()
        {
            bool hasValidMove = true;

            if (!m_Game.HasValidMoves(m_CurrentPlayer))
            {
                MessageBox.Show(string.Format("No more valid moves for {0}! Switch turns", m_CurrentPlayer.PlayerName));
                switchPlayer();
                this.Text = string.Format("Othello - {0}'s turn", m_CurrentPlayer.PlayerName);
                if (m_CurrentPlayer.PlayerName == "Computer")
                {
                    printGameBoard();
                    makeComputerMove();
                }

                hasValidMove = false;
            }

            return hasValidMove;
        }

        private void getWinner()
        {
            if (m_Game.PlayersArray[0].PlayerScore > m_Game.PlayersArray[1].PlayerScore)
            {
                m_BlackNumOfWins++;
                getWinnerMessage(m_Game.PlayersArray[0]);
            }
            else if (m_Game.PlayersArray[0].PlayerScore < m_Game.PlayersArray[1].PlayerScore)
            {
                m_WhiteNumOfWins++;
                getWinnerMessage(m_Game.PlayersArray[1]);
            }
            else
            {
                getTieMessage();
            }
        }

        private void getWinnerMessage(Player i_Winner)
        {
            string message = string.Format(
@"{0} Won!! ({1}/{2}) ({3}/{4})
Would you like another round?",
                i_Winner.PlayerName,
                m_Game.BoardGame.CountBlack,
                m_Game.BoardGame.CountWhite,
                m_BlackNumOfWins,
                m_WhiteNumOfWins);

            endGame(message);
        }

        private void getTieMessage()
        {
            string message = string.Format(
@"Game ends in tie! 
Would you like another round?");

            endGame(message);
        }

        private void endGame(string i_DialogMessage)
        {
            DialogResult dialogResult = MessageBox.Show(i_DialogMessage, "Othello", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                startNewGame();
            }
            else if (dialogResult == DialogResult.No)
            {
                this.Close();
            }
        }

        private void formGame_Load(object sender, EventArgs e)
        {
        }
    }
}
