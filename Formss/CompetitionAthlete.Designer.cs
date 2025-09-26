namespace Programming_Assigment.Classes
{
    partial class CompetitionAthlete
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
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.comid = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Search = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Contact1 = new System.Windows.Forms.Label();
            this.Nic123 = new System.Windows.Forms.Label();
            this.Feee = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Logout = new System.Windows.Forms.Button();
            this.Back = new System.Windows.Forms.Button();
            this.Clear = new System.Windows.Forms.Button();
            this.Delete = new System.Windows.Forms.Button();
            this.Update = new System.Windows.Forms.Button();
            this.id = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Insert = new System.Windows.Forms.Button();
            this.timee = new System.Windows.Forms.TextBox();
            this.aid = new System.Windows.Forms.ComboBox();
            this.plan = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.athname = new System.Windows.Forms.TextBox();
            this.comname = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(734, 255);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker1.TabIndex = 171;
            // 
            // comid
            // 
            this.comid.FormattingEnabled = true;
            this.comid.Location = new System.Drawing.Point(248, 253);
            this.comid.Name = "comid";
            this.comid.Size = new System.Drawing.Size(151, 24);
            this.comid.TabIndex = 169;
            this.comid.SelectedIndexChanged += new System.EventHandler(this.cname_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(47, 319);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1025, 204);
            this.dataGridView1.TabIndex = 167;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Search
            // 
            this.Search.AutoSize = true;
            this.Search.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Search.ForeColor = System.Drawing.Color.Yellow;
            this.Search.Location = new System.Drawing.Point(829, 31);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(81, 25);
            this.Search.TabIndex = 166;
            this.Search.Text = "Search";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(921, 31);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(151, 22);
            this.textBox1.TabIndex = 165;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Yellow;
            this.label7.Location = new System.Drawing.Point(76, 249);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(127, 25);
            this.label7.TabIndex = 164;
            this.label7.Text = "Competition";
            // 
            // Contact1
            // 
            this.Contact1.AutoSize = true;
            this.Contact1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Contact1.ForeColor = System.Drawing.Color.Yellow;
            this.Contact1.Location = new System.Drawing.Point(646, 250);
            this.Contact1.Name = "Contact1";
            this.Contact1.Size = new System.Drawing.Size(57, 25);
            this.Contact1.TabIndex = 163;
            this.Contact1.Text = "Date";
            // 
            // Nic123
            // 
            this.Nic123.AutoSize = true;
            this.Nic123.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Nic123.ForeColor = System.Drawing.Color.Yellow;
            this.Nic123.Location = new System.Drawing.Point(664, 92);
            this.Nic123.Name = "Nic123";
            this.Nic123.Size = new System.Drawing.Size(49, 25);
            this.Nic123.TabIndex = 162;
            this.Nic123.Text = "Fee";
            // 
            // Feee
            // 
            this.Feee.Location = new System.Drawing.Point(744, 93);
            this.Feee.Name = "Feee";
            this.Feee.Size = new System.Drawing.Size(151, 22);
            this.Feee.TabIndex = 161;
            this.Feee.Text = "90.50";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Yellow;
            this.label3.Location = new System.Drawing.Point(653, 178);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 25);
            this.label3.TabIndex = 159;
            this.label3.Text = "Time";
            // 
            // Logout
            // 
            this.Logout.Location = new System.Drawing.Point(978, 568);
            this.Logout.Name = "Logout";
            this.Logout.Size = new System.Drawing.Size(94, 41);
            this.Logout.TabIndex = 158;
            this.Logout.Text = "Logout";
            this.Logout.UseVisualStyleBackColor = true;
            this.Logout.Click += new System.EventHandler(this.Logout_Click);
            // 
            // Back
            // 
            this.Back.Location = new System.Drawing.Point(877, 568);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(94, 41);
            this.Back.TabIndex = 157;
            this.Back.Text = "Back";
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(349, 547);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(75, 35);
            this.Clear.TabIndex = 156;
            this.Clear.Text = "Clear";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // Delete
            // 
            this.Delete.Location = new System.Drawing.Point(248, 547);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(75, 35);
            this.Delete.TabIndex = 155;
            this.Delete.Text = "Delete";
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // Update
            // 
            this.Update.Location = new System.Drawing.Point(148, 547);
            this.Update.Name = "Update";
            this.Update.Size = new System.Drawing.Size(75, 35);
            this.Update.TabIndex = 154;
            this.Update.Text = "Update";
            this.Update.UseVisualStyleBackColor = true;
            this.Update.Click += new System.EventHandler(this.Update_Click);
            // 
            // id
            // 
            this.id.FormattingEnabled = true;
            this.id.Location = new System.Drawing.Point(248, 61);
            this.id.Name = "id";
            this.id.Size = new System.Drawing.Size(151, 24);
            this.id.TabIndex = 153;
            this.id.SelectedIndexChanged += new System.EventHandler(this.id_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(76, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 25);
            this.label1.TabIndex = 152;
            this.label1.Text = "Entroll ID";
            // 
            // Insert
            // 
            this.Insert.Location = new System.Drawing.Point(47, 547);
            this.Insert.Name = "Insert";
            this.Insert.Size = new System.Drawing.Size(75, 35);
            this.Insert.TabIndex = 151;
            this.Insert.Text = "Insert";
            this.Insert.UseVisualStyleBackColor = true;
            this.Insert.Click += new System.EventHandler(this.Insert_Click);
            // 
            // timee
            // 
            this.timee.Location = new System.Drawing.Point(744, 178);
            this.timee.Name = "timee";
            this.timee.Size = new System.Drawing.Size(151, 22);
            this.timee.TabIndex = 176;
            // 
            // aid
            // 
            this.aid.FormattingEnabled = true;
            this.aid.Location = new System.Drawing.Point(247, 123);
            this.aid.Name = "aid";
            this.aid.Size = new System.Drawing.Size(151, 24);
            this.aid.TabIndex = 177;
            this.aid.SelectedIndexChanged += new System.EventHandler(this.aname_SelectedIndexChanged);
            // 
            // plan
            // 
            this.plan.FormattingEnabled = true;
            this.plan.Location = new System.Drawing.Point(248, 192);
            this.plan.Name = "plan";
            this.plan.Size = new System.Drawing.Size(151, 24);
            this.plan.TabIndex = 180;
            this.plan.SelectedIndexChanged += new System.EventHandler(this.plan_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(75, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 25);
            this.label2.TabIndex = 174;
            this.label2.Text = "Athlete Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Yellow;
            this.label4.Location = new System.Drawing.Point(76, 188);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 25);
            this.label4.TabIndex = 179;
            this.label4.Text = "Training Plan";
            // 
            // athname
            // 
            this.athname.Location = new System.Drawing.Point(419, 123);
            this.athname.Name = "athname";
            this.athname.Size = new System.Drawing.Size(151, 22);
            this.athname.TabIndex = 181;
            // 
            // comname
            // 
            this.comname.Location = new System.Drawing.Point(419, 253);
            this.comname.Name = "comname";
            this.comname.Size = new System.Drawing.Size(151, 22);
            this.comname.TabIndex = 182;
            this.comname.TextChanged += new System.EventHandler(this.comname_TextChanged);
            // 
            // CompetitionAthlete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(1131, 647);
            this.Controls.Add(this.comname);
            this.Controls.Add(this.athname);
            this.Controls.Add(this.plan);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.aid);
            this.Controls.Add(this.timee);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.comid);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.Search);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Contact1);
            this.Controls.Add(this.Nic123);
            this.Controls.Add(this.Feee);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Logout);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.Delete);
            this.Controls.Add(this.Update);
            this.Controls.Add(this.id);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Insert);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CompetitionAthlete";
            this.Text = "CompetitionAthlete";
            this.Load += new System.EventHandler(this.CompetitionAthlete_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ComboBox comid;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label Search;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label Contact1;
        private System.Windows.Forms.Label Nic123;
        private System.Windows.Forms.TextBox Feee;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Logout;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.Button Delete;
        private new System.Windows.Forms.Button Update;
        private System.Windows.Forms.ComboBox id;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Insert;
        private System.Windows.Forms.TextBox timee;
        private System.Windows.Forms.ComboBox aid;
        private System.Windows.Forms.ComboBox plan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox athname;
        private System.Windows.Forms.TextBox comname;
    }
}