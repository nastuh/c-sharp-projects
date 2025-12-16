using System;
using System.Drawing;
using System.Windows.Forms;

namespace ButtonTicTacToe
{
    public partial class Form1 : Form
    {
        private Label[,] boardLabels;
        private Button[] controlButtons;
        private bool isPlayerXTurn = true;
        private int movesMade = 0;
        private int currentPosition = 0;
        private Color xColor = Color.Red;
        private Color oColor = Color.Blue;
        private Color selectedColor = Color.Yellow;
        private Color winColor = Color.Gold;

        private Label statusLabel;
        private Label instructionLabel;
        private Panel controlPanel;

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
            SetupKeyboardControls();
        }

        private void InitializeGame()
        {
            // Window settings
            this.Text = "Tic Tac Toe - Button Control";
            this.Size = new Size(600, 700);
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Title
            Label titleLabel = new Label();
            titleLabel.Text = "TIC TAC TOE - BUTTON CONTROL";
            titleLabel.Font = new Font("Arial", 20, FontStyle.Bold);
            titleLabel.ForeColor = Color.DarkBlue;
            titleLabel.Size = new Size(400, 40);
            titleLabel.Location = new Point(100, 20);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(titleLabel);

            // Status label
            statusLabel = new Label();
            statusLabel.Text = "Player X Turn (Red) - Use buttons below";
            statusLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            statusLabel.ForeColor = xColor;
            statusLabel.Size = new Size(500, 30);
            statusLabel.Location = new Point(50, 70);
            statusLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(statusLabel);

            // Create 3x3 board (using Labels instead of Buttons)
            boardLabels = new Label[3, 3];
            int cellSize = 100;
            int startX = 150;
            int startY = 120;

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    Label cellLabel = new Label();
                    cellLabel.Size = new Size(cellSize, cellSize);
                    cellLabel.Location = new Point(startX + col * cellSize, startY + row * cellSize);
                    cellLabel.Font = new Font("Arial", 36, FontStyle.Bold);
                    cellLabel.BackColor = Color.LightGray;
                    cellLabel.ForeColor = Color.Black;
                    cellLabel.BorderStyle = BorderStyle.FixedSingle;
                    cellLabel.TextAlign = ContentAlignment.MiddleCenter;
                    cellLabel.Tag = new Point(row, col);

                    boardLabels[row, col] = cellLabel;
                    this.Controls.Add(cellLabel);
                }
            }

            // Instruction label
            instructionLabel = new Label();
            instructionLabel.Text = "Select position with number buttons (1-9), then press PLACE";
            instructionLabel.Font = new Font("Arial", 11);
            instructionLabel.ForeColor = Color.DarkSlateGray;
            instructionLabel.Size = new Size(400, 30);
            instructionLabel.Location = new Point(100, 450);
            instructionLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(instructionLabel);

            // Create control panel
            controlPanel = new Panel();
            controlPanel.Size = new Size(500, 200);
            controlPanel.Location = new Point(50, 480);
            controlPanel.BackColor = Color.AliceBlue;
            controlPanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(controlPanel);

            // Number buttons (1-9) for position selection
            Button[] numberButtons = new Button[9];
            int buttonWidth = 50;
            int buttonHeight = 40;
            int buttonSpacing = 55;
            int startButtonsX = 50;
            int startButtonsY = 20;

            for (int i = 0; i < 9; i++)
            {
                Button numButton = new Button();
                numButton.Text = (i + 1).ToString();
                numButton.Size = new Size(buttonWidth, buttonHeight);
                numButton.Location = new Point(startButtonsX + (i % 3) * buttonSpacing,
                                                startButtonsY + (i / 3) * buttonSpacing);
                numButton.Font = new Font("Arial", 14, FontStyle.Bold);
                numButton.BackColor = Color.LightGray;
                numButton.ForeColor = Color.Black;
                numButton.Tag = i;
                numButton.Click += NumberButton_Click;

                numberButtons[i] = numButton;
                controlPanel.Controls.Add(numButton);
            }

            // Action buttons
            controlButtons = new Button[3];

            // Place button
            Button placeButton = new Button();
            placeButton.Text = "PLACE (Enter)";
            placeButton.Size = new Size(120, 50);
            placeButton.Location = new Point(200, 20);
            placeButton.Font = new Font("Arial", 12, FontStyle.Bold);
            placeButton.BackColor = Color.LightGreen;
            placeButton.ForeColor = Color.DarkGreen;
            placeButton.Click += PlaceButton_Click;
            controlButtons[0] = placeButton;
            controlPanel.Controls.Add(placeButton);

            // Clear button
            Button clearButton = new Button();
            clearButton.Text = "CLEAR (C)";
            clearButton.Size = new Size(120, 50);
            clearButton.Location = new Point(200, 80);
            clearButton.Font = new Font("Arial", 12, FontStyle.Bold);
            clearButton.BackColor = Color.LightCoral;
            clearButton.ForeColor = Color.DarkRed;
            clearButton.Click += ClearButton_Click;
            controlButtons[1] = clearButton;
            controlPanel.Controls.Add(clearButton);

            // New Game button
            Button newGameButton = new Button();
            newGameButton.Text = "NEW GAME (N)";
            newGameButton.Size = new Size(120, 50);
            newGameButton.Location = new Point(200, 140);
            newGameButton.Font = new Font("Arial", 12, FontStyle.Bold);
            newGameButton.BackColor = Color.LightSkyBlue;
            newGameButton.ForeColor = Color.DarkBlue;
            newGameButton.Click += NewGameButton_Click;
            controlButtons[2] = newGameButton;
            controlPanel.Controls.Add(newGameButton);

            // Highlight first position
            HighlightCurrentPosition();
        }

        private void SetupKeyboardControls()
        {
            this.KeyPreview = true;
            this.KeyDown += Form_KeyDown;
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D1:
                case Keys.NumPad1:
                    SelectPosition(0);
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    SelectPosition(1);
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    SelectPosition(2);
                    break;
                case Keys.D4:
                case Keys.NumPad4:
                    SelectPosition(3);
                    break;
                case Keys.D5:
                case Keys.NumPad5:
                    SelectPosition(4);
                    break;
                case Keys.D6:
                case Keys.NumPad6:
                    SelectPosition(5);
                    break;
                case Keys.D7:
                case Keys.NumPad7:
                    SelectPosition(6);
                    break;
                case Keys.D8:
                case Keys.NumPad8:
                    SelectPosition(7);
                    break;
                case Keys.D9:
                case Keys.NumPad9:
                    SelectPosition(8);
                    break;
                case Keys.Enter:
                case Keys.Space:
                    PlaceMark();
                    break;
                case Keys.C:
                    ClearSelection();
                    break;
                case Keys.N:
                    ResetGame();
                    break;
                case Keys.Escape:
                    this.Close();
                    break;
            }
        }

        private void NumberButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            int position = (int)clickedButton.Tag;
            SelectPosition(position);
        }

        private void SelectPosition(int position)
        {
            // Remove highlight from previous position
            int prevRow = currentPosition / 3;
            int prevCol = currentPosition % 3;
            if (boardLabels[prevRow, prevCol].Text == "")
            {
                boardLabels[prevRow, prevCol].BackColor = Color.LightGray;
            }

            // Set new position
            currentPosition = position;
            HighlightCurrentPosition();
        }

        private void HighlightCurrentPosition()
        {
            int row = currentPosition / 3;
            int col = currentPosition % 3;

            // Only highlight if cell is empty
            if (boardLabels[row, col].Text == "")
            {
                boardLabels[row, col].BackColor = selectedColor;
            }
        }

        private void PlaceButton_Click(object sender, EventArgs e)
        {
            PlaceMark();
        }

        private void PlaceMark()
        {
            int row = currentPosition / 3;
            int col = currentPosition % 3;
            Label currentLabel = boardLabels[row, col];

            // Check if cell is already taken
            if (currentLabel.Text != "")
            {
                System.Media.SystemSounds.Beep.Play();
                instructionLabel.Text = "Cell is already occupied! Choose another.";
                instructionLabel.ForeColor = Color.Red;
                return;
            }

            // Place X or O
            if (isPlayerXTurn)
            {
                currentLabel.Text = "X";
                currentLabel.ForeColor = xColor;
                currentLabel.BackColor = Color.LightGray;
            }
            else
            {
                currentLabel.Text = "O";
                currentLabel.ForeColor = oColor;
                currentLabel.BackColor = Color.LightGray;
            }

            movesMade++;

            // Check for win or draw
            if (CheckForWin())
            {
                string winner = isPlayerXTurn ? "X" : "O";
                ShowGameOver($"Player {winner} Wins!", false);
            }
            else if (movesMade == 9)
            {
                ShowGameOver("It's a Draw!", true);
            }
            else
            {
                // Switch player
                isPlayerXTurn = !isPlayerXTurn;
                UpdateStatusLabel();

                // Move to next available position
                MoveToNextAvailablePosition();
            }
        }

        private void MoveToNextAvailablePosition()
        {
            // Find next empty cell
            for (int i = 1; i <= 9; i++)
            {
                int nextPos = (currentPosition + i) % 9;
                int row = nextPos / 3;
                int col = nextPos % 3;

                if (boardLabels[row, col].Text == "")
                {
                    SelectPosition(nextPos);
                    return;
                }
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            ClearSelection();
        }

        private void ClearSelection()
        {
            int row = currentPosition / 3;
            int col = currentPosition % 3;

            // Only clear if cell is empty
            if (boardLabels[row, col].Text == "")
            {
                boardLabels[row, col].BackColor = Color.LightGray;
            }

            instructionLabel.Text = "Select position with number buttons (1-9), then press PLACE";
            instructionLabel.ForeColor = Color.DarkSlateGray;
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            ResetGame();
        }

        private bool CheckForWin()
        {
            // Check rows
            for (int row = 0; row < 3; row++)
            {
                if (boardLabels[row, 0].Text != "" &&
                    boardLabels[row, 0].Text == boardLabels[row, 1].Text &&
                    boardLabels[row, 1].Text == boardLabels[row, 2].Text)
                {
                    HighlightWinningCells(row, 0, row, 1, row, 2);
                    return true;
                }
            }

            // Check columns
            for (int col = 0; col < 3; col++)
            {
                if (boardLabels[0, col].Text != "" &&
                    boardLabels[0, col].Text == boardLabels[1, col].Text &&
                    boardLabels[1, col].Text == boardLabels[2, col].Text)
                {
                    HighlightWinningCells(0, col, 1, col, 2, col);
                    return true;
                }
            }

            // Check diagonals
            if (boardLabels[0, 0].Text != "" &&
                boardLabels[0, 0].Text == boardLabels[1, 1].Text &&
                boardLabels[1, 1].Text == boardLabels[2, 2].Text)
            {
                HighlightWinningCells(0, 0, 1, 1, 2, 2);
                return true;
            }

            if (boardLabels[0, 2].Text != "" &&
                boardLabels[0, 2].Text == boardLabels[1, 1].Text &&
                boardLabels[1, 1].Text == boardLabels[2, 0].Text)
            {
                HighlightWinningCells(0, 2, 1, 1, 2, 0);
                return true;
            }

            return false;
        }

        private void HighlightWinningCells(int r1, int c1, int r2, int c2, int r3, int c3)
        {
            boardLabels[r1, c1].BackColor = winColor;
            boardLabels[r2, c2].BackColor = winColor;
            boardLabels[r3, c3].BackColor = winColor;
        }

        private void UpdateStatusLabel()
        {
            if (isPlayerXTurn)
            {
                statusLabel.Text = "Player X Turn (Red) - Select position and press PLACE";
                statusLabel.ForeColor = xColor;
            }
            else
            {
                statusLabel.Text = "Player O Turn (Blue) - Select position and press PLACE";
                statusLabel.ForeColor = oColor;
            }
        }

        private void ShowGameOver(string message, bool isDraw)
        {
            // Disable control buttons
            foreach (Button button in controlButtons)
            {
                button.Enabled = false;
            }

            // Disable number buttons
            foreach (Control control in controlPanel.Controls)
            {
                if (control is Button button && button.Text.Length == 1)
                {
                    button.Enabled = false;
                }
            }

            // Show message
            string title = isDraw ? "Game Over - Draw" : "Congratulations!";
            MessageBox.Show(message + "\n\nPress 'NEW GAME' to play again.",
                          title, MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Update status label
            statusLabel.Text = isDraw ? "Game Over: It's a Draw!" : $"Winner: Player {(isPlayerXTurn ? "X" : "O")}!";
            statusLabel.ForeColor = isDraw ? Color.Purple : (isPlayerXTurn ? xColor : oColor);

            instructionLabel.Text = "Game Over! Press 'NEW GAME' to play again.";
        }

        private void ResetGame()
        {
            // Reset board
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    boardLabels[row, col].Text = "";
                    boardLabels[row, col].BackColor = Color.LightGray;
                    boardLabels[row, col].ForeColor = Color.Black;
                }
            }

            // Reset game state
            isPlayerXTurn = true;
            movesMade = 0;
            currentPosition = 0;

            // Re-enable all buttons
            foreach (Button button in controlButtons)
            {
                button.Enabled = true;
            }

            foreach (Control control in controlPanel.Controls)
            {
                if (control is Button button)
                {
                    button.Enabled = true;
                }
            }

            // Update display
            UpdateStatusLabel();
            HighlightCurrentPosition();

            instructionLabel.Text = "Select position with number buttons (1-9), then press PLACE";
            instructionLabel.ForeColor = Color.DarkSlateGray;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Menu for color settings
            MenuStrip menuStrip = new MenuStrip();

            // Game menu
            ToolStripMenuItem gameMenu = new ToolStripMenuItem("Game");

            ToolStripMenuItem newGameMenu = new ToolStripMenuItem("New Game");
            newGameMenu.Click += (s, e) => ResetGame();
            gameMenu.DropDownItems.Add(newGameMenu);

            ToolStripMenuItem exitMenu = new ToolStripMenuItem("Exit");
            exitMenu.Click += (s, e) => this.Close();
            gameMenu.DropDownItems.Add(exitMenu);

            // Settings menu
            ToolStripMenuItem settingsMenu = new ToolStripMenuItem("Settings");

            ToolStripMenuItem xColorMenu = new ToolStripMenuItem("Change X Color");
            xColorMenu.Click += XColorMenu_Click;
            settingsMenu.DropDownItems.Add(xColorMenu);

            ToolStripMenuItem oColorMenu = new ToolStripMenuItem("Change O Color");
            oColorMenu.Click += OColorMenu_Click;
            settingsMenu.DropDownItems.Add(oColorMenu);

            // Help menu
            ToolStripMenuItem helpMenu = new ToolStripMenuItem("Help");

            ToolStripMenuItem controlsMenu = new ToolStripMenuItem("Controls");
            controlsMenu.Click += ControlsMenu_Click;
            helpMenu.DropDownItems.Add(controlsMenu);

            ToolStripMenuItem aboutMenu = new ToolStripMenuItem("About");
            aboutMenu.Click += AboutMenu_Click;
            helpMenu.DropDownItems.Add(aboutMenu);

            menuStrip.Items.Add(gameMenu);
            menuStrip.Items.Add(settingsMenu);
            menuStrip.Items.Add(helpMenu);

            this.Controls.Add(menuStrip);
            this.MainMenuStrip = menuStrip;

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void XColorMenu_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = xColor;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                xColor = dialog.Color;
                UpdateXColors();
                UpdateStatusLabel();
            }
        }

        private void OColorMenu_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = oColor;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                oColor = dialog.Color;
                UpdateOColors();
                UpdateStatusLabel();
            }
        }

        private void UpdateXColors()
        {
            foreach (Label label in boardLabels)
            {
                if (label.Text == "X")
                {
                    label.ForeColor = xColor;
                }
            }
        }

        private void UpdateOColors()
        {
            foreach (Label label in boardLabels)
            {
                if (label.Text == "O")
                {
                    label.ForeColor = oColor;
                }
            }
        }

        private void ControlsMenu_Click(object sender, EventArgs e)
        {
            string controls = "GAME CONTROLS:\n\n" +
                             "BUTTONS:\n" +
                             "1-9: Select position on board\n" +
                             "PLACE: Put X or O in selected cell\n" +
                             "CLEAR: Clear current selection\n" +
                             "NEW GAME: Start new game\n\n" +
                             "KEYBOARD SHORTCUTS:\n" +
                             "1-9: Select position\n" +
                             "Enter/Space: Place mark\n" +
                             "C: Clear selection\n" +
                             "N: New game\n" +
                             "Esc: Exit game\n\n" +
                             "HOW TO PLAY:\n" +
                             "1. Press number button (1-9) to select cell\n" +
                             "2. Press PLACE button to put your mark\n" +
                             "3. First to get 3 in a row wins!";

            MessageBox.Show(controls, "Game Controls", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AboutMenu_Click(object sender, EventArgs e)
        {
            string about = "TIC TAC TOE - Button Control Version\n" +
                          "Created with C# Windows Forms\n\n" +
                          "Features:\n" +
                          "• Full button control (no mouse needed)\n" +
                          "• Keyboard shortcuts\n" +
                          "• Color customization\n" +
                          "• Win detection\n" +
                          "• Draw detection\n" +
                          "• Visual feedback";

            MessageBox.Show(about, "About This Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}