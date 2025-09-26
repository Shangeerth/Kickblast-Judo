using Programming_Assigment.Athlete;
using Programming_Assigment.Classes;
using Programming_Assigment.Database;
using Programming_Assigment.Formss;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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

namespace Programming_Assigment
{
    public partial class Form1 : Form 
    {
        private readonly Athlete_Clz link;
        public Form1()
        {
            InitializeComponent();
            link = new Athlete_Clz(new Sql());
            load();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = link.GetAthletes();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            design();
            clear();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Insert_Click(object sender, EventArgs e)
        {
            if (id.SelectedIndex != -1)
            {
                MessageBox.Show("Please clear the form before inserting a new athlete.");
                id.SelectedIndex = -1;
                return;
            }

            if (string.IsNullOrWhiteSpace(name.Text) ||
                string.IsNullOrWhiteSpace(weight.Text) ||
                string.IsNullOrWhiteSpace(Height.Text) ||
                string.IsNullOrWhiteSpace(address.Text) ||
                string.IsNullOrWhiteSpace(NIC.Text) ||
                string.IsNullOrWhiteSpace(Contact.Text) ||
                caid.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }


            try
            {
                string namePattern = @"^[a-zA-Z\s]+$";
                if (!Regex.IsMatch(name.Text.Trim(), namePattern))
                {
                    MessageBox.Show("Please enter a valid name containing only letters and spaces.");
                    return;
                }

               

                string nicPattern = @"^[a-zA-Z0-9]+$";
                if (!Regex.IsMatch(NIC.Text.Trim(), nicPattern))
                {
                    MessageBox.Show("Please enter a valid NIC containing only letters and numbers.");
                    return;
                }

                if (!int.TryParse(weight.Text.Trim(), out int weightVal) || weightVal <= 0)
                {
                    MessageBox.Show("Please enter a valid positive number for Weight.");
                    return;
                }

                if (!int.TryParse(Height.Text.Trim(), out int heightVal) || heightVal <= 0)
                {
                    MessageBox.Show("Please enter a valid positive number for Height.");
                    return;
                }

                if (!int.TryParse(Contact.Text.Trim(), out int contactVal) || contactVal <= 0)
                {
                    MessageBox.Show("Please enter a valid positive number for Contact.");
                    return;
                }


               

                DateTime dob = dateTimePicker1.Value;
                if (dob >= DateTime.Today)
                {
                    MessageBox.Show("Date of Birth must be in the past.");
                    return;
                }


                string nameVal = name.Text.Trim();
                string addressVal = address.Text.Trim();
                string nicVal = NIC.Text.Trim();
                string selectedCategoryName = caid.SelectedItem.ToString(); 

                string query = "SELECT CategoryID FROM WeightCategory WHERE Name = @Name";
                SqlParameter[] categoryParams = {
            new SqlParameter("@Name", selectedCategoryName)
        };

                object result = link.db.ExecuteScalar(query, categoryParams);
                if (result == null)
                {
                    MessageBox.Show("Selected category name does not exist.");
                    return;
                }

                string selectedCategoryNamecategoryId = result.ToString(); 

                bool insertResult = link.InsertAthlete(nameVal, weightVal, heightVal, addressVal, nicVal, contactVal, dob, selectedCategoryName);

                if (insertResult)
                {
                    MessageBox.Show("Athlete added successfully.");
                    id.SelectedIndex = -1;
                    dataGridView1.DataSource = link.GetAthletes();
                    load();
                    clear();
                }
                else
                {
                    MessageBox.Show("Failed to add athlete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }

        private void Update_Click(object sender, EventArgs e)
        {
            // Ensure an Athlete ID is selected
            if (id.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an athlete to update.");
                return;
            }
            string selectedCategoryName = caid.SelectedItem.ToString();
            int athleteId = Convert.ToInt32(id.SelectedValue);



            // Validate required fields
            if (string.IsNullOrWhiteSpace(name.Text) ||
                string.IsNullOrWhiteSpace(weight.Text) ||
                string.IsNullOrWhiteSpace(Height.Text) ||
                string.IsNullOrWhiteSpace(address.Text) ||
                string.IsNullOrWhiteSpace(NIC.Text) ||
                string.IsNullOrWhiteSpace(Contact.Text) ||
                caid.SelectedIndex == -1)
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
                if (!Regex.IsMatch(NIC.Text.Trim(), nicPattern))
                {
                    MessageBox.Show("Please enter a valid NIC containing only letters and numbers.");
                    return;
                }

                // Parse numeric fields
                if (!int.TryParse(weight.Text.Trim(), out int weightVal) || weightVal <= 0)
                {
                    MessageBox.Show("Please enter a valid positive number for Weight.");
                    return;
                }

                if (!int.TryParse(Height.Text.Trim(), out int heightVal) || heightVal <= 0)
                {
                    MessageBox.Show("Please enter a valid positive number for Height.");
                    return;
                }

                if (!int.TryParse(Contact.Text.Trim(), out int contactVal) || contactVal <= 0)
                {
                    MessageBox.Show("Please enter a valid positive number for Contact.");
                    return;
                }

                // Validate Date of Birth
                DateTime dob = dateTimePicker1.Value;
                if (dob >= DateTime.Today)
                {
                    MessageBox.Show("Date of Birth must be in the past.");
                    return;
                }

                // Assign values
                string nameVal = name.Text.Trim();
                string addressVal = address.Text.Trim();
                string nicVal = NIC.Text.Trim();

                string query = "SELECT CategoryID FROM WeightCategory WHERE Name = @Name";
                SqlParameter[] categoryParams = {
            new SqlParameter("@Name", selectedCategoryName)
        };
                object result = link.db.ExecuteScalar(query, categoryParams);

                if (result == null)
                {
                    MessageBox.Show("Selected category name does not exist.");
                    return;
                }

                string categoryId = result.ToString();


                bool updateResult = link.UpdateAthlete(athleteId, nameVal, weightVal, heightVal, addressVal, nicVal, contactVal, dob, selectedCategoryName);

                if (updateResult)
                {
                    MessageBox.Show("Athlete updated successfully.");
                    dataGridView1.DataSource = link.GetAthletes();
                    load();
                    clear();
                }
                else
                {
                    MessageBox.Show("Failed to update athlete.");
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
                MessageBox.Show("Please pick an Athlete ID.");
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this athlete?",
                                                  "Delete Confirmation",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int athleteId = Convert.ToInt32(id.SelectedValue); 
                    link.DeleteAthlete(athleteId);
                    MessageBox.Show("Athlete successfully deleted!");

                    dataGridView1.DataSource = link.GetAthletes();
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

            dateTimePicker1.Value = DateTime.Now.Date;

            id.DropDownStyle = ComboBoxStyle.DropDownList;
            id.DataSource = link.GetAthleteIds();
            caid.DataSource = link.GetCategoryNames();
            id.SelectedIndex = -1;

            caid.DropDownStyle = ComboBoxStyle.DropDownList;
            caid.SelectedIndex = -1;




        }

        
     

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";

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
            var r = link.SearchAthletes(searchText);
           dataGridView1.DataSource = r;  
        }

        private void fill(int athleteId)
        {
            var athletes = link.GetAthletes();  

            var athlete = athletes.FirstOrDefault(a => a.AthleteID == athleteId);

            if (athlete != null)
            {
                id.SelectedItem = athlete.AthleteID;

                name.Text = athlete.Name;
                weight.Text = athlete.Weight.ToString();
                Height.Text = athlete.Height.ToString();
                address.Text = athlete.Address;
                NIC.Text = athlete.NIC;
                Contact.Text = athlete.Contact.ToString();
                caid.SelectedItem = athlete.CategoryName; 
                dateTimePicker1.Value = athlete.DateOfBirth;
            }
            else
            {
                MessageBox.Show("Athlete not found.");
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void clear()
        {
            name.Clear();
            weight.Clear();
            Height.Clear();
            address.Clear();
            NIC.Clear();
            Contact.Clear();
            caid.SelectedIndex = -1;
            id.SelectedIndex = -1;
        }


        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = link.GetAthletes();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void caid_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (!string.IsNullOrWhiteSpace(weight.Text))
            {
                int weight1;
                if (int.TryParse(weight.Text, out weight1))
                {
                    if (weight1 >= 0 && weight1 <= 66)
                        caid.SelectedIndex = caid.FindStringExact("Flyweight");
                    else if (weight1 >= 67 && weight1 <= 73)
                        caid.SelectedIndex = caid.FindStringExact("Lightweight");
                    else if (weight1 >= 74 && weight1 <= 81)
                        caid.SelectedIndex = caid.FindStringExact("Light-Middleweight");
                    else if (weight1 >= 82 && weight1 <= 90)
                        caid.SelectedIndex = caid.FindStringExact("Light-Heavyweight");
                    else if (weight1 >= 91 && weight1 <= 100)
                        caid.SelectedIndex = caid.FindStringExact("Middleweight");
                    else if (weight1 >= 101)
                        caid.SelectedIndex = caid.FindStringExact("Heavyweight");
                    else
                        MessageBox.Show("Invalid weight.");
                }
                else
                {
                    MessageBox.Show("Please enter a valid number for weight.");
                }
            }





        }





        private void weight_TextChanged(object sender, EventArgs e)
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
            label8.ForeColor = formTextColor;
            label9.ForeColor = formTextColor;
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
    }
}
