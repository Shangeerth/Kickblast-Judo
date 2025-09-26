using Programming_Assigment.Athlete;
using Programming_Assigment.Classes;
using Programming_Assigment.Formss;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Programming_Assigment.Program;

namespace Programming_Assigment.Database
{
    public partial class Trainer : Form
    {
        private readonly TrainerCLZ link;
        public Trainer()
        {
            InitializeComponent();
            link = new TrainerCLZ(new Sql());
            dataGridView1.DataSource = link.GetTrainers();
            load();
            clear();
            design();


            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

        }

        private void Insert_Click(object sender, EventArgs e)
        {
            // Ensure no Trainer ID is selected
            if (id.SelectedIndex != -1)
            {
                MessageBox.Show("Please clear the form before inserting a new trainer.");
                id.SelectedIndex = -1;
                return;
            }
           

            // Validate required fields
            if (string.IsNullOrWhiteSpace(name.Text) ||
                string.IsNullOrWhiteSpace(agee.Text) ||
                string.IsNullOrWhiteSpace(exp.Text) ||
                string.IsNullOrWhiteSpace(Quali.Text) ||
                string.IsNullOrWhiteSpace(NIC1.Text) ||
                string.IsNullOrWhiteSpace(Salary123.Text) ||
                string.IsNullOrWhiteSpace(Contact12.Text))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            try
            {
                // Validate Name - only letters and spaces
                string namePattern = @"^[a-zA-Z\s]+$";
                if (!Regex.IsMatch(name.Text.Trim(), namePattern))
                {
                    MessageBox.Show("Please enter a valid name containing only letters and spaces.");
                    return;
                }

                // Validate NIC - alphanumeric only
                string nicPattern = @"^[a-zA-Z0-9]+$";
                if (!Regex.IsMatch(NIC1.Text.Trim(), nicPattern))
                {
                    MessageBox.Show("Please enter a valid NIC containing only letters and numbers.");
                    return;
                }

                // Parse and validate numeric fields
                if (!decimal.TryParse(Salary123.Text.Trim(), out decimal salaryVal) || salaryVal <= 0)
                {
                    MessageBox.Show("Please enter a valid positive number for Salary.");
                    return;
                }

                if (!int.TryParse(exp.Text.Trim(), out int experienceVal) || experienceVal <= 0)
                {
                    MessageBox.Show("Please enter a valid positive number for Experience.");
                    return;
                }

                if (!int.TryParse(Contact12.Text.Trim(), out int contactVal) || contactVal <= 0)
                {
                    MessageBox.Show("Please enter a valid positive number for Contact.");
                    return;
                }

                if (!int.TryParse(agee.Text.Trim(), out int age) || age <= 0)
                {
                    MessageBox.Show("Please enter a valid positive number for Weight.");
                    return;
                }

                // Get string fields
                string nameVal = name.Text.Trim();
                string qualificationVal = Quali.Text.Trim();
                string nicVal = NIC1.Text.Trim();
              

                // Insert into Trainer table
                bool insertResult = link.InsertTrainer(nameVal, age, experienceVal, qualificationVal, nicVal, contactVal, salaryVal);

                if (insertResult)
                {
                    MessageBox.Show("Trainer added successfully.");
                    id.SelectedIndex = -1;
                    dataGridView1.DataSource = link.GetTrainers(); // Refresh list
                    load();
                    clear();
                }
                else
                {
                    MessageBox.Show("Failed to add trainer.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }


        }

        private void Update_Click(object sender, EventArgs e)
        {

            // Ensure a Trainer ID is selected for update
            if (id.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a trainer to update.");
                return;
            }

            // Validate required fields
            if (string.IsNullOrWhiteSpace(name.Text) ||
                string.IsNullOrWhiteSpace(agee.Text) ||
                string.IsNullOrWhiteSpace(exp.Text) ||
                string.IsNullOrWhiteSpace(Quali.Text) ||
                string.IsNullOrWhiteSpace(NIC1.Text) ||
                string.IsNullOrWhiteSpace(Salary123.Text) ||
                string.IsNullOrWhiteSpace(Contact12.Text))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            try
            {
                // Validate Name - only letters and spaces
                string namePattern = @"^[a-zA-Z\s]+$";
                if (!Regex.IsMatch(name.Text.Trim(), namePattern))
                {
                    MessageBox.Show("Please enter a valid name containing only letters and spaces.");
                    return;
                }

                int trainerId = Convert.ToInt32(id.SelectedItem.ToString()); 

                

                // Validate NIC - alphanumeric only
                string nicPattern = @"^[a-zA-Z0-9]+$";
                if (!Regex.IsMatch(NIC1.Text.Trim(), nicPattern))
                {
                    MessageBox.Show("Please enter a valid NIC containing only letters and numbers.");
                    return;
                }

                // Parse and validate numeric fields
                if (!decimal.TryParse(Salary123.Text.Trim(), out decimal salaryVal) || salaryVal <= 0)
                {
                    MessageBox.Show("Please enter a valid positive number for Salary.");
                    return;
                }

                if (!int.TryParse(exp.Text.Trim(), out int experienceVal) || experienceVal <= 0)
                {
                    MessageBox.Show("Please enter a valid positive number for Experience.");
                    return;
                }

                if (!int.TryParse(Contact12.Text.Trim(), out int contactVal) || contactVal <= 0)
                {
                    MessageBox.Show("Please enter a valid positive number for Contact.");
                    return;
                }

                if (!int.TryParse(agee.Text.Trim(), out int age) || age <= 0)
                {
                    MessageBox.Show("Please enter a valid positive number for Age.");
                    return;
                }

                // Get values
                string nameVal = name.Text.Trim();
                string qualificationVal = Quali.Text.Trim();
                string nicVal = NIC1.Text.Trim();

                // Update Trainer
                bool updateResult = link.UpdateTrainer(trainerId, nameVal, age, experienceVal, qualificationVal, nicVal, contactVal, salaryVal);

                if (updateResult)
                {
                    MessageBox.Show("Trainer updated successfully.");
                    id.SelectedIndex = -1;
                    dataGridView1.DataSource = link.GetTrainers();
                    load();
                    clear();
                }
                else
                {
                    MessageBox.Show("Failed to update trainer.");
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
                MessageBox.Show("Please pick an Trainer ID.");
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this Trainer?",
                                                  "Delete Confirmation",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int Trainer = Convert.ToInt32(id.SelectedValue);
                    link.DeleteTrainer(Trainer);
                    MessageBox.Show("Trainer successfully deleted!");

                    dataGridView1.DataSource = link.GetTrainers();
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
            id.DataSource = link.GetTrainerIds();
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox1.Text;
            var r = link.SearchTrainers(searchText);
            dataGridView1.DataSource = r;
        }
        
        private void fill(int tr)
        {
            var trr = link.GetTrainers();  

            var tr1 = trr.FirstOrDefault(a => a.Id == tr);

            if (tr1 != null)
            {
                id.SelectedItem = tr1.Id;

                name.Text = tr1.Name;
                Salary123.Text = tr1.Salary.ToString();
                Quali.Text = tr1.Qualification.ToString();
                exp.Text = tr1.Experience.ToString();
                NIC1.Text = tr1.NIC;
                agee.Text = tr1.Age.ToString();
                Contact12.Text = tr1.Contact.ToString();

                
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
            name.Clear();
            agee.Clear();
            exp.Clear();
            Quali.Clear();
            NIC1.Clear();
            Contact12.Clear();
            Salary123.Clear();

            id.SelectedIndex = -1;
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = link.GetTrainers();
        }

       

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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

        private void Trainer_Load(object sender, EventArgs e)
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
            label8.ForeColor = formTextColor;
            label9.ForeColor = formTextColor;
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

        private void Search_Click(object sender, EventArgs e)
        {

        }
    }
}
