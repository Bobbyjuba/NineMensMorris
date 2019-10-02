using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nineMensMorris
{
    public partial class Board : Form
    {
        bool phase = true; //When true it is the placement phase, when false it is the movement phase.
        bool turn = true; //When true it is player 1's turn, when false it is player 2's turn.
        int player1_tokens = 9;
        int player2_tokens = 9;




        public Board()
        {
            InitializeComponent();
            textBox15.AppendText(Environment.NewLine);
            textBox15.AppendText("Player 1's Turn");
        }

        private void rulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("By: Mark Ekis\nBrian Roden\nSungho Lee\nRaymond Rennock","Creators");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_click(object sender, EventArgs e)
        {
            if (phase == true && player2_tokens > 0)
            {
                Button b = (Button)sender;
                if (turn)
                {
                    b.BackColor = Color.OrangeRed;
                    player1_tokens--;
                    p1Tokens.Text = player1_tokens.ToString();
                    textBox15.AppendText(Environment.NewLine);
                    textBox15.AppendText("Player 2's Turn");
                }
                else
                {
                    b.BackColor = Color.Aqua;
                    player2_tokens--;
                    p2Tokens.Text = player2_tokens.ToString();
                    textBox15.AppendText(Environment.NewLine);
                    textBox15.AppendText("Player 1's Turn");
                }
                turn = !turn;
                b.Enabled = false;
            }
            if (phase == true && player1_tokens <= 0 && player2_tokens <= 0)
            {
                phase = false;
                textBox15.AppendText(Environment.NewLine);
                textBox15.AppendText("Begining Phase 2, Movement");
            }



        }
    }
}
