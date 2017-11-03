using System;
using System.Collections.Generic;
using BandTracker.Models;

namespace BandTracker.ViewModels
{
    public class BandDetailsModel
    {
        public Band CurrentBand {get; private set;}
        public List<Concert> CurrentBandConcerts {get; private set;}
        public List<Venue> AllVenues {get; private set;}

        public BandDetailsModel(int bandId)
        {
            CurrentBand = Band.FindById(bandId);
            CurrentBandConcerts = CurrentBand.GetConcerts();
            AllVenues = Venue.GetAll();
        }
    }
}
