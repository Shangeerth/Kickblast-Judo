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
            dataGridView1.DataSource=link.GetAthleteCompetitionDetails();
            load();
            clear();

            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;


        }

        private void Insert_Click(object sender, EventArgs e)
        {
            // Exit if existing record is selected
            if (id != null && id.SelectedIndex != -1)
            {
                MessageBox.Show("Please clear the form before inserting a new Athlete Competition record.");
                id.SelectedIndex = -1;
                return;
            }

            string athleteName = aname.Text.Trim();
            string competitionName = cname.Text.Trim();
            string selectedPlan = plan.Text.Trim();

            // Exit if athlete or competition or plan is empty
            if (string.IsNullOrWhiteSpace(athleteName) || string.IsNullOrWhiteSpace(competitionName) || string.IsNullOrWhiteSpace(selectedPlan))
            {
                MessageBox.Show("Please fill in all fields: Athlete Name, Competition Name, and Training Plan.");
                return;
            }

            // Check if athlete is enrolled in the selected plan
            if (!link.IsAthleteEnrolledInTrainingPlan(athleteName, selectedPlan))
            {
                MessageBox.Show($"{athleteName} is NOT enrolled in the {selectedPlan} plan.");
                return;
            }

            // Check if athlete is already registered in this competition with this training plan
            if (link.CheckAthleteCompetitionExists(athleteName, competitionName, selectedPlan))
            {
                MessageBox.Show($"{athleteName} is already registered for {competitionName} under the {selectedPlan} plan.");
                MessageBox.Show($" One Athlete can enter one competition Name despite multiple Training      Plan," +
                                $"  Try entrolling Under different Competition name");

                return;
            }

            if (plan.SelectedItem != null && plan.SelectedItem.ToString() == "Beginner")
            {
                MessageBox.Show("Beginner plan is not allowed to enter any competition");
                return;
            }


            try
            {
                bool insertResult = link.InsertAthleteCompetition(athleteName, competitionName);

                if (insertResult)
                {
                    MessageBox.Show("Athlete registered for competition successfully.");
                    dataGridView1.DataSource = link.GetAthleteCompetitionDetails(); // Refresh data
                    load();
                   clear(); // Uncomment if you want to clear form
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
            // Ensure an existing record is selected
            if (id == null || id.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a record to update.");
                return;
            }

            string athleteName = aname.Text.Trim();
            string competitionName = cname.Text.Trim();
            string selectedPlan = plan.Text.Trim();

            // Exit if athlete or competition or plan is empty
            if (string.IsNullOrWhiteSpace(athleteName) || string.IsNullOrWhiteSpace(competitionName) || string.IsNullOrWhiteSpace(selectedPlan))
            {
                MessageBox.Show("Please fill in all fields: Athlete Name, Competition Name, and Training Plan.");
                return;
            }

            // Check if athlete is enrolled in the selected plan
            if (!link.IsAthleteEnrolledInTrainingPlan(athleteName, selectedPlan))
            {
                MessageBox.Show($"{athleteName} is NOT enrolled in the {selectedPlan} plan.");
                return;
            }

            if (plan.SelectedItem != null && plan.SelectedItem.ToString() == "Beginner")
            {
                MessageBox.Show("Beginner plan is not allowed to enter any competition");
                return;
            }



            // Get the selected AthleteCompetitionID from the dropdown
            int athleteCompetitionID = Convert.ToInt32(id.SelectedItem.ToString());

            try
            {
                bool updateResult = link.UpdateAthleteCompetition(athleteCompetitionID, athleteName, competitionName);

                if (updateResult)
                {
                    MessageBox.Show("Athlete competition record updated successfully.");
                    dataGridView1.DataSource = link.GetAthleteCompetitionDetails(); // Refresh data
                    load();
                    clear(); // Uncomment to clear form after update
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
                    link.Delete(competitionId); // This should call your delete method in the DAL
                    MessageBox.Show("Athlete Competition record successfully deleted!");

                    dataGridView1.DataSource = link.GetAthleteCompetitionDetails(); // Refresh the DataGrid
                    load();
                    clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

        }

        private void load()
        {
            aname.DataSource = link.GetAthleteNamesByTrainingPlan();
            plan.DataSource = link.GetAllTrainingPlanNames();
            cname.DataSource= link.GetAllCompetitionNames();
            cname.DropDownStyle = ComboBoxStyle.DropDownList;
            aname.DropDownStyle = ComboBoxStyle.DropDownList;
            id.DropDownStyle = ComboBoxStyle.DropDownList;
            id.DataSource = link.id();
            id.SelectedIndex = -1;
            aname.SelectedIndex = -1;
            cname.SelectedIndex = -1;
            dateTimePicker1.Enabled = false;
            Feee.Enabled = false;
            timee.Enabled = false;
            plan.DropDownStyle = ComboBoxStyle.DropDownList;
            plan.SelectedIndex = -1;
            


        }

        private void clear()
        {
            aname.SelectedIndex = -1;
            cname.SelectedIndex = -1;
            plan.SelectedIndex = -1;
            Feee.Clear();
            timee.Clear();
             
           this.id.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
        }

        private void aname_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void plan_SelectedIndexChanged(object sender, EventArgs e)
        {
            



        }

        private void cname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cname.SelectedItem != null)
            {
                string selectedPlan = cname.SelectedItem.ToString(); // Get the plan name as string

                decimal sessions = link.GetPlanFeeByName(selectedPlan); // Pass string

                Feee.Text = sessions.ToString();
            }

            if (cname.SelectedItem != null)
            {
                string selectedPlan = cname.SelectedItem.ToString(); 

                DateTime? date = link.GetPlanDateByName(selectedPlan); 

                dateTimePicker1.Text = date.HasValue ? date.Value.ToShortDateString() : "No date found for the selected training plan";
            }

            if (cname.SelectedItem != null)
            {
                string selectedPlan = cname.SelectedItem.ToString(); 

                string sessions = link.GetPlanTimeByName(selectedPlan);

                timee.Text = sessions.ToString();
            }


        }



        private void fill(int athleteCompetitionId)
        {
            // Assuming link.GetAthleteCompetitionDetails() returns List<AthleteCompetitionDetails>
            var details = link.GetAthleteCompetitionDetails();

            var competition = details.FirstOrDefault(ac => ac.AthleteCompetitionID == athleteCompetitionId);

            if (competition != null)
            {

               plan.SelectedItem = competition.Trainingplan;

                

                id.SelectedItem = competition.AthleteCompetitionID;

                aname.Text = competition.AthleteName;


                cname.Text = competition.CompetitionName;

                dateTimePicker1.Value = competition.CompetitionDate;
                Feee.Text = competition.fees.ToString(); 
                timee.Text = competition.CompetitionTime; 
               
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
                int id1 = Convert.ToInt32(id.SelectedValue);
                fill(id1);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox1.Text;
            var r = link.SearchAthleteCompetitionDetails(searchText);
            dataGridView1.DataSource = r;
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            clear();
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

        private void button3_Click(object sender, EventArgs e)
        {
            TrainningPlan newForm = new TrainningPlan();
            NavigationManager.OpenForm(this, newForm);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Competition newForm = new Competition();
            NavigationManager.OpenForm(this, newForm);
        }

        private void Athlete_Competition_Click(object sender, EventArgs e)
        {
            Athlete_Competition.Enabled=false;
            CompetitionAthlete newForm = new CompetitionAthlete();
            NavigationManager.OpenForm(this, newForm);

        }

        private void button1_Click(object sender, EventArgs e)
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

        private void Back_Click(object sender, EventArgs e)
        {
            NavigationManager.GoBack();
            this.Close();
        }
    }
}
