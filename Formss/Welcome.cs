using Programming_Assigment.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Programming_Assigment.Formss
{
    public partial class Welcome : Form
    {

        private Timer fadeTimer = new Timer();
        public Welcome()
        {
            InitializeComponent();


            progressBar1.Value = 0;
            progressBar1.Maximum = 100;

            timer1.Interval = 60; // 100 ms interval = 10 sec total
            timer1.Start();

            this.Opacity = 1.0;

            fadeTimer.Interval = 50;
            fadeTimer.Tick += FadeTimer_Tick;

            this.SetStyle(ControlStyles.UserPaint, true);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ForeColor = Color.OrangeRed; // Custom color


        }
        
        

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < progressBar1.Maximum)
            {
                progressBar1.Value += 1;
                progressBar1.Text = progressBar1.Value + " %";
            }
            else
            {
                timer1.Stop();
                fadeTimer.Start(); // Begin fade out
            }
        }


        private void FadeTimer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0)
            {
                this.Opacity -= 0.05;
            }
            else
            {
                fadeTimer.Stop();
                this.Hide();
                Login loginForm = new Login();
                loginForm.Show();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }





}
