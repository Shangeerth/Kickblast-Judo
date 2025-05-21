using Programming_Assigment.Classes;
using Programming_Assigment.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Programming_Assigment.Database.AthleteTrainingPlan;
using static Programming_Assigment.payment;

namespace Programming_Assigment.Formss
{
    public partial class payout : Form 
    {

        

     

        public payout()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = -1;

           
            comboBox1.Items.Add("Cash");
            comboBox1.Items.Add("Card");
            total.Enabled = false;
            name.Enabled = false;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;



        }

        private void payout_Load(object sender, EventArgs e)
        {
            decimal totalFeeFromOtherForm = SharedData.TotalFee;

            total.Text = "Total Fee: Rs. " + totalFeeFromOtherForm.ToString("N2");


            string name1 = SharedData.name;
            name.Text = name1.ToString();



        }

        private void total_TextChanged(object sender, EventArgs e)
        {

        }

        private void load()
        {
            decimal totalFeeFromOtherForm = SharedData.TotalFee;
            string name1 = SharedData.name;




            string userInput = user.Text.Trim(); // Get input as string
            string method = comboBox1.SelectedIndex.ToString();

            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a payment method.");
                return;
            }

            if (decimal.TryParse(userInput, out decimal userrr))
            {
                // Ensure userrr has two decimal places
                userrr = decimal.Round(userrr, 2); // This will round like 100 → 100.00, 99.5 → 99.50

                // Optional: show it in a label or textbox in correct format
                user.Text = userrr.ToString("N2"); // Format with .00 if needed



                // Check for overpayment before proceeding
                if (userrr > totalFeeFromOtherForm)
                {
                    MessageBox.Show($"You are overpaying. Required: Rs. {totalFeeFromOtherForm.ToString("N2")}, You entered: Rs. {userrr.ToString("N2")}");
                    return;
                }


            }
            else
            {
                MessageBox.Show("Please enter a valid amount with up to two decimal places (e.g., 100.00)",
                                "Invalid Input",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }


            decimal total2 = userrr - totalFeeFromOtherForm;
            decimal balance = totalFeeFromOtherForm - userrr;

            string message = $"Are you sure you want to make the payment of Rs. {userrr.ToString("N2")} using {method}?";

            DialogResult result = MessageBox.Show(
                message,
                "Confirm Payment",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                MessageBox.Show("Payment successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Proceed with regular difference check
                decimal difference = userrr - totalFeeFromOtherForm;

                if (difference == 0)
                {
                    MessageBox.Show("Payment successful! No balance remaining.");
                }
                else if (difference < 0)
                {
                    
                    
                    MessageBox.Show($"Payment successful! Balance remaining for this month: Rs. {Math.Abs(difference).ToString("N2")}");
                  
                }

                PrintDocument pd = new PrintDocument();
                pd.DefaultPageSettings.PaperSize = new PaperSize("A5", 583, 827); // A5 size
                pd.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

                pd.Print();

                if(result == DialogResult.No)
                {                     MessageBox.Show("Payment cancelled.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }

            payment payment1 = new payment();
            this.Hide(); // Hide the current form

            payment1.Show();
        

        }

        private void user_TextChanged(object sender, EventArgs e)
        {

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            load();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {

            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("A5", 583, 827); // A5 size in hundredths of an inch (1 unit = 0.01 inch)

            Graphics g = e.Graphics;
            Rectangle marginBounds = e.MarginBounds;

            Font titleFont = new Font("Arial", 18, FontStyle.Bold);
            Font contentFont = new Font("Arial", 12);
            Font footerFont = new Font("Arial", 10, FontStyle.Italic);

            float y = marginBounds.Top;

            string selectedMethod = comboBox1.SelectedItem?.ToString();

            string userInput = user.Text.Trim(); 
            decimal.TryParse(userInput, out decimal userrr);

            // Logo (optional)
            try
            {
                Image logo = Image.FromFile("C:/Users/shang/Downloads/Simple AM Letter Logo (Logo).jpg");
                int logoWidth = 100;
                int logoHeight = (int)((float)logo.Height / logo.Width * logoWidth);
                int logoX = marginBounds.Left + (marginBounds.Width - logoWidth) / 2;
                g.DrawImage(logo, logoX, (int)y, logoWidth, logoHeight);
                y += logoHeight + 10;
                logo.Dispose();
            }
            catch { y += 413; }

            // Header
            g.DrawString("Kickblast Judo - Payment Receipt", titleFont, Brushes.Black, marginBounds.Left, y);
            y += titleFont.GetHeight(g) + 14;

            // Date/Time of Payment
            g.DrawString($"Date: {DateTime.UtcNow:dd-MM-yyyy hh:mm tt}", contentFont, Brushes.Black, marginBounds.Left, y);
            y += 25;

            // Customer Details
            g.DrawString($"Athlete: {SharedData.name}", contentFont, Brushes.Black, marginBounds.Left, y); y += 20;
            g.DrawString($"Payment Method: {selectedMethod}", contentFont, Brushes.Black, marginBounds.Left, y); y += 20;

            // Fee Breakdown
            g.DrawString($"Total Fee: Rs. {SharedData.TotalFee:N2}", contentFont, Brushes.Black, marginBounds.Left, y); y += 20;
            g.DrawString($"Paid Amount: Rs. {user.Text:N2}", contentFont, Brushes.Black, marginBounds.Left, y); y += 20;

            decimal difference = userrr - SharedData.TotalFee;
            if (difference == 0)
                g.DrawString("Balance: Rs. 0.00", contentFont, Brushes.Black, marginBounds.Left, y);
            else if (difference < 0)
                g.DrawString($"Balance Remaining: Rs. {Math.Abs(difference):N2}", contentFont, Brushes.Black, marginBounds.Left, y);
            else
                g.DrawString($"Change to Return: Rs. {difference:N2}", contentFont, Brushes.Black, marginBounds.Left, y);

            y += 40;

            // Footer
          
            g.DrawString("Thank you for your dedication!\nKickblast Judo © 2025", footerFont, Brushes.Gray, marginBounds.Left, y);
       
        }

        private void name_TextChanged(object sender, EventArgs e)
        {

        }

        private void user_TextChanged_1(object sender, EventArgs e)
        {
            decimal totalFeeFromOtherForm = SharedData.TotalFee;

            string userInput = user.Text.Trim(); // Get input as string

            decimal.TryParse(userInput, out decimal userrr);

            // Check for overpayment before proceeding
            if (userrr > totalFeeFromOtherForm)
            {
                MessageBox.Show($"You are overpaying. Required: Rs. {totalFeeFromOtherForm.ToString("N2")}, You entered: Rs. {userrr.ToString("N2")}");
                return;
            }
        }
    }
}
