using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherTodayDAL
{
   
    public class WeatherTodayRepo
    {
        WeatherDBEntities context;

        public WeatherTodayRepo()
        {
            context = new WeatherDBEntities();
        }

        public bool AddWeatherDetails(WeatherDetail weatherDetail)
        {
            bool status = false;

            context.WeatherDetails.Add(weatherDetail);
            try
            {
                context.SaveChanges();
                status = true;
            }
            catch (Exception e){}

            return status;
        }

        public List<GETWEATHERBYCITY_Result> getWeatherByCity(string CityName)
        {   
            return context.GETWEATHERBYCITY(CityName).ToList();
        }

        public WeatherDetail GetWeatherDetailsByCityID(int cityID)
        {
            return context.WeatherDetails.Find(cityID);
        }

        public bool updateWeatherDetails(WeatherDetail weatherDetail)
        {
            bool status=false;

            WeatherDetail details = context.WeatherDetails.FirstOrDefault(wd => wd.CityID == weatherDetail.CityID);
            if (details != null)
            {
                details.Humidity = weatherDetail.Humidity;
                details.Temperature = weatherDetail.Temperature;
                details.Visibility = weatherDetail.Visibility;
                try
                {
                    context.SaveChanges();
                    status = true;
                }
                catch (Exception e) { }
            }

            return status;
        }

        public bool deleteWeatherDetails(int cityID)
        {
            bool status = false;
            WeatherDetail weatherDetail = context.WeatherDetails.Find(cityID);
            if (weatherDetail != null)
            {
                context.WeatherDetails.Remove(weatherDetail);
                try
                {
                    context.SaveChanges();
                    status = true;
                }
                catch (Exception e) { }
            }

            return status;
        }

        public List<GETALLCITYWEATHER_Result> viewAllCityWeatherList()
        {
            return context.GETALLCITYWEATHER().ToList();
        }

        public string validateUser(string Email, string Password)
        {
            string userEmail = (from userDetail in context.UserDetails
                               where (userDetail.User_Emaild.Equals(Email) && userDetail.User_Password.Equals(Password))
                               select userDetail.User_Emaild).FirstOrDefault();

            return userEmail;
        }

        public bool addUser(UserDetail detail)
        {
            bool status = false;

            context.UserDetails.Add(detail);
            try
            {
                context.SaveChanges();
                status = true;
            }
            catch(Exception e) { }

            return status;
        }

        public UserDetail GetUserByEmail(string eMailID)
        {
            return context.UserDetails.FirstOrDefault(user => user.User_Emaild.Equals(eMailID));
        }

        public bool updateUserDetails(UserDetail detail)
        {
            bool status = false;

            UserDetail userDetail = context.UserDetails.FirstOrDefault(ud => ud.User_Emaild.Equals(detail.User_Emaild));

            if (userDetail != null)
            {
                userDetail.User_First_Name = detail.User_First_Name;
                userDetail.User_Last_Name = detail.User_Last_Name;
                userDetail.User_Password = detail.User_Password;
                userDetail.User_Emaild = detail.User_Emaild;

                try
                {
                    context.SaveChanges();
                    status = true;
                }
                catch(Exception e) {}
            }

            return status;
        }
    }
}
