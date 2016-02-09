using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedX.Regulator.DBAccess.Exceptions{
    public enum ExceptionGravity{
        RUNTIME = 0x000000A0,
        SEVERE = 0x000000A1,
        HIGH = 0x000000A2,
        MEDIUM = 0x000000A3,
        LOW = 0x000000A4,
        IGNORE = 0x000000A5
    }

    public class DbGenericException : Exception{
        protected Exception _Caught;
        protected ExceptionGravity _Gravity;

        public DbGenericException(Exception e, String msg, ExceptionGravity status) : base(msg){
            _Caught = e;
            _Gravity = status;
        }

        public DbGenericException(String msg, ExceptionGravity status) : base(msg){
            _Caught = null;
            _Gravity = status;
        }

        public ExceptionGravity Gravity{
            get { return _Gravity; }
        }

        public override String ToString(){
            return this.Message + ", Status :" + this._Gravity;
        }
    }
}
