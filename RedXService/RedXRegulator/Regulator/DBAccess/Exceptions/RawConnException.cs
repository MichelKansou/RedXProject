using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedX.Regulator.DBAccess.Exceptions{
    public class RawConnException : DbGenericException{
        public RawConnException(String msg, ExceptionGravity stat) : base(msg, stat){ }
        public RawConnException(Exception origine, String msg, ExceptionGravity stat) : base(origine, msg, stat) { }
    }
}
