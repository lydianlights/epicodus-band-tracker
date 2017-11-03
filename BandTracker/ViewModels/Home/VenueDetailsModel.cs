using System;
using System.Collections.Generic;
using BandTracker.Models;

namespace BandTracker.ViewModels
{
    public class VenueDetailsModel
    {
        public Venue CurrentVenue {get; private set;}
        public List<Concert> CurrentVenueConcerts {get; private set;}
        public List<Band> AllBands {get; private set;}

        public VenueDetailsModel(int venueId)
        {
            CurrentVenue = Venue.FindById(venueId);
            CurrentVenueConcerts = CurrentVenue.GetConcerts();
            AllBands = Band.GetAll();
        }
    }
}
