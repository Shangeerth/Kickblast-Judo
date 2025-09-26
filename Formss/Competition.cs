using Programming_Assigment.Classes;
using Programming_Assigment.Database;
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
using System.Xml.Linq;
using static Programming_Assigment.Program;
using static System.Windows.Forms.LinkLabel;

namespace Programming_Assigment.Formss
{
    public partial class Competition : Form
    {
       
        private readonly competitionClz link;
        public Competition()
        {
            InitializeComponent();
            link = new competitionClz(new Sql());
            dataGridView1.DataSource = link.GetCompetitions();
            load();
            clear();
            design();

            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;


        }

        private void Insert_Click(object sender, EventArgs e)
        {
            if (id != null && id.SelectedIndex != -1)
            {
                MessageBox.Show("Please clear the form before inserting a new competition.");
                id.SelectedIndex = -1;
                return;
            }

            if (string.IsNullOrWhiteSpace(dateTimePicker1.Text) ||
                string.IsNullOrWhiteSpace(Time.Text) ||
                string.IsNullOrWhiteSpace(Feee.Text) ||
                string.IsNullOrWhiteSpace(name.Text))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            try
            {
              

                if (!DateTime.TryParse(dateTimePicker1.Text.Trim(), out DateTime competitionDate))
                {
                    MessageBox.Show("Please enter a valid competition date.");
                    return;
                }

                if (!(competitionDate.DayOfWeek == DayOfWeek.Saturday && competitionDate.Day >= 8 && competitionDate.Day <= 14))
                {
                    MessageBox.Show("Competition can only be scheduled on the 2nd Saturday of the month.");
                    return;
                }
                

                string competitionTime = Time.Text.Trim();
                string competitionName = name.Text.Trim();

                if (!decimal.TryParse(Feee.Text.Trim(), out decimal competitionFee) || competitionFee <= 0)
                {
                    MessageBox.Show("Please enter a valid competition fee.");
                    return;
                }

                // ✅ Insert logic here
                bool insertResult = link.InsertCompetition(competitionDate, competitionTime, competitionFee, competitionName);

                if (insertResult)
                {
                    MessageBox.Show("Competition added successfully.");
                    id.SelectedIndex = -1;
                    dataGridView1.DataSource = link.GetCompetitions();
                    load();
                    clear();
                }
                else
                {
                    MessageBox.Show("Failed to add competition.");
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
                MessageBox.Show("Please select a competition to update.");
                return;
            }

            if (string.IsNullOrWhiteSpace(dateTimePicker1.Text) ||
                string.IsNullOrWhiteSpace(Time.Text) ||
                string.IsNullOrWhiteSpace(Feee.Text) ||
                string.IsNullOrWhiteSpace(name.Text))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            try
            {
                string competitionName = name.Text.Trim();
                if (!int.TryParse(id.SelectedValue.ToString(), out int competitionId))
                {
                    MessageBox.Show("Invalid competition selection.");
                    return;
                }

                if (!DateTime.TryParse(dateTimePicker1.Text.Trim(), out DateTime competitionDate))
                {
                    MessageBox.Show("Please enter a valid competition date.");
                    return;
                }

                if (!(competitionDate.DayOfWeek == DayOfWeek.Saturday && competitionDate.Day >= 8 && competitionDate.Day <= 14))
                {
                    MessageBox.Show("Competition can only be scheduled on the 2nd Saturday of the month.");
                    return;
                }

                if (!link.DoesCompetitionNameExist(competitionName, competitionId))
                {
                    MessageBox.Show("This competition name already exists.", "Duplicate Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string competitionTime = Time.Text.Trim();
             

                if (!decimal.TryParse(Feee.Text.Trim(), out decimal competitionFee) || competitionFee <= 0)
                {
                    MessageBox.Show("Please enter a valid competition fee.");
                    return;
                }

             

                bool updateResult = link.UpdateCompetition(competitionId, competitionDate, competitionTime, competitionFee, competitionName);

                if (updateResult)
                {
                    MessageBox.Show("Competition updated successfully.");
                    id.SelectedIndex = -1;
                    dataGridView1.DataSource = link.GetCompetitions();
                    load();
                    clear();
                }
                else
                {
                    MessageBox.Show("Failed to update competition.");
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
                MessageBox.Show("Please pick a Competition ID.");
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this Competition?",
                                                  "Delete Confirmation",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int competitionId = Convert.ToInt32(id.SelectedValue);
                    link.DeleteCompetition(competitionId);
                    MessageBox.Show("Competition successfully deleted!");

                    dataGridView1.DataSource = link.GetCompetitions();
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
            id.DataSource = link.GetCompetitionIDs();
            dataGridView1.DataSource = link.GetCompetitions();
            id.DropDownStyle = ComboBoxStyle.DropDownList;
            id.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
            Time.Text = DateTime.Now.ToString("hh:mm tt"); 


        }


        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
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

        private void fill(int competitionId)
        {
            var competitions = link.GetCompetitions();

            var competition = competitions.FirstOrDefault(c => c.CompetitionID == competitionId);

            if (competition != null)
            {
                id.SelectedItem = competition.CompetitionID;

                Feee.Text = competition.CompetitionFee.ToString();
                dateTimePicker1.Value = competition.CompetitionDate;
                Time.Text = competition.CompetitionTime;
                name.Text = competition.CompetitionName;
            }
            else
            {
                MessageBox.Show("Competition not found.");
            }
        }


        private void clear()
        {
            id.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
            Time.Text = DateTime.Now.ToString("hh:mm tt"); 
            Time.Clear();
            Feee.Clear();
            name.Clear();
            
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox1.Text;
            var r = link.SearchCompetitions(searchText);
            dataGridView1.DataSource = r;
        }

        private void Time_TextChanged(object sender, EventArgs e)
        {
            Time.Text = DateTime.Now.ToString("hh:mm tt"); // e.g., "02:35 PM"

        }

        private void Competition_Load(object sender, EventArgs e)
        {

        }

        private void name_TextChanged(object sender, EventArgs e)
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
