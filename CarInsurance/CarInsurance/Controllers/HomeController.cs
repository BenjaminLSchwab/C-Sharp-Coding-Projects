using CarInsurance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarInsurance.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendForm(string firstName, string lastName, string emailAddress, DateTime dateOfBirth, DateTime carYear, string carMake, string carModel, bool dui, int speedTickets, bool fullCoverage)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress) || dateOfBirth == null || carYear == null || string.IsNullOrEmpty(carMake) || string.IsNullOrEmpty(carModel))
            {
                return View("~/Views/Shared/Error.cshtml");
            }

                decimal quoteAmount = 50;

            using (CarInsuranceEntities db = new CarInsuranceEntities())
            {
                var quote = new Quote();


                if(DateTime.Now - dateOfBirth < TimeSpan.FromDays(365 * 25))// if user is under 25
                {
                    quoteAmount += 25;
                }
                if (DateTime.Now - dateOfBirth < TimeSpan.FromDays(365 * 18))// if user is under 18
                {
                    quoteAmount += 100;
                }
                else if (DateTime.Now - dateOfBirth > TimeSpan.FromDays(365 * 100))// if user is over 100
                {
                    quoteAmount += 25;
                }

                if (carYear.Year < 2000)
                {
                    quoteAmount += 25;
                }
                else if (carYear.Year > 2015)
                {
                    quoteAmount += 25;
                }

                if (carMake.ToLower().Contains("porsche"))
                {
                    quoteAmount += 25;

                    if (carModel.ToLower().Contains("911 carrera"))
                    {
                        quoteAmount += 25;
                    }
                }

                quoteAmount += speedTickets * 10;

                if (dui)
                {
                    quoteAmount *= (decimal)1.25;
                }

                if (fullCoverage)
                {
                    quoteAmount *= (decimal)1.5;
                }

                quote.FirstName = firstName;
                quote.LastName = lastName;
                quote.EmailAddress = emailAddress;
                quote.DateOfBirth = dateOfBirth;
                quote.CarYear = carYear;
                quote.CarMake = carMake;
                quote.CarModel = carModel;
                quote.Dui = dui;
                quote.SpeedTickets = speedTickets;
                quote.FullCoverage = fullCoverage;
                quote.Quote1 = quoteAmount;

                db.Quotes.Add(quote);
                db.SaveChanges();
            }

                return View("Quote", quoteAmount);
        }
    }
}