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
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

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

                if (link.IsAthleteNameUnique(namePattern))
                {
                    MessageBox.Show("This athlete name already exists. Try entering Full Name", "Duplicate Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                if (!link.IsAthleteNameUnique(namePattern, athleteId))
                {
                    MessageBox.Show("This athlete name already exists. Try entering Full Name", "Duplicate Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            var athletes = link.GetAthletes();  // Assume this returns a list of athlete records

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

        private void Back_Click(object sender, EventArgs e)
        {
            NavigationManager.GoBack();
            this.Close();
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            Welcome wel = new Welcome();
            wel.Show();
            this.Close();
        }
    }
}
