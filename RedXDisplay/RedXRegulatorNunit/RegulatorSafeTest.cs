using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedX.Regulator.DBAccess.Framework;
using RedX.Regulator.DBAccess.Exceptions;
using RedX.Regulator.System;
using RedX.Diagnostics.Client;
using RedX.Regulator.Utility;
using RedX.Service.Util;
using RedX.Regulator.DBAccess;

namespace RedXRegulatorNunit{
    [TestClass]
    public class RegulatorSafeTest{
        public object SystemDiagnostics { get; private set; }

        [TestMethod]
        public void TestGetOs(){
            var value = Environment.OSVersion.VersionString;
            Assert.IsNotNull(value);
            Console.WriteLine(value);
        }

        [TestMethod]
        //[ExpectedException(typeof(RawInitException),"Aucune Erreur reçue", AllowDerivedTypes=true)]
        public void TestCreateSecure(){
            SecuredConnector sc = new SecuredConnector();
            Assert.IsNotNull(sc);
        }

        [TestMethod]
        public void TestCreateRaw(){
            RawConnector rc = new RawConnector();
            Assert.IsNotNull(rc);
            SysInfo info = new SysInfo();

            info.Environment = Environment.OSVersion.VersionString;
            var data = RedX.Diagnostics.Client.SystemDiagnostics.SystemInfo();
            info.PercentageCPU = data[0];
            info.PercentageRAM = data[1];
            info.Date = DateTime.Now;

            Console.WriteLine(info.Date.ToString("yyyy-MM-dd HH:mm:ss"));
            Console.WriteLine(rc.Add(info));
            Assert.IsTrue(rc.History().Count > 0);
            rc.History().RemoveAt(0);
        }

        [TestMethod]
        //[ExpectedException(typeof(RawConnException), "Aucune Erreur reçue", AllowDerivedTypes = true)]
        public void TestAdd(){
            SecuredConnector sc = new SecuredConnector();
            Assert.IsNotNull(sc);

            SysInfo info = new SysInfo();
            info.Environment = Environment.OSVersion.VersionString;
            var data = RedX.Diagnostics.Client.SystemDiagnostics.SystemInfo();

            info.PercentageCPU = data[0];
            info.PercentageRAM = data[1];
            info.Date = DateTime.Now;

            Assert.IsFalse(sc.Add(info));
            
        }
    }
}
