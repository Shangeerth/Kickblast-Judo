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
    internal class @private
    {

        private Sql db = new Sql();

        public @private(Sql database)
        {
            db = database;
        }

        public List<PrivateCoaching> GetPrivateCoachings()
        {
            List<PrivateCoaching> coachingList = new List<PrivateCoaching>();

            try
            {
                string query = @"SELECT * FROM PrivateCoaching";
                DataTable dt = db.ExecuteQuery(query, new SqlParameter[0]);

                foreach (DataRow dr in dt.Rows)
                {
                    coachingList.Add(new PrivateCoaching(
                        Convert.ToInt32(dr["PrivateCoachingID"]),
                        Convert.ToDateTime(dr["CoachingDate"]),
                        Convert.ToInt32(dr["HoursPerWeek"]),
                        Convert.ToDecimal(dr["FeesPerHour"]),
                        Convert.ToInt32(dr["AthleteID"]),
                        Convert.ToInt32(dr["TrainerID"])
                    ));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching private coachings: {ex.Message}", ex);
            }

            return coachingList;
        }

        public bool InsertPrivateCoaching(DateTime coachingDate, int hoursPerWeek, decimal feesPerHour, int athleteId, int trainerId)
        {
            try
            {
                string query = @"
            INSERT INTO PrivateCoaching (CoachingDate, HoursPerWeek, FeesPerHour, AthleteID, TrainerID)
            VALUES (@date, @hours, @fee, @athlete, @trainer)";

                SqlParameter[] parameters = {
            new SqlParameter("@date", coachingDate),
            new SqlParameter("@hours", hoursPerWeek),
            new SqlParameter("@fee", feesPerHour),
            new SqlParameter("@athlete", athleteId),
            new SqlParameter("@trainer", trainerId)
        };

                db.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inserting private coaching: {ex.Message}", ex);
            }
        }



        public bool UpdatePrivateCoaching(int id, DateTime coachingDate, int hoursPerWeek, decimal feesPerHour, int athleteId, int trainerId)
        {
            try
            {
                string query = @"
            UPDATE PrivateCoaching
            SET CoachingDate = @date,
                HoursPerWeek = @hours,
                FeesPerHour = @fee,
                AthleteID = @athlete,
                TrainerID = @trainer
            WHERE PrivateCoachingID = @id";

                SqlParameter[] parameters = {
            new SqlParameter("@id", id),
            new SqlParameter("@date", coachingDate),
            new SqlParameter("@hours", hoursPerWeek),
            new SqlParameter("@fee", feesPerHour),
            new SqlParameter("@athlete", athleteId),
            new SqlParameter("@trainer", trainerId)
        };

                db.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating private coaching: {ex.Message}", ex);
            }
        }


        public bool DeletePrivateCoaching(int id)
        {
            try
            {
                string query = "DELETE FROM PrivateCoaching WHERE PrivateCoachingID = @id";
                SqlParameter[] parameters = {
            new SqlParameter("@id", id)
        };

                db.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting private coaching: {ex.Message}", ex);
            }
        }


        public List<PrivateCoaching> SearchPrivateCoachings(string searchText)
        {
            List<PrivateCoaching> coachings = new List<PrivateCoaching>();

            try
            {
                string query = @"
        SELECT pc.PrivateCoachingID, pc.CoachingDate, pc.HoursPerWeek, pc.FeesPerHour, 
               pc.AthleteID, pc.TrainerID
        FROM PrivateCoaching pc
        INNER JOIN Athlete a ON pc.AthleteID = a.AthleteID
        INNER JOIN Trainer t ON pc.TrainerID = t.id
        WHERE 
            CAST(pc.AthleteID AS VARCHAR) LIKE @search OR
            CAST(pc.TrainerID AS VARCHAR) LIKE @search OR
            a.Name LIKE @search OR
            t.name LIKE @search";

                SqlParameter[] parameters = {
            new SqlParameter("@search", "%" + searchText + "%")
        };

                DataTable dt = db.ExecuteQuery(query, parameters);

                foreach (DataRow dr in dt.Rows)
                {
                    coachings.Add(new PrivateCoaching(
                        Convert.ToInt32(dr["PrivateCoachingID"]),
                        Convert.ToDateTime(dr["CoachingDate"]),
                        Convert.ToInt32(dr["HoursPerWeek"]),
                        Convert.ToDecimal(dr["FeesPerHour"]),
                        Convert.ToInt32(dr["AthleteID"]),
                        Convert.ToInt32(dr["TrainerID"])
                    ));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching private coaching records: {ex.Message}", ex);
            }

            return coachings;
        }


        public List<int> GetPrivateCoachingIds()
        {
            List<int> ids = new List<int>();

            try
            {
                string query = "SELECT PrivateCoachingID FROM PrivateCoaching";
                DataTable dt = db.ExecuteQuery(query, new SqlParameter[0]);

                foreach (DataRow dr in dt.Rows)
                {
                    ids.Add(Convert.ToInt32(dr["PrivateCoachingID"]));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching private coaching IDs: {ex.Message}", ex);
            }

            return ids;
        }



        public List<int> GetTrainerIds()
        {
            List<int> ids = new List<int>();

            try
            {
                string query = "SELECT id FROM trainer";
                DataTable dt = db.ExecuteQuery(query, new SqlParameter[0]);

                foreach (DataRow dr in dt.Rows)
                {
                    ids.Add(Convert.ToInt32(dr["id"]));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching trainer IDs: {ex.Message}", ex);
            }

            return ids;
        }

        public string GetTrainerNameById(int id)
        {
            string query = "SELECT name FROM trainer WHERE id = @id";
            SqlParameter[] parameters = {
        new SqlParameter("@id", id)
    };

            DataTable dt = db.ExecuteQuery(query, parameters);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["name"].ToString();
            }

            return string.Empty;
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
        public string GetAthleteNameById(int id)
        {
            string query = "SELECT Name FROM Athlete WHERE AthleteID = @id";
            SqlParameter[] parameters = {
        new SqlParameter("@id", id)
    };

            DataTable dt = db.ExecuteQuery(query, parameters);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["Name"].ToString();
            }

            return string.Empty;
        }


        public List<string> GetAllTrainerNames()
        {
            List<string> names = new List<string>();

            try
            {
                string query = "SELECT name FROM trainer";
                DataTable dt = db.ExecuteQuery(query, new SqlParameter[0]);

                foreach (DataRow row in dt.Rows)
                {
                    names.Add(row["name"].ToString());
                }

                return names;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching trainer names: {ex.Message}", ex);
            }
        }




        public int GetTotalHoursForAthleteInWeek(int athleteId, DateTime coachingDate, int excludePrivateCoachingId = 0)
        {
            string query = @"
            SELECT ISNULL(SUM(HoursPerWeek), 0) 
            FROM PrivateCoaching
            WHERE AthleteID = @athleteId
            AND DATEPART(week, CoachingDate) = DATEPART(week, @coachingDate)
            AND DATEPART(year, CoachingDate) = DATEPART(year, @coachingDate)";

            if (excludePrivateCoachingId > 0)
            {
                query += " AND PrivateCoachingID <> @excludePrivateCoachingId";
            }

            List<SqlParameter> parameters = new List<SqlParameter>
    {
        new SqlParameter("@athleteId", athleteId),
        new SqlParameter("@coachingDate", coachingDate)
    };

            if (excludePrivateCoachingId > 0)
            {
                parameters.Add(new SqlParameter("@excludePrivateCoachingId", excludePrivateCoachingId));
            }

            object result = db.ExecuteScalar(query, parameters.ToArray());
            return (result != DBNull.Value) ? Convert.ToInt32(result) : 0;
        }



    }
}
