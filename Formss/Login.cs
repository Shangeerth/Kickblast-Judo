using Programming_Assigment.Formss;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Programming_Assigment.Classes
{
    public partial class Login : Form
    {

        private Sql db = new Sql();

      

        public Login()
        {
            InitializeComponent();
            label2.BackColor = Color.FromArgb(0, 255, 255, 255);
            label1.BackColor = Color.FromArgb(0, 255, 255, 255);

            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;



        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text;

            try
            {



               
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) )
                {
                    MessageBox.Show("Username, Password are required!", "Validation Error");
                    return;
                }

                bool success = Logicn(username, password);
                if (success)
                {
                    MessageBox.Show("Login Successful!");
                    Dashboard dashboard = new Dashboard();
                    dashboard.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Welcome wel = new Welcome();
            wel.Show();
            this.Close();
        }


        public bool Logicn(string username, string password)
        {
            string query = @"SELECT COUNT(*) FROM loginuse 
                     WHERE username = @username AND password = @password";

            SqlParameter[] sqlParams = new SqlParameter[]
            {
        new SqlParameter("@username", username),
        new SqlParameter("@password", password)
            };

            int count = Convert.ToInt32(db.ExecuteScalar(query, sqlParams));
            return count > 0;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = !checkBox1.Checked; // Show or hide password based on checkbox

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.BorderStyle = BorderStyle.None;

        }
    }
}
