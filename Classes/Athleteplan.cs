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

        private Sql db = new Sql();

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



        public bool InsertAthleteTrainingPlan(DateTime sessionDate, string time, string athleteName, string planName, int athleteSession)
        {
            try
            {
                // Get PlanID from TrainingPlan name
                string planQuery = "SELECT PlanID FROM TrainingPlan WHERE Name = @PlanName";
                SqlParameter[] planParams = {
            new SqlParameter("@PlanName", planName)
        };

                object planIdObj = db.ExecuteScalar(planQuery, planParams);

                if (planIdObj == null)
                {
                    throw new Exception("Invalid plan name. No matching plan found.");
                }

                int planId = Convert.ToInt32(planIdObj);

                // Get AthleteID from Athlete name
                string athleteQuery = "SELECT AthleteID FROM Athlete WHERE Name = @AthleteName";
                SqlParameter[] athleteParams = {
            new SqlParameter("@AthleteName", athleteName)
        };

                object athleteIdObj = db.ExecuteScalar(athleteQuery, athleteParams);

                if (athleteIdObj == null)
                {
                    throw new Exception("Invalid athlete name. No matching athlete found.");
                }

                int athleteId = Convert.ToInt32(athleteIdObj);

                // Insert into AthleteTrainingPlan with athleteSession
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

        public bool UpdateAthleteTrainingPlan(int athleteTrainingPlanId, DateTime sessionDate, string time, string athleteName, string planName, int athleteSession)
        {
            try
            {
                // Get PlanID from TrainingPlan name
                string planQuery = "SELECT PlanID FROM TrainingPlan WHERE Name = @PlanName";
                SqlParameter[] planParams = {
            new SqlParameter("@PlanName", planName)
        };

                object planIdObj = db.ExecuteScalar(planQuery, planParams);
                if (planIdObj == null)
                {
                    throw new Exception("Invalid plan name. No matching plan found.");
                }
                int planId = Convert.ToInt32(planIdObj);

                // Get AthleteID from Athlete name
                string athleteQuery = "SELECT AthleteID FROM Athlete WHERE Name = @AthleteName";
                SqlParameter[] athleteParams = {
            new SqlParameter("@AthleteName", athleteName)
        };

                object athleteIdObj = db.ExecuteScalar(athleteQuery, athleteParams);
                if (athleteIdObj == null)
                {
                    throw new Exception("Invalid athlete name. No matching athlete found.");
                }
                int athleteId = Convert.ToInt32(athleteIdObj);

                // Update the AthleteTrainingPlan record by ID
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
        public bool IsAthleteInDifferentPlan(string athleteName, string planName)
        {
            string query = @"
        SELECT COUNT(*) 
        FROM AthleteTrainingPlan atp
        JOIN Athlete a ON atp.AthleteID = a.AthleteID
        JOIN TrainingPlan tp ON atp.PlanID = tp.PlanID
        WHERE a.Name = @name AND tp.Name != @plan";

            SqlParameter[] parameters = {
        new SqlParameter("@name", athleteName),
        new SqlParameter("@plan", planName)
    };

            int count = Convert.ToInt32(db.ExecuteScalar(query, parameters));
            return count > 0;
        }



        public int GetSessionsPerWeekByName(string planName)
        {
            try
            {
                string query = "SELECT SessionsPerWeek FROM TrainingPlan WHERE Name = @PlanName";

                SqlParameter[] parameters = {
            new SqlParameter("@PlanName", planName)
        };

                object result = db.ExecuteScalar(query, parameters); // Get the first column of the first row

                if (result != null && int.TryParse(result.ToString(), out int sessionsPerWeek))
                {
                    return sessionsPerWeek;
                }
                else
                {
                    throw new Exception("Plan not found or invalid SessionsPerWeek value.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching sessions per week by name: {ex.Message}", ex);
            }
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

        public bool IsAthleteEnrolledInPlan(string athleteName, string planName)
        {
            string query = @"
        SELECT COUNT(*) 
        FROM AthleteTrainingPlan atp
        JOIN TrainingPlan tp ON atp.PlanID = tp.PlanID
        JOIN Athlete a ON atp.AthleteID = a.AthleteID
        WHERE a.Name = @AthleteName AND tp.Name = @PlanName";

            SqlParameter[] parameters = {
        new SqlParameter("@AthleteName", athleteName),
        new SqlParameter("@PlanName", planName)
    };

            int count = Convert.ToInt32(db.ExecuteScalar(query, parameters));
            return count > 0;
        }

        public decimal GetPlanFeeByName(string planName)
        {
            try
            {
                string query = "SELECT WeeklyFee FROM TrainingPlan WHERE Name = @PlanName";

                SqlParameter[] parameters = {
            new SqlParameter("@PlanName", planName)
        };

                object result = db.ExecuteScalar(query, parameters);

                if (result != null && decimal.TryParse(result.ToString(), out decimal fee))
                {
                    return fee;
                }
                else
                {
                    throw new Exception("Plan fee not found or invalid.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching plan fee by name: {ex.Message}", ex);
            }
        }

        public int GetSessionsCountForAthleteWithinWeek(string athleteName, DateTime startOfWeek, DateTime endOfWeek)
        {
            try
            {
                string getAthleteIdQuery = "SELECT AthleteID FROM Athlete WHERE Name = @AthleteName";
                SqlParameter[] idParams = { new SqlParameter("@AthleteName", athleteName) };
                object idObj = db.ExecuteScalar(getAthleteIdQuery, idParams);

                if (idObj == null)
                    throw new Exception("No athlete found with the provided name.");

                int athleteId = Convert.ToInt32(idObj);

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



    }
}
