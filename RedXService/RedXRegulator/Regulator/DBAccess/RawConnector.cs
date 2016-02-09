using RedX.Regulator.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedX.Regulator.DBAccess{
    public class RawConnector{
        private static SqlServerConnector connector = null;

        public RawConnector(){
            if (connector == null)
                connector = new SqlServerConnector();
        }

        public bool Add(SysInfo info){
            return connector != null ? connector.Add(info) : false;
        }

        public bool Add(params SysInfo[] infos){
            return connector != null ? connector.Add(infos) : false;
        }

        public static SqlServerConnector Connector { get { return connector; } }
    }
}
