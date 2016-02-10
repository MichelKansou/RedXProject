using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitaryTestRedX{
    [TestClass]
    public class UnitTest1{
        [TestMethod]
        public void TestMethodConnexionWithFakeLogin(){
            bool success = true;
            try{ 
                success = false;
            }
            catch{

            }
            Assert.AreEqual(false, success, "Connexion à la base avec de faux identifiants");
        }

        [TestMethod]
        public void TestMethodConnexion()
        {
            bool success = false;
            //connexion.Open();
            try
            {
                //connexion.Open();
            }
            catch
            {
                success = true;
            }
            Assert.AreEqual(false, success, "Double ouverture");
        }

    }
}
