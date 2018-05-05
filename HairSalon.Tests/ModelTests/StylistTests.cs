using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using HairSalonApp;
using System;

namespace HairSalonApp.Tests
{

    [TestClass]
    public class StylistModelTests : IDisposable
    {
        public void Dispose()
        {
            Stylist.DeleteAll();
            Client.DeleteAll();
        }

        public void StylistTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=eva_antipina_test;";
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfDescriptionsAreTheSame_True()
        {
            // Arrange, Act
            Stylist firstStylist = new Stylist("Jill", "555555555", "Saturday, Sunday", "9am - 5pm");
            firstStylist.SetId(0);
            Stylist secondStylist = new Stylist("Jill", "555555555", "Saturday, Sunday", "9am - 5pm");
            secondStylist.SetId(0);

            // Assert
            Assert.AreEqual(firstStylist, secondStylist);
        }

        [TestMethod]
        public void GetAllStylists_DbStartsEmpty_0()
        {
            //Arrange
            //Act
            int result = Stylist.GetAllStylists().Count;

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GetAllStylists_DbHasOneEntry_1()
        {
            //Arrange
            Stylist newStylist = new Stylist("Inga Crouse", "4259740000", "Monday, Tuesday, Friday", "9am - 6pm", "colorist");

            //Act
            newStylist.Save();
            int result = Stylist.GetAllStylists().Count;

            //Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Save_SavesToDatabase_StylistList()
        {
            //Arrange
            Stylist testStylist = new Stylist("Rita", "4259740000", "Monday, Tuesday, Friday", "9am - 6pm", "colorist");

            //Act
            testStylist.Save();
            List<Stylist> result = Stylist.GetAllStylists();
            List<Stylist> testList = new List<Stylist>{testStylist};

            //Assert
            CollectionAssert.AreEqual(testList, result);
        }

        public void Save_AssignsIdToObject_Id()
        {
          //Arrange
          Stylist testStylist = new Stylist("Rita", "4259740000", "Monday, Tuesday, Friday", "9am - 6pm", "colorist");

          //Act
          testStylist.Save();
          Stylist savedStylist = Stylist.GetAllStylists()[0];

          int result = savedStylist.GetId();
          int testId = testStylist.GetId();

          //Assert
          Assert.AreEqual(testId, result);
        }

        [TestMethod]
        public void Find_FindsStylistInDatabase_Stylist()
        {
            //Arrange
            Stylist testStylist = new Stylist("Rita", "4259740000", "Monday, Tuesday, Friday", "9am - 6pm", "colorist");
            testStylist.Save();

            //Act
            Stylist foundStylist = Stylist.Find(testStylist.GetId());

            //Assert
            Assert.AreEqual(testStylist, foundStylist);
        }

        [TestMethod]
        public void DeleteStylist_DeleteStylistInDatabase_0()
        {
            //Arrange
            Stylist testStylist = new Stylist("Rita", "4259740000", "Monday, Tuesday, Friday", "9am - 6pm", "colorist");
            testStylist.Save();

            //Act
            Stylist.DeleteStylist(testStylist.GetId());
            List<Stylist> allStylists = Stylist.GetAllStylists();

            //Assert
            Assert.AreEqual(0, allStylists.Count);
        }

        [TestMethod]
        public void DeleteStylist_DeleteRightStylistInDatabase_Stylist()
        {
            //Arrange
            Stylist firstStylist = new Stylist("Rita", "4259740000", "colorist", "Monday, Tuesday, Friday", "9am - 6pm");
            Stylist secondStylist = new Stylist("Inga Crouse", "4259740000", "Monday, Tuesday, Friday", "9am - 6pm", "colorist");

            //Act
            firstStylist.Save();
            secondStylist.Save();
            Stylist.DeleteStylist(firstStylist.GetId());
            Stylist result = Stylist.Find(secondStylist.GetId());

            //Assert
            Assert.AreEqual(result, secondStylist);
        }
     }
  }
