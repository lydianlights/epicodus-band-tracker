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
        [HttpGet("/bands/{bandId}")]
        public ActionResult BandDetails(int bandId)
        {
            var model = new BandDetailsModel(bandId);
            return View(model);
        }
        [HttpPost("/bands/{bandId}/concerts/add")]
        public ActionResult AddConcertToBand(int bandId)
        {
            int venueId = Int32.Parse(Request.Form["concert-venue"]);
            DateTime date = DateTime.Parse(Request.Form["concert-date"]);
            Concert.AddByIds(bandId, venueId, date);
            return Redirect($"/bands/{bandId}");
        }
        [HttpGet("/venues/{venueId}")]
        public ActionResult VenueDetails(int venueId)
        {
            var model = new VenueDetailsModel(venueId);
            return View(model);
        }
        [HttpPost("/venues/{venueId}/concerts/add")]
        public ActionResult AddConcertToVenue(int venueId)
        {
            int bandId = Int32.Parse(Request.Form["concert-band"]);
            DateTime date = DateTime.Parse(Request.Form["concert-date"]);
            Concert.AddByIds(bandId, venueId, date);
            return Redirect($"/venues/{venueId}");
        }
    }
}
