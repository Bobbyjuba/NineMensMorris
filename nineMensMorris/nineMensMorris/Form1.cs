using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace nineMensMorris {
    public partial class Board : Form {
        String test = "";
        bool phase = true; //When true it is the placement phase, when false it is the movement phase.
        bool turn = true; //When true it is player 1's turn, when false it is player 2's turn.
        bool select = true; //When true, select a piece to move, when false select the desitnation
        int delete = 0;
        Button selectedButton;
        int player1_tokens = 9;
        int player2_tokens = 9;
        int p1_currentTokens = 9;
        int p2_currentTokens = 9;
        int[] boardArray = new int[25];
        int[] millArray = new int[25];
        static int[][] matrix = new int[][] {
            new int[] { 1, 2, 3 },
            new int[] { 1, 10, 22 },
            new int[] { 2, 5, 8 },
            new int[] { 3, 15, 24 },
            new int[] { 4, 5, 6 },
            new int[] { 4, 11, 19 },
            new int[] { 6, 14, 21 },
            new int[] { 7, 8, 9 },
            new int[] { 7, 12, 16 },
            new int[] { 9, 13, 18 },
            new int[] { 10, 11, 12 },
            new int[] { 13, 14, 15 },
            new int[] { 16, 17, 18 },
            new int[] { 17, 20, 23 },
            new int[] { 19, 20, 21 },
            new int[] { 22, 23, 24 }
    };

        static int[][] adjacency = new int[][] {
            new int[] { -1 },
            new int[] { 2, 10 },
            new int[] { 1, 3, 5 },
            new int[] { 2, 15 },
            new int[] { 5, 11 },
            new int[] { 2, 4, 6, 8 },
            new int[] { 5, 14 },
            new int[] { 8, 12 },
            new int[] { 5, 7, 9 },
            new int[] { 8, 13 },
            new int[] { 1, 11, 22 },
            new int[] { 4, 10, 12, 19 },
            new int[] { 7, 11, 16 },
            new int[] { 9, 14, 18 },
            new int[] { 6, 13, 15, 21 },
            new int[] { 3, 14, 24 },
            new int[] { 12, 17 },
            new int[] { 16, 18, 20 },
            new int[] { 13, 17 },
            new int[] { 11, 20 },
            new int[] { 17, 19, 21, 23 },
            new int[] { 14, 20 },
            new int[] { 10, 23 },
            new int[] { 20, 22, 24 },
            new int[] { 15, 23 }

        };

        int[][] indexMills = new int[][] {
            new int[] {-1 },
            new int[] { 0, 1 },
            new int[] { 0, 2 },
            new int[] { 0, 3 },
            new int[] { 4, 5 },
            new int[] { 2, 4 },
            new int[] { 4, 6 },
            new int[] { 7, 8 },
            new int[] { 2, 7 },
            new int[] { 7, 9 },
            new int[] { 1, 10 },
            new int[] { 5, 10 },
            new int[] { 8, 10 },
            new int[] { 9, 11 },
            new int[] { 6, 11 },
            new int[] { 3, 11 },
            new int[] { 8, 12 },
            new int[] { 12, 13 },
            new int[] { 9, 12 },
            new int[] { 5, 14 },
            new int[] { 13, 14 },
            new int[] { 6, 14 },
            new int[] { 1, 15 },
            new int[] { 13, 15 },
            new int[] { 3, 15 }
        };
        public Board() {
            // Initialize boardArray to be full of 0's, meaning those pieces are empty
            for (int i = 1; i < 25; i++)
                boardArray[i] = 0;

            InitializeComponent();
            textBox15.AppendText(Environment.NewLine);
            textBox15.AppendText("Player 1's Turn");
        }
        private void rulesToolStripMenuItem_Click(object sender, EventArgs e) {
            MessageBox.Show("Mark Ekis\nBrian Roden\nSungho Lee\nRaymond Rennock", "Creators");
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
            for (int i = 0; i < 25; i++)
                boardArray[i] = 0;

            // Reset game variables
            player1_tokens = 9;
            player2_tokens = 9;
            phase = true;
            turn = true;
            select = true;
            delete = 0;
            p1_currentTokens = 9;
            p2_currentTokens = 9;

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
        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void mill(int head, int player, int clicked) {
            int a = matrix[indexMills[head][0]][0];  //a,b,c is one potential mill
            int b = matrix[indexMills[head][0]][1];
            int c = matrix[indexMills[head][0]][2];

            int d = matrix[indexMills[head][1]][0]; //d,e,f is another potential mill
            int e = matrix[indexMills[head][1]][1];
            int f = matrix[indexMills[head][1]][2];

            // MessageBox.Show(a + " " + b + " " + c + " " + d + " " + e + " " + f);

            if (boardArray[a] == player && boardArray[b] == player && boardArray[c] == player && boardArray[d] == player && boardArray[e] == player && boardArray[f] == player) {
                millArray[a] = player;
                millArray[b] = player;
                millArray[c] = player;
                millArray[d] = player;
                millArray[e] = player;
                millArray[f] = player;

                if (head == clicked) {
                    delete = player;
                    if (player == 1) {
                        textBox15.AppendText(Environment.NewLine);
                        textBox15.AppendText("Player " + player + " has formed a mill. Select a blue piece to remove.");
                        delete = 1;
                    }

                    else if (player == 2) {
                        textBox15.AppendText(Environment.NewLine);
                        textBox15.AppendText("Player " + player + " has formed a mill. Select a red piece to remove.");
                        delete = 2;
                    }
                }
            }
            else if (boardArray[a] == player && boardArray[b] == player && boardArray[c] == player) {
                millArray[a] = player;
                millArray[b] = player;
                millArray[c] = player;

                if (head == clicked) {
                    delete = player;
                    if (player == 1) {
                        textBox15.AppendText(Environment.NewLine);
                        textBox15.AppendText("Player " + player + " has formed a mill. Select a blue piece to remove.");
                        delete = 1;
                    }

                    else if (player == 2) {
                        textBox15.AppendText(Environment.NewLine);
                        textBox15.AppendText("Player " + player + " has formed a mill. Select a red piece to remove.");
                        delete = 2;
                    }
                }
            }

            else if (boardArray[d] == player && boardArray[e] == player && boardArray[f] == player) {
                millArray[d] = player;
                millArray[e] = player;
                millArray[f] = player;

                if (head == clicked) {
                    delete = player;
                    if (player == 1) {
                        textBox15.AppendText(Environment.NewLine);
                        textBox15.AppendText("Player " + player + " has formed a mill. Select a blue piece to remove.");
                        delete = 1;
                    }

                    else if (player == 2) {
                        textBox15.AppendText(Environment.NewLine);
                        textBox15.AppendText("Player " + player + " has formed a mill. Select a red piece to remove.");
                        delete = 2;
                    }
                }
            }

            else {
                millArray[head] = 0;
            }
        }

        private void millDetection(int clicked) {
            for (int i = 1; i < 25; i++) {
                if (boardArray[i] == 1)
                    mill(i, 1, clicked);
            }

            for (int i = 1; i < 25; i++) {
                if (boardArray[i] == 2)
                    mill(i, 2, clicked);
            }

            for (int i = 1; i < 25; i++) {
                if (boardArray[i] == 0)
                    mill(i, 0, -1);
            }

            if ((p1Tokens.Text == "0") && (p2Tokens.Text == "0") && (phase == true) && (turn) && delete == 0) {
                phase = false;
                turn = true;
                select = true;

                enableButtons(1);
            }

            else if ((p1Tokens.Text == "0") && (p2Tokens.Text == "0") && (phase == true) && (turn == false) && delete == 0) {
                phase = false;
                turn = false;
                select = true;

                enableButtons(2);
            }

            test = "";
            for (int i = 1; i < millArray.Length; i++)
                test += Convert.ToString(millArray[i]) + ", ";

            test += "\n";

            for (int i = 1; i < boardArray.Length; i++)
                test += Convert.ToString(boardArray[i] + ", ");

            test += "\n";
            test += "Player Turn: ";
            if (turn) { test += 1; }
            else { test += 2; }
        }

        private void delete_piece(Button deleteButton, int head) {
            deleteButton.BackColor = Color.Khaki;
            deleteButton.Enabled = true;
            boardArray[deleteButton.TabIndex] = 0;
            millDetection(-1);
        }

        private void checkPhase() {
            if (phase == true && player1_tokens <= 0 && player2_tokens <= 0) {
                phase = false;
                turn = true;
                textBox15.AppendText(Environment.NewLine);
                textBox15.AppendText("Begining Phase 2, Movement");
                textBox15.AppendText("Player 1's turn");
                foreach (Control c in this.Controls) {
                    if (c is Button) {
                        if (c.BackColor != Color.OrangeRed) { c.Enabled = false; }
                        else { c.Enabled = true; }
                    }
                }
            }
        }

        private void enableButtons(int player) {
            if (player == 0) {
                foreach (Control d in this.Controls) {
                    if (d is Button) {
                        if ((d.BackColor == Color.Khaki) || (d.BackColor == Color.Green)) { d.Enabled = true; }
                        else { d.Enabled = false; }
                    }
                }
            }

            else if (player == 1) {
                foreach (Control c in this.Controls) {
                    if (c is Button) {
                        if (c.BackColor == Color.OrangeRed) { c.Enabled = true; }
                        else { c.Enabled = false; }
                    }
                }
            }

            else if (player == 2) {
                foreach (Control c in this.Controls) {
                    if (c is Button) {
                        if (c.BackColor == Color.Aqua) { c.Enabled = true; }
                        else { c.Enabled = false; }
                    }
                }
            }

            else {
                MessageBox.Show("You have reached an unreachable state. Please restart the game.");
            }
        }

        private void displayMillsToolStripMenuItem_Click(object sender, EventArgs e) {
            millDetection(-1);
            MessageBox.Show(test);
        }

        private void button_click(object sender, EventArgs e) {
            if (delete == 0) {
                // If player 1 selects an empty piece, then that index in boardArray becomes 1
                // If player 2 selects an empty piece, then that index in boardArray becomes 2
                if (phase == true) {
                    Button b = (Button)sender;
                    if (turn) {
                        b.BackColor = Color.OrangeRed;
                        boardArray[b.TabIndex] = 1;
                        player1_tokens--;
                        millDetection(b.TabIndex);
                        p1Tokens.Text = player1_tokens.ToString();
                        if (delete == 0) {
                            textBox15.AppendText(Environment.NewLine);
                            textBox15.AppendText("Player 2's Turn");
                        }
                    }
                    else {
                        b.BackColor = Color.Aqua;
                        boardArray[b.TabIndex] = 2;
                        player2_tokens--;
                        millDetection(b.TabIndex);
                        p2Tokens.Text = player2_tokens.ToString();
                        if (delete == 0) {
                            textBox15.AppendText(Environment.NewLine);
                            textBox15.AppendText("Player 1's Turn");
                        }
                    }
                    turn = !turn;
                    b.Enabled = false;
                    checkPhase();

                }
                else // Movement phase begins here
                {
                    Button b = (Button)sender;
                    if (turn) // Player 1 turn
                    {
                        if (select == true) // Selection Phase
                        {
                            enableButtons(1);

                            b.BackColor = Color.Green;

                            enableButtons(0);

                            selectedButton = b;
                            select = false;
                        }

                        else // Movement Phase
                        {
                            delete = 0;

                            if (adjacency[selectedButton.TabIndex].Contains(b.TabIndex)) {
                                boardArray[b.TabIndex] = 1;
                                b.BackColor = Color.OrangeRed;

                                boardArray[selectedButton.TabIndex] = 0;
                                selectedButton.BackColor = Color.Khaki;
                                for (int i = 0; i < matrix.Length; i++) {
                                    int x = matrix[i][0];
                                    int y = matrix[i][1];
                                    int z = matrix[i][2];

                                    if (b.TabIndex == x || b.TabIndex == y || b.TabIndex == z) {
                                        millArray[x] = 0;
                                        millArray[y] = 0;
                                        millArray[z] = 0;
                                    }

                                }

                                millDetection(b.TabIndex);
                                textBox15.AppendText(Environment.NewLine);
                                textBox15.AppendText("Player 2's turn");
                                turn = false;
                                select = true;

                                // When a piece has been moved, we need to check if the other player can move a piece
                                // If player 1 has more current tokens, it is likely that they are in a position to win
                                if (p1_currentTokens > p2_currentTokens) {
                                    bool foundOpenSpot = false; // Use this bool to check if we have an open spot

                                    foreach (Control s in ActiveForm.Controls) {
                                        if (foundOpenSpot == false) {
                                            Button c = s as Button;

                                            if (c != null) {
                                                if (c.BackColor == Color.Aqua) { // We are only iterating through the player 2 buttons 
                                                    for (int i = 0; i < adjacency[c.TabIndex].Length; i++) {
                                                        if (boardArray[adjacency[c.TabIndex][i]] == 0) {
                                                            foundOpenSpot = true; // If we find an open spot next to a player 2 button, then the game isn't over
                                                            break; // Break from the loop and end the current search for an open space
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    // If we didn't find an open space, then that means whoever has less tokens has lost
                                    if (foundOpenSpot == false) {
                                        foreach (Control s in ActiveForm.Controls) {
                                            Button c = s as Button;

                                            if (c != null) {
                                                c.Enabled = false;
                                            }
                                        }

                                        MessageBox.Show("Player 1 has won the game!");
                                        return;
                                    }
                                }

                                // Now we do the same search but for the case when player 2 has more tokens than player 1
                                if (p2_currentTokens > p1_currentTokens) {
                                    bool foundOpenSpot = false;

                                    foreach (Control s in ActiveForm.Controls) {
                                        if (foundOpenSpot == false) {
                                            Button c = s as Button;

                                            if (c != null) {
                                                if (c.BackColor == Color.OrangeRed) { // Iterate through player 1 buttons
                                                    for (int i = 0; i < adjacency[c.TabIndex].Length; i++) {
                                                        if (boardArray[adjacency[c.TabIndex][i]] == 0) {
                                                            foundOpenSpot = true;
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    if (foundOpenSpot == false) {
                                        foreach (Control s in ActiveForm.Controls) {
                                            Button c = s as Button;

                                            if (c != null) {
                                                c.Enabled = false;
                                            }
                                        }

                                        MessageBox.Show("Player 2 has won the game!");
                                        return;
                                    }
                                }

                                enableButtons(2);
                            }

                            else if (b.TabIndex == selectedButton.TabIndex) {
                                b.BackColor = Color.OrangeRed;
                                selectedButton = null;
                                select = true;
                                enableButtons(1);
                            }
                        }
                    }

                    else // Player 2 turn
                    {
                        delete = 0;

                        if (!turn) // Player 1 turn
                      {
                            if (select == true) // Selection Phase
                            {
                                enableButtons(2);

                                b.BackColor = Color.Green;

                                enableButtons(0);

                                selectedButton = b;
                                select = false;
                            }

                            else // Movement Phase
                            {
                                if (adjacency[selectedButton.TabIndex].Contains(b.TabIndex)) {
                                    boardArray[b.TabIndex] = 2;
                                    b.BackColor = Color.Aqua;

                                    boardArray[selectedButton.TabIndex] = 0;
                                    selectedButton.BackColor = Color.Khaki;
                                    for (int i = 0; i < matrix.Length; i++) {
                                        int x = matrix[i][0];
                                        int y = matrix[i][1];
                                        int z = matrix[i][2];

                                        if (b.TabIndex == x || b.TabIndex == y || b.TabIndex == z) {
                                            millArray[x] = 0;
                                            millArray[y] = 0;
                                            millArray[z] = 0;
                                        }

                                    }
                                    millDetection(b.TabIndex);
                                    textBox15.AppendText(Environment.NewLine);
                                    textBox15.AppendText("Player 1's turn");
                                    turn = true;
                                    select = true;

                                    // We must do all the same checks as before, but now do the checks whenever player 2 moves a piece
                                    if (p1_currentTokens > p2_currentTokens) {
                                        bool foundOpenSpot = false;

                                        foreach (Control s in ActiveForm.Controls) {
                                            if (foundOpenSpot == false) {
                                                Button c = s as Button;

                                                if (c != null) {
                                                    if (c.BackColor == Color.Aqua) {
                                                        for (int i = 0; i < adjacency[c.TabIndex].Length; i++) {
                                                            if (boardArray[adjacency[c.TabIndex][i]] == 0) {
                                                                foundOpenSpot = true;
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        if (foundOpenSpot == false) {
                                            foreach (Control s in ActiveForm.Controls) {
                                                Button c = s as Button;

                                                if (c != null) {
                                                    c.Enabled = false;
                                                }
                                            }

                                            MessageBox.Show("Player 1 has won the game!");
                                            return;
                                        }
                                    }

                                    if (p2_currentTokens > p1_currentTokens) {
                                        bool foundOpenSpot = false;

                                        foreach (Control s in ActiveForm.Controls) {
                                            if (foundOpenSpot == false) {
                                                Button c = s as Button;

                                                if (c != null) {
                                                    if (c.BackColor == Color.OrangeRed) {
                                                        for (int i = 0; i < adjacency[c.TabIndex].Length; i++) {
                                                            if (boardArray[adjacency[c.TabIndex][i]] == 0) {
                                                                foundOpenSpot = true;
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        if (foundOpenSpot == false) {
                                            foreach (Control s in ActiveForm.Controls) {
                                                Button c = s as Button;

                                                if (c != null) {
                                                    c.Enabled = false;
                                                }
                                            }

                                            MessageBox.Show("Player 2 has won the game!");
                                            return;
                                        }
                                    }

                                    enableButtons(1);
                                }

                                else if (b.TabIndex == selectedButton.TabIndex) {
                                    b.BackColor = Color.Aqua;
                                    selectedButton = null;
                                    select = true;
                                    enableButtons(2);
                                }

                                }
                            }
                        }
                    }
                }

                if (delete == 1) {
                    Button buttonToDelete = (Button)sender;
                    bool onlyMill = true;

                    foreach (Control s in ActiveForm.Controls) {
                        Button b = s as Button;

                        if (b != null) {
                            if (!b.Enabled && b.BackColor == Color.Aqua) {
                                b.Enabled = true;
                            }
                        }
                    }


                    if (buttonToDelete.Enabled) {
                        if (boardArray[buttonToDelete.TabIndex] == 2) {
                            if (millArray[buttonToDelete.TabIndex] == 2) {
                                for (int i = 1; i < boardArray.Length; i++) {
                                    if (boardArray[i] != millArray[i])
                                        onlyMill = false;
                                }

                                if (onlyMill == true) {
                                    delete_piece(buttonToDelete, buttonToDelete.TabIndex);
                                    foreach (Control s in ActiveForm.Controls) {
                                        Button b = s as Button;

                                        if (b != null) {
                                            if (b.Enabled && b.BackColor == Color.Aqua) {
                                                b.Enabled = false;
                                            }
                                        }
                                    }
                                    delete = 0;
                                    p2_currentTokens--;
                                    
                                    // If player 2's current tokens falls below 3, they have lost
                                    if (p2_currentTokens < 3) {
                                        foreach (Control s in ActiveForm.Controls) {
                                            Button b = s as Button;

                                            if (b != null) {
                                                b.Enabled = false;
                                            }
                                        }

                                        MessageBox.Show("Player 1 has won the game!");
                                        return;
                                    }

                                    textBox15.AppendText(Environment.NewLine);
                                    textBox15.AppendText("Player 2's turn");

                                    if (phase == false) {
                                        enableButtons(2);
                                    }

                                }

                            }

                            else {
                                delete_piece(buttonToDelete, buttonToDelete.TabIndex);
                                foreach (Control s in ActiveForm.Controls) {
                                    Button b = s as Button;

                                    if (b != null) {
                                        if (b.Enabled && b.BackColor == Color.Aqua) {
                                            b.Enabled = false;
                                        }
                                    }
                                }

                                delete = 0;
                                p2_currentTokens--;

                            // If player 2's current tokens falls below 3, they have lost
                            if (p2_currentTokens < 3) {
                                    foreach (Control s in ActiveForm.Controls) {
                                        Button b = s as Button;

                                        if (b != null) {
                                            b.Enabled = false;
                                        }
                                    }

                                    MessageBox.Show("Player 1 has won the game!");
                                    return;
                                }

                                textBox15.AppendText(Environment.NewLine);
                                textBox15.AppendText("Player 2's turn");

                                if (phase == false) {
                                    enableButtons(2);
                                }
                            }
                        }
                    }
                }

                if (delete == 2) {
                    Button buttonToDelete = (Button)sender;
                    bool onlyMill = true;

                    foreach (Control s in ActiveForm.Controls) {
                        Button b = s as Button;

                        if (b != null) {
                            if (!b.Enabled && b.BackColor == Color.OrangeRed) {
                                b.Enabled = true;
                            }
                        }
                    }


                    if (buttonToDelete.Enabled) {
                        if (boardArray[buttonToDelete.TabIndex] == 1) {
                            if (millArray[buttonToDelete.TabIndex] == 1) {
                                for (int i = 1; i < boardArray.Length; i++) {
                                    if (boardArray[i] != millArray[i])
                                        onlyMill = false;
                                }

                                if (onlyMill == true) {
                                    delete_piece(buttonToDelete, buttonToDelete.TabIndex);
                                    foreach (Control s in ActiveForm.Controls) {
                                        Button b = s as Button;

                                        if (b != null) {
                                            if (b.Enabled && b.BackColor == Color.OrangeRed) {
                                                b.Enabled = false;
                                            }
                                        }
                                    }

                                    delete = 0;
                                    p1_currentTokens--;

                                // If player 1's current tokens falls below 3, they have lost
                                if (p1_currentTokens < 3) {
                                        foreach (Control s in ActiveForm.Controls) {
                                            Button b = s as Button;

                                            if (b != null) {
                                                b.Enabled = false;
                                            }
                                        }

                                        MessageBox.Show("Player 2 has won the game!");
                                        return;
                                    }

                                    textBox15.AppendText(Environment.NewLine);
                                    textBox15.AppendText("Player 1's turn");

                                    if (phase == false) {
                                        enableButtons(1);
                                    }
                                }

                            }

                            else {
                                delete_piece(buttonToDelete, buttonToDelete.TabIndex);
                                foreach (Control s in ActiveForm.Controls) {
                                    Button b = s as Button;

                                    if (b != null) {
                                        if (b.Enabled && b.BackColor == Color.OrangeRed) {
                                            b.Enabled = false;
                                        }
                                    }
                                }

                                delete = 0;
                                p1_currentTokens--;

                            // If player 1's current tokens falls below 3, they have lost
                            if (p1_currentTokens < 3) {
                                    foreach (Control s in ActiveForm.Controls) {
                                        Button b = s as Button;

                                        if (b != null) {
                                            b.Enabled = false;
                                        }
                                    }

                                    MessageBox.Show("Player 2 has won the game!");
                                    return;
                                }

                                textBox15.AppendText(Environment.NewLine);
                                textBox15.AppendText("Player 1's turn");

                                if (phase == false) {
                                    enableButtons(1);
                                }
                            }
                        }
                    }
                }
            }
        }
    }