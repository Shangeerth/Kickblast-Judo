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
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker1.Value = DateTime.Now;
            design();
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
                aname.SelectedIndex == -1 ||
                planname.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill in all required fields: Time, Athlete, and Plan.");
                return;
            }

            try
            {
                DateTime sessionDate = dateTimePicker1.Value.Date;
                string time = dateTimePicker2.Text.Trim();

                int athid = Convert.ToInt32(aname.SelectedValue);   // AthleteID from ComboBox
                int planid = Convert.ToInt32(planname.SelectedValue); // PlanID from ComboBox

                if (link.IsAthleteInDifferentPlan(athid, planid))
                {
                    MessageBox.Show($"Athlete (ID: {athid}) is already enrolled in a different training plan.");
                    return;
                }

                int maxSessionsAllowed = link.GetSessionsPerWeekByPlanId(planid);

                if (maxSessionsAllowed == 0)
                {
                    MessageBox.Show("Unknown plan ID or plan has no sessions defined.");
                    return;
                }

                // Calculate start and end of the selected week
                int daysToSubtract = (int)sessionDate.DayOfWeek == 0 ? 6 : (int)sessionDate.DayOfWeek - 1;
                DateTime startOfWeek = sessionDate.AddDays(-daysToSubtract);
                DateTime endOfWeek = startOfWeek.AddDays(6);

                int currentSessionsThisWeek = link.GetSessionsCountForAthleteWithinWeek(athid, startOfWeek, endOfWeek);

                if (currentSessionsThisWeek >= maxSessionsAllowed)
                {
                    MessageBox.Show($"You have already reached the maximum allowed sessions ({maxSessionsAllowed}) for this plan in the selected week.");
                    return;
                }

                int athleteSession = currentSessionsThisWeek + 1;

                bool insertResult = link.InsertAthleteTrainingPlan(sessionDate, time, athid, planid, athleteSession);

                if (insertResult)
                {
                    MessageBox.Show("Athlete training plan inserted successfully.");
                    dataGridView1.DataSource = link.GetAthleteTrainingPlans();
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
                MessageBox.Show("Please select an Athlete Training record to update.");
                return;
            }

            if (aname.SelectedIndex == -1 || planname.SelectedIndex == -1 || string.IsNullOrWhiteSpace(dateTimePicker2.Text))
            {
                MessageBox.Show("Please fill in all required fields: Time, Athlete, and Plan.");
                return;
            }

            try
            {
                int selectedTrainingPlanId = Convert.ToInt32(id.SelectedValue);

                DateTime sessionDate = dateTimePicker1.Value.Date;
                DateTime selectedTime = dateTimePicker2.Value;
                string time = selectedTime.ToString("hh:mm:ss tt");

                int athleteId = Convert.ToInt32(aname.SelectedValue);
                int planId = Convert.ToInt32(planname.SelectedValue);

                string planText = plname.Text.ToLower();
                int maxSessionsAllowed = 0;

                switch (planText)
                {
                    case "beginner":
                        maxSessionsAllowed = 2;
                        break;
                    case "intermediate":
                        maxSessionsAllowed = 3;
                        break;
                    case "elite":
                        maxSessionsAllowed = 5;
                        break;
                    default:
                        maxSessionsAllowed = 0;
                        break;
                }


                if (maxSessionsAllowed == 0)
                {
                    MessageBox.Show("Unknown plan name. Cannot validate sessions per week.");
                    return;
                }

                // Calculate week range (Monday to Sunday)
                int daysToSubtract = (int)sessionDate.DayOfWeek == 0 ? 6 : (int)sessionDate.DayOfWeek - 1;
                DateTime startOfWeek = sessionDate.AddDays(-daysToSubtract);
                DateTime endOfWeek = startOfWeek.AddDays(6);

                int currentSessionsThisWeek = link.GetSessionsCountForAthleteWithinWeek(athleteId, startOfWeek, endOfWeek);

                // Exclude current session if editing an existing one on the same date
                DataGridViewRow selectedRow = dataGridView1.CurrentRow;
                if (selectedRow != null)
                {
                    DateTime existingDate = Convert.ToDateTime(selectedRow.Cells["SessionDate"].Value);
                    int existingAthleteId = Convert.ToInt32(selectedRow.Cells["AthleteID"].Value);

                    if (existingDate.Date == sessionDate.Date && existingAthleteId == athleteId)
                    {
                        currentSessionsThisWeek--; // subtract the existing session
                    }
                }

                int athleteSession = currentSessionsThisWeek + 1;

                if (athleteSession > maxSessionsAllowed)
                {
                    MessageBox.Show($"You have already reached the maximum allowed sessions ({maxSessionsAllowed}) for the selected plan in the week.");
                    return;
                }

                bool updateResult = link.UpdateAthleteTrainingPlan(
                    selectedTrainingPlanId,
                    sessionDate,
                    time,
                    athleteId,
                    planId,
                    athleteSession
                );

                if (updateResult)
                {
                    MessageBox.Show("Athlete training plan updated successfully.");
                    dataGridView1.DataSource = link.GetAthleteTrainingPlans();
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
            if (id.SelectedIndex == -1)
            {
                MessageBox.Show("Please pick an Coaching ID.");
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this Plan ?",
                                                  "Delete Confirmation",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int session = Convert.ToInt32(id.SelectedValue);
                    link.DeleteAthleteTrainingPlan(session);
                    MessageBox.Show("Plan successfully deleted!");

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
            planname.DataSource = link.GetTrainingPlanIDs();
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
                int selectedPlanId;
                if (int.TryParse(planname.SelectedValue?.ToString() ?? planname.SelectedItem.ToString(), out selectedPlanId))
                {
                    int sessions = link.GetSessionsPerWeekByPlanId(selectedPlanId);
                    tsession.Text = sessions.ToString();

                    decimal fee = link.GetPlanFeeByPlanId(selectedPlanId);
                    Feee.Text = fee.ToString();

                    string name = link.GetPlanNameById(selectedPlanId);
                    plname.Text = name;


                    int athleteId = 0;

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
                aname.Text = pl.AthleteID.ToString();
                planname.Text = pl.PlanID.ToString();
                sessionsss.Text = pl.athletesession.ToString();
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

        private void Athleteplanform_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

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
            label5.ForeColor = formTextColor;
            label6.ForeColor = formTextColor;
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
