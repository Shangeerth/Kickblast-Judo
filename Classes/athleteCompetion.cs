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
                FROM AthleteCompetitionTrainingPlan actp
                JOIN TrainingPlan tp ON actp.PlanID = tp.PlanID
                WHERE actp.AthleteCompetitionID = ac.AthleteCompetitionID
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




 
        public bool InsertAthleteCompetition(int athleteId, int competitionId, List<int> planIds)
        {
            try
            {
                if (CheckAthleteCompetitionExists(athleteId, competitionId, ""))
                    return false;

                // Step 1: Insert into AthleteCompetition and get the new ID
                string insertCompetitionQuery = @"
            INSERT INTO AthleteCompetition (CompetitionID, AthleteID)
            VALUES (@competitionId, @athleteId);
            SELECT SCOPE_IDENTITY();";

                SqlParameter[] insertParams = {
            new SqlParameter("@competitionId", competitionId),
            new SqlParameter("@athleteId", athleteId)
        };

                object result = db.ExecuteScalar(insertCompetitionQuery, insertParams);
                int athleteCompetitionId = Convert.ToInt32(result);

                // Step 2: Insert into AthleteCompetitionTrainingPlan for each selected plan
                foreach (int planId in planIds)
                {
                    string insertTrainingPlanQuery = @"
                INSERT INTO AthleteCompetitionTrainingPlan (AthleteCompetitionID, PlanID)
                VALUES (@athleteCompetitionId, @planId);";

                    SqlParameter[] trainingPlanParams = {
                new SqlParameter("@athleteCompetitionId", athleteCompetitionId),
                new SqlParameter("@planId", planId)
            };

                    db.ExecuteNonQuery(insertTrainingPlanQuery, trainingPlanParams);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inserting athlete competition with training plans: {ex.Message}", ex);
            }
        }



        public bool UpdateAthleteCompetition(int athleteCompetitionId, int newAthleteId, int newCompetitionId, List<int> newPlanIds)
        {
            try
            {
                // Step 1: Update AthleteCompetition
                string updateCompetitionQuery = @"
            UPDATE AthleteCompetition
            SET AthleteID = @athleteId,
                CompetitionID = @competitionId
            WHERE AthleteCompetitionID = @athleteCompetitionId;";

                SqlParameter[] updateParams = {
            new SqlParameter("@athleteId", newAthleteId),
            new SqlParameter("@competitionId", newCompetitionId),
            new SqlParameter("@athleteCompetitionId", athleteCompetitionId)
        };

                db.ExecuteNonQuery(updateCompetitionQuery, updateParams);

                // Step 2: Delete existing training plans for this AthleteCompetitionID
                string deletePlansQuery = @"
            DELETE FROM AthleteCompetitionTrainingPlan
            WHERE AthleteCompetitionID = @athleteCompetitionId;";

                SqlParameter[] deleteParams = {
            new SqlParameter("@athleteCompetitionId", athleteCompetitionId)
        };

                db.ExecuteNonQuery(deletePlansQuery, deleteParams);

                // Step 3: Insert updated training plan IDs
                foreach (int planId in newPlanIds)
                {
                    string insertPlanQuery = @"
                INSERT INTO AthleteCompetitionTrainingPlan (AthleteCompetitionID, PlanID)
                VALUES (@athleteCompetitionId, @planId);";

                    SqlParameter[] insertParams = {
                new SqlParameter("@athleteCompetitionId", athleteCompetitionId),
                new SqlParameter("@planId", planId)
            };

                    db.ExecuteNonQuery(insertPlanQuery, insertParams);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating athlete competition and training plans: {ex.Message}", ex);
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

        public int GetTrainingPlanIdByName(string planName)
        {
            string query = "SELECT PlanID FROM TrainingPlan WHERE Name = @name";
            SqlParameter[] parameters = {
        new SqlParameter("@name", planName)
    };

            object result = db.ExecuteScalar(query, parameters);

            return result != null ? Convert.ToInt32(result) : -1; // return -1 if not found
        }

        public List<int> GetAthleteIdsByTrainingPlan()
        {
            List<int> athleteIds = new List<int>();

            try
            {
                string query = @"
            SELECT DISTINCT a.AthleteID
            FROM Athlete a
            JOIN AthleteTrainingPlan atp ON a.AthleteID = atp.AthleteID
            JOIN TrainingPlan tp ON atp.PlanID = tp.PlanID
            WHERE tp.Name IN ('Intermediate', 'Elite')";

                DataTable dt = db.ExecuteQuery(query, new SqlParameter[0]);

                foreach (DataRow row in dt.Rows)
                {
                    athleteIds.Add(Convert.ToInt32(row["AthleteID"]));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching athlete IDs by training plan: {ex.Message}", ex);
            }

            return athleteIds;
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

        public bool IsAthleteEnrolledInTrainingPlan(int athleteId, string trainingPlanName)
        {
            try
            {
                string query = @"
            SELECT COUNT(*) 
            FROM Athlete a
            JOIN AthleteTrainingPlan atp ON a.AthleteID = atp.AthleteID
            JOIN TrainingPlan tp ON atp.PlanID = tp.PlanID
            WHERE a.AthleteID = @AthleteID AND tp.Name = @PlanName";

                SqlParameter[] parameters = {
            new SqlParameter("@AthleteID", athleteId),
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



        public bool CheckAthleteCompetitionExists(int athleteId, int competitionId, string planName)
{
    try
    {
        string query = @"
           SELECT COUNT(*)
FROM AthleteCompetition ac
JOIN AthleteTrainingPlan atp ON ac.AthleteID = atp.AthleteID
JOIN TrainingPlan tp ON atp.PlanID = tp.PlanID
WHERE ac.AthleteID = @AthleteID
  AND ac.CompetitionID = @CompetitionID
  AND tp.Name = @PlanName";

        SqlParameter[] parameters = {
            new SqlParameter("@AthleteID", athleteId),
            new SqlParameter("@CompetitionID", competitionId),
            new SqlParameter("@PlanName", planName)
        };

        object result = db.ExecuteScalar(query, parameters);
        int count = Convert.ToInt32(result);

        return count > 0;
    }
    catch (Exception ex)
    {
        throw new Exception($"Error checking athlete competition existence: {ex.Message}", ex);
    }
}




        public string GetCompetitionNameById(int competitionId)
        {
            try
            {
                string query = "SELECT CompetitionName FROM Competition WHERE CompetitionID = @CompetitionID";
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@CompetitionID", competitionId)
                };

                object result = db.ExecuteScalar(query, parameters);

                return result != DBNull.Value ? result.ToString() : string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching competition name: {ex.Message}", ex);
            }
        }


        public decimal GetCompetitionFeeById(int competitionId)
        {
            try
            {
                string query = "SELECT CompetitionFee FROM Competition WHERE CompetitionID = @CompetitionID";
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@CompetitionID", competitionId)
                };

                object result = db.ExecuteScalar(query, parameters);

                return result != DBNull.Value ? Convert.ToDecimal(result) : 0m;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching competition fee: {ex.Message}", ex);
            }
        }

        public string GetCompetitionTimeById(int competitionId)
        {
            try
            {
                string query = "SELECT CompetitionTime FROM Competition WHERE CompetitionID = @CompetitionID";
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@CompetitionID", competitionId)
                };

                object result = db.ExecuteScalar(query, parameters);

                return result != DBNull.Value ? result.ToString() : string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching competition time: {ex.Message}", ex);
            }
        }

        public DateTime? GetCompetitionDateById(int competitionId)
        {
            try
            {
                string query = "SELECT CompetitionDate FROM Competition WHERE CompetitionID = @CompetitionID";
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@CompetitionID", competitionId)
                };

                object result = db.ExecuteScalar(query, parameters);

                return result != DBNull.Value ? Convert.ToDateTime(result) : (DateTime?)null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching competition date: {ex.Message}", ex);
            }
        }

        public List<int> id()
        {
            List<int> ids = new List<int>();

            try
            {
                string query = "SELECT AthleteCompetitionID FROM AthleteCompetition";
                DataTable dt = db.ExecuteQuery(query, new SqlParameter[0]);

                foreach (DataRow row in dt.Rows)
                {
                    ids.Add(Convert.ToInt32(row["AthleteCompetitionID"]));
                }

                return ids;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching AthleteCompetition IDs: " + ex.Message, ex);
            }
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
                {
                    return result.ToString();
                }
                else
                {
                    throw new Exception("No athlete found with the provided ID.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching athlete name: {ex.Message}", ex);
            }
        }


        public List<int> GetCompetitionIds()
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
                throw new Exception($"Error fetching Competition IDs: {ex.Message}", ex);
            }

            return ids;
        }

    }
}
