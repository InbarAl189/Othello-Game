namespace Ex05.WindowsFormsUI
{
    public partial class FormGameSettings
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
            this.buttonBoardSize = new System.Windows.Forms.Button();
            this.buttonPlayAgainstComputer = new System.Windows.Forms.Button();
            this.buttonPlayAgainstFriend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonBoardSize
            // 
            this.buttonBoardSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBoardSize.Location = new System.Drawing.Point(15, 25);
            this.buttonBoardSize.Name = "buttonBoardSize";
            this.buttonBoardSize.Size = new System.Drawing.Size(654, 81);
            this.buttonBoardSize.TabIndex = 0;
            this.buttonBoardSize.Text = "Board Size: 6x6 (click to increase)";
            this.buttonBoardSize.UseVisualStyleBackColor = true;
            this.buttonBoardSize.Click += new System.EventHandler(this.buttonBoardSize_Click);
            // 
            // buttonPlayAgainstComputer
            // 
            this.buttonPlayAgainstComputer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPlayAgainstComputer.Location = new System.Drawing.Point(15, 125);
            this.buttonPlayAgainstComputer.Name = "buttonPlayAgainstComputer";
            this.buttonPlayAgainstComputer.Size = new System.Drawing.Size(327, 71);
            this.buttonPlayAgainstComputer.TabIndex = 1;
            this.buttonPlayAgainstComputer.Text = "Play against the computer";
            this.buttonPlayAgainstComputer.UseVisualStyleBackColor = true;
            this.buttonPlayAgainstComputer.Click += new System.EventHandler(this.buttonPlayAgainstComputer_Click);
            // 
            // buttonPlayAgainstFriend
            // 
            this.buttonPlayAgainstFriend.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPlayAgainstFriend.Location = new System.Drawing.Point(357, 125);
            this.buttonPlayAgainstFriend.Name = "buttonPlayAgainstFriend";
            this.buttonPlayAgainstFriend.Size = new System.Drawing.Size(312, 71);
            this.buttonPlayAgainstFriend.TabIndex = 2;
            this.buttonPlayAgainstFriend.Text = "Play against your friend";
            this.buttonPlayAgainstFriend.UseVisualStyleBackColor = true;
            this.buttonPlayAgainstFriend.Click += new System.EventHandler(this.buttonPlayAgainstFriend_Click);
            // 
            // FormGameSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 220);
            this.Controls.Add(this.buttonPlayAgainstFriend);
            this.Controls.Add(this.buttonPlayAgainstComputer);
            this.Controls.Add(this.buttonBoardSize);
            this.MaximizeBox = false;
            this.Name = "FormGameSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Othello - Game Settings";
            this.Load += new System.EventHandler(this.formGameSettings_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonBoardSize;
        private System.Windows.Forms.Button buttonPlayAgainstComputer;
        private System.Windows.Forms.Button buttonPlayAgainstFriend;
    }
}