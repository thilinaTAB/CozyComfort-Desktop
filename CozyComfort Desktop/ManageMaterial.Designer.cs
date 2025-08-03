namespace CozyComfort_Desktop
{
    partial class ManageMaterial
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
            this.btnMaterialViewAll = new System.Windows.Forms.Button();
            this.btnMaterialFind = new System.Windows.Forms.Button();
            this.txtMaterialID = new System.Windows.Forms.TextBox();
            this.lbl = new System.Windows.Forms.Label();
            this.dgvMaterial = new System.Windows.Forms.DataGridView();
            this.Blanket = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lblMaterialID = new System.Windows.Forms.Label();
            this.btnMaterialDelete = new System.Windows.Forms.Button();
            this.btnMaterialUpdate = new System.Windows.Forms.Button();
            this.btnMaterialAdd = new System.Windows.Forms.Button();
            this.txtMaterialDes = new System.Windows.Forms.TextBox();
            this.txtMaterialName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterial)).BeginInit();
            this.SuspendLayout();
            // 
            // btnMaterialViewAll
            // 
            this.btnMaterialViewAll.Location = new System.Drawing.Point(971, 34);
            this.btnMaterialViewAll.Name = "btnMaterialViewAll";
            this.btnMaterialViewAll.Size = new System.Drawing.Size(75, 23);
            this.btnMaterialViewAll.TabIndex = 53;
            this.btnMaterialViewAll.Text = "View All";
            this.btnMaterialViewAll.UseVisualStyleBackColor = true;
            this.btnMaterialViewAll.Click += new System.EventHandler(this.btnMaterialViewAll_Click);
            // 
            // btnMaterialFind
            // 
            this.btnMaterialFind.Location = new System.Drawing.Point(890, 34);
            this.btnMaterialFind.Name = "btnMaterialFind";
            this.btnMaterialFind.Size = new System.Drawing.Size(75, 23);
            this.btnMaterialFind.TabIndex = 54;
            this.btnMaterialFind.Text = "Find";
            this.btnMaterialFind.UseVisualStyleBackColor = true;
            this.btnMaterialFind.Click += new System.EventHandler(this.btnMaterialFind_Click);
            // 
            // txtMaterialID
            // 
            this.txtMaterialID.Location = new System.Drawing.Point(837, 36);
            this.txtMaterialID.Name = "txtMaterialID";
            this.txtMaterialID.Size = new System.Drawing.Size(47, 20);
            this.txtMaterialID.TabIndex = 52;
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(800, 39);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(18, 13);
            this.lbl.TabIndex = 51;
            this.lbl.Text = "ID";
            // 
            // dgvMaterial
            // 
            this.dgvMaterial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaterial.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Blanket});
            this.dgvMaterial.Location = new System.Drawing.Point(312, 62);
            this.dgvMaterial.Name = "dgvMaterial";
            this.dgvMaterial.Size = new System.Drawing.Size(744, 310);
            this.dgvMaterial.TabIndex = 50;
            this.dgvMaterial.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMaterial_CellContentClick);
            // 
            // Blanket
            // 
            this.Blanket.HeaderText = "Edit";
            this.Blanket.Name = "Blanket";
            // 
            // lblMaterialID
            // 
            this.lblMaterialID.AutoSize = true;
            this.lblMaterialID.Location = new System.Drawing.Point(276, 39);
            this.lblMaterialID.Name = "lblMaterialID";
            this.lblMaterialID.Size = new System.Drawing.Size(0, 13);
            this.lblMaterialID.TabIndex = 49;
            this.lblMaterialID.Visible = false;
            // 
            // btnMaterialDelete
            // 
            this.btnMaterialDelete.Location = new System.Drawing.Point(208, 218);
            this.btnMaterialDelete.Name = "btnMaterialDelete";
            this.btnMaterialDelete.Size = new System.Drawing.Size(75, 23);
            this.btnMaterialDelete.TabIndex = 48;
            this.btnMaterialDelete.Text = "Delete";
            this.btnMaterialDelete.UseVisualStyleBackColor = true;
            this.btnMaterialDelete.Click += new System.EventHandler(this.btnMaterialDelete_Click);
            // 
            // btnMaterialUpdate
            // 
            this.btnMaterialUpdate.Location = new System.Drawing.Point(127, 218);
            this.btnMaterialUpdate.Name = "btnMaterialUpdate";
            this.btnMaterialUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnMaterialUpdate.TabIndex = 47;
            this.btnMaterialUpdate.Text = "Update";
            this.btnMaterialUpdate.UseVisualStyleBackColor = true;
            this.btnMaterialUpdate.Click += new System.EventHandler(this.btnMaterialUpdate_Click);
            // 
            // btnMaterialAdd
            // 
            this.btnMaterialAdd.Location = new System.Drawing.Point(46, 218);
            this.btnMaterialAdd.Name = "btnMaterialAdd";
            this.btnMaterialAdd.Size = new System.Drawing.Size(75, 23);
            this.btnMaterialAdd.TabIndex = 46;
            this.btnMaterialAdd.Text = "Add";
            this.btnMaterialAdd.UseVisualStyleBackColor = true;
            this.btnMaterialAdd.Click += new System.EventHandler(this.btnMaterialAdd_Click);
            // 
            // txtMaterialDes
            // 
            this.txtMaterialDes.Location = new System.Drawing.Point(107, 112);
            this.txtMaterialDes.Multiline = true;
            this.txtMaterialDes.Name = "txtMaterialDes";
            this.txtMaterialDes.Size = new System.Drawing.Size(176, 70);
            this.txtMaterialDes.TabIndex = 42;
            // 
            // txtMaterialName
            // 
            this.txtMaterialName.Location = new System.Drawing.Point(107, 62);
            this.txtMaterialName.Name = "txtMaterialName";
            this.txtMaterialName.Size = new System.Drawing.Size(176, 20);
            this.txtMaterialName.TabIndex = 41;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 39;
            this.label5.Text = "Description";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Material Name";
            // 
            // ManageMaterial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1324, 450);
            this.Controls.Add(this.btnMaterialViewAll);
            this.Controls.Add(this.btnMaterialFind);
            this.Controls.Add(this.txtMaterialID);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.dgvMaterial);
            this.Controls.Add(this.lblMaterialID);
            this.Controls.Add(this.btnMaterialDelete);
            this.Controls.Add(this.btnMaterialUpdate);
            this.Controls.Add(this.btnMaterialAdd);
            this.Controls.Add(this.txtMaterialDes);
            this.Controls.Add(this.txtMaterialName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Name = "ManageMaterial";
            this.Text = "Manage Material";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ManageMaterial_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterial)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnMaterialViewAll;
        private System.Windows.Forms.Button btnMaterialFind;
        private System.Windows.Forms.TextBox txtMaterialID;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.DataGridView dgvMaterial;
        private System.Windows.Forms.DataGridViewButtonColumn Blanket;
        private System.Windows.Forms.Label lblMaterialID;
        private System.Windows.Forms.Button btnMaterialDelete;
        private System.Windows.Forms.Button btnMaterialUpdate;
        private System.Windows.Forms.Button btnMaterialAdd;
        private System.Windows.Forms.TextBox txtMaterialDes;
        private System.Windows.Forms.TextBox txtMaterialName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
    }
}