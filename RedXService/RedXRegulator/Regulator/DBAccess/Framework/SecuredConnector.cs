using RedX.Regulator.DBAccess.Exceptions;
using RedX.Regulator.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedX.Regulator.DBAccess.Framework{
    public sealed class SecuredConnector {
        private RawConnector r_connector;
 
        public SecuredConnector(){
            try {
                r_connector = new RawConnector();
            } catch(Exception e){
                throw new RawInitException(e, "Err_RawConnectorImpossibleConnection " + e.Message +","+e.StackTrace , ExceptionGravity.SEVERE);
            }
        }

        public bool Add(params SysInfo[] info){
            try{
                return r_connector.Add(info);
            } catch(Exception e){
                throw new RawConnException(e, "ActionNotAvailableException", ExceptionGravity.MEDIUM);
            }
        }
        
    }
}
