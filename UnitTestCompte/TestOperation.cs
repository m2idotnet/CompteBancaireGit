using CompteBancaireAdo.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestCompte
{
    [TestClass]
    public class TestOperation
    {
        [TestMethod]
        public void Add_Operation()
        {
            Operation o = new Operation(100, 1);
            o.Add();
            Assert.IsTrue(o.Id > 0);
        }

        [TestMethod]
        public void Get_Operation_Liste()
        {
            List<Operation> r = Operation.GetOperations(1);
            Assert.IsTrue(r.Count > 0);
        }
        [TestMethod]
        public void Get_Operation_NotNull()
        {
            List<Operation> r = Operation.GetOperations(1);
            Assert.IsNotNull(r);
        }
    }
}
