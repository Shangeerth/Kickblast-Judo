namespace Programming_Assigment.Classes
{
    partial class TrainningPlan
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
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.aname = new System.Windows.Forms.TextBox();
            this.session = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Search = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Nic123 = new System.Windows.Forms.Label();
            this.Feee = new System.Windows.Forms.TextBox();
            this.Logout = new System.Windows.Forms.Button();
            this.Back = new System.Windows.Forms.Button();
            this.Clear = new System.Windows.Forms.Button();
            this.Delete = new System.Windows.Forms.Button();
            this.Update = new System.Windows.Forms.Button();
            this.id = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Insert = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Yellow;
            this.label5.Location = new System.Drawing.Point(535, 216);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(190, 25);
            this.label5.TabIndex = 175;
            this.label5.Text = "SessionsPerWeek";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(143, 213);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 25);
            this.label2.TabIndex = 174;
            this.label2.Text = "Name";
            // 
            // aname
            // 
            this.aname.Location = new System.Drawing.Point(250, 216);
            this.aname.Name = "aname";
            this.aname.Size = new System.Drawing.Size(151, 22);
            this.aname.TabIndex = 173;
            // 
            // session
            // 
            this.session.Location = new System.Drawing.Point(761, 231);
            this.session.Name = "session";
            this.session.Size = new System.Drawing.Size(151, 22);
            this.session.TabIndex = 172;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(28, 289);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1065, 222);
            this.dataGridView1.TabIndex = 167;
            // 
            // Search
            // 
            this.Search.AutoSize = true;
            this.Search.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Search.ForeColor = System.Drawing.Color.Yellow;
            this.Search.Location = new System.Drawing.Point(854, 33);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(81, 25);
            this.Search.TabIndex = 166;
            this.Search.Text = "Search";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(942, 33);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(151, 22);
            this.textBox1.TabIndex = 165;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Nic123
            // 
            this.Nic123.AutoSize = true;
            this.Nic123.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Nic123.ForeColor = System.Drawing.Color.Yellow;
            this.Nic123.Location = new System.Drawing.Point(604, 109);
            this.Nic123.Name = "Nic123";
            this.Nic123.Size = new System.Drawing.Size(121, 25);
            this.Nic123.TabIndex = 162;
            this.Nic123.Text = "WeeklyFee";
            // 
            // Feee
            // 
            this.Feee.Location = new System.Drawing.Point(761, 112);
            this.Feee.Name = "Feee";
            this.Feee.Size = new System.Drawing.Size(151, 22);
            this.Feee.TabIndex = 161;
            this.Feee.Text = "90.50";
            // 
            // Logout
            // 
            this.Logout.Location = new System.Drawing.Point(995, 578);
            this.Logout.Name = "Logout";
            this.Logout.Size = new System.Drawing.Size(98, 40);
            this.Logout.TabIndex = 158;
            this.Logout.Text = "Logout";
            this.Logout.UseVisualStyleBackColor = true;
            this.Logout.Click += new System.EventHandler(this.Logout_Click);
            // 
            // Back
            // 
            this.Back.Location = new System.Drawing.Point(863, 578);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(98, 40);
            this.Back.TabIndex = 157;
            this.Back.Text = "Back";
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(326, 543);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(75, 42);
            this.Clear.TabIndex = 156;
            this.Clear.Text = "Clear";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // Delete
            // 
            this.Delete.Location = new System.Drawing.Point(231, 543);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(75, 42);
            this.Delete.TabIndex = 155;
            this.Delete.Text = "Delete";
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // Update
            // 
            this.Update.Location = new System.Drawing.Point(136, 543);
            this.Update.Name = "Update";
            this.Update.Size = new System.Drawing.Size(75, 42);
            this.Update.TabIndex = 154;
            this.Update.Text = "Update";
            this.Update.UseVisualStyleBackColor = true;
            this.Update.Click += new System.EventHandler(this.Update_Click);
            // 
            // id
            // 
            this.id.FormattingEnabled = true;
            this.id.Location = new System.Drawing.Point(250, 113);
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
            this.label1.Location = new System.Drawing.Point(146, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 25);
            this.label1.TabIndex = 152;
            this.label1.Text = "Plan ID";
            // 
            // Insert
            // 
            this.Insert.Location = new System.Drawing.Point(39, 543);
            this.Insert.Name = "Insert";
            this.Insert.Size = new System.Drawing.Size(75, 42);
            this.Insert.TabIndex = 151;
            this.Insert.Text = "Insert";
            this.Insert.UseVisualStyleBackColor = true;
            this.Insert.Click += new System.EventHandler(this.Insert_Click);
            // 
            // TrainningPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(1131, 647);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.aname);
            this.Controls.Add(this.session);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.Search);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Nic123);
            this.Controls.Add(this.Feee);
            this.Controls.Add(this.Logout);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.Delete);
            this.Controls.Add(this.Update);
            this.Controls.Add(this.id);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Insert);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TrainningPlan";
            this.Text = "TrainningPlan";
            this.Load += new System.EventHandler(this.TrainningPlan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox aname;
        private System.Windows.Forms.TextBox session;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label Search;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label Nic123;
        private System.Windows.Forms.TextBox Feee;
        private System.Windows.Forms.Button Logout;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.Button Delete;
        private new System.Windows.Forms.Button Update;
        private System.Windows.Forms.ComboBox id;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Insert;
    }
}