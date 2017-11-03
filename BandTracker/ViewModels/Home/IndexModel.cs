using System;
using System.Collections.Generic;
using BandTracker.Models;

namespace BandTracker.ViewModels
{
    public class IndexModel
    {
        public List<Band> AllBands {get; private set;}
        public List<Venue> AllVenues {get; private set;}

        public IndexModel()
        {
            AllBands = Band.GetAll();
            AllVenues = Venue.GetAll();
        }
    }
}
