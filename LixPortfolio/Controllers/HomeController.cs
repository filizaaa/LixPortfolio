using LixPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LixPortfolio.Controllers
{
    public class HomeController : Controller
    {
        private static StarLabs_PortfolioEntities db = new StarLabs_PortfolioEntities();


        public ActionResult Index(string searching)
        {

            return View(db.Personals.Where(x => x.Email.Contains(searching) || searching == null || searching =="" ).ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize]
        public ActionResult Portfolio()
        {
            var user = User.Identity.Name;
  UserData.LoggedEmail = user;
         //   var email = user.Email;
            UserData ud = new UserData(UserData.LoggedEmail);
            ViewBag.Message = "Your Portfolio page.";
            UserData.SearchEmail = "";
          
         
            return View(ud.vmodel);
        }
        public ActionResult SearchedPortfolio(string searching)
        {   UserData.SearchEmail = searching;
            UserData ud = new UserData(UserData.SearchEmail);
            ViewBag.Message = "Searched Portofolio";
            return View("Portfolio",ud.vmodel);
        }
        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Search(string searching)
        {
            Personal personal = db.Personals.Find(searching);
            if (personal != null)
            {
                return View();
            }
            else
            {
                return View("Search");
            } 


        }
    }
}