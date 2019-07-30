using Capstone.Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class Weather
    {
        public string ParkCode { get; set; }
        public int FiveDayForecast { get; set; }
        public int Low { get; set; }
        public int High { get; set; }
        public string Forecast { get; set; }

        public string[] FahrenheitAdvice
        {
            get
            {
                List<string> weatheradvice = new List<string>();
                if (High > 75)
                {
                    weatheradvice.Add("Bring extra water, its a scorcher!");
                }
                if (Low < 20)
                {
                    weatheradvice.Add("Brrrrr!Cold temps are dangerous.Dress accordingly.");
                }
                if (High - Low > 20)
                {
                    weatheradvice.Add("Best to wear breathable layers today.");
                }
                if (Forecast.Replace(" ", "") == "rain")
                {
                    weatheradvice.Add("It's best if you pack rain gear and waterproof shoes!");
                }
                if (Forecast.Replace(" ", "") == "snow")
                {
                    weatheradvice.Add("Better pack some snow shoes!");
                }
                if (Forecast.Replace(" ", "") == "sunny")
                {
                    weatheradvice.Add("Gonna be a hot one! Pack some sun block.");
                }
                if (Forecast.Replace(" ", "") == "thunderstorms")
                {
                    weatheradvice.Add("Seek shelter and avoid hiking on exposed ridges!");
                }
                return weatheradvice.ToArray();
            }
        }

        public string[] CelsiusAdvice
        {
            get
            {
                List<string> advice = new List<string>();


                if (GetHighTemp(High) > 24)
                {
                    advice.Add("Bring extra water, its a scorcher!");
                }
                if (GetLowTemp(Low) < -4)
                {
                    advice.Add("Brrrrr!Cold temps are dangerous.Dress accordingly.");
                }
                if (GetHighTemp(High) - GetLowTemp(Low) > 11)
                {
                    advice.Add("Best to wear breathable layers today.");
                }
                if (Forecast.Replace(" ", "") == "rain")
                {
                    advice.Add("It's best if you pack rain gear and waterproof shoes!");
                }
                if (Forecast.Replace(" ", "") == "snow")
                {
                    advice.Add("Better pack some snow shoes!");
                }
                if (Forecast.Replace(" ", "") == "sunny")
                {
                    advice.Add("Gonna be a hot one! Pack some sun block.");
                }
                if (Forecast.Replace(" ", "") == "thunderstorms")
                {
                    advice.Add("Seek shelter and avoid hiking on exposed ridges!");
                }
                return advice.ToArray();
            }
        }

        int GetHighTemp(int temp)
        {
            return (int)((temp - 32) * 5 / 9);
        }

        int GetLowTemp(int temp)
        {
            return (int)((temp - 32) * 5 / 9);
        }
    }
}
