namespace WinFormsApp2
{
   public partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private Button btnSubmit;
        private TextBox txtPlus1;
        private TextBox txtMinus1;
        private TextBox txtPlus3;
        private Label lbRandomNumber;
        private TextBox txtMinus3;
        private Label lbFeedBack;
        private Label labelMinus1;
        private Label labelPlus1;
        private Label labelMinus3;
        private Label labelPlus3;
        private System.Windows.Forms.Timer Timer;
        private TextBox txtMinus10;
        private TextBox txtMinus7;
        private TextBox txtPlus7;
        private TextBox txtPlus10;
        private Label labelPlus10;
        private Label labelMinus10;
        private Label labelMinus7;
        private Label labelPlus7;
        private Label lbTimer;
        private Label lbHighScore;
    }
}
