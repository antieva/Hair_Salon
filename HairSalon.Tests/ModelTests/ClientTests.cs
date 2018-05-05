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
            Client.DeleteAll();
        }

        public void ClientTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=eva_antipina_test;";
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfDescriptionsAreTheSame_True()
        {
            // Arrange, Act
            Client firstClient = new Client("Jill", "555555555", "03/03/92", "Bob haircut", 2);
            firstClient.SetId(0);
            Client secondClient = new Client("Jill", "555555555", "03/03/92", "Bob haircut", 2);
            secondClient.SetId(0);

            // Assert
            Assert.AreEqual(firstClient, secondClient);
        }


        [TestMethod]
        public void GetAllClients_DbStartsEmpty_0()
        {
            //Arrange
            //Act
            int result = Client.GetAllClients().Count;

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Save_SavesToDatabase_ClientList()
        {
            //Arrange
            Client testClient = new Client("Jill", "555555555", "03/03/92", "Bob haircut", 2);


            //Act
            testClient.Save();
            List<Client> result = Client.GetAllClients();
            List<Client> testList = new List<Client>{testClient};

            //Assert
            CollectionAssert.AreEqual(testList, result);
        }

        public void Save_AssignsIdToObject_Id()
        {
          //Arrange
          Client testClient = new Client("Jill", "555555555", "03/03/92", "Bob haircut", 2);

          //Act
          testClient.Save();
          Client savedClient = Client.GetAllClients()[0];

          int result = savedClient.GetId();
          int testId = testClient.GetId();

          //Assert
          Assert.AreEqual(testId, result);
        }

        [TestMethod]
        public void Find_FindClientInDatabase_Client()
        {
            //Arrange
            Client testClient = new Client("Jill", "555555555", "03/03/92", "Bob haircut", 2);

            //Act
            testClient.Save();
            Client foundClient = Client.Find(testClient.GetId());

            //Assert
            Assert.AreEqual(testClient, foundClient);
        }

        [TestMethod]
        public void DeleteClient_DeleteClientInDatabase_0()
        {
            //Arrange
            Client testClient = new Client("Jill", "555555555", "03/03/92", "Bob haircut", 2);

            //Act
            testClient.Save();
            Client.DeleteClient(testClient.GetId());
            List<Client> allClients = Client.GetAllClients();

            //Assert
            Assert.AreEqual(0, allClients.Count);
        }

        [TestMethod]
        public void DeleteThisStylistClients_DeleteAllClientsOfCirtainStylistFromDatabase_1()
        {
            //Arrange
            Client firstClient = new Client("Jill", "555555555", "03/03/92", "Bob haircut", 2);
            Client secondClient = new Client("Jane", "888888888", "03/10/72", "haircut", 2);
            Client thirdClient = new Client("Bill", "555555533", "07/05/98", "Nothing", 3);

            //Act
            firstClient.Save();
            secondClient.Save();
            thirdClient.Save();
            Client.DeleteThisStylistClients(2);
            List<Client> allClients = Client.GetAllClients();

            //Assert
            Assert.AreEqual(1, allClients.Count);
        }
     }
  }
