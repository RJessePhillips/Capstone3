using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class SurveySQLDAO : ISurveyDAO
    {
        private readonly string connectionString;

        public SurveySQLDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<SurveyVM> GetAllSurveys()
        {
            List<SurveyVM> surveyList = new List<SurveyVM>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = $"SELECT parkName, park.parkCode, COUNT(s.parkCode) as tally FROM park JOIN survey_result as s on s.parkCode = park.parkCode GROUP BY parkName, park.parkCode ORDER BY tally desc   ";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    // Loop through each row
                    while (reader.Read())
                    {
                        SurveyVM vm = new SurveyVM();
                        vm.Tally = Convert.ToInt32(reader ["tally"]);
                        vm.ParkName = Convert.ToString(reader["parkName"]);
                        vm.ParkCode = Convert.ToString(reader["parkCode"]);
                        surveyList.Add(vm);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return surveyList;
        }


        
        public int SaveNewSurvey(Survey survey)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = $"INSERT INTO survey_result VALUES (@parkCode, @emailAddress, @state, @activityLevel); Select @@Identity;";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@parkCode", survey.ParkName);
                    cmd.Parameters.AddWithValue("@emailAddress", survey.Email);
                    cmd.Parameters.AddWithValue("@state", survey.State);                   
                    cmd.Parameters.AddWithValue("@activityLevel", survey.ActivityLevel);
                    int surveyId = Convert.ToInt32(cmd.ExecuteScalar());
                    return surveyId;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
