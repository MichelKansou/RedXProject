using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RedX.Regulator.Utility{
    public class Const{
        // Source, Base, Integrated Security
        #region Utilities / Membres
        private static String _SOURCE = "Data Source = (localdb)\\ProjectsV12";
        // Répertoire à changer
        private static String _DBNAME = "Initial Catalog = RedXDatabase";
        
        private static String _INSECU = "Integrated Security = True";

        private static String _PERF_TABLE = "Performances";
        private static String _OS_TABLE = "OS";
        private static String _TABLE = "Tables";
        private static String _INSERT = "Insert into @table values";
        #endregion


        #region Getters 
        public static String SOURCE { get { return _SOURCE; } }
        public static String DBNAME { get { return _DBNAME; } }
        public static String INSECU { get { return _INSECU; } }
        public static String PERFORMANCE_TABLE{ get { return _PERF_TABLE; } }
        public static String OS_TABLE { get { return _OS_TABLE; } }
        public static String TABLE { get { return _TABLE; } }
        public static String INSERT { get { return _INSERT; } }
        #endregion



    }
}
