using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using BandTracker.Models;
using BandTracker.ViewModels;

namespace BandTracker.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
            var model = new IndexModel();
            return View(model);
        }
        [HttpPost("/bands/add")]
        public ActionResult AddBand()
        {
            string name = Request.Form["band-name"];
            var newBand = new Band(name);
            newBand.Save();
            return Redirect("/");
        }
        [HttpPost("/venues/add")]
        public ActionResult AddVenue()
        {
            string name = Request.Form["venue-name"];
            var newVenue = new Venue(name);
            newVenue.Save();
            return Redirect("/");
        }
    }
}
