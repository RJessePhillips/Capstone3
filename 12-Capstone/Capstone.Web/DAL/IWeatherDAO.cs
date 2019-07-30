using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public interface IWeatherDAO
    {
        //daily weather for 5 day forecast
        List<Weather> GetWeather(string parkCode);
    }
}
