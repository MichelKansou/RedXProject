using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedX.Diagnostics.Client.Exceptions{

    public enum ExceptionStatus{
        NO_INTERRUPT = 0x000000F0,
        WARNING = 0x000000F1,
        INTERRUPT = 0x000000F2
    }

    public class LauncherGenericException : Exception{
        protected Exception       _Caught;
        protected ExceptionStatus _Status;

        public LauncherGenericException(Exception e, String msg, ExceptionStatus status): base(msg){
            _Caught = e;
            _Status = status;
        }

        public LauncherGenericException(String msg, ExceptionStatus status) : base(msg){
            _Caught = null;
            _Status = status;
        }

        public ExceptionStatus Status{
            get { return _Status; }
        }

        public override String ToString(){
            return this.Message + ", Status :" + this._Status;
        }
    }
}
