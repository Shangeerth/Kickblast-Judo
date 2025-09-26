using iTextSharp.text;
using iTextSharp.text.pdf;
using Programming_Assigment.Classes;
using Programming_Assigment.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Programming_Assigment.Database.AthleteTrainingPlan;
using static Programming_Assigment.payment;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Programming_Assigment.Formss
{
    public partial class payout : Form 
    {

        

     

        public payout()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = -1;
            design();

            this.FormBorderStyle = FormBorderStyle.None;
            this.Opacity = 1.0;
            this.TransparencyKey = Color.Empty; // or don’t set it at all

            comboBox1.Items.Add("Cash");
            comboBox1.Items.Add("Card");
            total.Enabled = false;
            name.Enabled = false;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.StartPosition = FormStartPosition.CenterScreen;
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
            design();




            string userInput = user.Text.Trim(); // Get input as string
            string method = comboBox1.Text.ToString();

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

                GeneratePaymentReceiptPDFWithQR();

               

                if(result == DialogResult.No)
                {                     MessageBox.Show("Payment cancelled.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }

       
            this.Close(); 
           
        

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
        public void GeneratePaymentReceiptPDFWithQR()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Save PDF";
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.FileName = "PaymentReceipt.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    // Reduced margins for better space usage
                    Document pdfDoc = new Document(PageSize.A5, 20, 20, 20, 20);

                    try
                    {
                        using (FileStream stream = new FileStream(filePath, FileMode.Create))
                        {
                            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                            pdfDoc.Open();

                            // Add logo image if exists
                            string logoPath = @"C:/Users/shang/Downloads/Simple AM Letter Logo (Logo).jpg";
                            if (File.Exists(logoPath))
                            {
                                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                                logo.Alignment = Element.ALIGN_CENTER;
                                logo.ScaleToFit(100f, 100f);
                                pdfDoc.Add(logo);
                                pdfDoc.Add(new Paragraph("\n")); // spacer
                            }

                            // Fonts
                            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.BLACK);
                            var contentFont = FontFactory.GetFont(FontFactory.HELVETICA, 11, BaseColor.BLACK);  // slightly smaller
                            var footerFont = FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 10, BaseColor.GRAY);

                            // Header
                            Paragraph header = new Paragraph("Kickblast Judo - Payment Receipt", titleFont)
                            {
                                Alignment = Element.ALIGN_CENTER,
                                SpacingAfter = 10f
                            };
                            pdfDoc.Add(header);

                            // Date/Time
                            Paragraph dateTime = new Paragraph($"Date: {DateTime.UtcNow.ToString("dd-MM-yyyy hh:mm tt")}", contentFont)
                            {
                                SpacingAfter = 15f
                            };
                            pdfDoc.Add(dateTime);

                            // Payment info from UI or data
                            string selectedMethod = comboBox1.SelectedItem?.ToString() ?? "N/A";
                            decimal.TryParse(user.Text.Trim(), out decimal userrr);

                            // Info table: label/value two columns with borders and header color
                            PdfPTable infoTable = new PdfPTable(2)
                            {
                                WidthPercentage = 100,
                                SpacingAfter = 15f
                            };
                            infoTable.SetWidths(new float[] { 1f, 2f });

                            void AddInfoRow(string label, string value, bool isHeader = false)
                            {
                                var borderColor = BaseColor.BLACK;

                                PdfPCell labelCell = new PdfPCell(new Phrase(label, contentFont))
                                {
                                    Border = PdfPCell.BOX,
                                    PaddingBottom = 6f,
                                    BackgroundColor = isHeader ? new BaseColor(230, 230, 250) : BaseColor.WHITE,
                                    BorderColor = borderColor,
                                    HorizontalAlignment = Element.ALIGN_LEFT,
                                    VerticalAlignment = Element.ALIGN_MIDDLE
                                };

                                PdfPCell valueCell = new PdfPCell(new Phrase(value, contentFont))
                                {
                                    Border = PdfPCell.BOX,
                                    PaddingBottom = 6f,
                                    BackgroundColor = BaseColor.WHITE,
                                    BorderColor = borderColor,
                                    HorizontalAlignment = Element.ALIGN_LEFT,
                                    VerticalAlignment = Element.ALIGN_MIDDLE
                                };

                                infoTable.AddCell(labelCell);
                                infoTable.AddCell(valueCell);
                            }

                            AddInfoRow("Description", "Details", true);  // header row
                            AddInfoRow("Athlete:", SharedData.name);
                            AddInfoRow("Payment Method:", selectedMethod);
                            AddInfoRow("Total Fee:", $"Rs. {SharedData.TotalFee:N2}");
                            AddInfoRow("Paid Amount:", $"Rs. {userrr:N2}");

                            decimal difference = userrr - SharedData.TotalFee;
                            if (difference == 0)
                                AddInfoRow("Balance:", "Rs. 0.00");
                            else if (difference < 0)
                                AddInfoRow("Balance Remaining:", $"Rs. {Math.Abs(difference):N2}");
                            else
                                AddInfoRow("Change to Return:", $"Rs. {difference:N2}");

                            pdfDoc.Add(infoTable);

                            // Generate QR content string
                            string receiptNumber = "RCPT-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                            string qrContent = $"Kickblast Judo Payment Receipt | Receipt No: {receiptNumber} | Athlete: {SharedData.name} " +
                                $"| Total: Rs. {SharedData.TotalFee:N2} | Paid: Rs. {userrr:N2} | Date: {DateTime.UtcNow:dd-MM-yyyy}";

                            string encodedQR = Uri.EscapeDataString(qrContent);
                            string qrUrl = $"https://api.qrserver.com/v1/create-qr-code/?size=150x150&data={encodedQR}";

                            iTextSharp.text.Image qrImage = null;
                            using (WebClient wc = new WebClient())
                            {
                                byte[] qrBytes = wc.DownloadData(qrUrl);
                                using (MemoryStream ms = new MemoryStream(qrBytes))
                                {
                                    qrImage = iTextSharp.text.Image.GetInstance(ms);
                                }
                            }

                            qrImage.ScaleToFit(150f, 150f);
                            qrImage.Alignment = Element.ALIGN_CENTER;

                            // Bottom table with QR code and signature with borders
                            PdfPTable bottomTable = new PdfPTable(2)
                            {
                                WidthPercentage = 100,
                                SpacingBefore = 20f
                            };
                            bottomTable.SetWidths(new float[] { 1f, 2f });

                            PdfPCell qrCell = new PdfPCell(qrImage)
                            {
                                Border = PdfPCell.BOX,
                                BorderColor = BaseColor.BLACK,
                                HorizontalAlignment = Element.ALIGN_CENTER,
                                VerticalAlignment = Element.ALIGN_MIDDLE,
                                Padding = 5
                            };

                            PdfPCell signCell = new PdfPCell(new Phrase("Authorized by: ____________________\n(Admin Signature)", contentFont))
                            {
                                Border = PdfPCell.BOX,
                                BorderColor = BaseColor.BLACK,
                                HorizontalAlignment = Element.ALIGN_RIGHT,
                                VerticalAlignment = Element.ALIGN_MIDDLE,
                                PaddingTop = 20
                            };

                            bottomTable.AddCell(qrCell);
                            bottomTable.AddCell(signCell);

                            pdfDoc.Add(bottomTable);

                            // Footer
                            Paragraph footer = new Paragraph("Thank you for your dedication!\nKickblast Judo © 2025", footerFont)
                            {
                                Alignment = Element.ALIGN_CENTER,
                                SpacingBefore = 15f
                            };
                            pdfDoc.Add(footer);

                            pdfDoc.Close();
                        }

                        MessageBox.Show("PDF generated at: " + filePath);

                        Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error generating PDF: " + ex.Message);
                    }
                }
            }
        }

        private void name_TextChanged(object sender, EventArgs e)
        {

        }

        private void design()
        {
            Color formBackColor = Color.FromArgb(34, 34, 34); // Charcoal Black

            Color buttonBackColor = Color.FromArgb(191, 167, 111);   // #BFA76F (Gold)
            Color buttonForeColor = Color.FromArgb(26, 26, 26);      // #1A1A1A (Dark)
            Color formTextColor = Color.FromArgb(230, 225, 210); // #E6E1D2 – Ivory White
        

            this.BackColor = formBackColor;
            this.ForeColor = formTextColor;

            // Set panel background
            this.BackColor = formBackColor;

            this.ForeColor = formTextColor;
            label1.ForeColor = formTextColor;
            label2.ForeColor = formTextColor;
            label3.ForeColor = formTextColor;
            label4.ForeColor = formTextColor;
           


            this.ForeColor = formTextColor;

            Button[] buttons = new Button[]
                {
                    button1
                   

                };

            foreach (var btn in buttons)
            {
                btn.BackColor = buttonBackColor;
                btn.ForeColor = buttonForeColor;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
            }
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
