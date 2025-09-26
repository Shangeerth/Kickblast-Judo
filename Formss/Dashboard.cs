using FontAwesome.Sharp;
using Programming_Assigment.Database;
using Programming_Assigment.Formss;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using static Programming_Assigment.Program;

namespace Programming_Assigment.Classes
{
    public partial class Dashboard : Form
    {

        private Form activeForm = null;

        public Dashboard()
        {
            InitializeComponent();
            load();
          



        }



        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
                activeForm.Close();

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panel2.Controls.Clear();
            panel2.Controls.Add(childForm);
            panel2.Tag = childForm;

            childForm.BringToFront();
            childForm.Show();
        }



        private void load()
        {
            label1.Text=DateTime.Now.ToString("dd/MM/yyyy");
            label2.Text = DateTime.Now.ToString("hh:mm tt");


            Color formBackColor = Color.FromArgb(34, 34, 34); // Charcoal Black

            Color buttonBackColor = Color.FromArgb(191, 167, 111);   // #BFA76F (Gold)
            Color buttonForeColor = Color.FromArgb(26, 26, 26);      // #1A1A1A (Dark)
            Color formTextColor = Color.FromArgb(230, 225, 210);     // #E6E1D2 (Ivory)

            this.panel2.BackColor = formBackColor;

            foreach (Control ctrl in panel2.Controls)
            {
                if (ctrl is Label lbl)
                {
                    lbl.ForeColor = formTextColor;
                }
            }
            this.ForeColor = formTextColor;

            Button[] buttons = new Button[]
{
                    iconButton1,
                    iconButton2,
                    iconButton3,
                    iconButton4,
                    iconButton6,
                    iconButton5,
                    iconButton7,
                    iconButton8,
                    iconButton9
                    
                };

            foreach (var btn in buttons)
            {
                btn.BackColor = buttonBackColor;
                btn.ForeColor = buttonForeColor;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
            }
        }
    
       

        private void button1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form1(), sender);
        }

        private void iconButton9_Click(object sender, EventArgs e)
        {
            OpenChildForm(new payment(), sender);
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Trainer(), sender);
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Coaching(), sender);
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Athleteplanform(), sender);
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            OpenChildForm(new TrainningPlan(), sender);
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Competition(), sender);
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            OpenChildForm(new CompetitionAthlete(), sender);
        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Weightcategoryy(), sender);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
