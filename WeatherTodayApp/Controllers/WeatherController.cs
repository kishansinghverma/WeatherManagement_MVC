using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherTodayDAL;

namespace WeatherTodayApp.Controllers
{
    public class WeatherController : Controller
    {
        WeatherTodayRepo repo;

        public WeatherController()
        {
            repo = new WeatherTodayRepo();
        }

        [HttpGet]
        public ActionResult CreateWeather() {
            return View();
        }

        [HttpPost]
        public ActionResult CreateWeather(Models.WeatherDetails ModelWeatherDetails) {

            if (ModelState.IsValid)
            {
                WeatherDetail entityWeatherDetail = new WeatherDetail();
                entityWeatherDetail.CityID = ModelWeatherDetails.CityID;
                entityWeatherDetail.CityName = ModelWeatherDetails.CityName;
                entityWeatherDetail.CountryName = ModelWeatherDetails.CountryName;
                entityWeatherDetail.Humidity = ModelWeatherDetails.Humidity;
                entityWeatherDetail.Temperature = ModelWeatherDetails.Temperature;
                entityWeatherDetail.Visibility = ModelWeatherDetails.Visibility;

                bool status = repo.AddWeatherDetails(entityWeatherDetail);
                if (status)
                    ViewBag.Msg = "Weather Data Stored!";
                else
                    ViewBag.Err = "Something Went Wrong!";
            }
            return View();
        }

        [HttpGet]
        public ActionResult GetWeatherByCity() {
            return View("SearchWeatherByCity");
        }

        [HttpPost]
        public ActionResult GetWeatherByCity(FormCollection FC) {
            List<Models.WeatherDetails> modelWeatherDetails = new List<Models.WeatherDetails>();
            List<GETWEATHERBYCITY_Result> entityWeatherDetails = repo.getWeatherByCity(FC.Get("CityName"));

            foreach(GETWEATHERBYCITY_Result wd in entityWeatherDetails)
            {
                Models.WeatherDetails weatherDetails = new Models.WeatherDetails();
                weatherDetails.CityID = wd.CityID;
                weatherDetails.CityName = wd.CityName;
                weatherDetails.CountryName = wd.CountryName;
                weatherDetails.Humidity = wd.Humidity;
                weatherDetails.Temperature = wd.Temperature;
                weatherDetails.Visibility = wd.Visibility;
                modelWeatherDetails.Add(weatherDetails);
            }

            return View(modelWeatherDetails);
        }

        [HttpGet]
        public ActionResult UpdateWeather(int CityId) {
            Models.WeatherDetails modelWeatherDetails = new Models.WeatherDetails();
            WeatherDetail entityWeatherDetail = repo.GetWeatherDetailsByCityID(CityId);

            modelWeatherDetails.CityID = entityWeatherDetail.CityID;
            modelWeatherDetails.CityName = entityWeatherDetail.CityName;
            modelWeatherDetails.CountryName = entityWeatherDetail.CountryName;
            modelWeatherDetails.Humidity = entityWeatherDetail.Humidity;
            modelWeatherDetails.Temperature = entityWeatherDetail.Temperature;
            modelWeatherDetails.Visibility = entityWeatherDetail.Visibility;

            return View(modelWeatherDetails);

        }

        [HttpPost]
        public ActionResult UpdateWeather(Models.WeatherDetails ModelWeatherdetails)
        {
            if (ModelState.IsValid)
            {
                WeatherDetail entityWeatherDetail = new WeatherDetail();
                entityWeatherDetail.CityID = ModelWeatherdetails.CityID;
                entityWeatherDetail.CityName = ModelWeatherdetails.CityName;
                entityWeatherDetail.CountryName = ModelWeatherdetails.CountryName;
                entityWeatherDetail.Humidity = ModelWeatherdetails.Humidity;
                entityWeatherDetail.Temperature = ModelWeatherdetails.Temperature;
                entityWeatherDetail.Visibility = ModelWeatherdetails.Visibility;

                try
                {
                    bool status = repo.updateWeatherDetails(entityWeatherDetail);
                    if (status)
                        ViewBag.Msg = "Weather Updated Successfully";
                    else
                        ViewBag.Err = "Something Went Wrong!";
                }
                catch (Exception e) { }
            }
            return View();
        }

        [HttpGet]
        public ActionResult DeleteWeather(int CityId) {
            WeatherDetail entityWeatherDetail = repo.GetWeatherDetailsByCityID(CityId);
            Models.WeatherDetails modelWeatherDetails = new Models.WeatherDetails();

            modelWeatherDetails.CityName = entityWeatherDetail.CityName;
            modelWeatherDetails.CityID = entityWeatherDetail.CityID;
            modelWeatherDetails.CountryName = entityWeatherDetail.CountryName;
            modelWeatherDetails.Humidity = entityWeatherDetail.Humidity;
            modelWeatherDetails.Temperature = entityWeatherDetail.Temperature;
            modelWeatherDetails.Visibility = entityWeatherDetail.Visibility;

            return View(modelWeatherDetails);

        }

        [HttpPost]
        public ActionResult DeleteWeather(Models.WeatherDetails WeatherDetails) {
            bool status = repo.deleteWeatherDetails(WeatherDetails.CityID);
            if (status)
            {
                return RedirectToAction("ViewAllCityWeatherList");
            }
            else
            {
                ViewBag.Err = "Something went wrong!";
            }
            return View();
        }

        [HttpGet]
        public ActionResult ViewAllCityWeatherList() {
            List<GETALLCITYWEATHER_Result> entityWeatherDetails = repo.viewAllCityWeatherList();
            List<Models.WeatherDetails> modelWeatherDetails = new List<Models.WeatherDetails>();

            foreach(GETALLCITYWEATHER_Result wd in entityWeatherDetails)
            {
                Models.WeatherDetails weatherDetails = new Models.WeatherDetails();
                weatherDetails.CityID = wd.CityID;
                weatherDetails.CityName = wd.CityName;
                weatherDetails.CountryName = wd.CountryName;
                weatherDetails.Humidity = wd.Humidity;
                weatherDetails.Temperature = wd.Temperature;
                weatherDetails.Visibility = wd.Visibility;
                modelWeatherDetails.Add(weatherDetails);
            }
            return View(modelWeatherDetails);  
        }
    }
}