using Programming_Assigment.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming_Assigment.Athlete
{
    internal class TrainerCLZ
    {
        private Sql db = new Sql();

        public TrainerCLZ(Sql database)
        {
            db = database;
        }



        public List<Trainergetset> GetTrainers()
        {
            List<Trainergetset> trainerList = new List<Trainergetset>();

            try
            {
                string query = @"
        SELECT 
            id,
            name,
            age,
            qualification,
            nic,
            contact,
            experience,
            salary
        FROM trainer";

                DataTable dt = db.ExecuteQuery(query, new SqlParameter[0]);

                foreach (DataRow dr in dt.Rows)
                {
                    trainerList.Add(new Trainergetset(
                        Convert.ToInt32(dr["id"]),
                        dr["name"].ToString(),
                        Convert.ToInt32(dr["age"]),
                        dr["qualification"].ToString(),
                        dr["nic"].ToString(),
                        dr["contact"].ToString(),
                        Convert.ToInt32(dr["experience"]),
                        Convert.ToDecimal(dr["salary"])
                    ));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching trainers: {ex.Message}", ex);
            }

            return trainerList;
        }

        public bool InsertTrainer(string name, int age, int experience, string qualification, string nic, int contact, decimal salary)
        {
            try
            {
                string query = @"
        INSERT INTO trainer (name, age, qualification, nic, contact, experience, salary)
        VALUES (@name, @age, @qualification, @nic, @contact, @experience, @salary)";

                SqlParameter[] parameters = {
            new SqlParameter("@name", name),
            new SqlParameter("@age", age),
            new SqlParameter("@qualification", qualification),
            new SqlParameter("@nic", nic),
            new SqlParameter("@contact", contact),
            new SqlParameter("@experience", experience),
            new SqlParameter("@salary", salary)
        };

                db.ExecuteNonQuery(query, parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inserting trainer: {ex.Message}", ex);
            }
        }

        public bool UpdateTrainer(int trainerId, string name, int age, int experience, string qualification, string nic, int contact, decimal salary)
        {
            try
            {
                string query = @"
        UPDATE trainer
        SET 
            name = @name,
            age = @age,
            qualification = @qualification,
            nic = @nic,
            contact = @contact,
            experience = @experience,
            salary = @salary
        WHERE id = @id";

                SqlParameter[] parameters = {
            new SqlParameter("@id", trainerId),
            new SqlParameter("@name", name),
            new SqlParameter("@age", age),
            new SqlParameter("@qualification", qualification),
            new SqlParameter("@nic", nic),
            new SqlParameter("@contact", contact),
            new SqlParameter("@experience", experience),
            new SqlParameter("@salary", salary)
        };

                db.ExecuteNonQuery(query, parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating trainer: {ex.Message}", ex);
            }
        }

        public bool DeleteTrainer(int id)
        {
            try
            {
                string query = "DELETE FROM trainer WHERE id = @id";
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

        public List<Trainergetset> SearchTrainers(string searchText)
        {
            List<Trainergetset> trainers = new List<Trainergetset>();

            try
            {
                string query = @"
        SELECT * FROM trainer
        WHERE 
            name LIKE @search OR
            qualification LIKE @search OR
            nic LIKE @search OR
            id LIKE @search";

                SqlParameter[] parameters = {
            new SqlParameter("@search", "%" + searchText + "%")
        };

                DataTable dt = db.ExecuteQuery(query, parameters);

                foreach (DataRow dr in dt.Rows)
                {
                    trainers.Add(new Trainergetset(
                        Convert.ToInt32(dr["id"]),
                        dr["name"].ToString(),
                        Convert.ToInt32(dr["age"]),
                        dr["qualification"].ToString(),
                        dr["nic"].ToString(),
                        dr["contact"].ToString(),
                        Convert.ToInt32(dr["experience"]),
                        Convert.ToDecimal(dr["salary"])
                    ));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching trainers: {ex.Message}", ex);
            }

            return trainers;
        }

        public bool IsTrainerNameUnique(string trainerName, int trainerId = 0)
        {
            string query = "SELECT COUNT(*) FROM Trainer WHERE Name = @name";

            // Exclude the current trainer when updating
            if (trainerId > 0)
            {
                query += " AND TrainerID != @id";
            }

            List<SqlParameter> parameters = new List<SqlParameter>
    {
        new SqlParameter("@name", trainerName)
    };

            if (trainerId > 0)
            {
                parameters.Add(new SqlParameter("@id", trainerId));
            }

            DataTable dt = db.ExecuteQuery(query, parameters.ToArray());

            if (dt.Rows.Count > 0)
            {
                int count = Convert.ToInt32(dt.Rows[0][0]);
                return count == 0; // true = unique
            }

            return true; // fallback: treat as unique
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
    }
}
