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
    }
}
