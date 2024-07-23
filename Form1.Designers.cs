namespace SnakeGame
{
    partial class Form1
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
            this.gamepanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // gamepanel
            // 
            this.gamepanel.Location = new System.Drawing.Point(12, 18);
            this.gamepanel.Name = "gamepanel";
            this.gamepanel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.gamepanel.Size = new System.Drawing.Size(1200, 600);
            this.gamepanel.TabIndex = 0;
            this.gamepanel.Paint += new System.Windows.Forms.PaintEventHandler(this.gamepanel_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 628);
            this.Controls.Add(this.gamepanel);
            this.Location = new System.Drawing.Point(12, 18);
            this.Name = "Form1";
            this.Text = "snake";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel gamepanel;
    }
}

