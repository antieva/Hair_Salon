using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using HairSalonApp;
using System;

namespace HairSalonApp.Tests
{

    [TestClass]
    public class ClientModelTests : IDisposable
    {
        public void Dispose()
        {
            Stylist.DeleteAll();
            //Client.DeleteAll();
        }

        public void ClientTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=eva_antipina_test;";
        }


        // [TestMethod]
        // public void GetAllClients_DbStartsEmpty_0()
        // {
        //     //Arrange
        //     //Act
        //     int result = Client.GetAll().Count;
        //
        //     //Assert
        //     Assert.AreEqual(0, result);
        // }
     }
  }
