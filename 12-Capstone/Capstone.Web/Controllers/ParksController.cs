using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Web.Controllers
{
    public class ParksController : Controller
    {
        private IParkDAO parkDAO;
        private IWeatherDAO weatherDAO;

        public ParksController(IParkDAO parkDAO, IWeatherDAO weatherDAO)
        {
            //saves dependency injected DAO
            this.parkDAO = parkDAO;
            this.weatherDAO = weatherDAO;
        }


        [HttpGet]
        public IActionResult GetParks()
        {
            //get parks from database as a list
            IList<Park> parks = parkDAO.GetParks();
            return View(parks);
        }


        [HttpGet]
        public IActionResult GetPark(string parkCode)
        {
            //if user clicks on a park - calls GET and returns a lists park details
            ParkDetailVM vm = new ParkDetailVM();
            vm.Park = parkDAO.GetPark(parkCode);
            vm.Weather = weatherDAO.GetWeather(parkCode);
            vm.Temperature = GetPreferredTemp();
            return View(vm);
        }



        [HttpPost]
        public IActionResult GetPark(ParkDetailVM vm)
        {
            //posts selected temperature preference to session
            SetPreferredTemp(vm.Temperature);
            return RedirectToAction("GetPark", "Parks", new { ParkCode = vm.Park.ParkCode });
        }



        private string GetPreferredTemp()
        {
            //get temp setting F/C from session and return it
            string temp = HttpContext.Session.GetString("PreferredTemp");
            return temp ?? "Fahrenheit";
        }


        private void SetPreferredTemp(string temp)
        {
            //setting F/C into session
            if (temp == null || temp.Trim().Length == 0)
            {
                ClearPreferredTemp();
            }
            else
            {
                HttpContext.Session.SetString("PreferredTemp", temp);
            }
        }


        private void ClearPreferredTemp()
        {
            HttpContext.Session.Remove("PreferredTemp");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}