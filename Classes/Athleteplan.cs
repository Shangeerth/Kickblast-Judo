using Programming_Assigment.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Programming_Assigment.Database.AthleteTrainingPlan;
using static System.Windows.Forms.LinkLabel;

namespace Programming_Assigment.Classes
{
    internal class Athleteplan
    {

        private readonly Sql db = new Sql();

        public Athleteplan(Sql database)
        {
            db = database;
        }

        public List<Athelan> GetAthleteTrainingPlans()
        {
            List<Athelan> planList = new List<Athelan>();

            try
            {
                string query = @"
        SELECT
            atp.ID,
            atp.SessionDate,
            atp.Time,
            atp.athletesession,
            a.AthleteID,
            a.Name AS AthleteName,
            tp.PlanID,
            tp.name AS PlanName,
            tp.SessionsPerWeek,
            tp.WeeklyFee
        FROM AthleteTrainingPlan atp
        INNER JOIN Athlete a ON atp.AthleteID = a.AthleteID
        INNER JOIN TrainingPlan tp ON atp.PlanID = tp.PlanID";

                DataTable dt = db.ExecuteQuery(query, new SqlParameter[0]);

                foreach (DataRow dr in dt.Rows)
                {
                    planList.Add(new Athelan(
                        Convert.ToInt32(dr["ID"]),
                        Convert.ToDateTime(dr["SessionDate"]),
                        dr["Time"].ToString(),
                        Convert.ToInt32(dr["athletesession"]),       // <-- new column added here
                        Convert.ToInt32(dr["AthleteID"]),
                        dr["AthleteName"].ToString(),
                        Convert.ToInt32(dr["PlanID"]),
                        dr["PlanName"].ToString(),
                        Convert.ToInt32(dr["SessionsPerWeek"]),
                        Convert.ToDecimal(dr["WeeklyFee"])
                    ));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching athlete training plans: {ex.Message}", ex);
            }

            return planList;
        }


        public bool InsertAthleteTrainingPlan(DateTime sessionDate, string time, int athleteId, int planId, int athleteSession)
        {
            try
            {
                string insertQuery = @"
            INSERT INTO AthleteTrainingPlan (SessionDate, Time, AthleteID, PlanID, athletesession)
            VALUES (@SessionDate, @Time, @AthleteID, @PlanID, @AthleteSession)";

                SqlParameter[] insertParams = {
            new SqlParameter("@SessionDate", sessionDate),
            new SqlParameter("@Time", time),
            new SqlParameter("@AthleteID", athleteId),
            new SqlParameter("@PlanID", planId),
            new SqlParameter("@AthleteSession", athleteSession)
        };

                db.ExecuteNonQuery(insertQuery, insertParams);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inserting athlete training plan: {ex.Message}", ex);
            }
        }

        public bool UpdateAthleteTrainingPlan(int athleteTrainingPlanId, DateTime sessionDate, string time, int athleteId, int planId, int athleteSession)
        {
            try
            {
                string updateQuery = @"
            UPDATE AthleteTrainingPlan
            SET SessionDate = @SessionDate,
                Time = @Time,
                AthleteID = @AthleteID,
                PlanID = @PlanID,
                athletesession = @AthleteSession
            WHERE ID = @ID";

                SqlParameter[] updateParams = {
            new SqlParameter("@SessionDate", sessionDate),
            new SqlParameter("@Time", time),
            new SqlParameter("@AthleteID", athleteId),
            new SqlParameter("@PlanID", planId),
            new SqlParameter("@AthleteSession", athleteSession),
            new SqlParameter("@ID", athleteTrainingPlanId)
        };

                int rowsAffected = db.ExecuteNonQuery(updateQuery, updateParams);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating athlete training plan: {ex.Message}", ex);
            }
        }

        public bool DeleteAthleteTrainingPlan(int trainingPlanId)
        {
            try
            {
                string query = "DELETE FROM AthleteTrainingPlan WHERE ID = @trainingPlanId";
                SqlParameter[] parameters = {
            new SqlParameter("@trainingPlanId", trainingPlanId)
        };

                db.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting athlete training plan: {ex.Message}", ex);
            }
        }
        public List<Athelan> SearchAthleteTrainingPlans(string searchText)
        {
            List<Athelan> plans = new List<Athelan>();

            try
            {
                string query = @"
                SELECT 
                    atp.ID, 
                    atp.SessionDate, 
                    atp.Time, 
                    atp.athletesession, 
                    a.AthleteID, 
                    a.Name AS AthleteName, 
                    tp.PlanID, 
                    tp.Name AS PlanName, 
                    tp.SessionsPerWeek, 
                    tp.WeeklyFee
                FROM AthleteTrainingPlan atp
                INNER JOIN Athlete a ON atp.AthleteID = a.AthleteID
                INNER JOIN TrainingPlan tp ON atp.PlanID = tp.PlanID
                WHERE a.Name LIKE @search OR tp.Name LIKE @search";

                SqlParameter[] parameters = {
            new SqlParameter("@search", "%" + searchText + "%")
        };

                DataTable dt = db.ExecuteQuery(query, parameters);

                foreach (DataRow dr in dt.Rows)
                {
                    plans.Add(new Athelan(
                        Convert.ToInt32(dr["ID"]),
                        Convert.ToDateTime(dr["SessionDate"]),
                        dr["Time"].ToString(),
                        Convert.ToInt32(dr["athletesession"]),       // int
                        Convert.ToInt32(dr["AthleteID"]),            // int
                        dr["AthleteName"].ToString(),                // string
                        Convert.ToInt32(dr["PlanID"]),               // int
                        dr["PlanName"].ToString(),                    // string
                        Convert.ToInt32(dr["SessionsPerWeek"]),      // int
                        Convert.ToDecimal(dr["WeeklyFee"])           // decimal
                    ));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching athlete training plans: {ex.Message}", ex);
            }

            return plans;
        }

        public List<int> GetAthleteIDs()
        {
            var athleteIds = new List<int>();

            try
            {
                string query = "SELECT AthleteID FROM Athlete";

                DataTable dt = db.ExecuteQuery(query, new SqlParameter[0]);

                foreach (DataRow dr in dt.Rows)
                {
                    athleteIds.Add(Convert.ToInt32(dr["AthleteID"]));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching athlete IDs: {ex.Message}", ex);
            }

            return athleteIds;
        }


        public List<int> GetTrainingPlanIDs()
        {
            var planIds = new List<int>();

            try
            {
                string query = "SELECT PlanID FROM TrainingPlan";

                DataTable dt = db.ExecuteQuery(query, new SqlParameter[0]);

                foreach (DataRow dr in dt.Rows)
                {
                    planIds.Add(Convert.ToInt32(dr["PlanID"]));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching training plan IDs: {ex.Message}", ex);
            }

            return planIds;
        }


        public bool IsAthleteInDifferentPlan(int athleteId, int planId)
        {
            string query = @"
        SELECT COUNT(*) 
        FROM AthleteTrainingPlan atp
        JOIN Athlete a ON atp.AthleteID = a.AthleteID
        JOIN TrainingPlan tp ON atp.PlanID = tp.PlanID
        WHERE a.AthleteID = @athleteId AND tp.PlanID != @planId";

            SqlParameter[] parameters = {
        new SqlParameter("@athleteId", athleteId),
        new SqlParameter("@planId", planId)
    };

            int count = Convert.ToInt32(db.ExecuteScalar(query, parameters));
            return count > 0;
        }



        public int GetSessionsPerWeekByPlanId(int planId)
        {
            try
            {
                string query = "SELECT SessionsPerWeek FROM TrainingPlan WHERE PlanID = @PlanID";
                SqlParameter[] parameters = { new SqlParameter("@PlanID", planId) };

                object result = db.ExecuteScalar(query, parameters);

                if (result != null && int.TryParse(result.ToString(), out int sessionsPerWeek))
                    return sessionsPerWeek;
                else
                    throw new Exception("Plan not found or invalid SessionsPerWeek value.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching sessions per week by PlanID: {ex.Message}", ex);
            }
        }

        public decimal GetPlanFeeByPlanId(int planId)
        {
            try
            {
                string query = "SELECT WeeklyFee FROM TrainingPlan WHERE PlanID = @PlanID";
                SqlParameter[] parameters = { new SqlParameter("@PlanID", planId) };

                object result = db.ExecuteScalar(query, parameters);

                if (result != null && decimal.TryParse(result.ToString(), out decimal fee))
                    return fee;
                else
                    throw new Exception("Plan fee not found or invalid.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching plan fee by PlanID: {ex.Message}", ex);
            }
        }


        public bool IsAthleteEnrolledInPlan(int athleteId, int planId)
        {
            string query = @"
        SELECT COUNT(*) 
        FROM AthleteTrainingPlan
        WHERE AthleteID = @AthleteID AND PlanID = @PlanID";

            SqlParameter[] parameters = {
        new SqlParameter("@AthleteID", athleteId),
        new SqlParameter("@PlanID", planId)
    };

            int count = Convert.ToInt32(db.ExecuteScalar(query, parameters));
            return count > 0;
        }



        public List<string> GetAllAthleteNames()
        {
            List<string> names = new List<string>();

            try
            {
                string query = "SELECT Name FROM Athlete";
                DataTable dt = db.ExecuteQuery(query, new SqlParameter[0]);

                foreach (DataRow row in dt.Rows)
                {
                    names.Add(row["Name"].ToString());
                }

                return names;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching athlete names: {ex.Message}", ex);
            }
        }
        
        public List<string> GetAllPlanNames()
        {
            List<string> planNames = new List<string>();

            try
            {
                string query = "SELECT Name FROM TrainingPlan";
                DataTable dt = db.ExecuteQuery(query, new SqlParameter[0]);

                foreach (DataRow row in dt.Rows)
                {
                    planNames.Add(row["Name"].ToString());
                }

                return planNames;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching plan names: {ex.Message}", ex);
            }
        }


        public int GetSessionsCountForAthleteWithinWeek(int athleteId, DateTime startOfWeek, DateTime endOfWeek)
        {
            try
            {
                string countQuery = @"
            SELECT COUNT(*) 
            FROM AthleteTrainingPlan 
            WHERE AthleteID = @AthleteID 
              AND SessionDate BETWEEN @StartOfWeek AND @EndOfWeek";

                SqlParameter[] parameters = {
            new SqlParameter("@AthleteID", athleteId),
            new SqlParameter("@StartOfWeek", startOfWeek),
            new SqlParameter("@EndOfWeek", endOfWeek)
        };

                object countObj = db.ExecuteScalar(countQuery, parameters);
                return countObj != null ? Convert.ToInt32(countObj) : 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error counting athlete sessions this week: " + ex.Message, ex);
            }
        }




        public int GetAthleteSessionById(int athleteTrainingPlanId)
        {
            try
            {
                string query = "SELECT AthleteSession FROM AthleteTrainingPlan WHERE ID = @ID";
                SqlParameter[] parameters = {
            new SqlParameter("@ID", athleteTrainingPlanId)
        };

                object result = db.ExecuteScalar(query, parameters);

                if (result != null && int.TryParse(result.ToString(), out int athleteSession))
                {
                    return athleteSession;
                }
                else
                {
                    throw new Exception("AthleteSession not found for the provided ID.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching AthleteSession: " + ex.Message, ex);
            }
        }

        public List<int> GetAthleteTrainingPlanIds()
        {
            List<int> ids = new List<int>();

            try
            {
                string query = "SELECT ID FROM AthleteTrainingPlan";
                DataTable dt = db.ExecuteQuery(query, new SqlParameter[0]);

                foreach (DataRow dr in dt.Rows)
                {
                    ids.Add(Convert.ToInt32(dr["ID"]));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching AthleteTrainingPlan IDs: {ex.Message}", ex);
            }

            return ids;
        }

        public string GetAthleteNameById(int athleteId)
        {
            try
            {
                string query = "SELECT Name FROM Athlete WHERE AthleteID = @AthleteID";

                SqlParameter[] parameters = {
            new SqlParameter("@AthleteID", athleteId)
        };

                object result = db.ExecuteScalar(query, parameters);

                if (result != null)
                    return result.ToString();
                else
                    throw new Exception("Athlete not found.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching athlete name: {ex.Message}", ex);
            }
        }


        public string GetPlanNameById(int planId)
        {
            string query = "SELECT Name FROM TrainingPlan WHERE PlanID = @PlanID";

            SqlParameter[] parameters = {
        new SqlParameter("@PlanID", planId)
    };

            object result = db.ExecuteScalar(query, parameters);
            if (result != null)
                return result.ToString();
            else
                throw new Exception("Plan name not found for given ID.");
        }


    }
}
