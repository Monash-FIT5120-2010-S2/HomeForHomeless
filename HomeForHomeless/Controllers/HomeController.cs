using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeForHomeless.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VolunteerOrgan()
        {
            ViewBag.Message = "Volunteer Organisation";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Volunteer()
        {
            return View();
        }

        public ActionResult Visualisation()
        {
            ViewBag.Message = "Homeless people by suburbs in Victoria";
            return View();
        }

        public ActionResult VisualisationDetailsNumberByYear()
        {
            return View();
        }
        public ActionResult VisualisationGender()
        {
            return View();
        }

        public ActionResult VisualisationDetailsSuburb()
        {
            return View();
        }

        public ActionResult VisualisationDetailsState()
        {
            return View();
        }

        public ActionResult VisualisationDetailsAgeGroup()
        {
            return View();
        }

        public ActionResult VisualisationDetailsOperationGroups()
        {
            return View();
        }

        public ActionResult Quiz()
        {
            return View();
        }
        public ActionResult VolunteerQuiz()
        {
            return View();
        }
        public ActionResult VolunteerLearn()
        {
            return View();
        }
        public ActionResult VolunteerTrainingQuiz()
        {
            return View();
        }

        public ActionResult VolunteerTraining()
        {
            return View();
        }
        public ActionResult HomelessQuiz()
        {
            return View();
        }

        public ActionResult HomelessTraining()

        {
            return View();
        }
    }
}