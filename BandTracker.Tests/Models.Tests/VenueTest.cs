using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BandTracker.Models;

namespace BandTracker.Models.Tests
{
    [TestClass]
    public class VenueTest : IDisposable
    {
        private Venue sampleVenue_ThePlace = new Venue("The Place");
        private Venue sampleVenue_ThePlace_2 = new Venue("The Place");
        private Venue sampleVenue_ASpot = new Venue("A Spot");
        private Band sampleBand_Transatlantic = new Band("Transatlantic");
        private Band sampleBand_Haken = new Band("Haken");

        public VenueTest()
        {
            DBConfiguration.ConnectionString = "server=localhost; user id=root; password=root; port=8889; database=band_tracker_test;";
        }
        public void Dispose()
        {
            DB.ClearAllTables();
        }

        [TestMethod]
        public void Equals_TwoEqualObjects_True()
        {
            Assert.AreEqual(sampleVenue_ThePlace, sampleVenue_ThePlace_2);
        }
        [TestMethod]
        public void Equals_TwoUnequalObjects_False()
        {
            Assert.AreNotEqual(sampleVenue_ThePlace, sampleVenue_ASpot);
        }
        [TestMethod]
        public void GetAll_DatabaseIsEmptyAtFirst_EmptyList()
        {
            int result = Venue.GetAll().Count;

            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void Save_SavesToDatabase_EntrySaved()
        {
            sampleVenue_ThePlace.Save();
            Venue result = Venue.GetAll()[0];

            Assert.AreEqual(sampleVenue_ThePlace, result);
        }
        [TestMethod]
        public void GetAll_GetsAllEntries_AllEntries()
        {
            sampleVenue_ThePlace.Save();
            sampleVenue_ASpot.Save();

            List<Venue> result = Venue.GetAll();
            List<Venue> test = new List<Venue> {sampleVenue_ThePlace, sampleVenue_ASpot};

            CollectionAssert.AreEqual(test, result);
        }
        [TestMethod]
        public void FindById_GetEntryWithMatchingId_Entry()
        {
            sampleVenue_ThePlace.Save();
            sampleVenue_ASpot.Save();

            Venue result = Venue.FindById(sampleVenue_ASpot.Id);

            Assert.AreEqual(sampleVenue_ASpot, result);
        }
        [TestMethod]
        public void UpdateAtId_UpdateEntryWithMatchingId_EntryUpdated()
        {
            sampleVenue_ThePlace.Save();
            string newName = "Not the place";
            Venue.UpdateAtId(sampleVenue_ThePlace.Id, newName);

            Venue result = Venue.FindById(sampleVenue_ThePlace.Id);
            Venue test = new Venue(newName, sampleVenue_ASpot.Id);

            Assert.AreEqual(test, result);
        }
        [TestMethod]
        public void DeleteAtId_DeletesEntryAtMatchingId_EntryDeleted()
        {
            sampleVenue_ThePlace.Save();
            sampleVenue_ASpot.Save();
            Venue.DeleteAtId(sampleVenue_ASpot.Id);

            List<Venue> result = Venue.GetAll();
            List<Venue> test = new List<Venue> {sampleVenue_ThePlace};

            CollectionAssert.AreEqual(test, result);
        }
        // [TestMethod]
        // public void GetConcerts_DatabaseIsEmptyAtFirst_EmptyList()
        // {
        //     sampleVenue_ThePlace.Save();
        //     int result = sampleVenue_ThePlace.GetConcerts().Count;
        //
        //     Assert.AreEqual(0, result);
        // }
        // [TestMethod]
        // public void GetConcerts_GetsAllConcertsAtVenue_Concerts()
        // {
        //     sampleVenue_ThePlace.Save();
        //     sampleBand_Haken.Save();
        //     sampleBand_Transatlantic.Save();
        //     sampleVenue_ThePlace.AddConcert(sampleBand_Haken.Id, new DateTime(2017, 11, 3));
        //     sampleVenue_ThePlace.AddConcert(sampleBand_Transatlantic.Id, new DateTime(2017, 11, 14));
        //
        //     List<Concert> result = sampleVenue_ThePlace.GetConcerts();
        //     List<Concert> test = new List<Concert> {sampleBand_Haken, sampleBand_Transatlantic};
        //
        //     CollectionAssert.AreEqual(test, result);
        // }
    }
}
