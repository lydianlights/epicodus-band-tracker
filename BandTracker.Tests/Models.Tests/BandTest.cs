using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BandTracker.Models;

namespace BandTracker.Models.Tests
{
    [TestClass]
    public class BandTest : IDisposable
    {
        private Band sampleBand_Transatlantic = new Band("Transatlantic");
        private Band sampleBand_Transatlantic_2 = new Band("Transatlantic");
        private Band sampleBand_Haken = new Band("Haken");
        private Venue sampleVenue_ThePlace = new Venue("The Place");
        private Venue sampleVenue_ASpot = new Venue("A Spot");

        public BandTest()
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
            Assert.AreEqual(sampleBand_Transatlantic, sampleBand_Transatlantic_2);
        }
        [TestMethod]
        public void Equals_TwoUnequalObjects_False()
        {
            Assert.AreNotEqual(sampleBand_Transatlantic, sampleBand_Haken);
        }
        [TestMethod]
        public void GetAll_DatabaseIsEmptyAtFirst_EmptyList()
        {
            int result = Band.GetAll().Count;

            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void Save_SavesToDatabase_EntrySaved()
        {
            sampleBand_Transatlantic.Save();
            Band result = Band.GetAll()[0];

            Assert.AreEqual(sampleBand_Transatlantic, result);
        }
        [TestMethod]
        public void GetAll_GetsAllEntries_AllEntries()
        {
            sampleBand_Transatlantic.Save();
            sampleBand_Haken.Save();

            List<Band> result = Band.GetAll();
            List<Band> test = new List<Band> {sampleBand_Transatlantic, sampleBand_Haken};

            CollectionAssert.AreEqual(test, result);
        }
        [TestMethod]
        public void FindById_GetEntryWithMatchingId_Entry()
        {
            sampleBand_Transatlantic.Save();
            sampleBand_Haken.Save();

            Band result = Band.FindById(sampleBand_Haken.Id);

            Assert.AreEqual(sampleBand_Haken, result);
        }
        [TestMethod]
        public void UpdateAtId_UpdateEntryWithMatchingId_EntryUpdated()
        {
            sampleBand_Transatlantic.Save();
            string newName = "Not the place";
            Band.UpdateAtId(sampleBand_Transatlantic.Id, newName);

            Band result = Band.FindById(sampleBand_Transatlantic.Id);
            Band test = new Band(newName, sampleBand_Haken.Id);

            Assert.AreEqual(test, result);
        }
        [TestMethod]
        public void DeleteAtId_DeletesEntryAtMatchingId_EntryDeleted()
        {
            sampleBand_Transatlantic.Save();
            sampleBand_Haken.Save();
            Band.DeleteAtId(sampleBand_Haken.Id);

            List<Band> result = Band.GetAll();
            List<Band> test = new List<Band> {sampleBand_Transatlantic};

            CollectionAssert.AreEqual(test, result);
        }
        [TestMethod]
        public void GetConcerts_DatabaseIsEmptyAtFirst_EmptyList()
        {
            sampleVenue_ThePlace.Save();
            int result = sampleVenue_ThePlace.GetConcerts().Count;

            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void GetConcerts_GetsAllConcertsOfBand_Concerts()
        {
            sampleBand_Haken.Save();
            sampleVenue_ThePlace.Save();
            sampleVenue_ASpot.Save();
            sampleBand_Haken.AddConcert(sampleVenue_ThePlace, new DateTime(2017, 11, 3));
            sampleBand_Haken.AddConcert(sampleVenue_ASpot, new DateTime(2017, 11, 14));

            Concert resultConcert1 = sampleBand_Haken.GetConcerts()[0];
            Concert resultConcert2 = sampleBand_Haken.GetConcerts()[1];
            List<Venue> result = new List<Venue> {resultConcert1.Venue, resultConcert2.Venue};
            List<Venue> test = new List<Venue> {sampleVenue_ThePlace, sampleVenue_ASpot};

            CollectionAssert.AreEqual(test, result);
        }
    }
}
