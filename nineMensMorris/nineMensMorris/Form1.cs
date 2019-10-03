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
        int[] boardArray = new int[25];



        public Board()
        {
            // Initialize boardArray to be full of 0's, meaning those pieces are empty
            for (int i = 0; i < 24; i++)
                boardArray[i] = 0;

            InitializeComponent();
            textBox15.AppendText(Environment.NewLine);
            textBox15.AppendText("Player 1's Turn");
        }
        private void rulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("By: Mark Ekis\nBrian Roden\nSungho Lee\nRaymond Rennock","Creators");
        }

        private void NewGameToolStripMenuItem_Click(object sender, EventArgs e) {
            // Loop through all controls, and re-enable any button that isn't enabled
            // Set each button to its neutral color
            foreach (Control s in ActiveForm.Controls) {
                Button b = s as Button;

                if (b != null) {
                    b.Enabled = true;
                    b.BackColor = System.Drawing.Color.Khaki;
                }
            }

            // Reset boardArray
            for (int i = 0; i < 24; i++)
                boardArray[i] = 0;

            // Reset game variables
            player1_tokens = 9;
            player2_tokens = 9;
            phase = true;
            turn = true;

            // Reset the counters for each player's token
            p1Tokens.Text = player1_tokens.ToString();
            p2Tokens.Text = player2_tokens.ToString();

            // Reset the game dialogue box, then inform user the game is reset
            textBox15.ResetText();
            textBox15.AppendText("Phase 1, Placement");
            textBox15.AppendText(Environment.NewLine);
            textBox15.AppendText("Player 1's Turn");
            MessageBox.Show("The game has been reset.");
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_click(object sender, EventArgs e)
        {
            // If player 1 selects an empty piece, then that index in boardArray becomes 1
            // If player 2 selects an empty piece, then that index in boardArray becomes 2
            if (phase == true && player2_tokens > 0)
            {
                Button b = (Button)sender;
                if (turn)
                {
                    b.BackColor = Color.OrangeRed;
                    boardArray[b.TabIndex] = 1;
                    player1_tokens--;
                    p1Tokens.Text = player1_tokens.ToString();
                    textBox15.AppendText(Environment.NewLine);
                    textBox15.AppendText("Player 2's Turn");
                }
                else
                {
                    b.BackColor = Color.Aqua;
                    boardArray[b.TabIndex] = 2;
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
