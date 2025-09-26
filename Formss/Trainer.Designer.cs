namespace Programming_Assigment.Database
{
    partial class Trainer
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Search = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.exp = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Quali = new System.Windows.Forms.TextBox();
            this.Contact1 = new System.Windows.Forms.Label();
            this.Contact12 = new System.Windows.Forms.TextBox();
            this.Nic123 = new System.Windows.Forms.Label();
            this.NIC1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.agee = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Logout = new System.Windows.Forms.Button();
            this.Back = new System.Windows.Forms.Button();
            this.Clear = new System.Windows.Forms.Button();
            this.Delete = new System.Windows.Forms.Button();
            this.Update = new System.Windows.Forms.Button();
            this.id = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.Insert = new System.Windows.Forms.Button();
            this.Salary123 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(35, 319);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1064, 205);
            this.dataGridView1.TabIndex = 115;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Search
            // 
            this.Search.AutoSize = true;
            this.Search.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Search.ForeColor = System.Drawing.Color.Yellow;
            this.Search.Location = new System.Drawing.Point(861, 24);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(81, 25);
            this.Search.TabIndex = 114;
            this.Search.Text = "Search";
            this.Search.Click += new System.EventHandler(this.Search_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(948, 24);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(151, 22);
            this.textBox1.TabIndex = 113;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Yellow;
            this.label9.Location = new System.Drawing.Point(637, 269);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 25);
            this.label9.TabIndex = 111;
            this.label9.Text = "Salary";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Yellow;
            this.label8.Location = new System.Drawing.Point(136, 250);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(120, 25);
            this.label8.TabIndex = 110;
            this.label8.Text = "Experience";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // exp
            // 
            this.exp.Location = new System.Drawing.Point(277, 254);
            this.exp.Name = "exp";
            this.exp.Size = new System.Drawing.Size(151, 22);
            this.exp.TabIndex = 109;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Yellow;
            this.label7.Location = new System.Drawing.Point(573, 202);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 25);
            this.label7.TabIndex = 108;
            this.label7.Text = "Qualification ";
            // 
            // Quali
            // 
            this.Quali.Location = new System.Drawing.Point(742, 202);
            this.Quali.Name = "Quali";
            this.Quali.Size = new System.Drawing.Size(151, 22);
            this.Quali.TabIndex = 107;
            // 
            // Contact1
            // 
            this.Contact1.AutoSize = true;
            this.Contact1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Contact1.ForeColor = System.Drawing.Color.Yellow;
            this.Contact1.Location = new System.Drawing.Point(624, 139);
            this.Contact1.Name = "Contact1";
            this.Contact1.Size = new System.Drawing.Size(87, 25);
            this.Contact1.TabIndex = 106;
            this.Contact1.Text = "Contact";
            // 
            // Contact12
            // 
            this.Contact12.Location = new System.Drawing.Point(742, 139);
            this.Contact12.Name = "Contact12";
            this.Contact12.Size = new System.Drawing.Size(151, 22);
            this.Contact12.TabIndex = 105;
            // 
            // Nic123
            // 
            this.Nic123.AutoSize = true;
            this.Nic123.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Nic123.ForeColor = System.Drawing.Color.Yellow;
            this.Nic123.Location = new System.Drawing.Point(662, 74);
            this.Nic123.Name = "Nic123";
            this.Nic123.Size = new System.Drawing.Size(49, 25);
            this.Nic123.TabIndex = 104;
            this.Nic123.Text = "NIC";
            // 
            // NIC1
            // 
            this.NIC1.Location = new System.Drawing.Point(742, 75);
            this.NIC1.Name = "NIC1";
            this.NIC1.Size = new System.Drawing.Size(151, 22);
            this.NIC1.TabIndex = 103;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Yellow;
            this.label4.Location = new System.Drawing.Point(176, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 25);
            this.label4.TabIndex = 102;
            this.label4.Text = "Age";
            // 
            // agee
            // 
            this.agee.Location = new System.Drawing.Point(277, 183);
            this.agee.Name = "agee";
            this.agee.Size = new System.Drawing.Size(151, 22);
            this.agee.TabIndex = 101;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Yellow;
            this.label3.Location = new System.Drawing.Point(188, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 25);
            this.label3.TabIndex = 100;
            this.label3.Text = "Name";
            // 
            // Logout
            // 
            this.Logout.Location = new System.Drawing.Point(1005, 579);
            this.Logout.Name = "Logout";
            this.Logout.Size = new System.Drawing.Size(94, 40);
            this.Logout.TabIndex = 99;
            this.Logout.Text = "Logout";
            this.Logout.UseVisualStyleBackColor = true;
            this.Logout.Click += new System.EventHandler(this.Logout_Click);
            // 
            // Back
            // 
            this.Back.Location = new System.Drawing.Point(887, 579);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(94, 40);
            this.Back.TabIndex = 98;
            this.Back.Text = "Back";
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(360, 545);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(89, 40);
            this.Clear.TabIndex = 97;
            this.Clear.Text = "Clear";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // Delete
            // 
            this.Delete.Location = new System.Drawing.Point(250, 545);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(89, 40);
            this.Delete.TabIndex = 96;
            this.Delete.Text = "Delete";
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // Update
            // 
            this.Update.Location = new System.Drawing.Point(144, 545);
            this.Update.Name = "Update";
            this.Update.Size = new System.Drawing.Size(89, 40);
            this.Update.TabIndex = 95;
            this.Update.Text = "Update";
            this.Update.UseVisualStyleBackColor = true;
            this.Update.Click += new System.EventHandler(this.Update_Click);
            // 
            // id
            // 
            this.id.FormattingEnabled = true;
            this.id.Location = new System.Drawing.Point(277, 39);
            this.id.Name = "id";
            this.id.Size = new System.Drawing.Size(151, 24);
            this.id.TabIndex = 94;
            this.id.SelectedIndexChanged += new System.EventHandler(this.id_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(149, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 25);
            this.label1.TabIndex = 93;
            this.label1.Text = "Trainer ID";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(277, 112);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(151, 22);
            this.name.TabIndex = 92;
            // 
            // Insert
            // 
            this.Insert.Location = new System.Drawing.Point(35, 545);
            this.Insert.Name = "Insert";
            this.Insert.Size = new System.Drawing.Size(89, 40);
            this.Insert.TabIndex = 91;
            this.Insert.Text = "Insert";
            this.Insert.UseVisualStyleBackColor = true;
            this.Insert.Click += new System.EventHandler(this.Insert_Click);
            // 
            // Salary123
            // 
            this.Salary123.Location = new System.Drawing.Point(742, 274);
            this.Salary123.Name = "Salary123";
            this.Salary123.Size = new System.Drawing.Size(151, 22);
            this.Salary123.TabIndex = 116;
            // 
            // Trainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(1131, 647);
            this.Controls.Add(this.Salary123);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.Search);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.exp);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Quali);
            this.Controls.Add(this.Contact1);
            this.Controls.Add(this.Contact12);
            this.Controls.Add(this.Nic123);
            this.Controls.Add(this.NIC1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.agee);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Logout);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.Delete);
            this.Controls.Add(this.Update);
            this.Controls.Add(this.id);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.name);
            this.Controls.Add(this.Insert);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Trainer";
            this.Text = "Trainer";
            this.Load += new System.EventHandler(this.Trainer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label Search;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox exp;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox Quali;
        private System.Windows.Forms.Label Contact1;
        private System.Windows.Forms.TextBox Contact12;
        private System.Windows.Forms.Label Nic123;
        private System.Windows.Forms.TextBox NIC1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox agee;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Logout;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.Button Delete;
        private new System.Windows.Forms.Button Update;
        private System.Windows.Forms.ComboBox id;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Button Insert;
        private System.Windows.Forms.TextBox Salary123;
    }
}