using CompteBancaireAdo.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestCompte
{
    [TestClass]
    public class TestClient
    {
        [TestMethod]
        public void Add_Client()
        {
            Client client = new Client { Nom = "toto", Prenom = "tata", Tel = "0606060606" };
            client.Add();
            Assert.IsTrue(client.Id > 0);
        }

        [TestMethod]
        public void Client_Client()
        {
            Client client = new Client(1002);
            Assert.AreEqual("test", client.Prenom);
        }

        [TestMethod]
        public void Client_Client_Null()
        {
            Client client = new Client(10000);
            Assert.AreEqual(default(int), client.Id);
        }
    }
}
