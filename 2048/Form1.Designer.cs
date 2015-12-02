namespace WindowsFormsApplication1
{
    partial class game2048
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cheatBox = new System.Windows.Forms.TextBox();
            this.Back = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe Print", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "Score :";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe Print", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(148, 16);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(45, 54);
            this.label2.TabIndex = 1;
            this.label2.Text = "0";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // cheatBox
            // 
            this.cheatBox.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.cheatBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.cheatBox.Location = new System.Drawing.Point(0, 0);
            this.cheatBox.Name = "cheatBox";
            this.cheatBox.Size = new System.Drawing.Size(183, 22);
            this.cheatBox.TabIndex = 2;
            this.cheatBox.Visible = false;
            this.cheatBox.VisibleChanged += new System.EventHandler(this.cheatBox_VisibleChanged);
            this.cheatBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cheatBox_KeyDown);
            // 
            // Back
            // 
            this.Back.AutoSize = true;
            this.Back.BackColor = System.Drawing.Color.Transparent;
            this.Back.Dock = System.Windows.Forms.DockStyle.Right;
            this.Back.Font = new System.Drawing.Font("Segoe Print", 19F, System.Drawing.FontStyle.Bold);
            this.Back.ForeColor = System.Drawing.Color.Silver;
            this.Back.Location = new System.Drawing.Point(680, 0);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(102, 57);
            this.Back.TabIndex = 3;
            this.Back.Text = "Back";
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // game2048
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(782, 820);
            this.ControlBox = false;
            this.Controls.Add(this.Back);
            this.Controls.Add(this.cheatBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "game2048";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "2048";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.game2048_FormClosed);
            this.Load += new System.EventHandler(this.game2048_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.game2048_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.game2048_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.game2048_KeyUp);
            this.Resize += new System.EventHandler(this.game2048_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox cheatBox;
        private System.Windows.Forms.Label Back;

    }
}

