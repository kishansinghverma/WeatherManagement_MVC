using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherTodayDAL;

namespace WeatherTodayApp.Controllers
{
    public class UserController : Controller
    {
        WeatherTodayRepo repo;

        public UserController()
        {
            repo = new WeatherTodayRepo();
        }

        [HttpGet]
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(Models.UserDetails ModelUserDetails)
        {
            if (ModelState.IsValid)
            {
                UserDetail entityUserDetail = new UserDetail();
                entityUserDetail.User_Emaild = ModelUserDetails.User_Emaild;
                entityUserDetail.User_First_Name = ModelUserDetails.User_First_Name;
                entityUserDetail.User_ID = ModelUserDetails.User_ID;
                entityUserDetail.User_Last_Name = ModelUserDetails.User_Last_Name;
                entityUserDetail.User_Password = ModelUserDetails.User_Password;

                if (repo.addUser(entityUserDetail))
                {
                    return RedirectToAction("LoginUser", "User");
                }
                else
                {
                    ViewBag.Err = "Something went wrong";
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult LoginUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginUser(string UEmail, string Password)
        {
            string emailID = repo.validateUser(UEmail, Password);

            if (emailID != null)
            {
                Session.Add("UEmail", emailID);
                return RedirectToAction("ViewAllCityWeatherList", "Weather");
            }
            else
            {
                ViewBag.Err = "User Not Registered";
                return View();
            }
        }

        [HttpGet]
        public ActionResult UpdateProfile() {

            if (Session["UEmail"] != null)
            {
                String email = (string)Session["UEmail"];
                UserDetail currentUserDetail = repo.GetUserByEmail(email);
                Models.UserDetails modelUserDetail = new Models.UserDetails();
                modelUserDetail.User_Emaild = currentUserDetail.User_Emaild;
                modelUserDetail.User_First_Name = currentUserDetail.User_First_Name;
                modelUserDetail.User_ID = currentUserDetail.User_ID;
                modelUserDetail.User_Last_Name = currentUserDetail.User_Last_Name;
                modelUserDetail.User_Password = currentUserDetail.User_Password;
                return View(modelUserDetail);
            }
            else
            {
                ViewBag.Err = "No Such User Exist";
                return View();
            }
        }

        [HttpPost]
        public ActionResult UpdateProfile(Models.UserDetails ModelUserDetail)
        {
            if (ModelState.IsValid)
            {
                UserDetail entityUserDetail = new UserDetail();
                entityUserDetail.User_Emaild = ModelUserDetail.User_Emaild;
                entityUserDetail.User_First_Name = ModelUserDetail.User_First_Name;
                entityUserDetail.User_Last_Name = ModelUserDetail.User_Last_Name;
                entityUserDetail.User_Password = ModelUserDetail.User_Password;
                bool status = repo.updateUserDetails(entityUserDetail);

                if (status)
                    ViewBag.Msg = "Profile Updated Successfully!";
                else
                    ViewBag.Err = "Something Went Wrong";
            }

            return View();
        }

    }
}