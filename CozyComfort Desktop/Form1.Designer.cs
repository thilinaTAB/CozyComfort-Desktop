namespace CozyComfort_Desktop
{
    partial class CozyComfort
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.blanketModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.materialInventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.blanketModelToolStripMenuItem,
            this.materialInventoryToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // blanketModelToolStripMenuItem
            // 
            this.blanketModelToolStripMenuItem.Name = "blanketModelToolStripMenuItem";
            this.blanketModelToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.blanketModelToolStripMenuItem.Text = "Blanket Model";
            this.blanketModelToolStripMenuItem.Click += new System.EventHandler(this.blanketModelToolStripMenuItem_Click);
            // 
            // materialInventoryToolStripMenuItem
            // 
            this.materialInventoryToolStripMenuItem.Name = "materialInventoryToolStripMenuItem";
            this.materialInventoryToolStripMenuItem.Size = new System.Drawing.Size(115, 20);
            this.materialInventoryToolStripMenuItem.Text = "Material inventory";
            // 
            // CozyComfort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CozyComfort";
            this.Text = "Cozy Comfort";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem blanketModelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem materialInventoryToolStripMenuItem;
    }
}

