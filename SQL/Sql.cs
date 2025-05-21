using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Programming_Assigment
{
    internal class Sql
    {


        private readonly string connectionString  = @"Data Source=shangeerth_king;Initial Catalog=Programming;Integrated Security=True;";




        public DataTable ExecuteQuery(string query, SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error");
                return null;
            }
        }

        //  ExecuteNonQuery Method (Fixed Duplicate Issue)
        public int ExecuteNonQuery(string query, SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error");
                return 0;
            }
        }

        public object ExecuteScalar(string query, SqlParameter[] parameters)
        {
            try
            {
                DataTable dt = ExecuteQuery(query, parameters);  // Using ExecuteQuery to execute the SQL

                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0];  // Return the first column of the first row
                }

                return null;  // If no rows returned, return null
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error");
                return null;
            }
        }


    }
}
