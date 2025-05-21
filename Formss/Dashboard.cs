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

namespace Programming_Assigment.Classes
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            load();



           
        
            panel1.BackColor = Color.FromArgb(100, panel1.BackColor);

        }




        private void load()
        {
            label1.Text=DateTime.Now.ToString("dd/MM/yyyy");
            label2.Text = DateTime.Now.ToString("hh:mm tt");
        }
        private void Athlete_Click(object sender, EventArgs e)
        {
            Form1 newForm = new Form1();
            NavigationManager.OpenForm(this, newForm);
        }

        private void Trainer_Click(object sender, EventArgs e)
        {
            Trainer newForm = new Trainer();
            NavigationManager.OpenForm(this, newForm);
        }

        private void Private_Coaching_Click(object sender, EventArgs e)
        {
            Coaching newForm = new Coaching();
            NavigationManager.OpenForm(this, newForm);
        }

        private void Training_plan_Click(object sender, EventArgs e)
        {
            Athleteplanform newForm = new Athleteplanform();
            NavigationManager.OpenForm(this, newForm);
        }

        private void Plan_Click(object sender, EventArgs e)
        {
            TrainningPlan newForm = new TrainningPlan();
            NavigationManager.OpenForm(this, newForm);
        }

        private void Competition_Click(object sender, EventArgs e)
        {
            Competition newForm = new Competition();
            NavigationManager.OpenForm(this, newForm);

        }

        private void Athlete_Competition_Click(object sender, EventArgs e)
        {
            CompetitionAthlete newForm = new CompetitionAthlete();
            NavigationManager.OpenForm(this, newForm);
        }

        private void Weight_Click(object sender, EventArgs e)
        {
            Weightcategoryy newForm = new Weightcategoryy();
            NavigationManager.OpenForm(this, newForm);

        }

        private void Payment_Click(object sender, EventArgs e)
        {
            payment newForm = new payment();
            NavigationManager.OpenForm(this, newForm);

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
    }
}
