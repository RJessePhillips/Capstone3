using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class WeatherSQLDAO : IWeatherDAO
    {
        private readonly string connectionString;

        public WeatherSQLDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }


        public List<Weather> GetWeather(string parkCode)
        {
            List<Weather> weathers = new List<Weather>();
            try
            {
                // Create a new connection object
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    string sql = $"SELECT * FROM weather where parkCode = @parkCode";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);

                    // Execute the command
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Loop through each row
                
                    while (reader.Read())
                    {
                        Weather weather = new Weather();
                        weather.ParkCode = Convert.ToString(reader["parkCode"]);
                        weather.FiveDayForecast = Convert.ToInt32(reader["fiveDayForecastValue"]);
                        weather.Forecast = Convert.ToString(reader["forecast"]);
                        weather.Low = Convert.ToInt32(reader["low"]);
                        weather.High = Convert.ToInt32(reader["high"]);
                        weathers.Add(weather);
                     
                    }
                    return weathers;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }


        private List<Weather> RowToObject(SqlDataReader reader)
        {
            List<Weather> weathers = new List<Weather>();
      
            return weathers;
        }

    }
}
