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
using System.Threading.Tasks;
using System.Windows.Forms;
using static Programming_Assigment.Program;

namespace Programming_Assigment.Classes
{
    public partial class CompetitionAthlete : Form
    {
        private readonly athleteCompetion link;
        public CompetitionAthlete()
        {
            InitializeComponent();
            link = new athleteCompetion(new Sql());

            // Load data grid and combo boxes
            dataGridView1.DataSource = link.GetAthleteCompetitionDetails();
            LoadComboBoxes();
            ClearForm();
            design(); // Apply custom design
            // Form settings
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;


        }

        private void Insert_Click(object sender, EventArgs e)
        {

            // If an existing record is selected in ID dropdown, block insert
            if (id != null && id.SelectedIndex != -1)
            {
                MessageBox.Show("Please clear the form before inserting a new Athlete Competition record.");
                id.SelectedIndex = -1;
                return;
            }

            // Validate combo box selections
            if (aid.SelectedItem == null || comid.SelectedItem == null || plan.SelectedItem == null)
            {
                MessageBox.Show("Please select Athlete ID, Competition ID, and Training Plan.");
                return;
            }

            int selectedAthleteId = (int)aid.SelectedValue;
            int selectedCompetitionId = (int)comid.SelectedValue;
            string selectedPlanName = plan.SelectedItem.ToString();

            // Get the athlete name for display
            string athleteName = link.GetAthleteNameById(selectedAthleteId);

            // Check if athlete is enrolled in selected plan
            if (!link.IsAthleteEnrolledInTrainingPlan(selectedAthleteId, selectedPlanName))
            {
                MessageBox.Show($"{athleteName} is NOT enrolled in the {selectedPlanName} plan.");
                return;
            }

          

            // Beginner plan restriction
            if (selectedPlanName == "Beginner")
            {
                MessageBox.Show("Beginner plan is not allowed to enter any competition.");
                return;
            }

            try
            {
                int selectedPlanId = link.GetTrainingPlanIdByName(selectedPlanName);
                List<int> selectedPlanIds = new List<int> { selectedPlanId };

                bool insertResult = link.InsertAthleteCompetition(selectedAthleteId, selectedCompetitionId, selectedPlanIds);

                if (insertResult)
                {
                    MessageBox.Show("Athlete registered for competition successfully.");
                    dataGridView1.DataSource = link.GetAthleteCompetitionDetails(); // Refresh data grid
                    LoadComboBoxes();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Failed to register athlete for competition.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }
        private void Update_Click(object sender, EventArgs e)
        {
            // Check if an AthleteCompetition record is selected
            if (id == null || id.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a record to update.");
                return;
            }

            // Validate combo box selections
            if (aid.SelectedItem == null || comid.SelectedItem == null || plan.SelectedItem == null)
            {
                MessageBox.Show("Please select Athlete ID, Competition ID, and Training Plan.");
                return;
            }

            int athleteCompetitionID = (int)id.SelectedValue;
            int selectedAthleteId = (int)aid.SelectedValue;
            int selectedCompetitionId = (int)comid.SelectedValue;
            string selectedPlanName = plan.SelectedItem.ToString();

            // Check if athlete is enrolled in the selected training plan
            if (!link.IsAthleteEnrolledInTrainingPlan(selectedAthleteId, selectedPlanName))
            {
                MessageBox.Show($"Athlete is NOT enrolled in the {selectedPlanName} plan.");
                return;
            }

            if (selectedPlanName == "Beginner")
            {
                MessageBox.Show("Beginner plan is not allowed to enter any competition.");
                return;
            }

            try
            {
             
                int planId = link.GetTrainingPlanIdByName(selectedPlanName);
                if (planId == 0)
                {
                    MessageBox.Show("Invalid Training Plan selected.");
                    return;
                }

                List<int> planIds = new List<int> { planId };

                bool updateResult = link.UpdateAthleteCompetition(athleteCompetitionID, selectedAthleteId, selectedCompetitionId, planIds);

                if (updateResult)
                {
                    MessageBox.Show("Athlete competition record updated successfully.");
                    dataGridView1.DataSource = link.GetAthleteCompetitionDetails(); // Refresh data grid
                    LoadComboBoxes();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Failed to update athlete competition record.");
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
                MessageBox.Show("Please pick an Athlete Competition ID.");
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this Athlete Competition record?",
                                                  "Delete Confirmation",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int competitionId = Convert.ToInt32(id.SelectedValue);
                    link.Delete(competitionId);
                    MessageBox.Show("Athlete Competition record successfully deleted!");

                    dataGridView1.DataSource = link.GetAthleteCompetitionDetails();
                    LoadComboBoxes();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void LoadComboBoxes()
        {
            var athletes = link.GetAthleteIdsByTrainingPlan(); // List<Athlete> with ID and Name
            aid.DataSource = athletes;
            aid.SelectedIndex = -1;

            var competitions = link.GetCompetitionIds(); // List<Competition> with ID and Name
            comid.DataSource = competitions;
            comid.SelectedIndex = -1;

            plan.DataSource = link.GetAllTrainingPlanNames();
            plan.SelectedIndex = -1;

            id.DataSource = link.id();
            id.SelectedIndex = -1;

            // Set drop down style
            comid.DropDownStyle = ComboBoxStyle.DropDownList;
            aid.DropDownStyle = ComboBoxStyle.DropDownList;
            plan.DropDownStyle = ComboBoxStyle.DropDownList;
            id.DropDownStyle = ComboBoxStyle.DropDownList;

            dateTimePicker1.Enabled = false;
            Feee.Enabled = false;
            timee.Enabled = false;
        }
   
        private void aname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (aid.SelectedValue != null)
            {
                if (int.TryParse(aid.SelectedValue.ToString(), out int id))
                {
                    string athleteName = link.GetAthleteNameById(id);
                    athname.Text = athleteName;
                }
                else
                {
                    MessageBox.Show("Selected athlete ID is invalid.");
                }
            }

        }

        private void plan_SelectedIndexChanged(object sender, EventArgs e)
        {
            



        }

        private void cname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comid.SelectedItem != null)
            {
                if (int.TryParse(comid.SelectedItem.ToString(), out int selectedCompetitionId))
                {
                    decimal fee = link.GetCompetitionFeeById(selectedCompetitionId);
                    Feee.Text = fee.ToString();

                    DateTime? date = link.GetCompetitionDateById(selectedCompetitionId);
                    dateTimePicker1.Text = date.HasValue ? date.Value.ToShortDateString() : "No date available";

                    string time = link.GetCompetitionTimeById(selectedCompetitionId);
                    timee.Text = time;


                    string name = link.GetCompetitionNameById(selectedCompetitionId);
                    comname.Text = name;
                }
                else
                {
                    MessageBox.Show("Selected competition ID is invalid.");
                }
            }

        }



        private void fill(int athleteCompetitionId)
        {
            var details = link.GetAthleteCompetitionDetails();
            var competition = details.FirstOrDefault(ac => ac.AthleteCompetitionID == athleteCompetitionId);

            if (competition != null)
            {
                plan.SelectedItem = competition.Trainingplan;
                id.SelectedItem = competition.AthleteCompetitionID;
                aid.Text = competition.AthleteID.ToString();
                comid.Text = competition.CompetitionID.ToString();
                dateTimePicker1.Value = competition.CompetitionDate;
                Feee.Text = competition.fees.ToString();
                timee.Text = competition.CompetitionTime;
                plan.Text = competition.Trainingplan;
            }
            else
            {
                MessageBox.Show("Athlete Competition record not found.");
            }
        }

        private void id_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (id.SelectedValue != null)
            {
                int selectedId = Convert.ToInt32(id.SelectedValue);
                fill(selectedId);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox1.Text;
            var results = link.SearchAthleteCompetitionDetails(searchText);
            dataGridView1.DataSource = results;
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }


        private void ClearForm()
        {
            aid.SelectedIndex = -1;
            comid.SelectedIndex = -1;
            plan.SelectedIndex = -1;
            Feee.Clear();
            timee.Clear();
            athname.Clear();
            comname.Clear();
            id.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
        }


        private void Logout_Click(object sender, EventArgs e)
        {
            // Get the Dashboard (top-level parent)
            Form parentDashboard = this.TopLevelControl as Dashboard;
            if (parentDashboard != null)
            {
                parentDashboard.Close(); // This will close the main Dashboard
            }

            // Restart app with Welcome/Login
            System.Diagnostics.Process.Start(Application.ExecutablePath);
            Application.Exit();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void CompetitionAthlete_Load(object sender, EventArgs e)
        {

        }

        private void comname_TextChanged(object sender, EventArgs e)
        {

        }






        private void design()
        {
            Color formBackColor = Color.FromArgb(34, 34, 34); // Charcoal Black

            Color buttonBackColor = Color.FromArgb(191, 167, 111);   // #BFA76F (Gold)
            Color buttonForeColor = Color.FromArgb(26, 26, 26);      // #1A1A1A (Dark)
            Color formTextColor = Color.FromArgb(230, 225, 210); // #E6E1D2 – Ivory White


            this.BackColor = formBackColor;


            // Set panel background
            this.BackColor = formBackColor;


            label1.ForeColor = formTextColor;
            label2.ForeColor = formTextColor;
            label3.ForeColor = formTextColor;
            label4.ForeColor = formTextColor;
            label7.ForeColor = formTextColor;
            Contact1.ForeColor = formTextColor;
            Nic123.ForeColor = formTextColor;
            Search.ForeColor = buttonBackColor;




            dataGridView1.BackgroundColor = Color.White; // Or any color you want for background
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;  // Text color inside grid cells
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = buttonBackColor; // Or any header text color
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = buttonBackColor; // Header background


            Button[] buttons = new Button[]
                {
                    Insert,
                    Clear,
                    Update,
                    Delete,
                    Back,
                    Logout



                };

            foreach (var btn in buttons)
            {
                btn.BackColor = buttonBackColor;
                btn.ForeColor = buttonForeColor;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
            }
        }


    }
}
