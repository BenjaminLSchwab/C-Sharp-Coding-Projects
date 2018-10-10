using NewsLetterAppMVC.Models;
using NewsLetterAppMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsLetterAppMVC.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(string firstName, string lastName, string emailAddress)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress))
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            using (NewsletterEntities db = new NewsletterEntities())
            {
                var signUp = new SignUp();
                signUp.FirstName = firstName;
                signUp.LastName = lastName;
                signUp.EmailAddress = emailAddress;

                db.SignUps.Add(signUp);
                db.SaveChanges();
            }
                return View("Success");
        }

    }
}