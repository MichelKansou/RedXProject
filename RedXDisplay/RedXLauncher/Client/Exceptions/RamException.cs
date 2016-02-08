using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedX.Diagnostics.Client.Exceptions{
    public class RamException : LauncherGenericException{
        public RamException(String msg, ExceptionStatus stat) : base(msg, stat){}
        public RamException(Exception origine, String msg, ExceptionStatus stat) : base(origine, msg, stat) { }
    }
}
