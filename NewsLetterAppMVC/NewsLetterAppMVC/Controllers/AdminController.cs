using NewsLetterAppMVC.Models;
using NewsLetterAppMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsLetterAppMVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            using (NewsletterEntities db = new NewsletterEntities())
            {
                var signUps = db.SignUps;
                List<SignUpVm> SignUpVms = new List<SignUpVm>();
                foreach (var signUp in signUps)
                {
                    SignUpVm signUpVm = new SignUpVm();
                    signUpVm.Id = signUp.Id;
                    signUpVm.FirstName = signUp.FirstName;
                    signUpVm.LastName = signUp.LastName;
                    signUpVm.EmailAddress = signUp.EmailAddress;
                    SignUpVms.Add(signUpVm);
                }

                return View(SignUpVms);
            }
        }
    }
}