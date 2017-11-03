using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BandTracker.Models;

namespace BandTracker.Models.Tests
{
    [TestClass]
    public class ConcertTest : IDisposable
    {
        private Venue sampleVenue_ThePlace = new Venue("The Place");
        private Venue sampleVenue_ASpot = new Venue("A Spot");
        private Band sampleBand_Transatlantic = new Band("Transatlantic");
        private Band sampleBand_Haken = new Band("Haken");

        private Concert sampleConcert_TransatlanticAtThePlace;
        private Concert sampleConcert_TransatlanticAtThePlace_2;
        private Concert sampleConcert_TransatlanticAtASpot;
        private Concert sampleConcert_HakenAtASpot;

        public ConcertTest()
        {
            DBConfiguration.ConnectionString = "server=localhost; user id=root; password=root; port=8889; database=band_tracker_test;";

            sampleVenue_ThePlace.Save();
            sampleVenue_ASpot.Save();
            sampleBand_Transatlantic.Save();
            sampleBand_Haken.Save();

            sampleConcert_TransatlanticAtThePlace = new Concert(sampleBand_Transatlantic, sampleVenue_ThePlace, new DateTime(2017, 11, 3));
            sampleConcert_TransatlanticAtThePlace_2 = new Concert(sampleBand_Transatlantic, sampleVenue_ThePlace, new DateTime(2017, 11, 3));
            sampleConcert_TransatlanticAtASpot = new Concert(sampleBand_Transatlantic, sampleVenue_ASpot, new DateTime(2017, 11, 27));
            sampleConcert_HakenAtASpot = new Concert(sampleBand_Haken, sampleVenue_ASpot, new DateTime(2017, 11, 14));
        }
        public void Dispose()
        {
            DB.ClearAllTables();
        }

        [TestMethod]
        public void Equals_TwoEqualObjects_True()
        {
            Assert.AreEqual(sampleConcert_TransatlanticAtThePlace, sampleConcert_TransatlanticAtThePlace_2);
        }
        [TestMethod]
        public void Equals_TwoUnequalObjects_False()
        {
            Assert.AreNotEqual(sampleConcert_TransatlanticAtThePlace, sampleConcert_HakenAtASpot);
        }
        [TestMethod]
        public void GetAll_DatabaseIsEmptyAtFirst_EmptyList()
        {
            int result = Concert.GetAll().Count;

            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void Save_SavesToDatabase_EntrySaved()
        {
            sampleConcert_TransatlanticAtThePlace.Save();
            Concert result = Concert.GetAll()[0];

            Assert.AreEqual(sampleConcert_TransatlanticAtThePlace, result);
        }
        [TestMethod]
        public void GetAll_GetsAllEntries_AllEntries()
        {
            sampleConcert_TransatlanticAtThePlace.Save();
            sampleConcert_TransatlanticAtASpot.Save();
            sampleConcert_HakenAtASpot.Save();

            List<Concert> result = Concert.GetAll();
            List<Concert> test = new List<Concert> {sampleConcert_TransatlanticAtThePlace, sampleConcert_TransatlanticAtASpot, sampleConcert_HakenAtASpot};

            CollectionAssert.AreEqual(test, result);
        }
    }
}
