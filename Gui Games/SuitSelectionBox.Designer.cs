namespace Gui_Games
{
    partial class SuitSelectionBox
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rClub = new System.Windows.Forms.RadioButton();
            this.rDiamonds = new System.Windows.Forms.RadioButton();
            this.rHearts = new System.Windows.Forms.RadioButton();
            this.rSpades = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rSpades);
            this.groupBox1.Controls.Add(this.rHearts);
            this.groupBox1.Controls.Add(this.rDiamonds);
            this.groupBox1.Controls.Add(this.rClub);
            this.groupBox1.Location = new System.Drawing.Point(25, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(127, 143);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose a suit...";
            // 
            // rClub
            // 
            this.rClub.AutoSize = true;
            this.rClub.Location = new System.Drawing.Point(7, 39);
            this.rClub.Name = "rClub";
            this.rClub.Size = new System.Drawing.Size(51, 17);
            this.rClub.TabIndex = 0;
            this.rClub.TabStop = true;
            this.rClub.Text = "Clubs";
            this.rClub.UseVisualStyleBackColor = true;
            this.rClub.CheckedChanged += new System.EventHandler(this.rClub_CheckedChanged);
            // 
            // rDiamonds
            // 
            this.rDiamonds.AutoSize = true;
            this.rDiamonds.Location = new System.Drawing.Point(7, 63);
            this.rDiamonds.Name = "rDiamonds";
            this.rDiamonds.Size = new System.Drawing.Size(72, 17);
            this.rDiamonds.TabIndex = 1;
            this.rDiamonds.TabStop = true;
            this.rDiamonds.Text = "Diamonds";
            this.rDiamonds.UseVisualStyleBackColor = true;
            this.rDiamonds.CheckedChanged += new System.EventHandler(this.rDiamonds_CheckedChanged);
            // 
            // rHearts
            // 
            this.rHearts.AutoSize = true;
            this.rHearts.Location = new System.Drawing.Point(7, 87);
            this.rHearts.Name = "rHearts";
            this.rHearts.Size = new System.Drawing.Size(56, 17);
            this.rHearts.TabIndex = 2;
            this.rHearts.TabStop = true;
            this.rHearts.Text = "Hearts";
            this.rHearts.UseVisualStyleBackColor = true;
            this.rHearts.CheckedChanged += new System.EventHandler(this.rHearts_CheckedChanged);
            // 
            // rSpades
            // 
            this.rSpades.AutoSize = true;
            this.rSpades.Location = new System.Drawing.Point(7, 111);
            this.rSpades.Name = "rSpades";
            this.rSpades.Size = new System.Drawing.Size(61, 17);
            this.rSpades.TabIndex = 3;
            this.rSpades.TabStop = true;
            this.rSpades.Text = "Spades";
            this.rSpades.UseVisualStyleBackColor = true;
            this.rSpades.CheckedChanged += new System.EventHandler(this.rSpades_CheckedChanged);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(52, 202);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // suitSelectionBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(198, 261);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.Name = "suitSelectionBox";
            this.Text = "Suit...";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rSpades;
        private System.Windows.Forms.RadioButton rHearts;
        private System.Windows.Forms.RadioButton rDiamonds;
        private System.Windows.Forms.RadioButton rClub;
        private System.Windows.Forms.Button btnOK;
    }
}