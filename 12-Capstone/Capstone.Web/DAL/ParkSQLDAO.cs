using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class ParkSQLDAO : IParkDAO
    {
        private readonly string connectionString;

        
        public ParkSQLDAO(string connectionString)
        {
            //saving connection string so it is usable
            this.connectionString = connectionString;
        }

        //single park
        public Park GetPark(string parkCode)
        {
            Park park = new Park();
            try
            {
                // Create a new connection object
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();
                    //SQL statement with parameter 
                    string sql = $"SELECT * FROM park where parkCode = @parkCode";
                    //sql command object to use statement
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    //binding parameter to park code passed in
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);

                    // Execute the command
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Loop through each row
                    park = null;
                    while (reader.Read())
                    {
                        // Create a park
                        park = RowToObject(reader);
                    }
                    return park;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        //all parks
        public IList<Park> GetParks()
        {
            List<Park> output = new List<Park>();

            try
            {
                // Create a new connection object
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    string sql = $"SELECT * FROM park order by parkName";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    // Execute the command
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Loop through each row
                    while (reader.Read())
                    {
                        //get all parks as a list
                        Park post = RowToObject(reader);
                        output.Add(post);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            //returns list of parks
            return output;
        }


        private Park RowToObject(SqlDataReader reader)
        {
            Park  park = new Park();
            park.ParkCode = Convert.ToString(reader["parkCode"]);
            park.ParkName = Convert.ToString(reader["parkName"]);
            park.State = Convert.ToString(reader["state"]);        
            park.Acreage = String.Format("{0:n0}",Convert.ToInt32(reader["acreage"]));
            park.Elevation = String.Format("{0:n0}", Convert.ToInt32(reader["elevationInFeet"]));
            park.Miles = Convert.ToInt32(reader["milesOfTrail"]);
            park.Campsites = String.Format("{0:n0}", Convert.ToInt32(reader["numberOfCampsites"]));
            park.Climate = Convert.ToString(reader["climate"]);
            park.Founded = Convert.ToInt32(reader["yearFounded"]);
            park.Visitors = String.Format("{0:n0}", Convert.ToInt32(reader["annualVisitorCount"]));
            park.Quote = Convert.ToString(reader["inspirationalQuote"]);
            park.QuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
            park.Description = Convert.ToString(reader["parkDescription"]);
            park.Fee = Convert.ToInt32(reader["entryFee"]);
            park.Animals = Convert.ToInt32(reader["numberOfAnimalSpecies"]);
            return park;
        }           
    }
}
