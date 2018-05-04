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
            //Client.DeleteAll();
        }

        public void StylistTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=eva_antipina_test;";
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
     }
  }
