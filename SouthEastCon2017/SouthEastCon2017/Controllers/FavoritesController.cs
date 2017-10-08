using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SouthEastCon2017.Controllers
{
    public class FavoritesController : Controller
    {

        /// <summary>

        /// Get the names of all of the people in the demo database and supply to the view

        /// </summary>

        /// <returns>list of people in the object simpleinfo and redirects to the associated view</returns>

        public ActionResult Favorites()

        {

            string connectionString;

            string strCurDate = DateTime.Now.ToString("MM/dd/yyyy");

            string strYesterdaysDate = DateTime.Now.AddDays(-1).ToString("MM/dd/yyyy");

            ViewBag.strCurDate = strCurDate;

            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            simpledata simpleinfo = new simpledata();

            peopleData PeopleInfo = new peopleData();

            simpleinfo = PeopleInfo.getPeople(connectionString);

            simpleinfo.strYesterdaysDate = strYesterdaysDate;

            return View("Favorites", simpleinfo);

        }



        public ActionResult getFavorites(string userID)

        {

            string connectionString;

            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            favorites favoriteinfo = new favorites();

            peopleData PeopleInfo = new peopleData();

            favoriteinfo = PeopleInfo.getFavoritesData(connectionString, userID);



            return Json(new

            {

                Result = "OK",

                favoriteinfo = favoriteinfo

            });

        }





    }

}