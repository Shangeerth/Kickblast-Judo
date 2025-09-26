using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;
using static Programming_Assigment.Database.AthleteTrainingPlan;

namespace Programming_Assigment.Classes
{
    internal class paymentClz
    {

        private Sql db = new Sql();

        public paymentClz(Sql database)
        {
            db = database;
        }




        public bool InsertPayment(int athleteId, decimal amount, DateTime paymentDate)
        {
            try
            {
                string query = @"INSERT INTO Payment (AthleteID, Amount, PaymentDate)
                                 VALUES (@AthleteID, @Amount, @PaymentDate)";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@AthleteID", athleteId),
                    new SqlParameter("@Amount", amount),
                    new SqlParameter("@PaymentDate", paymentDate)
                };

                int rowsAffected = db.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inserting payment: {ex.Message}", ex);
            }
        }



        public List<int> GetAllAthleteIds()
        {
            List<int> athleteIds = new List<int>();

            try
            {
                string query = "SELECT AthleteID FROM Athlete";
                DataTable dt = db.ExecuteQuery(query, new SqlParameter[0]);

                foreach (DataRow row in dt.Rows)
                {
                    athleteIds.Add(Convert.ToInt32(row["AthleteID"]));
                }

                return athleteIds;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching athlete IDs: {ex.Message}", ex);
            }
        }
        public string GetAthleteNameById(int athleteId)
        {
            try
            {
                string query = "SELECT Name FROM Athlete WHERE AthleteID = @AthleteID";
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@AthleteID", athleteId)
                };

                DataTable dt = db.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Name"].ToString();
                }
                else
                {
                    throw new Exception("Athlete not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching athlete name: {ex.Message}", ex);
            }
        }

        public decimal GetTotalCompetitionFeeForMonthById(int athleteId, DateTime selectedDate)
        {
            decimal totalFee = 0;

            try
            {
                string query = @"
        SELECT 
            SUM(C.CompetitionFee) AS TotalFee
        FROM 
            AthleteCompetition AC
        JOIN 
            Athlete A ON AC.AthleteID = A.AthleteID
        JOIN 
            Competition C ON AC.CompetitionID = C.CompetitionID
        WHERE 
            A.AthleteID = @AthleteID AND 
            MONTH(C.CompetitionDate) = @Month AND 
            YEAR(C.CompetitionDate) = @Year";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@AthleteID", athleteId),
            new SqlParameter("@Month", selectedDate.Month),
            new SqlParameter("@Year", selectedDate.Year)
                };

                DataTable dt = db.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0 && dt.Rows[0]["TotalFee"] != DBNull.Value)
                {
                    totalFee = Convert.ToDecimal(dt.Rows[0]["TotalFee"]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error calculating total competition fee: " + ex.Message, ex);
            }

            return totalFee;
        }
        public decimal GetTotalPrivateCoachingPaymentForMonthById(int athleteId, DateTime selectedDate)
        {
            decimal totalPayment = 0;

            try
            {
                string query = @"
        SELECT 
            SUM(PC.HoursPerWeek * PC.FeesPerHour) AS TotalPayment
        FROM 
            PrivateCoaching PC
        JOIN 
            Athlete A ON PC.AthleteID = A.AthleteID
        WHERE 
            A.AthleteID = @AthleteID AND 
            MONTH(PC.CoachingDate) = @Month AND 
            YEAR(PC.CoachingDate) = @Year";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@AthleteID", athleteId),
            new SqlParameter("@Month", selectedDate.Month),
            new SqlParameter("@Year", selectedDate.Year)
                };

                DataTable dt = db.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0 && dt.Rows[0]["TotalPayment"] != DBNull.Value)
                {
                    totalPayment = Convert.ToDecimal(dt.Rows[0]["TotalPayment"]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error calculating total private coaching payment: " + ex.Message, ex);
            }

            return totalPayment;
        }
        public decimal GetTotalTrainingPlanPaymentForMonthById(int athleteId, DateTime selectedDate)
        {
            decimal totalPayment = 0m;

            try
            {
                string query = @"
        SELECT DISTINCT
            TP.PlanID,
            TP.WeeklyFee
        FROM AthleteTrainingPlan ATP
        JOIN TrainingPlan TP ON ATP.PlanID = TP.PlanID
        JOIN Athlete A ON ATP.AthleteID = A.AthleteID
        WHERE A.AthleteID = @AthleteID
          AND MONTH(ATP.SessionDate) = @Month
          AND YEAR(ATP.SessionDate) = @Year";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@AthleteID", athleteId),
            new SqlParameter("@Month", selectedDate.Month),
            new SqlParameter("@Year", selectedDate.Year)
                };

                DataTable dt = db.ExecuteQuery(query, parameters);

                foreach (DataRow dr in dt.Rows)
                {
                    decimal weeklyFee = Convert.ToDecimal(dr["WeeklyFee"]);

                    // Payment for the month = weekly fee * 4 weeks
                    decimal paymentForPlan = weeklyFee * 4;

                    totalPayment += paymentForPlan;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error calculating training plan payment: " + ex.Message, ex);
            }

            return totalPayment;
        }

        //
        public string GetTrainerNameFromPrivateCoachingById(int athleteId, DateTime selectedDate)
        {
            string trainerName = "";

            try
            {
                string query = @"
        SELECT TOP 1 T.name AS TrainerName
        FROM PrivateCoaching PC
        JOIN Athlete A ON PC.AthleteID = A.AthleteID
        JOIN Trainer T ON PC.TrainerID = T.id
        WHERE A.AthleteID = @AthleteID
          AND MONTH(PC.CoachingDate) = @Month
          AND YEAR(PC.CoachingDate) = @Year";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@AthleteID", athleteId),
            new SqlParameter("@Month", selectedDate.Month),
            new SqlParameter("@Year", selectedDate.Year)
                };

                DataTable dt = db.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    trainerName = dt.Rows[0]["TrainerName"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching trainer name: " + ex.Message, ex);
            }

            return trainerName;
        }
        //
        public string GetCategoryWithMaxWeightByAthleteId(int athleteId)
        {
            string result = "";

            try
            {
                string query = @"
        SELECT WC.Name, WC.MaxWeight
        FROM Athlete A
        JOIN WeightCategory WC ON A.CategoryID = WC.CategoryID
        WHERE A.AthleteID = @AthleteID";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@AthleteID", athleteId)
                };

                DataTable dt = db.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    string categoryName = dt.Rows[0]["Name"]?.ToString() ?? "Unknown";
                    string maxWeight = dt.Rows[0]["MaxWeight"]?.ToString() ?? "N/A";

                    if (categoryName.Equals("Heavyweight", StringComparison.OrdinalIgnoreCase))
                    {
                        result = $"{categoryName} (<100)";
                    }
                    else if (maxWeight != "N/A")
                    {
                        result = $"{categoryName} (Max: {maxWeight} kg)";
                    }
                    else
                    {
                        result = $"{categoryName} (Max weight not found)";
                    }
                }
                else
                {
                    result = "No category found";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching category and max weight: " + ex.Message, ex);
            }

            return result;
        }
        ///
        public string GetTrainingPlanNameByAthleteId(int athleteId)
        {
            string trainingPlanName = "";

            try
            {
                string query = @"
        SELECT TOP 1 TP.Name AS TrainingPlanName
        FROM AthleteTrainingPlan ATP
        JOIN Athlete A ON ATP.AthleteID = A.AthleteID
        JOIN TrainingPlan TP ON ATP.PlanID = TP.PlanID
        WHERE A.AthleteID = @AthleteID
        ORDER BY ATP.SessionDate DESC";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@AthleteID", athleteId)
                };

                DataTable dt = db.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    trainingPlanName = dt.Rows[0]["TrainingPlanName"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching training plan name: " + ex.Message, ex);
            }

            return trainingPlanName;
        }



        public string GetMaxWeightByCategoryName(string categoryName)
        {
            string maxWeightStr = "";

            try
            {
                string query = @"
          SELECT MaxWeight
          FROM WeightCategory
          WHERE Name = @CategoryName";

                SqlParameter[] parameters = new SqlParameter[]
                {
          new SqlParameter("@CategoryName", categoryName)
                };

                DataTable dt = db.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0 && dt.Rows[0]["MaxWeight"] != DBNull.Value)
                {
                    int maxWeight = Convert.ToInt32(dt.Rows[0]["MaxWeight"]);
                    maxWeightStr = maxWeight.ToString(); // Convert to string
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching MaxWeight: " + ex.Message, ex);
            }

            return maxWeightStr;
     
        
        
        }

        public int GetMaxWeightByAthleteId(int athleteId)
        {
            try
            {
                string query = @"
        SELECT WC.MaxWeight
        FROM Athlete A
        JOIN WeightCategory WC ON A.CategoryID = WC.CategoryID
        WHERE A.AthleteID = @AthleteID";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@AthleteID", athleteId)
                };

                DataTable dt = db.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0 && dt.Rows[0]["MaxWeight"] != DBNull.Value)
                {
                    return Convert.ToInt32(dt.Rows[0]["MaxWeight"]);
                }
                else
                {
                    return -1; // No result or null
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching max weight: " + ex.Message, ex);
            }
        }


        public int GetAthleteWeightById(int athleteId)
        {
            try
            {
                string query = @"
        SELECT Weight
        FROM Athlete
        WHERE AthleteID = @AthleteID";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@AthleteID", athleteId)
                };

                DataTable dt = db.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0 && dt.Rows[0]["Weight"] != DBNull.Value)
                {
                    return Convert.ToInt32(dt.Rows[0]["Weight"]);
                }
                else
                {
                    return -1; // No athlete weight found
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching athlete weight: " + ex.Message, ex);
            }
        }

    }
}
