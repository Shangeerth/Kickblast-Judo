using Programming_Assigment.Database;
using Programming_Assigment.Formss;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
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

            Design();
            this.StartPosition = FormStartPosition.CenterScreen;
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


                string nameVal = aname.Text.Trim();



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

                bool insertResult = link.InsertPlan(nameVal, weeklyFee, sessionsPerWeek);

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

                string nameVal = aname.Text.Trim();

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
                     bool updateResult = link.UpdatePlan(planId, nameVal, weeklyFee, sessionsPerWeek);

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

        private void clear()
        {
            Feee.Clear();
            session.Clear();
            aname.Clear();
            id.SelectedIndex = -1;
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox1.Text;
            var r = link.SearchPlans(searchText);
            dataGridView1.DataSource = r;
        }

        private void Back_Click(object sender, EventArgs e)
        {
            this.Hide();

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





        private void Design()
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
            label5.ForeColor = formTextColor;
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
