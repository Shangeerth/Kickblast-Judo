using FontAwesome.Sharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Programming_Assigment.Classes;
using Programming_Assigment.Database;
using Programming_Assigment.Formss;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;

using static Programming_Assigment.Program;

namespace Programming_Assigment
{
    public partial class payment : Form
    {

        private readonly paymentClz link;
        private bool isCalculated = false;



        public payment()
        {
            InitializeComponent();
            link = new paymentClz(new Sql());

            Color formBackColor = Color.FromArgb(34, 34, 34); // Charcoal Black
            load();
            clear();
            load1();
            Adetails.ForeColor = Color.FromArgb(191, 167, 111);   // #BFA76F (Gold)
            Adetails.ForeColor = Color.FromArgb(191, 167, 111);
            Adetails.Refresh(); // force redraw   
            Adetails.Paint += (s, e) => { Adetails.ForeColor = Color.FromArgb(191, 167, 111); };
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        

       


        private void load()
        {
            id.DataSource = link.GetAllAthleteIds();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MMMM yyyy";
            dateTimePicker1.ShowUpDown = true;
            Adetails.ForeColor = Color.FromArgb(191, 167, 111);   // #BFA76F (Gold)

            Color formBackColor = Color.FromArgb(34, 34, 34); // Charcoal Black

        }
        


        private void id_SelectedIndexChanged(object sender, EventArgs e)
        {

            UpdateTotalPaymentDisplay();




		}


       

        private void payment_Load(object sender, EventArgs e)
        {

            Color formBackColor = Color.FromArgb(34, 34, 34); // Charcoal Black
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MMMM yyyy";  // Month + Year
            dateTimePicker1.ShowUpDown = true;
            design(); // Apply the design method to style the form
        }

        private void weight1()
        {
            if (id.SelectedIndex == -1 || id.SelectedItem == null)
            {

                return;
            }

            int selectedId = Convert.ToInt32(id.SelectedValue);
            string selectedName = id.Text; // for display purposes only

            int maxWeight = link.GetMaxWeightByAthleteId(selectedId); 
            int currentWeight = link.GetAthleteWeightById(selectedId);      

            string weightCategory = link.GetCategoryWithMaxWeightByAthleteId(selectedId);

           

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

            int selectedId = Convert.ToInt32(id.SelectedValue);
            string selectedName = id.Text; // for display purposes only

            SharedData.name = txtAthleteName.Text;
			string athleteName = link.GetAthleteNameById(selectedId);
			txtAthleteName.Text = athleteName;


			DateTime selectedDate = dateTimePicker1.Value;

            // --- Private Coaching Fee ---
            decimal privateCoachingFee = link.GetTotalPrivateCoachingPaymentForMonthById(selectedId, selectedDate);
            privatefee.Text = privateCoachingFee > 0
                ? "Rs. " + privateCoachingFee.ToString("N2")
                : "No coaching payments found.";

            // --- Competition Fee ---
            decimal competitionFee = link.GetTotalCompetitionFeeForMonthById(selectedId, selectedDate);
            compfee.Text = competitionFee > 0
                ? "Rs. " + competitionFee.ToString("N2")
                : "No competition fee found.";

            // --- Training Plan Fee ---
            decimal trainingPlanFee = link.GetTotalTrainingPlanPaymentForMonthById(selectedId, selectedDate);
            trainingplanfee.Text = trainingPlanFee > 0
                ? "Rs. " + trainingPlanFee.ToString("N2")
                : "No training payments found.";

            // --- Total Payment ---
            decimal totalFee = privateCoachingFee + competitionFee + trainingPlanFee;
            SharedData.TotalFee = totalFee;

            totalvalue.Text = totalFee > 0
                ? "Rs. " + totalFee.ToString("N2")
                : "No payments found.";

            // --- Coach Name ---
            string coachName = link.GetTrainerNameFromPrivateCoachingById(selectedId, selectedDate);
            trainer.Text = !string.IsNullOrEmpty(coachName)
                ? coachName
                : "No coach selected month.";

            // --- Weight Category ---
            string weightCategory = link.GetCategoryWithMaxWeightByAthleteId(selectedId);
            weight.Text = !string.IsNullOrEmpty(weightCategory)
                ? weightCategory
                : "No weight category found.";

            // --- Training Plan Name ---
            string trainingPlan = link.GetTrainingPlanNameByAthleteId(selectedId);
            planname.Text = !string.IsNullOrEmpty(trainingPlan)
                ? trainingPlan
                : "No training plan found.";
        }



        public static class SharedData
        {
            public static decimal TotalFee { get; set; }
            public static string name { get; set; }
        }


        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
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
            txtAthleteName.Clear();
            txtAthleteName.Enabled = false;
            dateTimePicker1.Value = DateTime.Now; // Reset to current date
        }

        private void PDF_Click(object sender, EventArgs e)
        {
            if (id.SelectedItem == null || SharedData.TotalFee <= 0 )
            {
                MessageBox.Show("Please select an athlete and ensure the total fee is greater than zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            cal_Click(this, EventArgs.Empty);

            GenerateAndOpenPDF();
            
        }

        private void GenerateAndOpenPDF()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Save PDF";
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.FileName = "payment.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    // Ask user to select logo image
                    string logoPath = null;
                    using (OpenFileDialog openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.Title = "Select Logo Image";
                        openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp";

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            logoPath = openFileDialog.FileName;
                        }
                    }

                    Document pdfDoc = new Document(PageSize.A4, 40, 40, 40, 40);

                    try
                    {
                        using (FileStream stream = new FileStream(filePath, FileMode.Create))
                        {
                            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                            pdfDoc.Open();

                            // Background color
                            PdfContentByte background = writer.DirectContentUnder;
                            background.SaveState();
                            background.SetColorFill(new BaseColor(240, 240, 240)); // light grey
                            background.Rectangle(0, 0, pdfDoc.PageSize.Width, pdfDoc.PageSize.Height);
                            background.Fill();
                            background.RestoreState();

                            // Border frame
                            PdfContentByte canvas = writer.DirectContent;
                            iTextSharp.text.Rectangle frame = new iTextSharp.text.Rectangle(pdfDoc.PageSize);
                            frame.Left += 20;
                            frame.Right -= 20;
                            frame.Top -= 20;
                            frame.Bottom += 20;
                            frame.BorderWidth = 2;
                            frame.BorderColor = new BaseColor(139, 0, 0); // dark red
                            frame.Border = iTextSharp.text.Rectangle.BOX;
                            canvas.Rectangle(frame);



                            // Logo - only if user selected a file
                            if (!string.IsNullOrEmpty(logoPath) && File.Exists(logoPath))
                            {
                                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                                logo.Alignment = Element.ALIGN_CENTER;
                                float maxLogoHeight = pdfDoc.PageSize.Height / 6; // 1/6th of page
                                logo.ScaleToFit(100f, maxLogoHeight); logo.SpacingAfter = 0f;   // remove bottom gap
                                logo.SpacingBefore = 0f;
                                pdfDoc.Add(logo);
                            }

                            // Fonts
                            var headingFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 22, new BaseColor(255, 0, 0));
                            var sectionTitleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, new BaseColor(75, 0, 130));
                            var labelFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.WHITE);
                            var valueFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK);

                            // Heading
                            Paragraph heading = new Paragraph("Kickblast Judo\nPayment Receipt", headingFont)
                            {
                                Alignment = Element.ALIGN_CENTER,
                                SpacingBefore = 0f, // reduce gap from logo
                                SpacingAfter = 10f
                            };
                            pdfDoc.Add(heading);

                            // Date Information Section
                            Paragraph dateInfo = new Paragraph(
                                "Payment For Date: " + dateTimePicker1.Value.ToString("MM-yyyy") + "\n" +
                                "Date Issued: " + DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"), valueFont)
                            {
                                Alignment = Element.ALIGN_RIGHT,
                                SpacingAfter = 10f
                            };
                            pdfDoc.Add(dateInfo);

                            // Athlete Information Section
                            pdfDoc.Add(new Paragraph("Athlete Information", sectionTitleFont));
                            pdfDoc.Add(new Paragraph("\n"));

                            PdfPTable athleteTable = new PdfPTable(2)
                            {
                                WidthPercentage = 100
                            };
                            athleteTable.SetWidths(new float[] { 1f, 2f });

                            void AddRow(PdfPTable table, string label, string value)
                            {
                                PdfPCell labelCell = new PdfPCell(new Phrase(label, labelFont))
                                {
                                    BackgroundColor = new BaseColor(75, 0, 130),
                                    Padding = 8,
                                    BorderColor = BaseColor.GRAY
                                };
                                PdfPCell valueCell = new PdfPCell(new Phrase(value, valueFont))
                                {
                                    Padding = 8,
                                    BorderColor = BaseColor.GRAY
                                };
                                table.AddCell(labelCell);
                                table.AddCell(valueCell);
                            }

                            AddRow(athleteTable, "Athlete ID", id.Text);
                            AddRow(athleteTable, "Athlete Name", txtAthleteName.Text);
                            AddRow(athleteTable, "Training Plan", planname.Text);
                            AddRow(athleteTable, "Trainer Name", trainer.Text);
                            AddRow(athleteTable, "Weight", weight.Text + " kg");
                            AddRow(athleteTable, "Weight Difference", max.Text + " kg");

                            pdfDoc.Add(athleteTable);
                            pdfDoc.Add(new Paragraph("\n"));

                            // Payment Details Section
                            pdfDoc.Add(new Paragraph("Payment Details", sectionTitleFont));
                            pdfDoc.Add(new Paragraph("\n"));

                            PdfPTable paymentTable = new PdfPTable(2)
                            {
                                WidthPercentage = 100
                            };
                            paymentTable.SetWidths(new float[] { 1f, 2f });

                            AddRow(paymentTable, "Training Fee", "Rs. " + trainingplanfee.Text);
                            AddRow(paymentTable, "Coaching Fee", "Rs. " + privatefee.Text);
                            AddRow(paymentTable, "Competition Fee", "Rs. " + compfee.Text);

                            decimal trainingFee = decimal.TryParse(trainingplanfee.Text, out var tFee) ? tFee : 0;
                            decimal coachingFee = decimal.TryParse(privatefee.Text, out var cFee) ? cFee : 0;
                            decimal competitionFee = decimal.TryParse(compfee.Text, out var compFee) ? compFee : 0;
                            decimal subtotal = trainingFee + coachingFee + competitionFee;

                            AddRow(paymentTable, "Subtotal", "Rs. " + subtotal.ToString("F2"));
                            AddRow(paymentTable, "Total Fee", "Rs. " + totalvalue.Text);

                            pdfDoc.Add(paymentTable);
                            pdfDoc.Add(new Paragraph("\n"));

                            // Create receipt number and QR content
                            string receiptNumber = "RCPT-" + DateTime.Now.ToString("yyyyMMddHHmmss");

                            string qrContent = $"Kickblast Judo Payment Receipt | " +
                                               $"Receipt No: {receiptNumber} | " +
                                               $"Athlete ID: {id.Text} | " +
                                               $"Name: {txtAthleteName.Text} | " +
                                               $"Total Payment: Rs. {totalvalue.Text} | " +
                                               $"Date: {DateTime.Now:dd-MM-yyyy}";

                            // URL encode the content for safe transmission
                            string encodedContent = Uri.EscapeDataString(qrContent);

                            // QR code API URL
                            string url = $"https://api.qrserver.com/v1/create-qr-code/?size=150x150&data={encodedContent}";

                            iTextSharp.text.Image qrCodeImage = null;

                            // Download QR code image from API
                            using (WebClient wc = new WebClient())
                            {
                                byte[] qrBytes = wc.DownloadData(url);
                                using (MemoryStream ms = new MemoryStream(qrBytes))
                                {
                                    qrCodeImage = iTextSharp.text.Image.GetInstance(ms);
                                }
                            }

                            // Scale and align the QR code image
                            qrCodeImage.SpacingBefore = 0f;
                            qrCodeImage.ScaleToFit(100, 100);
                            qrCodeImage.Alignment = Element.ALIGN_LEFT;

                            // Bottom Table with QR and Signature side by side
                            PdfPTable bottomTable = new PdfPTable(2)
                            {
                                WidthPercentage = 100,
                                SpacingBefore = 20f
                            };
                            bottomTable.SetWidths(new float[] { 1f, 2f });

                            PdfPCell qrCell = new PdfPCell(qrCodeImage)
                            {
                                Border = PdfPCell.NO_BORDER,
                                HorizontalAlignment = Element.ALIGN_LEFT,
                                VerticalAlignment = Element.ALIGN_MIDDLE,
                                PaddingRight = 10
                            };

                            PdfPCell signCell = new PdfPCell(new Phrase("Authorized by: ____________________\n(Admin Signature)", valueFont))
                            {
                                Border = PdfPCell.NO_BORDER,
                                HorizontalAlignment = Element.ALIGN_RIGHT,
                                VerticalAlignment = Element.ALIGN_MIDDLE,
                                PaddingTop = 20
                            };

                            bottomTable.AddCell(qrCell);
                            bottomTable.AddCell(signCell);

                            pdfDoc.Add(bottomTable);

                            pdfDoc.Close();
                        }

                        MessageBox.Show("PDF generated at: " + filePath);

                        try
                        {
                            ProcessStartInfo psi = new ProcessStartInfo
                            {
                                FileName = filePath,
                                UseShellExecute = true
                            };
                            Process.Start(psi);
                        }
                        catch
                        {
                            MessageBox.Show("PDF saved, but no viewer is installed to open it.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error creating PDF: " + ex.Message);
                    }
                }
            }
        }





        public static iTextSharp.text.Image GenerateQRCode(string content, int width = 150, int height = 150)
        {
            // Add prefix to force QR readers to treat it as plain text
            string qrContent = "TEXT:\n" + content;

            var writer = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new EncodingOptions
                {
                    Height = height,
                    Width = width,
                    Margin = 1
                }
            };

            var pixelData = writer.Write(qrContent);

            using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppRgb))
            {
                var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height),
                                ImageLockMode.ReadOnly, bitmap.PixelFormat);

                try
                {
                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }

                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    ms.Position = 0;
                    return iTextSharp.text.Image.GetInstance(ms);
                }
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
                this.clear();
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

     private void design ()
        {
            Color formBackColor = Color.FromArgb(34, 34, 34); // Charcoal Black

            Color buttonBackColor = Color.FromArgb(191, 167, 111);   // #BFA76F (Gold)
            Color buttonForeColor = Color.FromArgb(26, 26, 26);      // #1A1A1A (Dark)
            Color formTextColor = Color.FromArgb(230, 225, 210); // #E6E1D2 – Ivory White
            label8.ForeColor = Color.FromArgb(191, 167, 111);   // #BFA76F (Gold)
            Adetails.ForeColor = Color.FromArgb(191, 167, 111);   // #BFA76F (Gold)

            this.BackColor = formBackColor;
            this.ForeColor = formTextColor;

            // Set panel background
            this.BackColor = formBackColor;

            this.ForeColor = formTextColor;
            label1.ForeColor = formTextColor;
            label2.ForeColor = formTextColor;
            label3.ForeColor = formTextColor;
            label4.ForeColor = formTextColor;
            label5.ForeColor = formTextColor;
            label6.ForeColor = formTextColor;
            label7.ForeColor = formTextColor;
        
            label10.ForeColor = formTextColor;
            Nic123.ForeColor = formTextColor;
            Contact1.ForeColor = formTextColor;

            panel2.BackColor= Color.FromArgb(48, 48, 48); // Graphite Gray
           panel3.BackColor= Color.FromArgb(48, 48, 48); // Graphite Gray

            foreach (Control ctrl in panel3.Controls)
            {
                if (ctrl is Label lbl)
                {
                    lbl.ForeColor = formTextColor;
                }
            }
            this.ForeColor = formTextColor;

            Button[] buttons = new Button[]
                {
                    Clear,
                    Logout,
                    Back,
                    button1,
                    PDF,
                    cal
                };

            foreach (var btn in buttons)
            {
                btn.BackColor = buttonBackColor;
                btn.ForeColor = buttonForeColor;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
            }
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

        private void Back_Click(object sender, EventArgs e)
        {
           
            this.Hide();
        }

        private void txtAthleteName_TextChanged(object sender, EventArgs e)
        {

        }

        private void Nic123_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void compfee_TextChanged(object sender, EventArgs e)
        {

        }

        private void privatefee_TextChanged(object sender, EventArgs e)
        {

        }

        private void trainingplanfee_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {
            Adetails.ForeColor = Color.FromArgb(191, 167, 111);   // #BFA76F (Gold)

        }

        private void cal_Click(object sender, EventArgs e)
        {


            UpdateTotalPaymentDisplay();
            weight1();
        
            if (id.SelectedValue != null)
            {
                int selectedAthleteId = Convert.ToInt32(id.SelectedValue);
                string athleteName = link.GetAthleteNameById(selectedAthleteId);
                txtAthleteName.Text = athleteName;
            }
            else
            {
                MessageBox.Show("Please select an athlete from the dropdown.");
                return;
            }

            
            int athleteId = Convert.ToInt32(id.SelectedValue);
            DateTime paymentDate = dateTimePicker1.Value;
            decimal totalFee = SharedData.TotalFee;

            if (totalFee <= 0)
            {
                MessageBox.Show("No fee calculated to insert.");
                return;
            }

            try
            {
                Sql db = new Sql();
                paymentClz payment = new paymentClz(db);

                bool success = payment.InsertPayment(athleteId, totalFee, paymentDate);
                MessageBox.Show(success ? "Payment calculated successfully!" : "Payment calculation failed for this month try different month.");

                isCalculated = true; // mark that calculation is done
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }



        }
    }
}




