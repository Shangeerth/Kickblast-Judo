using Programming_Assigment.Classes;
using Programming_Assigment.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Programming_Assigment.Program;

namespace Programming_Assigment.Formss
{
    public partial class Athleteplanform : Form
    {
        private readonly Athleteplan link;
        public Athleteplanform()
        {
            InitializeComponent();
            link = new Athleteplan(new Sql());
            dataGridView1.DataSource = link.GetAthleteTrainingPlans();
            load();
            clear();
            dateTimePicker2.Value=DateTime.Now;
            dateTimePicker1.Value=DateTime.Now;

            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

        }

        private void Insert_Click(object sender, EventArgs e)
        {
            if (id != null && id.SelectedIndex != -1)
            {
                MessageBox.Show("Please clear the form before inserting a new Athlete Training record.");
                id.SelectedIndex = -1;
                return;
            }
            
            if (string.IsNullOrWhiteSpace(dateTimePicker2.Text) ||
                string.IsNullOrWhiteSpace(aname.Text) ||
                string.IsNullOrWhiteSpace(planname.Text))
            {
                MessageBox.Show("Please fill in all required fields: Time, Athlete Name, and Plan Name.");
                return;
            }

            try
            {
                DateTime sessionDate = dateTimePicker1.Value.Date;
                string time = dateTimePicker2.Text.Trim();
                string athleteName = aname.Text.Trim();
                string planName = planname.Text.Trim();

                if (link.IsAthleteInDifferentPlan(athleteName, planName))
                {
                    MessageBox.Show($"Athlete '{athleteName}' is already enrolled in a different training plan.");
                    return;
                }


                int maxSessionsAllowed = 0;
                string planLower = planName.ToLower();

                if (planLower == "beginner")
                    maxSessionsAllowed = 2;
                else if (planLower == "intermediate")
                    maxSessionsAllowed = 3;
                else if (planLower == "elite")
                    maxSessionsAllowed = 5;
                else
                {
                    MessageBox.Show("Unknown plan name. Cannot validate sessions per week.");
                    return;
                }

                // 🆕 Calculate week range
                int daysToSubtract = (int)sessionDate.DayOfWeek == 0 ? 6 : (int)sessionDate.DayOfWeek - 1; // Sunday = 0
                DateTime startOfWeek = sessionDate.AddDays(-daysToSubtract);
                DateTime endOfWeek = startOfWeek.AddDays(6);

                int currentSessionsThisWeek = link.GetSessionsCountForAthleteWithinWeek(athleteName, startOfWeek, endOfWeek);

                if (currentSessionsThisWeek >= maxSessionsAllowed)
                {
                    MessageBox.Show($"You have already reached the maximum allowed sessions ({maxSessionsAllowed}) for the '{planName}' plan in the selected week.");
                    return;
                }

                int athleteSession = currentSessionsThisWeek + 1;

                bool insertResult = link.InsertAthleteTrainingPlan(sessionDate, time, athleteName, planName, athleteSession);

                if (insertResult)
                {
                    MessageBox.Show("Athlete training plan inserted successfully.");
                    dataGridView1.DataSource = link.GetAthleteTrainingPlans(); // Refresh data grid
                    load();
                    clear();
                }
                else
                {
                    MessageBox.Show("Failed to insert athlete training plan.");
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
                MessageBox.Show("Please select a Athlete Training record to update.");
                return;
            }

            

            if (string.IsNullOrWhiteSpace(dateTimePicker2.Text) ||
                string.IsNullOrWhiteSpace(aname.Text) ||
                string.IsNullOrWhiteSpace(planname.Text))
            {
                MessageBox.Show("Please fill in all required fields: Time, Athlete Name, and Plan Name.");
                return;
            }

            try
            {
                if (string.IsNullOrWhiteSpace(tsession.Text) || dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Please select a session to update.");
                    return;
                }

                // Format time string
                DateTime selectedTime = dateTimePicker2.Value;
                string time = selectedTime.ToString("hh:mm:ss tt");

                DateTime sessionDate = dateTimePicker1.Value.Date;
                string athleteName = aname.Text.Trim();
                string planName = planname.Text.Trim();

                int maxSessionsAllowed = 0;
                string planLower = planName.ToLower();

                if (planLower == "beginner")
                    maxSessionsAllowed = 2;
                else if (planLower == "intermediate")
                    maxSessionsAllowed = 3;
                else if (planLower == "elite")
                    maxSessionsAllowed = 5;
                else
                {
                    MessageBox.Show("Unknown plan name. Cannot validate sessions per week.");
                    return;
                }
                ;

                if (maxSessionsAllowed == 0)
                {
                    MessageBox.Show("Unknown plan name. Cannot validate sessions per week.");
                    return;
                }


                int selectedTrainingPlanId = Convert.ToInt32(id.SelectedValue);
                MessageBox.Show($"Selected ID to update: {selectedTrainingPlanId}");


                int daysToSubtract = (int)sessionDate.DayOfWeek == 0 ? 6 : (int)sessionDate.DayOfWeek - 1;
                DateTime startOfWeek = sessionDate.AddDays(-daysToSubtract);
                DateTime endOfWeek = startOfWeek.AddDays(6);

                int currentSessionsThisWeek = link.GetSessionsCountForAthleteWithinWeek(athleteName, startOfWeek, endOfWeek);

                int athleteSession = currentSessionsThisWeek + 1;

                if (athleteSession > maxSessionsAllowed)
                {
                    MessageBox.Show($"You have already reached the maximum allowed sessions ({maxSessionsAllowed}) for the '{planName}' plan in the selected week.");
                    return;
                }

                
                bool updateResult = link.UpdateAthleteTrainingPlan(selectedTrainingPlanId, sessionDate, time, athleteName, planName, athleteSession);

                if (updateResult)
                {
                    MessageBox.Show("Athlete training plan updated successfully.");
                    dataGridView1.DataSource = link.GetAthleteTrainingPlans(); // Refresh
                    load();
                    clear();
                }
                else
                {
                    MessageBox.Show("Failed to update athlete training plan.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }

        private void Delete_Click(object sender, EventArgs e)
        {

        }

        private void planid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (id.SelectedIndex == -1)
            {
                MessageBox.Show("Please pick an Athlete Session ID.");
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this  Athlete Session?",
                                                  "Delete Confirmation",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int session = Convert.ToInt32(id.SelectedValue);
                    link.DeleteAthleteTrainingPlan(session);
                    MessageBox.Show("Athlete Session successfully deleted!");

                    dataGridView1.DataSource = link.GetAthleteTrainingPlans();
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
           
            aname.DataSource = link.GetAthleteIDs();
            planname.DataSource=link.GetTrainingPlanIDs();
            id.DataSource = link.GetAthleteTrainingPlanIds();
            id.DropDownStyle = ComboBoxStyle.DropDownList;
            planname.DropDownStyle = ComboBoxStyle.DropDownList;
            aname.DropDownStyle = ComboBoxStyle.DropDownList;
            id.DataSource = link.GetAthleteTrainingPlanIds();
            id.SelectedIndex = -1;
            planname.SelectedIndex = -1;
            aname.SelectedIndex = -1;
            DateTime now = DateTime.Now;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            sessionsss.Enabled = false;
            sessionsss.ReadOnly = true;
        }

        

        private void aname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (aname.SelectedValue != null)
            {
                int athleteId;

                // Safely parse athlete ID from ComboBox
                if (int.TryParse(aname.SelectedValue.ToString(), out athleteId))
                {
                    // Get athlete name using ID
                    string athleteName = link.GetAthleteNameById(athleteId);

                    // Set the name to your text field
                    athname.Text = athleteName;
                }
                else
                {
                    MessageBox.Show("Invalid athlete ID selected.");
                }
            }
        }

        private void planname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (planname.SelectedItem != null)
            {
                // Assuming planname's SelectedValue or SelectedItem is PlanID (int)
                int selectedPlanId;
                if (int.TryParse(planname.SelectedValue?.ToString() ?? planname.SelectedItem.ToString(), out selectedPlanId))
                {
                    // Get sessions and fee by PlanID directly
                    int sessions = link.GetSessionsPerWeekByPlanId(selectedPlanId);
                    tsession.Text = sessions.ToString();

                    decimal fee = link.GetPlanFeeByPlanId(selectedPlanId);
                    Feee.Text = fee.ToString();

                    string name = link.GetPlanNameById(selectedPlanId);
                    plname.Text = name;


                    // Retrieve athlete ID from your form (replace this with actual logic)
                    int athleteId = 0; // <-- e.g., int.Parse(athleteComboBox.SelectedValue.ToString());

                    if (athleteId > 0)
                    {
                        // Check enrollment by IDs only
                        bool isEnrolled = link.IsAthleteEnrolledInPlan(athleteId, selectedPlanId);

                        if (isEnrolled)
                        {
                            MessageBox.Show("Athlete is already enrolled in the selected training plan.");
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Plan selection.");
                }
            }

        }

        private void sessionsss_TextChanged(object sender, EventArgs e)
        {
           

        }

        private void id_SelectedIndexChanged(object sender, EventArgs e)
        {

            



            if (id.SelectedValue != null)
            {
                int id1 = Convert.ToInt32(id.SelectedValue);
                fill(id1);
            }



        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox1.Text;
            var r = link.SearchAthleteTrainingPlans(searchText);
            dataGridView1.DataSource = r;
        }


        private void clear()
        {
            Feee.Clear();
            sessionsss.Clear();
            plname.Clear();
            athname.Clear();
            tsession.Clear();
            id.SelectedIndex = -1;
            aname.SelectedIndex = -1;
            planname.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;

        }

        private void fill(int coa)
        {
            var co = link.GetAthleteTrainingPlans();

            var pl = co.FirstOrDefault(a => a.ID == coa);

            if (pl != null)
            {
                id.SelectedItem = pl.ID;

                Feee.Text = pl.WeeklyFee.ToString();
                dateTimePicker1.Text = pl.SessionDate.ToString();
                dateTimePicker2.Text = pl.Time.ToString();
                aname.Text = pl.AthleteName.ToString();
                planname.Text = pl.PlanName.ToString();
                sessionsss.Text = pl.athletesession.ToString();
                tsession.Text = pl.SessionsPerWeek.ToString();



            }
            else
            {
                MessageBox.Show("Trainer not found.");
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void Athleteplanform_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

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
