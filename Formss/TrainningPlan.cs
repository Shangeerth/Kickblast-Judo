using Programming_Assigment.Database;
using Programming_Assigment.Formss;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static Programming_Assigment.Program;
using static System.Windows.Forms.LinkLabel;

namespace Programming_Assigment.Classes
{
    public partial class TrainningPlan : Form
    {
        private readonly planclz link;
        public TrainningPlan()
        {
            InitializeComponent();
            link = new planclz (new Sql());
            load();
            clear();
            dataGridView1.DataSource=link.GetPlans();


            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;


        }

        private void Insert_Click(object sender, EventArgs e)
        {
            if (id != null && id.SelectedIndex != -1)
            {
                MessageBox.Show("Please clear the form before inserting a new plan.");
                id.SelectedIndex = -1;
                return;
            }

            if (string.IsNullOrWhiteSpace(aname.Text) ||
                string.IsNullOrWhiteSpace(Feee.Text) ||
                string.IsNullOrWhiteSpace(session.Text))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            try
            {


                string namePattern = @"^[a-zA-Z\s]+$";
                if (!Regex.IsMatch(aname.Text.Trim(), namePattern))
                {
                    MessageBox.Show("Please enter a valid name containing only letters and spaces.");
                    return;
                }

                if (link.IsPlanNameUnique(namePattern))
                {
                    MessageBox.Show("This plan name already exists. Please use a different name.", "Duplicate Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }



                if (!decimal.TryParse(Feee.Text.Trim(), out decimal weeklyFee) || weeklyFee <= 0)
                {
                    MessageBox.Show("Please enter a valid weekly fee.");
                    return;
                }

                if (!int.TryParse(session.Text.Trim(), out int sessionsPerWeek) || sessionsPerWeek <= 0 || sessionsPerWeek > 7)
                {
                    MessageBox.Show("Please enter a valid number of sessions per week (1 to 7).");
                    return;
                }

              

                bool insertResult = link.InsertPlan(namePattern, weeklyFee, sessionsPerWeek);

                if (insertResult)
                {
                    MessageBox.Show("Plan added successfully.");
                    id.SelectedIndex = -1;
                    dataGridView1.DataSource = link.GetPlans();
                    load();
                   clear();
                }
                else
                {
                    MessageBox.Show("Failed to add plan.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }

        private void Update_Click(object sender, EventArgs e)
        {
            if (id == null || id.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a plan to update.");
                return;
            }

            if (string.IsNullOrWhiteSpace(aname.Text) ||
                string.IsNullOrWhiteSpace(Feee.Text) ||
                string.IsNullOrWhiteSpace(session.Text))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            try
            {





                string namePattern = @"^[a-zA-Z\s]+$";
                if (!Regex.IsMatch(aname.Text.Trim(), namePattern))
                {
                    MessageBox.Show("Please enter a valid name containing only letters and spaces.");
                    return;
                }

                if (!int.TryParse(id.SelectedValue.ToString(), out int planId))
                {
                    MessageBox.Show("Invalid plan selected.");
                    return;
                }

                if (!link.IsPlanNameUnique(namePattern, planId))
                {
                    MessageBox.Show("This training plan name already exists.", "Duplicate Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }



              

                if (!decimal.TryParse(Feee.Text.Trim(), out decimal weeklyFee) || weeklyFee <= 0)
                {
                    MessageBox.Show("Please enter a valid weekly fee.");
                    return;
                }

                if (!int.TryParse(session.Text.Trim(), out int sessionsPerWeek) || sessionsPerWeek <= 0 || sessionsPerWeek > 7)
                {
                    MessageBox.Show("Please enter a valid number of sessions per week (1 to 7).");
                    return;
                }

              

                bool updateResult = link.UpdatePlan(planId, namePattern, weeklyFee, sessionsPerWeek);

                if (updateResult)
                {
                    MessageBox.Show("Plan updated successfully.");
                    id.SelectedIndex = -1;
                    dataGridView1.DataSource = link.GetPlans();
                     load();
                     clear();
                }
                else
                {
                    MessageBox.Show("Failed to update plan.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (id.SelectedIndex == -1)
            {
                MessageBox.Show("Please pick an Plan ID.");
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this Plan?",
                                                  "Delete Confirmation",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int session = Convert.ToInt32(id.SelectedValue);
                    link.DeletePlan(session);
                    MessageBox.Show("Plan successfully deleted!");

                    dataGridView1.DataSource = link.GetPlans();
                    load();
                    clear();


                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }


        internal void load()
        {
            id.DropDownStyle = ComboBoxStyle.DropDownList;
            id.DataSource = link.GetPlanIds();
            id.SelectedIndex = -1;
        }

        private void id_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (id.SelectedValue != null)
            {
                int id1 = Convert.ToInt32(id.SelectedValue);
                fill(id1);
            }
        }

        private void clear()
        {
            Feee.Clear();
            session.Clear();
            aname.Clear();
            id.SelectedIndex = -1;
        }

        private void fill(int coa)
        {
            var co = link.GetPlans();

            var plan = co.FirstOrDefault(a => a.PlanID == coa);

            if (plan != null)
            {
                id.SelectedItem = plan.PlanID;

                Feee.Text = plan.WeeklyFee.ToString();
                session.Text = plan.SessionsPerWeek.ToString();
                aname.Text = plan.Name.ToString();



            }
            else
            {
                MessageBox.Show("Trainer not found.");
            }
        }

        private void TrainningPlan_Load(object sender, EventArgs e)
        {

        }

        private void Clear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox1.Text;
            var r = link.SearchPlans(searchText);
            dataGridView1.DataSource = r;
        }

        private void Back_Click(object sender, EventArgs e)
        {
            NavigationManager.GoBack();
            this.Close(); // Close the current form after going back


        }

        private void Plan_Click(object sender, EventArgs e)
        {
            Plan.Enabled = false;
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

        private void Logout_Click(object sender, EventArgs e)
        {
            Welcome wel = new Welcome();
            wel.Show();
            this.Close();
        }
    }
}
