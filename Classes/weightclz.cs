using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming_Assigment.Database
{
    internal class weightclz
    {

        private Sql db;

        public weightclz(Sql database)
        {
            db = database;
        }

        public List<weight> GetWeightCategories()
        {
            List<weight> categoryList = new List<weight>();

            try
            {
                string query = "SELECT CategoryID, Name, MinWeight, MaxWeight FROM WeightCategory";
                DataTable dt = db.ExecuteQuery(query, new SqlParameter[0]);

                foreach (DataRow dr in dt.Rows)
                {
                    categoryList.Add(new weight(
                        Convert.ToInt32(dr["CategoryID"]),
                        dr["Name"].ToString(),
                        Convert.ToInt32(dr["MinWeight"]),
                        dr["MaxWeight"] != DBNull.Value ? Convert.ToInt32(dr["MaxWeight"]) : (int?)null
                    ));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching weight categories: {ex.Message}", ex);
            }

            return categoryList;
        }

        public bool InsertWeightCategory(string name, int minWeight, int? maxWeight)
        {
            try
            {
                string query = @"
                    INSERT INTO WeightCategory (Name, MinWeight, MaxWeight)
                    VALUES (@name, @minWeight, @maxWeight)";

                SqlParameter[] parameters = {
                    new SqlParameter("@name", name),
                    new SqlParameter("@minWeight", minWeight),
                    new SqlParameter("@maxWeight", maxWeight.HasValue ? (object)maxWeight.Value : DBNull.Value)
                };

                db.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inserting weight category: {ex.Message}", ex);
            }
        }

        public bool UpdateWeightCategory(int categoryId, string name, int minWeight, int? maxWeight)
        {
            try
            {
                string query = @"
                    UPDATE WeightCategory
                    SET Name = @name,
                        MinWeight = @minWeight,
                        MaxWeight = @maxWeight
                    WHERE CategoryID = @categoryId";

                SqlParameter[] parameters = {
                    new SqlParameter("@categoryId", categoryId),
                    new SqlParameter("@name", name),
                    new SqlParameter("@minWeight", minWeight),
                    new SqlParameter("@maxWeight", maxWeight.HasValue ? (object)maxWeight.Value : DBNull.Value)

                };

                db.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating weight category: {ex.Message}", ex);
            }
        }

        public bool DeleteWeightCategory(int categoryId)
        {
            try
            {
                string query = "DELETE FROM WeightCategory WHERE CategoryID = @categoryId";
                SqlParameter[] parameters = {
                    new SqlParameter("@categoryId", categoryId)
                };

                db.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting weight category: {ex.Message}", ex);
            }
        }

        public List<weight> SearchWeightCategories(string searchText)
        {
            List<weight> categories = new List<weight>();

            try
            {
                string query = @"
                    SELECT * FROM WeightCategory
                    WHERE 
                        Name LIKE @search OR
                        CategoryID LIKE @search";

                SqlParameter[] parameters = {
                    new SqlParameter("@search", "%" + searchText + "%")
                };

                DataTable dt = db.ExecuteQuery(query, parameters);

                foreach (DataRow dr in dt.Rows)
                {
                    categories.Add(new weight(
                        Convert.ToInt32(dr["CategoryID"]),
                        dr["Name"].ToString(),
                        Convert.ToInt32(dr["MinWeight"]),
                        dr["MaxWeight"] != DBNull.Value ? Convert.ToInt32(dr["MaxWeight"]) : (int?)null
                    ));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching weight categories: {ex.Message}", ex);
            }

            return categories;
        }

        public bool IsWeightCategoryNameUnique(string categoryName, int categoryId = 0)
        {
            string query = "SELECT COUNT(*) FROM WeightCategory WHERE Name = @name";

            // Exclude current category during update
            if (categoryId > 0)
            {
                query += " AND CategoryID != @id";
            }

            List<SqlParameter> parameters = new List<SqlParameter>
    {
        new SqlParameter("@name", categoryName)
    };

            if (categoryId > 0)
            {
                parameters.Add(new SqlParameter("@id", categoryId));
            }

            DataTable dt = db.ExecuteQuery(query, parameters.ToArray());

            if (dt.Rows.Count > 0)
            {
                int count = Convert.ToInt32(dt.Rows[0][0]);
                return count == 0; // true if unique
            }

            return true; // fallback to true
        }


        public List<int> GetCategoryIds()
        {
            List<int> ids = new List<int>();

            try
            {
                string query = "SELECT CategoryID FROM WeightCategory";
                DataTable dt = db.ExecuteQuery(query, new SqlParameter[0]);

                foreach (DataRow dr in dt.Rows)
                {
                    ids.Add(Convert.ToInt32(dr["CategoryID"]));
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

