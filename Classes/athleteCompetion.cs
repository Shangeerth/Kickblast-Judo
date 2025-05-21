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
    internal class athleteCompetion
    {

        private Sql db;

        public athleteCompetion(Sql database)
        {
            db = database;
        }

        public List<AthleteCompetitionDetails> GetAthleteCompetitionDetails()
        {
            List<AthleteCompetitionDetails> detailsList = new List<AthleteCompetitionDetails>();

            try
            {
                string query = @"
                    SELECT    
                        ac.AthleteCompetitionID,
                        a.AthleteID,
                        a.Name AS AthleteName,
                        tp.TrainingPlanNames,
                        c.CompetitionID,
                        c.CompetitionName,
                        c.CompetitionDate,
                        c.CompetitionTime,
                        c.CompetitionFee
                    FROM AthleteCompetition ac
                    JOIN Athlete a ON ac.AthleteID = a.AthleteID
                    JOIN Competition c ON ac.CompetitionID = c.CompetitionID
                    OUTER APPLY (
                        SELECT STRING_AGG(tp.Name, ', ') AS TrainingPlanNames
                        FROM AthleteTrainingPlan atp
                        JOIN TrainingPlan tp ON atp.PlanID = tp.PlanID
                        WHERE atp.AthleteID = a.AthleteID
                    ) tp;";

                DataTable dt = db.ExecuteQuery(query, new SqlParameter[0]);

                foreach (DataRow dr in dt.Rows)
                {
                    detailsList.Add(new AthleteCompetitionDetails(
                        Convert.ToInt32(dr["AthleteCompetitionID"]),
                        Convert.ToInt32(dr["AthleteID"]),
                        dr["AthleteName"].ToString(),
                        dr["TrainingPlanNames"] == DBNull.Value ? "" : dr["TrainingPlanNames"].ToString(),
                        Convert.ToInt32(dr["CompetitionID"]),
                        dr["CompetitionName"].ToString(),
                        Convert.ToDateTime(dr["CompetitionDate"]),
                        dr["CompetitionTime"].ToString(),
                        dr["CompetitionFee"] == DBNull.Value ? 0m : Convert.ToDecimal(dr["CompetitionFee"])

                    ));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching athlete competition details: {ex.Message}", ex);
            }

            return detailsList;
        }







        public bool InsertAthleteCompetition(string athleteName, string competitionName)
        {
            try
            {
                // Get AthleteID from name
                string athleteQuery = "SELECT AthleteID FROM Athlete WHERE Name = @AthleteName";
                SqlParameter[] athleteParams = {
            new SqlParameter("@AthleteName", athleteName)
        };

                object athleteIdObj = db.ExecuteScalar(athleteQuery, athleteParams);
                if (athleteIdObj == null)
                    throw new Exception("Invalid athlete name. No matching athlete found.");

                int athleteId = Convert.ToInt32(athleteIdObj);

                // Get CompetitionID from name
                string competitionQuery = "SELECT CompetitionID FROM Competition WHERE CompetitionName = @CompetitionName";
                SqlParameter[] competitionParams = {
            new SqlParameter("@CompetitionName", competitionName)
        };

                object competitionIdObj = db.ExecuteScalar(competitionQuery, competitionParams);
                if (competitionIdObj == null)
                    throw new Exception("Invalid competition name. No matching competition found.");

                int competitionId = Convert.ToInt32(competitionIdObj);

                // Insert into AthleteCompetition
                string insertQuery = @"
            INSERT INTO AthleteCompetition (CompetitionID, AthleteID)
            VALUES (@competitionId, @athleteId)";

                SqlParameter[] insertParams = {
            new SqlParameter("@competitionId", competitionId),
            new SqlParameter("@athleteId", athleteId)
        };

                db.ExecuteNonQuery(insertQuery, insertParams);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inserting athlete competition: {ex.Message}", ex);
            }
        }


        public bool UpdateAthleteCompetition(int athleteCompetitionId, string athleteName, string competitionName)
        {
            try
            {
                // Get AthleteID from name
                string athleteQuery = "SELECT AthleteID FROM Athlete WHERE Name = @AthleteName";
                SqlParameter[] athleteParams = {
            new SqlParameter("@AthleteName", athleteName)
        };

                object athleteIdObj = db.ExecuteScalar(athleteQuery, athleteParams);
                if (athleteIdObj == null)
                    throw new Exception("Invalid athlete name. No matching athlete found.");

                int athleteId = Convert.ToInt32(athleteIdObj);

                // Get CompetitionID from name
                string competitionQuery = "SELECT CompetitionID FROM Competition WHERE CompetitionName = @CompetitionName";
                SqlParameter[] competitionParams = {
            new SqlParameter("@CompetitionName", competitionName)
        };

                object competitionIdObj = db.ExecuteScalar(competitionQuery, competitionParams);
                if (competitionIdObj == null)
                    throw new Exception("Invalid competition name. No matching competition found.");

                int competitionId = Convert.ToInt32(competitionIdObj);

                // Perform the update
                string updateQuery = @"
            UPDATE AthleteCompetition
            SET AthleteID = @AthleteID,
                CompetitionID = @CompetitionID
            WHERE AthleteCompetitionID = @AthleteCompetitionID";

                SqlParameter[] updateParams = {
            new SqlParameter("@AthleteID", athleteId),
            new SqlParameter("@CompetitionID", competitionId),
            new SqlParameter("@AthleteCompetitionID", athleteCompetitionId)
        };

                db.ExecuteNonQuery(updateQuery, updateParams);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating athlete competition: {ex.Message}", ex);
            }
        }


        public bool Delete(int id)
        {
            try
            {
                string query = "DELETE FROM AthleteCompetition WHERE AthleteCompetitionID = @id";
                SqlParameter[] parameters = {
            new SqlParameter("@id", id)
        };

                db.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting trainer: {ex.Message}", ex);
            }
        }

        public List<AthleteCompetitionDetails> SearchAthleteCompetitionDetails(string searchText)
        {
            List<AthleteCompetitionDetails> detailsList = new List<AthleteCompetitionDetails>();

            try
            {
                string query = @"
            SELECT    
                ac.AthleteCompetitionID,
                a.AthleteID,
                a.Name AS AthleteName,
                tp.TrainingPlanNames,
                c.CompetitionID,
                c.CompetitionName,
                c.CompetitionDate,
                c.CompetitionFee,
                c.CompetitionTime
            FROM AthleteCompetition ac
            JOIN Athlete a ON ac.AthleteID = a.AthleteID
            JOIN Competition c ON ac.CompetitionID = c.CompetitionID
            OUTER APPLY (
                SELECT STRING_AGG(tp.Name, ', ') AS TrainingPlanNames
                FROM AthleteTrainingPlan atp
                JOIN TrainingPlan tp ON atp.PlanID = tp.PlanID
                WHERE atp.AthleteID = a.AthleteID
            ) tp
            WHERE a.Name LIKE @search OR c.CompetitionName LIKE @search";

                SqlParameter[] parameters = {
            new SqlParameter("@search", "%" + searchText + "%")
        };

                DataTable dt = db.ExecuteQuery(query, parameters);

                foreach (DataRow dr in dt.Rows)
                {
                    detailsList.Add(new AthleteCompetitionDetails(
                        Convert.ToInt32(dr["AthleteCompetitionID"]),
                        Convert.ToInt32(dr["AthleteID"]),
                        dr["AthleteName"].ToString(),
                        dr["TrainingPlanNames"] == DBNull.Value ? "" : dr["TrainingPlanNames"].ToString(),
                        Convert.ToInt32(dr["CompetitionID"]),
                        dr["CompetitionName"].ToString(),
                        Convert.ToDateTime(dr["CompetitionDate"]),
                        dr["CompetitionTime"].ToString(),
                        dr["CompetitionFee"] == DBNull.Value ? 0m : Convert.ToDecimal(dr["CompetitionFee"])
                    ));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching athlete competition details: {ex.Message}", ex);
            }

            return detailsList;
        }


        public List<string> GetAthleteNamesByTrainingPlan()
        {
            List<string> athleteNames = new List<string>();

            try
            {
                string query = @"
            SELECT DISTINCT a.Name AS AthleteName
            FROM Athlete a
            JOIN AthleteTrainingPlan atp ON a.AthleteID = atp.AthleteID
            JOIN TrainingPlan tp ON atp.PlanID = tp.PlanID
            WHERE tp.Name IN ('Intermediate', 'Elite')";

                DataTable dt = db.ExecuteQuery(query, new SqlParameter[0]);

                foreach (DataRow row in dt.Rows)
                {
                    athleteNames.Add(row["AthleteName"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching athlete names by training plan: {ex.Message}", ex);
            }

            return athleteNames;
        }


        


        public List<string> GetAllTrainingPlanNames()
        {
            try
            {
                string query = @"
            SELECT DISTINCT Name 
            FROM TrainingPlan 
            WHERE Name IN ('Intermediate', 'Elite')";

                DataTable dt = db.ExecuteQuery(query, null); // no parameters needed

                List<string> plans = new List<string>();
                foreach (DataRow row in dt.Rows)
                {
                    plans.Add(row["Name"].ToString());
                }

                return plans;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching Intermediate and Elite training plan names: {ex.Message}", ex);
            }
        }


        public bool IsAthleteEnrolledInTrainingPlan(string athleteName, string trainingPlanName)
        {
            try
            {
                string query = @"
            SELECT COUNT(*) 
            FROM Athlete a
            JOIN AthleteTrainingPlan atp ON a.AthleteID = atp.AthleteID
            JOIN TrainingPlan tp ON atp.PlanID = tp.PlanID
            WHERE a.Name = @AthleteName AND tp.Name = @PlanName";

                SqlParameter[] parameters = {
            new SqlParameter("@AthleteName", athleteName),
            new SqlParameter("@PlanName", trainingPlanName)
        };

                int count = Convert.ToInt32(db.ExecuteScalar(query, parameters));
                return count > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error checking training plan enrollment: " + ex.Message, ex);
            }
        }
        

        public bool CheckAthleteCompetitionExists(string athleteName, string competitionName, string planName)
        {
            string query = @"
        SELECT COUNT(*) FROM AthleteCompetition ac
        JOIN Athlete a ON ac.AthleteID = a.AthleteID
        JOIN Competition c ON ac.CompetitionID = c.CompetitionID
        JOIN AthleteTrainingPlan atp ON a.AthleteID = atp.AthleteID
        JOIN TrainingPlan tp ON atp.PlanID = tp.PlanID
        WHERE a.Name = @AthleteName AND c.CompetitionName = @CompetitionName AND tp.Name = @PlanName";

            SqlParameter[] parameters = {
        new SqlParameter("@AthleteName", athleteName),
        new SqlParameter("@CompetitionName", competitionName),
        new SqlParameter("@PlanName", planName)
    };

            int count = Convert.ToInt32(db.ExecuteScalar(query, parameters));
            return count > 0;
        }



        public List<string> GetAllCompetitionNames()
        {
            try
            {
                string query = "SELECT CompetitionName FROM Competition";

                DataTable dt = db.ExecuteQuery(query, null); // Assuming `db` is your database helper

                List<string> competitionNames = new List<string>();
                foreach (DataRow row in dt.Rows)
                {
                    competitionNames.Add(row["CompetitionName"].ToString());
                }

                return competitionNames;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching competition names: {ex.Message}", ex);
            }
        }

        public decimal GetPlanFeeByName(string planName)
        {
            try
            {
                string query = "SELECT CompetitionFee FROM Competition WHERE CompetitionName = @PlanName";
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@PlanName", planName)
                };

                object result = db.ExecuteScalar(query, parameters);

                return result != DBNull.Value ? Convert.ToDecimal(result) : 0m;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching training plan fee: {ex.Message}", ex);
            }
        }


        public string GetPlanTimeByName(string planName)
        {
            try
            {
                string query = "SELECT CompetitionTime FROM Competition WHERE CompetitionName = @PlanName";
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@PlanName", planName)
                };

                object result = db.ExecuteScalar(query, parameters);

                return result != DBNull.Value ? result.ToString() : string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching training plan time: {ex.Message}", ex);
            }
        }

        public DateTime? GetPlanDateByName(string planName)
        {
            try
            {
                string query = "SELECT CompetitionDate FROM Competition WHERE CompetitionName = @PlanName";
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@PlanName", planName)
                };

                object result = db.ExecuteScalar(query, parameters);

                return result != DBNull.Value ? Convert.ToDateTime(result) : (DateTime?)null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching training plan date: {ex.Message}", ex);
            }
        }

        public List<int> id()
        {
            List<int> ids = new List<int>();

            try
            {
                string query = "SELECT AthleteCompetitionID FROM AthleteCompetition";
                DataTable dt = db.ExecuteQuery(query, new SqlParameter[0]);

                foreach (DataRow dr in dt.Rows)
                {
                    ids.Add(Convert.ToInt32(dr["AthleteCompetitionID"]));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching category IDs: {ex.Message}", ex);
            }

            return ids;
        }


    }
}
