using CompteBancaireAdo.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace UnitTestCompte
{
    //unité de test unitaire
    //pour controler le fonctionnement des méthodes au fur et à mesure
    [TestClass]
    public class TestCompte
    {
        [TestMethod]
        public void Compte_Compte_Null()
        {
            Compte compte = new Compte("ertertertertertert");
            Assert.AreEqual(default(int), compte.Id);
        }

        [TestMethod]
        public void Compte_Compte()
        {
            Compte compte = new Compte("168d8076-14e9-4826-8549-499dd0eebe2b");
            Assert.AreEqual(1, compte.Id);
        }

        [TestMethod]
        public void Add_Compte()
        {
            Compte compte = new Compte()
            {
                ClientId = 1
            };
            compte.Add();
            Assert.IsTrue(compte.Id > 0);
        }
        [TestMethod]
        public void Update_Compte_Exception()
        {
            Compte compte = new Compte("168d6-14e9-4826-8549-499dd02b");
            compte.Solde = 100;
            Assert.ThrowsException<AggregateException>(() => compte.Update());
        }
    }
}
