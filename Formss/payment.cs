using Programming_Assigment.Classes;
using Programming_Assigment.Database;
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
using static Programming_Assigment.Program;

namespace Programming_Assigment
{
    public partial class payment : Form
    {

        private readonly paymentClz link;
        

        public payment()
        {
            InitializeComponent();
            link = new paymentClz(new Sql());
            load();
            clear();
            load1();

            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }


        private void load()
        {
            id.DataSource = link.GetAllAthleteNames();
            
        }

        private void id_SelectedIndexChanged(object sender, EventArgs e)
        {


            UpdateTotalPaymentDisplay();

            weight1();

        }

        private void payment_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MMMM yyyy";  // Month + Year
            dateTimePicker1.ShowUpDown = true;
        }

        private void weight1()
        {
            if (id.SelectedIndex == -1 || id.SelectedItem == null)
            {

                return;
            }

            string selectedName = id.SelectedItem.ToString();

            int maxWeight = link.GetMaxWeightByAthleteName(selectedName); // like 'man'
            int currentWeight = link.GetAthleteWeight(selectedName);      // like 'weight1'

            string weightCategory = link.GetCategoryWithMaxWeightByAthleteName(selectedName);

            /* if (weightCategory == "Heavyweight (<100)")
             {
                 max.Text = "ur idiot";
             }
             else
             {
                 int difference = maxWeight - currentWeight; // e.g., 66 - 56 = 10
                 int midpoint = maxWeight / 2;               // e.g., 66 / 2 = 33
                 string result;

                 if (difference > midpoint)
                 {
                     result = "-" + difference.ToString();   // Show as negative if above mid
                 }
                 else if (difference < midpoint)
                 {
                     result = "+" + difference.ToString();   // Show as positive if below mid
                 }
                 else
                 {
                     result = "0";
                 }

                 max.Text = result;
             }*/




            // Fetch category

            int difference = maxWeight - currentWeight;
            int midpoint = maxWeight / 2;
            string result;

            // Check category
            if (weightCategory != "Heavyweight (<100)")
            {


                // Debug log
                MessageBox.Show($"MaxWeight: {maxWeight}, CurrentWeight: {currentWeight}, Difference: {difference}, Midpoint: {midpoint}");

                if (difference > midpoint)
                {
                    result = "+" + difference.ToString();
                }
                else if (difference < midpoint)
                {
                    result = "-" + difference.ToString();

                }
                else
                {
                    result = "0";
                }

                max.Text = result;  

            }
            else
            {

                result = "+" + difference.ToString();
                max.Text = result;  



            }









        }




        private void UpdateTotalPaymentDisplay()
        {
            if (id.SelectedItem == null)
            {
                id.Text = "Select an athlete";
                return;
            }

            string selectedName = id.SelectedItem.ToString();
            SharedData.name = selectedName;  // Store decimal value here

            DateTime selectedDate = dateTimePicker1.Value;

            // --- Private Coaching Fee ---
            decimal privateCoachingFee = link.GetTotalPrivateCoachingPaymentForMonth(selectedName, selectedDate);
            if (privateCoachingFee > 0)
                privatefee.Text = "Rs. " + privateCoachingFee.ToString("N2");
            else
                privatefee.Text = "No coaching payments found.";

            // --- Competition Fee ---
            decimal competitionFee = link.GetTotalCompetitionFeeForMonth(selectedName, selectedDate);
            if (competitionFee > 0)
                compfee.Text = "Rs. " + competitionFee.ToString("N2");
            else
                compfee.Text = "No competition fee found.";

            // --- Training Plan Fee ---
            decimal trainingPlanFee = link.GetTotalTrainingPlanPaymentForMonthByAthleteName(selectedName, selectedDate);
            if (trainingPlanFee > 0)
                trainingplanfee.Text = "Rs. " + trainingPlanFee.ToString("N2");
            else
                trainingplanfee.Text = "No training payments found.";

            // --- Total Payment ---
            decimal totalFee = privateCoachingFee + competitionFee + trainingPlanFee;
            SharedData.TotalFee = totalFee;  // Store decimal value here

            if (totalFee > 0)
                totalvalue.Text = "Rs. " + totalFee.ToString("N2");
            else
                totalvalue.Text = "No payments found.";



            // --- Coach Name ---
            string coachName = link.GetTrainerNameFromPrivateCoaching(selectedName, selectedDate);
            trainer.Text = !string.IsNullOrEmpty(coachName) ? coachName : "No coach selected month.";

            // --- Weight Category ---

            string weightCategory = link.GetCategoryWithMaxWeightByAthleteName(selectedName);
            weight.Text = !string.IsNullOrEmpty(weightCategory) ? weightCategory : "No weight category found.";



          





            // --- Training Plan Name ---
            string trainingPlan = link.GetTrainingPlanNameByAthleteName(selectedName);
            planname.Text = !string.IsNullOrEmpty(trainingPlan) ? trainingPlan : "No training plan found.";






            /*string selectedName2 = id.SelectedItem.ToString();
           DateTime selectedDate2 = dateTimePicker1.Value;

           decimal totalTrainingPayment = link.GetTotalTrainingPlanPaymentForMonthByAthleteName(selectedName2, selectedDate2);

           if (totalTrainingPayment > 0)
               trainingplanfee.Text = "₹ " + totalTrainingPayment.ToString("N2");
           else
               trainingplanfee.Text = "No training payments found.";*/


        }


        public static class SharedData
        {
            public static decimal TotalFee { get; set; }
            public static string name { get; set; }
        }


        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            UpdateTotalPaymentDisplay();
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            clear();
        }

       



        private void clear()
        {
            max.Clear();
            id.SelectedIndex = -1;
            privatefee.Clear();
            compfee.Clear();
            trainingplanfee.Clear();
            totalvalue.Clear();
            trainer.Clear();
            weight.Clear();
            planname.Clear();
            dateTimePicker1.Value = DateTime.Now; // Reset to current date
        }

        private void PDF_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (id.SelectedItem == null)
            {
                MessageBox.Show("Please select an athlete before printing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                // Get selected athlete name safely
                string athleteName = id.SelectedItem != null ? id.SelectedItem.ToString() : "No athlete selected";


                Graphics g = e.Graphics;

                Rectangle marginBounds = e.MarginBounds;
                int pageWidth = marginBounds.Width;
                int pageHeight = marginBounds.Height;

                // Load logo from file
                Image logo = Image.FromFile("C:/Users/shang/Downloads/Simple AM Letter Logo (Logo).jpg");

                int logoMaxWidth = 200;
                int logoWidth = logo.Width > logoMaxWidth ? logoMaxWidth : logo.Width;
                int logoHeight = (int)((float)logo.Height / logo.Width * logoWidth);

                int logoX = marginBounds.Left + (pageWidth - logoWidth) / 2;
                int logoY = marginBounds.Top - 50;

                g.DrawImage(logo, logoX, logoY, logoWidth, logoHeight);

                Font headerFont = new Font("Arial", 24, FontStyle.Bold);
                Font contentFont = new Font("Arial", 16);
                Font footerFont = new Font("Arial", 12, FontStyle.Italic);

                string headerText = $"Kickblast Judo  {athleteName}  Payment Details";
                SizeF headerSize = g.MeasureString(headerText, headerFont);
                float headerX = marginBounds.Left + (pageWidth - headerSize.Width) / 2;
                float headerY = logoY + logoHeight + 20;

                g.DrawString(headerText, headerFont, Brushes.Black, headerX, headerY);

                float contentStartY = headerY + headerSize.Height + 40;
                string paymentDateText = dateTimePicker1.Value.ToString("MMMM yyyy");


                // Prepare lines separately
                string[] contentLines = new string[]
                {
                $"Athlete Name: {athleteName}",
                $"Payment Month: {paymentDateText}",
                $"Training Plan Fee: {trainingplanfee.Text}",
                $"Private Coaching Fee: {privatefee.Text}",
                $"Competition Fee: {compfee.Text}",
                $"Coach Name: {trainer.Text}",
                $"Weight Category: {weight.Text}",
                $"Training Plan: {planname.Text}"
                };

                // Set your desired line spacing here (in pixels)
                float lineSpacing = contentFont.GetHeight(g) + 10; // 8 pixels extra spacing

                float currentY = contentStartY;
                foreach (string line in contentLines)
                {
                    g.DrawString(line, contentFont, Brushes.Black, marginBounds.Left, currentY);
                    currentY += lineSpacing;
                }

                string footerText = "Thank you for your dedication!";
                SizeF footerSize = g.MeasureString(footerText, footerFont);
                float footerX = marginBounds.Left + (pageWidth - footerSize.Width) / 2;
                float footerY = marginBounds.Top + pageHeight - footerSize.Height - 30;

                g.DrawString(footerText, footerFont, Brushes.Gray, footerX, footerY);

                logo.Dispose();

            }

        }

        private void weight_TextChanged(object sender, EventArgs e)
        {

        }

        private void totalvalue_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (id.SelectedItem == null || SharedData.TotalFee <= 0)
            {
                MessageBox.Show("Please select an athlete and ensure the total fee is greater than zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            else
            {
                payout pay = new payout();
                pay.Show();
                this.Hide();
            }



        }

        private void load1()
        {
            id.SelectedIndex = -1;
            totalvalue.Enabled = false;
            privatefee.Enabled = false;
            compfee.Enabled = false;
            trainingplanfee.Enabled = false;
            trainer.Enabled = false;
            weight.Enabled = false;
            planname.Enabled = false;
            id.Enabled = true;
            dateTimePicker1.Enabled = true;
            max.Enabled = false;
            id.DropDownStyle = ComboBoxStyle.DropDownList;


        }

        private void Athlete_Click(object sender, EventArgs e)
        {
            Form1 newForm = new Form1();
            NavigationManager.OpenForm(this, newForm);
        }

        private void button6_Click(object sender, EventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
        {
            TrainningPlan newForm = new TrainningPlan();
            NavigationManager.OpenForm(this, newForm);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Competition newForm = new Competition();
            NavigationManager.OpenForm(this, newForm);


        }

        private void Athlete_Competition_Click(object sender, EventArgs e)
        {
            CompetitionAthlete newForm = new CompetitionAthlete();
            NavigationManager.OpenForm(this, newForm);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Weightcategoryy newForm = new Weightcategoryy();
            NavigationManager.OpenForm(this, newForm);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            button2.Enabled = false;
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




