using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Programming_Assigment.Database.AthleteTrainingPlan;

namespace Programming_Assigment.Classes
{
    internal class competitionClz
    {

        private Sql db = new Sql();

        public competitionClz(Sql database)
        {
            db = database;
        }

        public List<Competitionget> GetCompetitions()
        {
            List<Competitionget> list = new List<Competitionget>();

            try
            {
                string query = @"SELECT CompetitionID, CompetitionDate, CompetitionTime, CompetitionFee, CompetitionName FROM Competition";
                DataTable dt = db.ExecuteQuery(query, new SqlParameter[0]);

                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new Competitionget(
                        Convert.ToInt32(dr["CompetitionID"]),
                        Convert.ToDateTime(dr["CompetitionDate"]),
                        dr["CompetitionTime"].ToString(),
                        Convert.ToDecimal(dr["CompetitionFee"]),
                        dr["CompetitionName"].ToString()
                    ));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching competitions: {ex.Message}", ex);
            }

            return list;
        }

        public bool InsertCompetition(DateTime date, string time, decimal fee, string name)
        {
            try
            {
                string query = @"
                INSERT INTO Competition (CompetitionDate, CompetitionTime, CompetitionFee, CompetitionName)
                VALUES (@date, @time, @fee, @name)";

                SqlParameter[] parameters = {
                new SqlParameter("@date", date),
                new SqlParameter("@time", time),
                new SqlParameter("@fee", fee),
                new SqlParameter("@name", name)
            };

                db.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inserting competition: {ex.Message}", ex);
            }
        }

        public bool UpdateCompetition(int id, DateTime date, string time, decimal fee, string name)
        {
            try
            {
                string query = @"
                UPDATE Competition
                SET CompetitionDate = @date, CompetitionTime = @time, CompetitionFee = @fee, CompetitionName = @name
                WHERE CompetitionID = @id";

                SqlParameter[] parameters = {
                new SqlParameter("@id", id),
                new SqlParameter("@date", date),
                new SqlParameter("@time", time),
                new SqlParameter("@fee", fee),
                new SqlParameter("@name", name)
            };

                db.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating competition: {ex.Message}", ex);
            }
        }

        public bool DeleteCompetition(int id)
        {
            try
            {
                string query = "DELETE FROM Competition WHERE CompetitionID = @id";
                SqlParameter[] parameters = { new SqlParameter("@id", id) };

                db.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting competition: {ex.Message}", ex);
            }
        }

        public List<Competitionget> SearchCompetitions(string searchText)
        {
            List<Competitionget> list = new List<Competitionget>();

            try
            {
                string query = @"
                SELECT * FROM Competition
                WHERE CompetitionName LIKE @search OR CompetitionTime LIKE @search";

                SqlParameter[] parameters = {
                new SqlParameter("@search", "%" + searchText + "%")
            };

                DataTable dt = db.ExecuteQuery(query, parameters);

                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new Competitionget(
                        Convert.ToInt32(dr["CompetitionID"]),
                        Convert.ToDateTime(dr["CompetitionDate"]),
                        dr["CompetitionTime"].ToString(),
                        Convert.ToDecimal(dr["CompetitionFee"]),
                        dr["CompetitionName"].ToString()
                    ));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching competitions: {ex.Message}", ex);
            }

            return list;
        }
   
        public bool DoesCompetitionNameExist(string competitionName, int competitionId = 0)
        {
            string query = "SELECT COUNT(*) FROM Competition WHERE CompetitionName = @name";

            // Exclude current competition when updating
            if (competitionId > 0)
            {
                query += " AND CompetitionID != @id";
            }

            List<SqlParameter> parameters = new List<SqlParameter>
    {
        new SqlParameter("@name", competitionName)
    };

            if (competitionId > 0)
            {
                parameters.Add(new SqlParameter("@id", competitionId));
            }

            DataTable dt = db.ExecuteQuery(query, parameters.ToArray());

            if (dt.Rows.Count > 0)
            {
                int count = Convert.ToInt32(dt.Rows[0][0]);
                return count == 0; // true if unique
            }

            return true; // fallback, treat as unique
        }


        public List<int> GetCompetitionIDs()
        {
            List<int> ids = new List<int>();

            try
            {
                string query = "SELECT CompetitionID FROM Competition";
                DataTable dt = db.ExecuteQuery(query, new SqlParameter[0]);

                foreach (DataRow dr in dt.Rows)
                {
                    ids.Add(Convert.ToInt32(dr["CompetitionID"]));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching competition IDs: {ex.Message}", ex);
            }

            return ids;
        }
    
    }
}
