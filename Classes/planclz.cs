using Programming_Assigment.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming_Assigment.Classes
{
    internal class planclz
    {
        private Sql db;

        public planclz(Sql database)
        {
            db = database;
        }





        public List<Plan> GetPlans()
        {
            List<Plan> plans = new List<Plan>();

            try
            {
                string query = @"SELECT PlanID, Name, WeeklyFee, SessionsPerWeek FROM TrainingPlan";
                DataTable dt = db.ExecuteQuery(query, new SqlParameter[0]);

                foreach (DataRow dr in dt.Rows)
                {
                    plans.Add(new Plan(
                        Convert.ToInt32(dr["PlanID"]),
                        dr["Name"].ToString(),
                        Convert.ToDecimal(dr["WeeklyFee"]),
                        Convert.ToInt32(dr["SessionsPerWeek"])
                    ));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching plans: {ex.Message}", ex);
            }

            return plans;
        }

        public bool InsertPlan(string name, decimal weeklyFee, int sessionsPerWeek)
        {
            try
            {
                string query = @"
                    INSERT INTO TrainingPlan (Name, WeeklyFee, SessionsPerWeek)
                    VALUES (@name, @weeklyFee, @sessionsPerWeek)";

                SqlParameter[] parameters = {
                    new SqlParameter("@name", name),
                    new SqlParameter("@weeklyFee", weeklyFee),
                    new SqlParameter("@sessionsPerWeek", sessionsPerWeek)
                };

                db.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inserting plan: {ex.Message}", ex);
            }
        }

        public bool UpdatePlan(int planID, string name, decimal weeklyFee, int sessionsPerWeek)
        {
            try
            {
                string query = @"
                    UPDATE TrainingPlan
                    SET Name = @name,
                        WeeklyFee = @weeklyFee,
                        SessionsPerWeek = @sessionsPerWeek
                    WHERE PlanID = @planID";

                SqlParameter[] parameters = {
                    new SqlParameter("@planID", planID),
                    new SqlParameter("@name", name),
                    new SqlParameter("@weeklyFee", weeklyFee),
                    new SqlParameter("@sessionsPerWeek", sessionsPerWeek)
                };

                db.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating plan: {ex.Message}", ex);
            }
        }

        public bool DeletePlan(int planID)
        {
            try
            {
                string query = "DELETE FROM TrainingPlan WHERE PlanID = @planID";

                SqlParameter[] parameters = {
                    new SqlParameter("@planID", planID)
                };

                db.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting plan: {ex.Message}", ex);
            }
        }

        public List<Plan> SearchPlans(string searchText)
        {
            List<Plan> plans = new List<Plan>();

            try
            {
                string query = @"
                    SELECT * FROM TrainingPlan
                    WHERE Name LIKE @search";

                SqlParameter[] parameters = {
                    new SqlParameter("@search", "%" + searchText + "%")
                };

                DataTable dt = db.ExecuteQuery(query, parameters);

                foreach (DataRow dr in dt.Rows)
                {
                    plans.Add(new Plan(
                        Convert.ToInt32(dr["PlanID"]),
                        dr["Name"].ToString(),
                        Convert.ToDecimal(dr["WeeklyFee"]),
                        Convert.ToInt32(dr["SessionsPerWeek"])
                    ));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching plans: {ex.Message}", ex);
            }

            return plans;
        }

        public List<int> GetPlanIds()
        {
            List<int> ids = new List<int>();

            try
            {
                string query = "SELECT PlanID FROM TrainingPlan";
                DataTable dt = db.ExecuteQuery(query, new SqlParameter[0]);

                foreach (DataRow dr in dt.Rows)
                {
                    ids.Add(Convert.ToInt32(dr["PlanID"]));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching plan IDs: {ex.Message}", ex);
            }

            return ids;
        }



        public bool IsPlanNameUnique(string planName, int planId = 0)
        {
            string query = "SELECT COUNT(*) FROM TrainingPlan WHERE Name = @name";

            // Exclude the current plan when updating
            if (planId > 0)
            {
                query += " AND PlanID != @id";
            }

            List<SqlParameter> parameters = new List<SqlParameter>
    {
        new SqlParameter("@name", planName)
    };

            if (planId > 0)
            {
                parameters.Add(new SqlParameter("@id", planId));
            }

            DataTable dt = db.ExecuteQuery(query, parameters.ToArray());

            if (dt.Rows.Count > 0)
            {
                int count = Convert.ToInt32(dt.Rows[0][0]);
                return count == 0; 
            }

            return true;
        }

    }

}
