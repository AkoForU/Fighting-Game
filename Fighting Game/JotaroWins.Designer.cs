namespace Winner
{
    partial class JotaroWinning
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JotaroWinning));
            WinningText = new PictureBox();
            Jotaro = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)WinningText).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Jotaro).BeginInit();
            SuspendLayout();
            // 
            // WinningText
            // 
            WinningText.Image = (Image)resources.GetObject("WinningText.Image");
            WinningText.Location = new Point(-44, -200);
            WinningText.Name = "WinningText";
            WinningText.Size = new Size(449, 201);
            WinningText.SizeMode = PictureBoxSizeMode.Zoom;
            WinningText.TabIndex = 0;
            WinningText.TabStop = false;
            // 
            // Jotaro
            // 
            Jotaro.Image = (Image)resources.GetObject("Jotaro.Image");
            Jotaro.Location = new Point(-320, 130);
            Jotaro.Name = "Jotaro";
            Jotaro.Size = new Size(495, 371);
            Jotaro.SizeMode = PictureBoxSizeMode.Zoom;
            Jotaro.TabIndex = 1;
            Jotaro.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowText;
            ClientSize = new Size(364, 446);
            Controls.Add(Jotaro);
            Controls.Add(WinningText);
            ForeColor = SystemColors.WindowText;
            Name = "Form1";
            Text = "Winner";
            ((System.ComponentModel.ISupportInitialize)WinningText).EndInit();
            ((System.ComponentModel.ISupportInitialize)Jotaro).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox WinningText;
        private PictureBox Jotaro;
    }
}
