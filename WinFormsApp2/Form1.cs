using System.IO.Enumeration;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer gameTimer; // Timer til at håndtere tidsstyring
        private int timeLeft = 39; // Initial tid i sekunder
        Random Random = new Random();
        int randomNumber;
        private int highScore;
        private const string highScoreFile = "highscore.txt";
        private int currentscore = 0;

        public Form1()
        {
            InitializeComponent();
            LoadHighScore();
            InitializeTimer();
            GenerateRandomNumber();
        }

        private void LoadHighScore()
        {
            if (File.Exists(highScoreFile))
            {
                string scoreString = File.ReadAllText(highScoreFile);
                if (int.TryParse(scoreString, out int score))
                {
                    highScore = score;
                }
                else
                {
                    highScore = 0;
                }
            }
            else
            {
                highScore = 0;
            }
            lbHighScore.Text = $"High Score: {highScore}"; // Opdater highscore label
        }

        private void SaveHighScore()
        {
            File.WriteAllText(highScoreFile, highScore.ToString());
        }
        private void CheckAndUpdateHighScore(int score)
        {
            if (score > highScore)
            {
                highScore = score;
                SaveHighScore(); // Gem den nye highscore
                lbHighScore.Text = $"High Score: {highScore}"; // Opdater label for highscore
            }
        }
        private void InitializeTimer()
        {
            gameTimer = new System.Windows.Forms.Timer(); // Opretter en ny Timer
            gameTimer.Interval = 1000; // Indstil intervallet til 1 sekund (1000 millisekunder)
            gameTimer.Tick += new EventHandler(TimerTick); // Abonnerer på Tick-hændelsen
            gameTimer.Start(); // Starter timeren
            lbTimer.Text = "Tid tilbage: " + timeLeft.ToString() + " sek."; // Viser initial tid
        }
        private void GenerateRandomNumber()
        {
            Random rnd = new Random();
            randomNumber = rnd.Next(1, 101); // Generer et nyt tilfældigt tal
            lbRandomNumber.Text = randomNumber.ToString(); // Viser tallet i midten

            // Nulstil timeren hver gang et nyt tal genereres
            timeLeft = 30; // F.eks. 30 sekunder til hver runde
            lbTimer.Text = "Tid tilbage: " + timeLeft.ToString() + " sek.";
            gameTimer.Start(); // Genstart timeren for næste runde
            btnSubmit.Enabled = true; // Aktivér indsendelsesknappen igen
        }
        private void TimerTick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                // Opdater timeren, hvis der er tid tilbage
                timeLeft--;
                lbTimer.Text = "Tid tilbage: " + timeLeft.ToString() + " sek.";
            }
            else
            {
                // Stop timeren, når tiden er udløbet
                gameTimer.Stop();
                lbFeedBack.Text = "Tiden er udløbet!";
                lbFeedBack.ForeColor = System.Drawing.Color.Red;

                // Deaktiver indsendelsesknappen, når tiden er udløbet
                btnSubmit.Enabled = false;
            }
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                // Håndter input fra tekstbokse
                int plus1 = int.Parse(txtPlus1.Text);
                int plus10 = int.Parse(txtPlus10.Text);
                int minus1 = int.Parse(txtMinus1.Text);
                int minus10 = int.Parse(txtMinus10.Text);
                int minus3 = int.Parse(txtMinus3.Text);
                int minus7 = int.Parse(txtMinus7.Text);
                int plus3 = int.Parse(txtPlus3.Text);
                int plus7 = int.Parse(txtPlus7.Text);

                bool allCorrect = true; // Variabel til at tracke om alle svar er korrekte

                // Tjekker input for plus og minus beregninger
                if (plus1 == randomNumber + 1)
                {
                    txtPlus1.BackColor = System.Drawing.Color.Green;
                }
                else
                {
                    txtPlus1.BackColor = System.Drawing.Color.Red;
                    allCorrect = false;
                }

                if (plus10 == randomNumber + 10)
                {
                    txtPlus10.BackColor = System.Drawing.Color.Green;
                }
                else
                {
                    txtPlus10.BackColor = System.Drawing.Color.Red;
                    allCorrect = false;
                }

                if (minus1 == randomNumber - 1)
                {
                    txtMinus1.BackColor = System.Drawing.Color.Green;
                }
                else
                {
                    txtMinus1.BackColor = System.Drawing.Color.Red;
                    allCorrect = false;
                }

                if (minus10 == randomNumber - 10)
                {
                    txtMinus10.BackColor = System.Drawing.Color.Green;
                }
                else
                {
                    txtMinus10.BackColor = System.Drawing.Color.Red;
                    allCorrect = false;
                }

                if (minus3 == randomNumber - 3)
                {
                    txtMinus3.BackColor = System.Drawing.Color.Green;
                }
                else
                {
                    txtMinus3.BackColor = System.Drawing.Color.Red;
                    allCorrect = false;
                }

                if (minus7 == randomNumber - 7)
                {
                    txtMinus7.BackColor = System.Drawing.Color.Green;
                }
                else
                {
                    txtMinus7.BackColor = System.Drawing.Color.Red;
                    allCorrect = false;
                }

                if (plus3 == randomNumber + 3)
                {
                    txtPlus3.BackColor = System.Drawing.Color.Green;
                }
                else
                {
                    txtPlus3.BackColor = System.Drawing.Color.Red;
                    allCorrect = false;
                }

                if (plus7 == randomNumber + 7)
                {
                    txtPlus7.BackColor = System.Drawing.Color.Green;
                }
                else
                {
                    txtPlus7.BackColor = System.Drawing.Color.Red;
                    allCorrect = false;
                }

                // Hvis alle svar er korrekte, giv feedback og opdater score
                if (allCorrect)
                {
                    lbFeedBack.Text = "Tillykke, alle svar er korrekte!";
                    lbFeedBack.ForeColor = System.Drawing.Color.Green;
                    currentscore += 10; // Forøg score hvis alle svar er rigtige
                }
                else
                {
                    lbFeedBack.Text = "Nogle svar er forkerte, prøv igen!";
                    lbFeedBack.ForeColor = System.Drawing.Color.Red;
                }

                // Opdater highscoren hvis nødvendigt
                CheckAndUpdateHighScore(currentscore);

                // Ryd tekstbokse for næste runde
                ClearTextBoxes();

                // Generer et nyt tilfældigt tal for næste runde
                GenerateRandomNumber();
            }
            catch (FormatException)
            {
                lbFeedBack.Text = "Indtast venligst gyldige tal!";
                lbFeedBack.ForeColor = System.Drawing.Color.Red;
                // Ryd tekstbokse for næste runde
                ClearTextBoxes();
                // Generer et nyt tilfældigt tal
                GenerateRandomNumber();
            }
        }
        private void ClearTextBoxes()
        {
            txtPlus1.Clear();
            txtPlus10.Clear();
            txtMinus1.Clear();
            txtMinus10.Clear();
            txtMinus3.Clear();
            txtMinus7.Clear();
            txtPlus3.Clear();
            txtPlus7.Clear();
        }
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            btnSubmit = new Button();
            txtPlus1 = new TextBox();
            txtMinus1 = new TextBox();
            txtPlus3 = new TextBox();
            lbRandomNumber = new Label();
            txtMinus3 = new TextBox();
            lbFeedBack = new Label();
            labelMinus1 = new Label();
            labelPlus1 = new Label();
            labelMinus3 = new Label();
            labelPlus3 = new Label();
            Timer = new System.Windows.Forms.Timer(components);
            txtMinus10 = new TextBox();
            txtMinus7 = new TextBox();
            txtPlus7 = new TextBox();
            txtPlus10 = new TextBox();
            labelPlus10 = new Label();
            labelMinus10 = new Label();
            labelMinus7 = new Label();
            labelPlus7 = new Label();
            lbTimer = new Label();
            lbHighScore = new Label();
            SuspendLayout();
            // 
            // btnSubmit
            // 
            btnSubmit.Font = new Font("Segoe UI", 15F);
            btnSubmit.Location = new Point(668, 406);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(122, 42);
            btnSubmit.TabIndex = 5;
            btnSubmit.Text = "Submit";
            btnSubmit.UseVisualStyleBackColor = true;
            btnSubmit.Click += btnSubmit_Click;
            // 
            // txtPlus1
            // 
            txtPlus1.Font = new Font("Segoe UI", 32F);
            txtPlus1.Location = new Point(410, 250);
            txtPlus1.Multiline = true;
            txtPlus1.Name = "txtPlus1";
            txtPlus1.Size = new Size(100, 60);
            txtPlus1.TabIndex = 41;
            // 
            // txtMinus1
            // 
            txtMinus1.Font = new Font("Segoe UI", 32F);
            txtMinus1.Location = new Point(570, 250);
            txtMinus1.Multiline = true;
            txtMinus1.Name = "txtMinus1";
            txtMinus1.Size = new Size(100, 60);
            txtMinus1.TabIndex = 40;
            // 
            // txtPlus3
            // 
            txtPlus3.Font = new Font("Segoe UI", 32F);
            txtPlus3.Location = new Point(488, 126);
            txtPlus3.Multiline = true;
            txtPlus3.Name = "txtPlus3";
            txtPlus3.Size = new Size(100, 60);
            txtPlus3.TabIndex = 39;
            // 
            // lbRandomNumber
            // 
            lbRandomNumber.AutoSize = true;
            lbRandomNumber.Font = new Font("Segoe UI", 32F);
            lbRandomNumber.Location = new Point(516, 250);
            lbRandomNumber.MaximumSize = new Size(350, 400);
            lbRandomNumber.Name = "lbRandomNumber";
            lbRandomNumber.Size = new Size(48, 59);
            lbRandomNumber.TabIndex = 11;
            lbRandomNumber.Text = "0";
            // 
            // txtMinus3
            // 
            txtMinus3.Font = new Font("Segoe UI", 32F);
            txtMinus3.Location = new Point(488, 374);
            txtMinus3.Multiline = true;
            txtMinus3.Name = "txtMinus3";
            txtMinus3.Size = new Size(100, 60);
            txtMinus3.TabIndex = 38;
            // 
            // lbFeedBack
            // 
            lbFeedBack.AutoSize = true;
            lbFeedBack.Font = new Font("Segoe UI", 15F);
            lbFeedBack.Location = new Point(200, 452);
            lbFeedBack.Name = "lbFeedBack";
            lbFeedBack.Size = new Size(142, 28);
            lbFeedBack.TabIndex = 13;
            lbFeedBack.Text = "ShowFeedBack";
            // 
            // labelMinus1
            // 
            labelMinus1.AutoSize = true;
            labelMinus1.Font = new Font("Segoe UI", 16F);
            labelMinus1.Location = new Point(676, 259);
            labelMinus1.Name = "labelMinus1";
            labelMinus1.Size = new Size(34, 30);
            labelMinus1.TabIndex = 37;
            labelMinus1.Text = "-1";
            // 
            // labelPlus1
            // 
            labelPlus1.AutoSize = true;
            labelPlus1.Font = new Font("Segoe UI", 16F);
            labelPlus1.Location = new Point(364, 259);
            labelPlus1.Name = "labelPlus1";
            labelPlus1.Size = new Size(40, 30);
            labelPlus1.TabIndex = 36;
            labelPlus1.Text = "+1";
            // 
            // labelMinus3
            // 
            labelMinus3.AutoSize = true;
            labelMinus3.Font = new Font("Segoe UI", 16F);
            labelMinus3.Location = new Point(522, 328);
            labelMinus3.Name = "labelMinus3";
            labelMinus3.Size = new Size(34, 30);
            labelMinus3.TabIndex = 35;
            labelMinus3.Text = "-3";
            // 
            // labelPlus3
            // 
            labelPlus3.AutoSize = true;
            labelPlus3.Font = new Font("Segoe UI", 16F);
            labelPlus3.Location = new Point(516, 201);
            labelPlus3.Name = "labelPlus3";
            labelPlus3.Size = new Size(40, 30);
            labelPlus3.TabIndex = 35;
            labelPlus3.Text = "+3";
            // 
            // txtMinus10
            // 
            txtMinus10.Font = new Font("Segoe UI", 32F);
            txtMinus10.Location = new Point(716, 250);
            txtMinus10.Multiline = true;
            txtMinus10.Name = "txtMinus10";
            txtMinus10.Size = new Size(100, 60);
            txtMinus10.TabIndex = 34;
            // 
            // txtMinus7
            // 
            txtMinus7.Font = new Font("Segoe UI", 32F);
            txtMinus7.Location = new Point(488, 485);
            txtMinus7.Multiline = true;
            txtMinus7.Name = "txtMinus7";
            txtMinus7.Size = new Size(100, 60);
            txtMinus7.TabIndex = 33;
            // 
            // txtPlus7
            // 
            txtPlus7.Font = new Font("Segoe UI", 32F);
            txtPlus7.Location = new Point(488, 2);
            txtPlus7.Multiline = true;
            txtPlus7.Name = "txtPlus7";
            txtPlus7.Size = new Size(100, 60);
            txtPlus7.TabIndex = 33;
            // 
            // txtPlus10
            // 
            txtPlus10.Font = new Font("Segoe UI", 32F);
            txtPlus10.Location = new Point(258, 250);
            txtPlus10.Multiline = true;
            txtPlus10.Name = "txtPlus10";
            txtPlus10.Size = new Size(100, 60);
            txtPlus10.TabIndex = 32;
            // 
            // labelPlus10
            // 
            labelPlus10.AutoSize = true;
            labelPlus10.Font = new Font("Segoe UI", 16F);
            labelPlus10.Location = new Point(200, 259);
            labelPlus10.Name = "labelPlus10";
            labelPlus10.Size = new Size(52, 30);
            labelPlus10.TabIndex = 31;
            labelPlus10.Text = "+10";
            // 
            // labelMinus10
            // 
            labelMinus10.AutoSize = true;
            labelMinus10.Font = new Font("Segoe UI", 16F);
            labelMinus10.Location = new Point(828, 259);
            labelMinus10.Name = "labelMinus10";
            labelMinus10.Size = new Size(46, 30);
            labelMinus10.TabIndex = 30;
            labelMinus10.Text = "-10";
            // 
            // labelMinus7
            // 
            labelMinus7.AutoSize = true;
            labelMinus7.Font = new Font("Segoe UI", 16F);
            labelMinus7.Location = new Point(522, 452);
            labelMinus7.Name = "labelMinus7";
            labelMinus7.Size = new Size(34, 30);
            labelMinus7.TabIndex = 29;
            labelMinus7.Text = "-7";
            // 
            // labelPlus7
            // 
            labelPlus7.AutoSize = true;
            labelPlus7.Font = new Font("Segoe UI", 16F);
            labelPlus7.Location = new Point(516, 76);
            labelPlus7.Name = "labelPlus7";
            labelPlus7.Size = new Size(40, 30);
            labelPlus7.TabIndex = 0;
            labelPlus7.Text = "+7";
            // 
            // lbTimer
            // 
            lbTimer.AutoSize = true;
            lbTimer.Font = new Font("Segoe UI", 25F);
            lbTimer.Location = new Point(836, 138);
            lbTimer.Name = "lbTimer";
            lbTimer.Size = new Size(109, 46);
            lbTimer.TabIndex = 20;
            lbTimer.Text = "label5";
            // 
            // lbHighScore
            // 
            lbHighScore.AutoSize = true;
            lbHighScore.Font = new Font("Segoe UI", 30F);
            lbHighScore.Location = new Point(101, 62);
            lbHighScore.Name = "lbHighScore";
            lbHighScore.Size = new Size(205, 54);
            lbHighScore.TabIndex = 42;
            lbHighScore.Text = "HighScore";
            // 
            // Form1
            // 
            ClientSize = new Size(1059, 772);
            Controls.Add(lbHighScore);
            Controls.Add(lbTimer);
            Controls.Add(lbFeedBack);
            Controls.Add(lbRandomNumber);
            Controls.Add(btnSubmit);
            Controls.Add(txtPlus1);
            Controls.Add(labelPlus1);
            Controls.Add(txtPlus10);
            Controls.Add(labelPlus10);
            Controls.Add(txtMinus1);
            Controls.Add(labelMinus1);
            Controls.Add(txtMinus10);
            Controls.Add(labelMinus10);
            Controls.Add(txtMinus3);
            Controls.Add(labelMinus3);
            Controls.Add(txtMinus7);
            Controls.Add(labelMinus7);
            Controls.Add(txtPlus3);
            Controls.Add(labelPlus3);
            Controls.Add(txtPlus7);
            Controls.Add(labelPlus7);
            Name = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
