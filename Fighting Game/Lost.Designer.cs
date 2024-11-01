namespace Lost
{
    partial class LostGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LostGame));
            Josuke = new PictureBox();
            text = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)Josuke).BeginInit();
            ((System.ComponentModel.ISupportInitialize)text).BeginInit();
            SuspendLayout();
            // 
            // Josuke
            // 
            Josuke.Image = (Image)resources.GetObject("Josuke.Image");
            Josuke.Location = new Point(1, -1);
            Josuke.Name = "Josuke";
            Josuke.Size = new Size(598, 309);
            Josuke.SizeMode = PictureBoxSizeMode.StretchImage;
            Josuke.TabIndex = 0;
            Josuke.TabStop = false;
            Josuke.Click += MouseClick;
            // 
            // text
            // 
            text.Image = (Image)resources.GetObject("text.Image");
            text.Location = new Point(1, 314);
            text.Name = "text";
            text.Size = new Size(598, 198);
            text.SizeMode = PictureBoxSizeMode.Zoom;
            text.TabIndex = 1;
            text.TabStop = false;
            // 
            // LostGame
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(600, 524);
            Controls.Add(text);
            Controls.Add(Josuke);
            Name = "LostGame";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Lost";
            ((System.ComponentModel.ISupportInitialize)Josuke).EndInit();
            ((System.ComponentModel.ISupportInitialize)text).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox Josuke;
        private PictureBox text;
        private PictureBox gameover;
    }
}
