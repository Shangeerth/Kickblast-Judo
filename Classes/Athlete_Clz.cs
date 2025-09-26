using Programming_Assigment.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Programming_Assigment.Athlete
{
    internal class Athlete_Clz 
    {

        public Sql db = new Sql();

        public Athlete_Clz(Sql database)
        {
            db = database;
        }

        public List<Athleteclz> GetAthletes()
        {
            List<Athleteclz> athleteList = new List<Athleteclz>();

            try
            {
                string query = @"
            SELECT 
                a.AthleteID,
                a.Name,
                a.Weight,
                a.Height,
                a.Address,
                a.NIC,
                a.Contact,
                a.DateOfBirth,
                w.CategoryID,
                w.Name AS CategoryName
            FROM Athlete a
            INNER JOIN WeightCategory w ON a.CategoryID = w.CategoryID";

                DataTable dt = db.ExecuteQuery(query, new SqlParameter[0]);

                if (dt == null)
                    throw new Exception("Query execution returned null. Check your database connection or query.");

                foreach (DataRow dr in dt.Rows)
                {
                    athleteList.Add(new Athleteclz(
                        Convert.ToInt32(dr["AthleteID"]),
                        dr["Name"].ToString(),
                        Convert.ToInt32(dr["Weight"]),
                        Convert.ToInt32(dr["Height"]),
                        dr["Address"].ToString(),
                        dr["NIC"].ToString(),
                        Convert.ToInt32(dr["Contact"]),
                        Convert.ToDateTime(dr["DateOfBirth"]),
                        Convert.ToInt32(dr["CategoryID"]),
                        dr["CategoryName"].ToString()
                    ));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving athlete list: {ex.Message}", ex);
            }

            return athleteList;
        }




        public bool InsertAthlete(string name, int weight, int height, string address, string nic, int contact, DateTime dob, string categoryName)
        {
            try
            {

                string categoryQuery = "SELECT CategoryID FROM WeightCategory WHERE Name = @CategoryName";
                SqlParameter[] categoryParams = {
            new SqlParameter("@CategoryName", categoryName)
        };

                object categoryIdObj = db.ExecuteScalar(categoryQuery, categoryParams);

                if (categoryIdObj == null)
                {
                    throw new Exception("Invalid category name. No matching category found.");
                }

                int categoryId = Convert.ToInt32(categoryIdObj);

                string insertQuery = @"
                INSERT INTO Athlete (Name, Weight, Height, Address, NIC, Contact, DateOfBirth, CategoryID)
                VALUES (@Name, @Weight, @Height, @Address, @NIC, @Contact, @DateOfBirth, @CategoryID)";

                SqlParameter[] insertParams = {
            new SqlParameter("@Name", name),
            new SqlParameter("@Weight", weight),
            new SqlParameter("@Height", height),
            new SqlParameter("@Address", address),
            new SqlParameter("@NIC", nic),
            new SqlParameter("@Contact", contact),
            new SqlParameter("@DateOfBirth", dob),
            new SqlParameter("@CategoryID", categoryId)
        };

                db.ExecuteNonQuery(insertQuery, insertParams);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inserting athlete: {ex.Message}", ex);
            }
        }





        public bool UpdateAthlete(int id, string name, int weight, int height, string address, string nic, int contact, DateTime dob, string categoryName)
        {
            try
            {
                string categoryQuery = "SELECT CategoryID FROM WeightCategory WHERE Name = @CategoryName";
                SqlParameter[] categoryParams = {
            new SqlParameter("@CategoryName", categoryName)
        };

                object categoryIdObj = db.ExecuteScalar(categoryQuery, categoryParams);

                if (categoryIdObj == null)
                {
                    throw new Exception("Invalid category name. No matching category found.");
                }

                int categoryId = Convert.ToInt32(categoryIdObj);

                string updateQuery = @"
            UPDATE Athlete
            SET Name = @Name,
                Weight = @Weight,
                Height = @Height,
                Address = @Address,
                NIC = @NIC,
                Contact = @Contact,
                DateOfBirth = @DateOfBirth,
                CategoryID = @CategoryID
            WHERE AthleteID = @AthleteID";

                SqlParameter[] updateParams = {
            new SqlParameter("@AthleteID", id),
            new SqlParameter("@Name", name),
            new SqlParameter("@Weight", weight),
            new SqlParameter("@Height", height),
            new SqlParameter("@Address", address),
            new SqlParameter("@NIC", nic),
            new SqlParameter("@Contact", contact),
            new SqlParameter("@DateOfBirth", dob),
            new SqlParameter("@CategoryID", categoryId)
        };

                db.ExecuteNonQuery(updateQuery, updateParams);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating athlete: {ex.Message}", ex);
            }
        }


        public bool DeleteAthlete(int athleteId)
        {
            try
            {
                string query = "DELETE FROM Athlete WHERE AthleteID = @AthleteID";
                SqlParameter[] parameters = {
                    new SqlParameter("@AthleteID", athleteId)
                };

                db.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting athlete: {ex.Message}", ex);
            }
        }

        public List<Athleteclz> SearchAthletes(string searchText)
        {
            List<Athleteclz> athletes = new List<Athleteclz>();

            try
            {
                string query = @"
            SELECT 
                A.AthleteID,
                A.Name,
                A.Weight,
                A.Height,
                A.Address,
                A.NIC,
                A.Contact,
                A.DateOfBirth,
                A.CategoryID,
                WC.Name AS CategoryName
            FROM Athlete A
            INNER JOIN WeightCategory WC ON A.CategoryID = WC.CategoryID
            WHERE 
                A.Name LIKE @SearchText OR
                A.NIC LIKE @SearchText OR
                A.AthleteID LIKE @SearchText OR
                A.Address LIKE @SearchText";

                SqlParameter[] parameters = {
            new SqlParameter("@SearchText", "%" + searchText + "%")
        };

                DataTable dt = db.ExecuteQuery(query, parameters);

                foreach (DataRow dr in dt.Rows)
                {
                    athletes.Add(new Athleteclz(
                        Convert.ToInt32(dr["AthleteID"]),
                        dr["Name"].ToString(),
                        Convert.ToInt32(dr["Weight"]),
                        Convert.ToInt32(dr["Height"]),
                        dr["Address"].ToString(),
                        dr["NIC"].ToString(),
                        Convert.ToInt32(dr["Contact"]),
                        Convert.ToDateTime(dr["DateOfBirth"]),
                        Convert.ToInt32(dr["CategoryID"]),
                        dr["CategoryName"].ToString()
                    ));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching athletes: {ex.Message}", ex);
            }

            return athletes;
        }

        public List<int> GetAthleteIds()
        {
            List<int> ids = new List<int>();

            try
            {
                string query = "SELECT AthleteID FROM Athlete";
                DataTable dt = db.ExecuteQuery(query, new SqlParameter[0]);

                foreach (DataRow dr in dt.Rows)
                {
                    ids.Add(Convert.ToInt32(dr["AthleteID"]));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching athlete IDs: {ex.Message}", ex);
            }

            return ids;
        }



        public List<string> GetCategoryNames()
        {
            List<string> names = new List<string>();

            try
            {
                string query = "SELECT Name FROM WeightCategory";
                DataTable dt = db.ExecuteQuery(query, new SqlParameter[0]);

                foreach (DataRow dr in dt.Rows)
                {
                    names.Add(dr["Name"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching category names: {ex.Message}", ex);
            }

            return names;
        }

       




    }
}
