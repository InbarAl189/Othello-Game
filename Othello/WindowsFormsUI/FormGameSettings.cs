using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex05.WindowsFormsUI
{
    public partial class FormGameSettings : Form
    {
        public const int k_MaxBoardSize = 12;
        public const int k_MinBoardSize = 6;

        private bool m_IsAgainstComputer = false;
        private int m_BoardSize = k_MinBoardSize;

        public FormGameSettings()
        {
            InitializeComponent();
        }

        private void formGameSettings_Load(object sender, EventArgs e)
        {   
        }

        private void changeBoardSize()
        {
            if(m_BoardSize == k_MaxBoardSize)
            {
                m_BoardSize = k_MinBoardSize;
            }
            else
            {
                m_BoardSize += 2;
            }
        }

        private void buttonBoardSize_Click(object sender, EventArgs e)
        {
            changeBoardSize();
            Button boardSize = sender as Button;
            string message = string.Format("Board size: {0}x{1} (click to increase)", m_BoardSize, m_BoardSize);
            boardSize.Text = message;
        }

        private void buttonPlayAgainstFriend_Click(object sender, EventArgs e)
        {
            startNewGame();
        }

        private void buttonPlayAgainstComputer_Click(object sender, EventArgs e)
        {
            m_IsAgainstComputer = true;
            startNewGame(); 
        }

        private void startNewGame()
        {
            this.Hide();
            new FormGame(m_BoardSize, m_IsAgainstComputer).ShowDialog();
            this.Close();
        }
    }
}
