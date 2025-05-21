using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Programming_Assigment.Database
{
    internal class Common
    {
        public class CommonDatabaseCodes
        {
            private SqlConnection conn;

            public CommonDatabaseCodes(string sqlConnection)
            {
                conn = new SqlConnection(sqlConnection);
            }

            // Execute INSERT, UPDATE, DELETE
            public bool Execute(string sql)
            {
                bool status = false;

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    int effectedRows = cmd.ExecuteNonQuery();
                    status = effectedRows > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message, "Error");
                }
                finally
                {
                    conn.Close();
                }

                return status;
            }

            // SELECT queries
            public DataTable GetQueryData(string sql)
            {
                DataTable dataTable = new DataTable();

                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message, "Error");
                }
                finally
                {
                    conn.Close();
                }

                return dataTable;
            }

            // Insert with message
            public void ExecuteInsertQuery(string sql, string message = "Data Inserted Successfully")
            {
                if (Execute(sql))
                {
                    MessageBox.Show(message);
                }
            }

            // Update with message
            public void ExecuteUpdateQuery(string sql, string message = "Data Updated Successfully")
            {
                if (Execute(sql))
                {
                    MessageBox.Show(message);
                }
            }

            // Delete with message
            public void ExecuteDeleteQuery(string sql, string message = "Data Deleted Successfully")
            {
                if (Execute(sql))
                {
                    MessageBox.Show(message);
                }
            }

            // Load to GridView
            public void LoadDataInGridView(string sql, DataGridView dataGridView)
            {
                DataTable dataTable = GetQueryData(sql);
                dataGridView.DataSource = dataTable;
            }

            // Load to ComboBox
            public void LoadFkDataInComboBox(string sql, ComboBox comboBox, string idColumn, string nameColumn)
            {
                DataTable dataTable = GetQueryData(sql);
                comboBox.DataSource = dataTable;
                comboBox.DisplayMember = nameColumn;
                comboBox.ValueMember = idColumn;
            }
        }


    }
}
