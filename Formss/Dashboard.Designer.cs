namespace Programming_Assigment.Classes
{
    partial class Dashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            this.Athlete = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Payment = new System.Windows.Forms.Button();
            this.Weight = new System.Windows.Forms.Button();
            this.Athlete_Competition = new System.Windows.Forms.Button();
            this.Competition = new System.Windows.Forms.Button();
            this.Plan = new System.Windows.Forms.Button();
            this.Training_plan = new System.Windows.Forms.Button();
            this.Private_Coaching = new System.Windows.Forms.Button();
            this.Trainer = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Athlete
            // 
            this.Athlete.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Athlete.Location = new System.Drawing.Point(22, 42);
            this.Athlete.Name = "Athlete";
            this.Athlete.Size = new System.Drawing.Size(163, 34);
            this.Athlete.TabIndex = 7;
            this.Athlete.Text = "Athlete";
            this.Athlete.UseVisualStyleBackColor = true;
            this.Athlete.Click += new System.EventHandler(this.Athlete_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.Payment);
            this.panel1.Controls.Add(this.Weight);
            this.panel1.Controls.Add(this.Athlete_Competition);
            this.panel1.Controls.Add(this.Competition);
            this.panel1.Controls.Add(this.Plan);
            this.panel1.Controls.Add(this.Training_plan);
            this.panel1.Controls.Add(this.Private_Coaching);
            this.panel1.Controls.Add(this.Trainer);
            this.panel1.Controls.Add(this.Athlete);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(202, 549);
            this.panel1.TabIndex = 8;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // Payment
            // 
            this.Payment.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Payment.Location = new System.Drawing.Point(22, 495);
            this.Payment.Name = "Payment";
            this.Payment.Size = new System.Drawing.Size(163, 34);
            this.Payment.TabIndex = 16;
            this.Payment.Text = "Payment";
            this.Payment.UseVisualStyleBackColor = true;
            this.Payment.Click += new System.EventHandler(this.Payment_Click);
            // 
            // Weight
            // 
            this.Weight.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Weight.Location = new System.Drawing.Point(22, 435);
            this.Weight.Name = "Weight";
            this.Weight.Size = new System.Drawing.Size(163, 34);
            this.Weight.TabIndex = 14;
            this.Weight.Text = "Weight Category";
            this.Weight.UseVisualStyleBackColor = true;
            this.Weight.Click += new System.EventHandler(this.Weight_Click);
            // 
            // Athlete_Competition
            // 
            this.Athlete_Competition.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Athlete_Competition.Location = new System.Drawing.Point(22, 379);
            this.Athlete_Competition.Name = "Athlete_Competition";
            this.Athlete_Competition.Size = new System.Drawing.Size(163, 34);
            this.Athlete_Competition.TabIndex = 13;
            this.Athlete_Competition.Text = "Athlete Competition";
            this.Athlete_Competition.UseVisualStyleBackColor = true;
            this.Athlete_Competition.Click += new System.EventHandler(this.Athlete_Competition_Click);
            // 
            // Competition
            // 
            this.Competition.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Competition.Location = new System.Drawing.Point(22, 323);
            this.Competition.Name = "Competition";
            this.Competition.Size = new System.Drawing.Size(163, 34);
            this.Competition.TabIndex = 12;
            this.Competition.Text = "Competition";
            this.Competition.UseVisualStyleBackColor = true;
            this.Competition.Click += new System.EventHandler(this.Competition_Click);
            // 
            // Plan
            // 
            this.Plan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Plan.Location = new System.Drawing.Point(22, 267);
            this.Plan.Name = "Plan";
            this.Plan.Size = new System.Drawing.Size(163, 34);
            this.Plan.TabIndex = 11;
            this.Plan.Text = "Training Plan";
            this.Plan.UseVisualStyleBackColor = true;
            this.Plan.Click += new System.EventHandler(this.Plan_Click);
            // 
            // Training_plan
            // 
            this.Training_plan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Training_plan.Location = new System.Drawing.Point(22, 211);
            this.Training_plan.Name = "Training_plan";
            this.Training_plan.Size = new System.Drawing.Size(163, 34);
            this.Training_plan.TabIndex = 10;
            this.Training_plan.Text = "Athlete Training Plan";
            this.Training_plan.UseVisualStyleBackColor = true;
            this.Training_plan.Click += new System.EventHandler(this.Training_plan_Click);
            // 
            // Private_Coaching
            // 
            this.Private_Coaching.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Private_Coaching.Location = new System.Drawing.Point(22, 156);
            this.Private_Coaching.Name = "Private_Coaching";
            this.Private_Coaching.Size = new System.Drawing.Size(163, 34);
            this.Private_Coaching.TabIndex = 9;
            this.Private_Coaching.Text = "Private Coaching";
            this.Private_Coaching.UseVisualStyleBackColor = true;
            this.Private_Coaching.Click += new System.EventHandler(this.Private_Coaching_Click);
            // 
            // Trainer
            // 
            this.Trainer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Trainer.Location = new System.Drawing.Point(22, 101);
            this.Trainer.Name = "Trainer";
            this.Trainer.Size = new System.Drawing.Size(163, 34);
            this.Trainer.TabIndex = 8;
            this.Trainer.Text = "Trainer";
            this.Trainer.UseVisualStyleBackColor = true;
            this.Trainer.Click += new System.EventHandler(this.Trainer_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Red;
            this.button1.Location = new System.Drawing.Point(901, 495);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(143, 34);
            this.button1.TabIndex = 17;
            this.button1.Text = "Logout";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(915, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 25);
            this.label1.TabIndex = 18;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(915, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 25);
            this.label2.TabIndex = 19;
            this.label2.Text = "label2";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1056, 549);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(1056, 549);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Athlete;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Weight;
        private System.Windows.Forms.Button Athlete_Competition;
        private System.Windows.Forms.Button Competition;
        private System.Windows.Forms.Button Plan;
        private System.Windows.Forms.Button Training_plan;
        private System.Windows.Forms.Button Private_Coaching;
        private System.Windows.Forms.Button Trainer;
        private System.Windows.Forms.Button Payment;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}