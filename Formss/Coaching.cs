using Programming_Assigment.Athlete;
using Programming_Assigment.Classes;
using Programming_Assigment.Formss;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static Programming_Assigment.Program;

namespace Programming_Assigment.Database
{
    public partial class Coaching : Form
    {
        private readonly @private link;
        public Coaching()
        {
            InitializeComponent();
            link = new @private(new Sql());
            dataGridView1.DataSource = link.GetPrivateCoachings();
            load();
            clear();
            dateTimePicker1.Value = DateTime.Now;

            Feee.Text = "90.50";


            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;


        }

        private void Insert_Click(object sender, EventArgs e)
        {
            if (id != null && id.SelectedIndex != -1)
            {
                MessageBox.Show("Please clear the form before inserting a new private coaching record.");
                id.SelectedIndex = -1;
                return;
            }

            if (string.IsNullOrWhiteSpace(dateTimePicker1.Text) ||
                string.IsNullOrWhiteSpace(hrs.Text) ||
                string.IsNullOrWhiteSpace(aid.Text) ||
                string.IsNullOrWhiteSpace(tid.Text))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            try
            {
                if (!DateTime.TryParse(dateTimePicker1.Text.Trim(), out DateTime coachingDate))
                {
                    MessageBox.Show("Please enter a valid coaching date.");
                    return;
                }

                if (!int.TryParse(hrs.Text.Trim(), out int hoursPerWeek) || hoursPerWeek <= 0 || hoursPerWeek > 5)
                {
                    MessageBox.Show("Please enter a valid number of hours per week (1 to 5).");
                    return;
                }

                if (!int.TryParse(aid.SelectedValue.ToString(), out int athleteId))
                {
                    MessageBox.Show("Please select a valid athlete.");
                    return;
                }

                if (!int.TryParse(tid.SelectedValue.ToString(), out int trainerId))
                {
                    MessageBox.Show("Please select a valid trainer.");
                    return;
                }


                int totalHoursThisWeek = link.GetTotalHoursForAthleteInWeek(athleteId, coachingDate);

                if (totalHoursThisWeek + hoursPerWeek > 5)
                {
                    MessageBox.Show($"Athlete already has {totalHoursThisWeek} coaching hours booked this week. Cannot exceed 5 hours per week.");
                    return;
                }

                decimal feesPerHour;

                if (!decimal.TryParse(Feee.Text.Trim(), out feesPerHour))
                {
                    MessageBox.Show("Please enter a valid 2 decimal number for the fee.");
                    return;
                }
                bool insertResult = link.InsertPrivateCoaching(coachingDate, hoursPerWeek, feesPerHour, athleteId, trainerId);

                if (insertResult)
                {
                    MessageBox.Show("Private coaching record added successfully.");
                    id.SelectedIndex = -1;
                    dataGridView1.DataSource = link.GetPrivateCoachings();
                    load();
                    clear();
                }
                else
                {
                    MessageBox.Show("Failed to add private coaching record.");
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
                MessageBox.Show("Please select a private coaching record to update.");
                return;
            }

            if (string.IsNullOrWhiteSpace(dateTimePicker1.Text) ||
                string.IsNullOrWhiteSpace(hrs.Text) ||
                string.IsNullOrWhiteSpace(aid.Text) ||
                string.IsNullOrWhiteSpace(tid.Text))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            try
            {
                if (!int.TryParse(id.SelectedValue.ToString(), out int privateCoachingId))
                {
                    MessageBox.Show("Invalid Private Coaching ID.");
                    return;
                }

                if (!DateTime.TryParse(dateTimePicker1.Text.Trim(), out DateTime coachingDate))
                {
                    MessageBox.Show("Please enter a valid coaching date.");
                    return;
                }

                if (!int.TryParse(hrs.Text.Trim(), out int hoursPerWeek) || hoursPerWeek <= 0 || hoursPerWeek > 5)
                {
                    MessageBox.Show("Please enter a valid number of hours per week (1 to 5).");
                    return;
                }

                if (!int.TryParse(aid.SelectedValue.ToString(), out int athleteId))
                {
                    MessageBox.Show("Please select a valid athlete.");
                    return;
                }

                if (!int.TryParse(tid.SelectedValue.ToString(), out int trainerId))
                {
                    MessageBox.Show("Please select a valid trainer.");
                    return;
                }

                // Calculate total hours in week excluding the current record's hours
                int totalHoursThisWeekExcludingCurrent = link.GetTotalHoursForAthleteInWeek(athleteId, coachingDate, privateCoachingId);

                if (totalHoursThisWeekExcludingCurrent + hoursPerWeek > 5)
                {
                    MessageBox.Show($"Athlete already has {totalHoursThisWeekExcludingCurrent} coaching hours booked this week excluding this record. Cannot exceed 5 hours per week.");
                    return;
                }

                decimal feesPerHour;

                if (!decimal.TryParse(Feee.Text.Trim(), out feesPerHour))
                {
                    MessageBox.Show("Please enter a valid 2  decimal number for the fee.");
                    return; 
                }


                bool updateResult = link.UpdatePrivateCoaching(privateCoachingId, coachingDate, hoursPerWeek, feesPerHour, athleteId, trainerId);

                if (updateResult)
                {
                    MessageBox.Show("Private coaching record updated successfully.");
                    id.SelectedIndex = -1;
                    dataGridView1.DataSource = link.GetPrivateCoachings();
                    load();
                    clear();
                }
                else
                {
                    MessageBox.Show("Failed to update private coaching record.");
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

            DialogResult result = MessageBox.Show("Are you sure you want to delete this Coaching Session?",
                                                  "Delete Confirmation",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int session = Convert.ToInt32(id.SelectedValue);
                    link.DeletePrivateCoaching(session);
                    MessageBox.Show("Coaching Session successfully deleted!");

                    dataGridView1.DataSource = link.GetPrivateCoachings();
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


            aid.DataSource=link.GetAthleteIds();
            tid.DataSource=link.GetTrainerIds();
            id.DropDownStyle = ComboBoxStyle.DropDownList;
            aid.DropDownStyle = ComboBoxStyle.DropDownList;
            tid.DropDownStyle = ComboBoxStyle.DropDownList;
            hrs.DropDownStyle = ComboBoxStyle.DropDownList;
            id.DataSource = link.GetTrainerIds();
            id.SelectedIndex = -1;
            aid.SelectedIndex = -1;
            tid.SelectedIndex = -1;
            DateTime now = DateTime.Now;
            dateTimePicker1.Value = DateTime.Now;
            aname.Enabled = false;
            tname.Enabled = false;

        }

        private void tid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tid.SelectedValue != null)
            {
                int selectedTrainerId = Convert.ToInt32(tid.SelectedValue);

                
                string trainerName = link.GetTrainerNameById(selectedTrainerId);

                tname.Text = trainerName;
            }
        }

        private void aid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (aid.SelectedValue != null)
            {
                int selectedAthleteId = Convert.ToInt32(aid.SelectedValue);

                
                string athleteName = link.GetAthleteNameById(selectedAthleteId);

                aname.Text = athleteName;
            }
     
        
        }

        private void clear()
        {
            Feee.Clear();
            tname.Clear();
            aname.Clear();
           id.SelectedIndex = -1;
           aid.SelectedIndex = -1;
           tid.SelectedIndex = -1;
           hrs.SelectedIndex = -1;
           dateTimePicker1.Value = DateTime.Now;

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
            var r = link.SearchPrivateCoachings(searchText);
            dataGridView1.DataSource = r;
        }

        private void fill(int coa)
        {
            var co = link.GetPrivateCoachings();  

            var coaching = co.FirstOrDefault(a => a.PrivateCoachingID == coa);

            if (coaching != null)
            {
                id.SelectedItem = coaching.PrivateCoachingID;

                Feee.Text = coaching.FeesPerHour.ToString();
                dateTimePicker1.Text = coaching.CoachingDate.ToString();
                aid.Text = coaching.AthleteID.ToString();
                tid.Text = coaching.TrainerID.ToString();
                hrs.Text = coaching.HoursPerWeek.ToString();
                


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

        private void Coaching_Load(object sender, EventArgs e)
        {

        }

        private void Feee_TextChanged(object sender, EventArgs e)
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
