namespace Fighting_Game
{
    partial class buttons
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Attack = new Button();
            GeHurt = new Button();
            SuspendLayout();
            // 
            // Attack
            // 
            Attack.Location = new Point(12, 12);
            Attack.Name = "Attack";
            Attack.Size = new Size(184, 154);
            Attack.TabIndex = 0;
            Attack.Text = "Attack";
            Attack.UseVisualStyleBackColor = true;
            Attack.Click += Attack_Click;
            // 
            // GeHurt
            // 
            GeHurt.Location = new Point(202, 12);
            GeHurt.Name = "GeHurt";
            GeHurt.Size = new Size(184, 154);
            GeHurt.TabIndex = 1;
            GeHurt.Text = "GetHurt";
            GeHurt.UseVisualStyleBackColor = true;
            GeHurt.Click += GeHurt_Click;
            // 
            // buttons
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(396, 174);
            Controls.Add(GeHurt);
            Controls.Add(Attack);
            Name = "buttons";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "buttons";
            ResumeLayout(false);
        }

        #endregion

        private Button Attack;
        private Button GeHurt;
    }
}