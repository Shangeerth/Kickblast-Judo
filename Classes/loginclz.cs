using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming_Assigment.Classes
{
    internal class loginclz
    {


        private Sql db = new Sql();

        public loginclz(Sql database)
        {
            db = database;
        }
        public bool Login(string role, string username, string password)
        {
            // Debug print to check what role was passed
            Console.WriteLine($"Role received: '{role}'");

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            // Trim and normalize role string
            role = role.Trim();

            string query = "";
            var parameters = new Dictionary<string, object>();

            if (string.Equals(role, "Athlete", StringComparison.OrdinalIgnoreCase))
            {
                query = @"SELECT COUNT(*) FROM loginuse 
                  WHERE roles = @role 
                    AND athletename = @username 
                    AND athletepassword = @password";

                parameters.Add("@role", "Athlete");  // Always pass the exact string stored in DB
                parameters.Add("@username", username);
                parameters.Add("@password", password);
            }
            else if (string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                query = @"SELECT COUNT(*) FROM loginuse 
                  WHERE roles = @role 
                    AND adminusername = @username 
                    AND adminpassword = @password";

                parameters.Add("@role", "Admin");
                parameters.Add("@username", username);
                parameters.Add("@password", password);
            }
            else
            {
                throw new ArgumentException("Invalid role: " + role);
            }

            // Convert Dictionary<string, object> to SqlParameter[]
            SqlParameter[] sqlParams = parameters
                .Select(kv => new SqlParameter(kv.Key, kv.Value ?? DBNull.Value))
                .ToArray();

            int count = Convert.ToInt32(db.ExecuteScalar(query, sqlParams));
            return count > 0;
        }




        public List<string> roles()
        {
            List<string> names = new List<string>();

            try
            {
                string query = "SELECT DISTINCT roles FROM loginuse";
                DataTable dt = db.ExecuteQuery(query, new SqlParameter[0]);

                foreach (DataRow row in dt.Rows)
                {
                    names.Add(row["roles"].ToString());
                }

                return names;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching Roles : {ex.Message}", ex);
            }
        }
    }
}
