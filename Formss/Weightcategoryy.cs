using Programming_Assigment.Classes;
using Programming_Assigment.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Programming_Assigment.Program;

namespace Programming_Assigment.Formss
{
    public partial class Weightcategoryy : Form
    {
        private readonly weightclz link;

        public Weightcategoryy()
        {
            InitializeComponent();
            link = new weightclz(new Sql());
            dataGridView1.DataSource = link.GetWeightCategories();
            load();
            clear();

            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;



        }

        private void Insert_Click(object sender, EventArgs e)
        {
            if (id != null && id.SelectedIndex != -1)
            {
                MessageBox.Show("Please clear the form before inserting a new weight category.");
                id.SelectedIndex = -1;
                return;
            }

            if (string.IsNullOrWhiteSpace(Nametxt.Text) ||
                string.IsNullOrWhiteSpace(mintxt.Text) ||
                string.IsNullOrWhiteSpace(maxtxt.Text))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            try
            {
                if (!int.TryParse(mintxt.Text.Trim(), out int minWeight))
                {
                    MessageBox.Show("Please enter a valid number for Minimum Weight.");
                    return;
                }

                int? maxWeight = null;
                if (!string.IsNullOrWhiteSpace(maxtxt.Text))
                {
                    if (int.TryParse(maxtxt.Text.Trim(), out int parsedMax))
                    {
                        maxWeight = parsedMax;
                        if (maxWeight < minWeight)
                        {
                            MessageBox.Show("Maximum Weight cannot be less than Minimum Weight.");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid number for Maximum Weight.");
                        return;
                    }
                }

                this.Name = "Weightcategory"; // Now safe, refers to the form's name
                this.Text = "Weightcategory"; // This is okay, sets the form's title




             

                string namePattern = @"^[a-zA-Z\s]+$";
                if (!Regex.IsMatch(Nametxt.Text.Trim(), namePattern))
                {
                    MessageBox.Show("Please enter a valid name containing only letters and spaces.");
                    return;
                }

              
                if (link.IsWeightCategoryNameUnique(namePattern))
                {
                    MessageBox.Show("This weight category name already exists. Please use a different name.", "Duplicate Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }







                bool insertResult = link.InsertWeightCategory(namePattern, minWeight, maxWeight);


                if (insertResult)
                {
                    MessageBox.Show("Weight category added successfully.");
                    id.SelectedIndex = -1;
                    dataGridView1.DataSource = link.GetWeightCategories();
                    load();
                    clear();
                }
                else
                {
                    MessageBox.Show("Failed to add weight category.");
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
                MessageBox.Show("Please select a weight category to update.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Nametxt.Text) ||
                string.IsNullOrWhiteSpace(mintxt.Text))
            {
                MessageBox.Show("Please fill in all required fields (Name and Minimum Weight).");
                return;
            }

            try
            {



                string namePattern = @"^[a-zA-Z\s]+$";
                if (!Regex.IsMatch(Nametxt.Text.Trim(), namePattern))
                {
                    MessageBox.Show("Please enter a valid name containing only letters and spaces.");
                    return;
                }


                if (!int.TryParse(mintxt.Text.Trim(), out int minWeight))
                {
                    MessageBox.Show("Please enter a valid number for Minimum Weight.");
                    return;
                }

                // Get selected CategoryID from id control (e.g., ComboBox or ListBox)
                if (!int.TryParse(id.SelectedValue.ToString(), out int categoryId))
                {
                    MessageBox.Show("Invalid weight category selected.");
                    return;
                }


                if (!link.IsWeightCategoryNameUnique(namePattern, categoryId))
                {
                    MessageBox.Show("This weight category name already exists.", "Duplicate Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }



                int? maxWeight = null;
                if (!string.IsNullOrWhiteSpace(maxtxt.Text))
                {
                    if (int.TryParse(maxtxt.Text.Trim(), out int parsedMax))
                    {
                        maxWeight = parsedMax;

                        if (maxWeight < minWeight)
                        {
                            MessageBox.Show("Maximum Weight cannot be less than Minimum Weight.");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid number for Maximum Weight.");
                        return;
                    }
                }

                

                bool updateResult = link.UpdateWeightCategory(categoryId, namePattern, minWeight, maxWeight);

                if (updateResult)
                {
                    MessageBox.Show("Weight category updated successfully.");
                    id.SelectedIndex = -1;
                    dataGridView1.DataSource = link.GetWeightCategories();
                    clear();
                    load();
                }
                else
                {
                    MessageBox.Show("Failed to update weight category.");
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
                MessageBox.Show("Please pick an WeightCategory ID.");
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this WeightCategory?",
                                                  "Delete Confirmation",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int WeightCategory = Convert.ToInt32(id.SelectedValue);
                    link.DeleteWeightCategory(WeightCategory);
                    MessageBox.Show("WeightCategory successfully deleted!");

                    dataGridView1.DataSource = link.GetWeightCategories();
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
            id.DataSource = link.GetCategoryIds();
            id.SelectedIndex = -1;

        }
        private void clear()
        {
            Nametxt.Clear();
            mintxt.Clear();
            maxtxt.Clear();
            id.SelectedIndex = -1;


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox1.Text;
            var r = link.SearchWeightCategories(searchText);
            dataGridView1.DataSource = r;

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
            var co = link.GetWeightCategories();

            var WeightCategory = co.FirstOrDefault(a => a.CategoryID == coa);

            if (WeightCategory != null)
            {
                id.SelectedItem = WeightCategory.CategoryID;

                Nametxt.Text = WeightCategory.Name.ToString();
                maxtxt.Text = WeightCategory.MaxWeight.ToString();
                mintxt.Text = WeightCategory.MinWeight.ToString();


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

        private void Athlete_Click(object sender, EventArgs e)
        {
            Form1 newForm = new Form1();
            NavigationManager.OpenForm(this, newForm);

        }

        private void button2_Click(object sender, EventArgs e)
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
            button1.Enabled = false;
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Nametxt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
