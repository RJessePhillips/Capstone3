using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Web.Controllers
{
    public class SurveyController : Controller
    {
        private ISurveyDAO surveyDAO;

        public SurveyController(ISurveyDAO surveyDAO)
        {
            //saves dependency injected DAO
            this.surveyDAO = surveyDAO;
        }

        [HttpGet]
        public IActionResult GetAllSurvey()
        {
            IList<SurveyVM> surveyList = surveyDAO.GetAllSurveys();

            return View(surveyList);
        }

        [HttpGet]
        public IActionResult AddSurvey()
        {
            //gets invoked when user clicks on survey in nav bar
            return View();
        }

        [HttpPost]
        public IActionResult AddSurvey(Survey survey)
        {
            //called when survey is posted
            if (!ModelState.IsValid)
            {
                return View(survey);
            }

            surveyDAO.SaveNewSurvey(survey);

            TempData["message"] = $"Your message was posted.";

            return RedirectToAction("GetAllSurvey");
        }
    }
}