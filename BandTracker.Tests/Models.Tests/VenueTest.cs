using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BandTracker.Models;

namespace BandTracker.Models.Tests
{
    [TestClass]
    public class VenueTest
    {
        private Venue sampleVenue_ThePlace = new Venue("The Place");
        private Venue sampleVenue_ThePlace_2 = new Venue("The Place");
        private Venue sampleVenue_ASpot = new Venue("A Spot");

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
    }
}
