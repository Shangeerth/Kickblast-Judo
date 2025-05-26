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
using System.Windows.Forms;
using static Programming_Assigment.Program;
using FontAwesome.Sharp;

namespace Programming_Assigment.Classes
{
    public partial class Dashboard : Form
    {

        private Form activeForm = null;
        private IconButton currentBtn;
        private Color highlightColor = Color.FromArgb(37, 36, 81);

        public Dashboard()
        {
            InitializeComponent();
            load();
          
            this.BackColor = Color.FromArgb(34, 33, 74);
            panel1.BackColor = Color.FromArgb(100, panel1.BackColor);
            DisableButton();

        }



        private void ActivateButton(object btnSender)
        {
            currentBtn = btnSender as IconButton;
            if (currentBtn != null)
            {
                currentBtn.BackColor = highlightColor;
                currentBtn.ForeColor = Color.White;
                currentBtn.IconColor = Color.White;
            }
        }


        private void DisableButton()
        {
            foreach (Control control in panel1.Controls)
            {
                IconButton btn = control as IconButton;
                if (btn != null)
                {
                    btn.BackColor = Color.FromArgb(31, 30, 68);
                    btn.ForeColor = Color.Gainsboro;
                    btn.IconColor = Color.Gainsboro;
                }
            }
        }

        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
                activeForm.Close();

            ActivateButton(btnSender);
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
    }
}
